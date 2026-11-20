using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountApp;
using System;

namespace BankAccountTests
{
    /// <summary>
    /// Exercise 3 - BankAccount: [DataTestMethod] + [DataRow] test cases designed with
    /// Equivalence Partitioning (EP) and Boundary Value Analysis (BVA).
    /// </summary>
    [TestClass]
    public class BankAccountTests
    {
        // ===================== Debit =====================
        [DataTestMethod]
        [DataRow("Trung Pham", 1000.0, 200.0, 800.0)]    // EP: valid amount < balance
        [DataRow("Ky Pham", 500.0, 100.0, 400.0)]        // EP: valid amount < balance
        [DataRow("Thai Pham", 300.0, 50.0, 250.0)]       // EP: valid amount < balance
        [DataRow("Boundary Max", 100.0, 100.0, 0.0)]     // BVA: amount == balance (upper bound)
        [DataRow("Boundary Below", 100.0, 99.99, 0.01)]  // BVA: just below balance
        [DataRow("Boundary Min", 100.0, 0.0, 100.0)]     // BVA: amount == 0 (lower bound)
        public void Debit_ValidAmount_UpdatesBalance(string name, double initial, double amount, double expected)
        {
            var account = new BankAccount(name, (decimal)initial);
            account.Debit((decimal)amount);
            Assert.AreEqual((decimal)expected, account.Balance);
        }

        [DataTestMethod]
        [DataRow("Ky Trung Pham", 100.0, 150.0)]         // EP: amount > balance
        [DataRow("Boundary Above", 100.0, 100.01)]       // BVA: just above balance
        [DataRow("Empty Account", 0.0, 0.01)]            // BVA: empty account
        public void Debit_InsufficientFunds_ThrowsException(string name, double initial, double amount)
        {
            var account = new BankAccount(name, (decimal)initial);
            var ex = Assert.ThrowsException<InvalidOperationException>(() => account.Debit((decimal)amount));
            Assert.AreEqual("Insufficient funds", ex.Message);
            Assert.AreEqual((decimal)initial, account.Balance);   // balance unchanged
        }

        [DataTestMethod]
        [DataRow(-0.01)]                                 // BVA: just below 0
        [DataRow(-100.0)]                                // EP: negative amount
        public void Debit_NegativeAmount_ThrowsArgumentOutOfRange(double amount)
        {
            var account = new BankAccount("Negative", 100m);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Debit((decimal)amount));
        }

        // ===================== Credit =====================
        [DataTestMethod]
        [DataRow("Trung Pham", 1000.0, 200.0, 1200.0)]   // EP: positive amount
        [DataRow("Ky Pham", 500.0, 100.0, 600.0)]        // EP: positive amount
        [DataRow("Thai Pham", 300.0, 50.0, 350.0)]       // EP: positive amount
        [DataRow("Boundary Min", 300.0, 0.0, 300.0)]     // BVA: amount == 0
        [DataRow("Boundary Small", 0.0, 0.01, 0.01)]     // BVA: smallest positive amount
        public void Credit_ValidAmount_UpdatesBalance(string name, double initial, double amount, double expected)
        {
            var account = new BankAccount(name, (decimal)initial);
            account.Credit((decimal)amount);
            Assert.AreEqual((decimal)expected, account.Balance);
        }

        [DataTestMethod]
        [DataRow(-0.01)]                                 // BVA: just below 0
        [DataRow(-50.0)]                                 // EP: negative amount
        public void Credit_NegativeAmount_ThrowsArgumentOutOfRange(double amount)
        {
            var account = new BankAccount("Negative", 100m);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Credit((decimal)amount));
        }

        // ===================== Withdraw =====================
        [DataTestMethod]
        [DataRow("Trung Pham", 1000.0, 200.0, 800.0)]    // EP: valid amount
        [DataRow("Ky Pham", 500.0, 100.0, 400.0)]        // EP: valid amount
        [DataRow("Thai Pham", 300.0, 50.0, 250.0)]       // EP: valid amount
        [DataRow("Boundary Max", 300.0, 300.0, 0.0)]     // BVA: withdraw whole balance
        public void Withdraw_ValidAmount_UpdatesBalance(string name, double initial, double amount, double expected)
        {
            var account = new BankAccount(name, (decimal)initial);
            account.Withdraw((decimal)amount);
            Assert.AreEqual((decimal)expected, account.Balance);
        }

        [DataTestMethod]
        [DataRow(300.0, 300.01)]                         // BVA: just above balance
        [DataRow(300.0, 1000.0)]                         // EP: far above balance
        public void Withdraw_InsufficientFunds_ThrowsException(double initial, double amount)
        {
            var account = new BankAccount("Withdraw", (decimal)initial);
            Assert.ThrowsException<InvalidOperationException>(() => account.Withdraw((decimal)amount));
        }
    }
}
