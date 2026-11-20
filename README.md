# SE Lab 9 – Software Testing (C# / .NET 8)

| Project | Type | Exercise |
|---|---|---|
| BasicMath | Console app – `BasicMaths` (double Add/Subtract/Multiply/Divide) | 1 |
| BasicMathTests | MSTest – `[DataTestMethod]` + `[DataRow]` (EP & BVA) – 23 tests | 1 |
| CalculatorApp | Class library – integer `Calculator` | 1–2 |
| CalculatorApp.Tests | xUnit – `[Theory]/[InlineData]` + CSV `[MemberData]` with CsvHelper – 33 tests | 1–2 |
| BankAccountApp | Console app – `BankAccount` (Debit/Credit/Withdraw) | 3 |
| BankAccountTests | MSTest – EP/BVA `[DataRow]` + CSV via `[DynamicData]` – 28 tests | 3 |
| LoginAutomation | xUnit + Selenium WebDriver + CsvHelper – 6 UI tests | 4 |

## Run
* Visual Studio 2022: open `Lab9Testing.sln` → **Test > Test Explorer > Run All**.
* Command line: `dotnet test` (or `.\run-tests.ps1` for TRX/HTML reports).
* Selenium needs Google Chrome installed; Selenium Manager downloads the matching chromedriver automatically.
  Set `HEADLESS=1` to run without a visible browser window.

## Corrections made to the lab handout
* `[InlineData(int.MinValue, 1, int.MinValue - 1)]` does not compile (constant overflow, CS0220) →
  expected value is `int.MaxValue` (unchecked wrap-around).
* `subtract_testdata.csv` row `-2147483648,1,-2147483649` cannot be parsed as `int` → `2147483647`.
* CSV tests read `Expected` as `double`, so the test parameters are `double` (xUnit cannot convert double→int).
* CSV files are copied to the output folder (`CopyToOutputDirectory`) and opened from `AppContext.BaseDirectory`.
* MSTest `[DataSource]` exists only in .NET Framework; in .NET Core the CSV is loaded with `[DynamicData]`.
* `BankAccountTestData.csv` header has spaces after commas → CsvHelper `PrepareHeaderForMatch` trims them.
* Namespace `BankApp` in the handout → `BankAccountApp`.
* Selenium test asserts the success/error message and uses `WebDriverWait` instead of `Thread.Sleep`.
