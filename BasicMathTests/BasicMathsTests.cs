using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicMath;
using System;

namespace BasicMathTests
{
    [TestClass]
    public class BasicMathsTests
    {
        private BasicMaths bm;

        [TestInitialize]
        public void Setup() => bm = new BasicMaths();

        // ---------- Add ----------
        [DataTestMethod]
        [DataRow(1, 1, 2.0)]                                       // EP: Positive numbers
        [DataRow(-1, -1, -2.0)]                                    // EP: Negative numbers
        [DataRow(0, 0, 0.0)]                                       // EP: Zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue + 1)]     // BVA: Upper boundary
        [DataRow(int.MinValue, -1, (double)int.MinValue - 1)]    // BVA: Lower boundary
        public void Test_AddMV(int a, int b, double expected)
        {
            double actual = bm.Add(a, b);
            Assert.AreEqual(expected, actual);
        }

        // ---------- Subtract ----------
        [DataTestMethod]
        [DataRow(5, 3, 2.0)]                                       // EP: Positive numbers
        [DataRow(-5, -3, -2.0)]                                    // EP: Negative numbers
        [DataRow(0, 0, 0.0)]                                       // EP: Zero
        [DataRow(int.MaxValue, -1, (double)int.MaxValue + 1)]    // BVA: Upper boundary
        [DataRow(int.MinValue, 1, (double)int.MinValue - 1)]     // BVA: Lower boundary
        public void Test_SubtractMV(int a, int b, double expected)
        {
            Assert.AreEqual(expected, bm.Subtract(a, b));
        }

        // ---------- Multiply ----------
        [DataTestMethod]
        [DataRow(2, 3, 6.0)]                                       // EP: Positive x Positive
        [DataRow(-2, -3, 6.0)]                                     // EP: Negative x Negative
        [DataRow(-2, 3, -6.0)]                                     // EP: Negative x Positive
        [DataRow(0, 5, 0.0)]                                       // EP: Zero
        [DataRow(int.MaxValue, 2, (double)int.MaxValue * 2)]     // BVA: Upper boundary
        [DataRow(int.MinValue, 2, (double)int.MinValue * 2)]     // BVA: Lower boundary
        public void Test_MultiplyMV(int a, int b, double expected)
        {
            Assert.AreEqual(expected, bm.Multiply(a, b));
        }

        // ---------- Divide ----------
        [DataTestMethod]
        [DataRow(6, 3, 2.0)]                                       // EP: Positive numbers
        [DataRow(-6, -3, 2.0)]                                     // EP: Negative numbers
        [DataRow(-6, 3, -2.0)]                                     // EP: Mixed signs
        [DataRow(0, 5, 0.0)]                                       // EP: Zero dividend
        [DataRow(int.MaxValue, 1, (double)int.MaxValue)]         // BVA: Upper boundary
        [DataRow(int.MinValue, 1, (double)int.MinValue)]         // BVA: Lower boundary
        public void Test_DivideMV(int a, int b, double expected)
        {
            Assert.AreEqual(expected, bm.Divide(a, b));
        }

        [TestMethod]
        public void Test_Divide_ByZero_ThrowsException()         // EP: invalid partition (divisor = 0)
        {
            Assert.ThrowsException<DivideByZeroException>(() => bm.Divide(10, 0));
        }
    }
}
