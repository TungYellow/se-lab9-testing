using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;

namespace LoginAutomation
{
    public class LoginTests
    {
        private static readonly string BaseDir = AppContext.BaseDirectory;
        private static readonly string LoginUrl = new Uri(Path.Combine(BaseDir, "login.html")).AbsoluteUri;

        public static IEnumerable<object[]> GetTestData()
        {
            using (var reader = new StreamReader(Path.Combine(BaseDir, "LoginTestData.csv")))
            // the CSV header is lower-case ("username,password"): match headers case-insensitively
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant()
            };
            using (var csv = new CsvReader(reader, config))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[] { record.Username, record.Password };
                }
            }
        }

        private static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            // Set HEADLESS=1 to run without opening a browser window (e.g. in CI)
            if (Environment.GetEnvironmentVariable("HEADLESS") == "1")
                options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1000,700");
            return new ChromeDriver(options);   // Selenium Manager downloads the matching chromedriver
        }

        private static string Login(IWebDriver driver, string username, string password, string shotName)
        {
            // Navigate to the login page
            driver.Navigate().GoToUrl(LoginUrl);

            // Find the username and password fields and fill them
            driver.FindElement(By.Id("username")).SendKeys(username);
            driver.FindElement(By.Id("password")).SendKeys(password);

            // Click the login button
            driver.FindElement(By.Id("loginButton")).Click();

            // Wait for the result message instead of a fixed Thread.Sleep
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            var message = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("message"));
                return string.IsNullOrEmpty(el.Text) ? null : el;
            });

            // Save evidence for the report
            Directory.CreateDirectory(Path.Combine(BaseDir, "Screenshots"));
            ((ITakesScreenshot)driver).GetScreenshot()
                .SaveAsFile(Path.Combine(BaseDir, "Screenshots", shotName + ".png"));
            return message.Text;
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Login_WithValidCredentials_ShouldSucceed(string username, string password)
        {
            using (IWebDriver driver = CreateDriver())
            {
                string text = Login(driver, username, password, "valid_" + username);
                Assert.Equal("Login successful! Welcome, " + username, text);
            }
        }

        [Theory]
        [InlineData("user1", "wrongpass")]   // valid user, wrong password
        [InlineData("nobody", "password1")]  // unknown user
        [InlineData("", "")]                 // empty fields
        public void Login_WithInvalidCredentials_ShouldShowError(string username, string password)
        {
            using (IWebDriver driver = CreateDriver())
            {
                string text = Login(driver, username, password,
                    "invalid_" + (username == "" ? "empty" : username));
                Assert.Equal("Invalid username or password", text);
            }
        }

        public class TestData
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
