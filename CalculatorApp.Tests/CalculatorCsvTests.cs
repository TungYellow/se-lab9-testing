using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;
using CalculatorApp;

namespace BasicMathTests
{
    public class CalculatorCsvTests
    {
        // Method to read test data from a CSV file (the file is copied to the output folder)
        public static IEnumerable<object[]> GetTestData(string fileName)
        {
            var path = Path.Combine(AppContext.BaseDirectory, fileName);
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[] { record.A, record.B, record.Expected };
                }
            }
        }

        // Test method for Add operation using CSV data
        [Theory]
        [MemberData(nameof(GetTestData), parameters: "add_testdata.csv")]
        public void Add_CsvData_ReturnsCorrectSum(int a, int b, double expected)
        {
            var calculator = new Calculator();
            int result = calculator.Add(a, b);
            Assert.Equal(expected, result);
        }

        // Test method for Subtract operation using CSV data
        [Theory]
        [MemberData(nameof(GetTestData), parameters: "subtract_testdata.csv")]
        public void Subtract_CsvData_ReturnsCorrectDifference(int a, int b, double expected)
        {
            var calculator = new Calculator();
            int result = calculator.Subtract(a, b);
            Assert.Equal(expected, result);
        }

        // Test method for Multiply operation using CSV data
        [Theory]
        [MemberData(nameof(GetTestData), parameters: "multiply_testdata.csv")]
        public void Multiply_CsvData_ReturnsCorrectProduct(int a, int b, double expected)
        {
            var calculator = new Calculator();
            int result = calculator.Multiply(a, b);
            Assert.Equal(expected, result);
        }

        // Test method for Divide operation using CSV data
        [Theory]
        [MemberData(nameof(GetTestData), parameters: "divide_testdata.csv")]
        public void Divide_CsvData_ReturnsCorrectQuotient(int a, int b, double expected)
        {
            var calculator = new Calculator();
            double result = calculator.Divide(a, b);
            Assert.Equal(expected, result);
        }

        // Class to represent the test data structure
        public class TestData
        {
            public int A { get; set; }
            public int B { get; set; }
            public double Expected { get; set; }
        }
    }
}
