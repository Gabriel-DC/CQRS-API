using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.ValueObjects
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateAnEmailAddressFromAString()
        {
            var email = new Email("");

            Assert.IsFalse(email.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenDocumentIsInvalid()
        {
            var email = new Email("invalid");

            Assert.IsFalse(email.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeValidWhenDocumentIsValid()
        {
            var email = new Email("gabriel@gabriel.com");

            Assert.IsTrue(email.IsValid);
        }
    }
}