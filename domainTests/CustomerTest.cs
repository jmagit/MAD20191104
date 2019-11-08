using System;
using domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace domainTests {
    [TestClass]
    public class CustomerTest {
        [TestMethod]
        public void IsValidTest() {
            var c = new Customer() { FirstName = "MAYUSCULAS" };
            Assert.IsTrue(c.IsValid);
        }
        [TestMethod]
        public void IsInvalidTest() {
            var c = new Customer() { FirstName = "Mminu" };
            Assert.IsFalse(c.IsValid);
            c = new Customer() { FirstName = "1111" };
            Assert.IsFalse(c.IsValid);
        }
    }
}
