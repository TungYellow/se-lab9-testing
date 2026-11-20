using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountApp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BankAccountTests
{
    /// <summary>
    /// Exercise 3 - data-driven test that reads BankAccountTestData.csv.
    /// Note: the classic MSTest [DataSource] attribute only exists in .NET Framework,
    /// so in .NET Core the CSV is loaded through [DynamicData] (the supported replacement).
    /// </summary>
    [TestClass]
    public class BankAccountCsvTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // the CSV header has spaces after the commas, e.g. "customerName, initialBalance"
                PrepareHeaderForMatch = args => args.Header.Trim().ToLowerInvariant(),
                TrimOptions = TrimOptions.Trim
            };
            var path = Path.Combine(AppContext.BaseDirectory, "BankAccountTestData.csv");
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);
            foreach (var r in csv.GetRecords<TestData>())
                yield return new object[] { r.CustomerName, r.InitialBalance, r.DebitAmount, r.ExpectedBalance };
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
        public void Debit_CsvData_UpdatesBalance(string customerName, decimal initialBalance,
                                                 decimal debitAmount, string expectedBalance)
        {
            // Arrange
            var account = new BankAccount(customerName, initialBalance);

            // Act & Assert
            if (expectedBalance == "Insufficient funds")
            {
                Assert.ThrowsException<InvalidOperationException>(() => account.Debit(debitAmount));
            }
            else
            {
                account.Debit(debitAmount);
                Assert.AreEqual(decimal.Parse(expectedBalance, CultureInfo.InvariantCulture), account.Balance);
            }
        }

        public class TestData
        {
            public string CustomerName { get; set; }
            public decimal InitialBalance { get; set; }
            public decimal DebitAmount { get; set; }
            public string ExpectedBalance { get; set; }
        }
    }
}
