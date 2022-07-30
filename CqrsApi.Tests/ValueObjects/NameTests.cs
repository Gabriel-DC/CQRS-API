using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.ValueObjects
{
    [TestClass]
    public class NameTests
    {
        //Should be able to create a name from a string
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToCreateNameFromString()
        {
            var name = new Name("John", "Doe");
            Assert.AreEqual("John", name.FirstName);
            Assert.AreEqual("Doe", name.LastName);
        }

        //Should be invalid when name is empty string
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenNameAndLastNameIsEmptyString()
        {
            var name = new Name("", "");

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when name is null
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenNameIsNull()
        {
            var name = new Name(null!, "Doe");

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when last name is null
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenLastNameIsNull()
        {
            var name = new Name("John", null!);

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when name is empty string
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenNameIsEmptyString()
        {
            var name = new Name("", "Doe");

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when last name is empty string
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenLastNameIsEmptyString()
        {
            var name = new Name("John", "");

            Assert.IsFalse(name.IsValid);
        }

        //Should be valid when name and last name are valid
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeValidWhenNameAndLastNameAreValid()
        {
            var name = new Name("John", "Doe");

            Assert.IsTrue(name.IsValid);
        }

        //Should be able to get a string representation of the name
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeAbleToGetStringRepresentationOfName()
        {
            var name = new Name("John", "Doe");

            Assert.AreEqual("John Doe", name.ToString());
        }

        //Should be invalid when first name is bigger than 100 characters
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenFirstNameIsBiggerThan100Characters()
        {
            var name = new Name(new string('a', 101), "Doe");

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when last name is bigger than 100 characters
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenLastNameIsBiggerThan100Characters()
        {
            var name = new Name("John", new string('a', 101));

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when first name is less than 3 characters
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenFirstNameIsLessThan3Characters()
        {
            var name = new Name("Jo", "Doe");

            Assert.IsFalse(name.IsValid);
        }

        //Should be invalid when last name is less than 3 characters
        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenLastNameIsLessThan3Characters()
        {
            var name = new Name("John", "Do");

            Assert.IsFalse(name.IsValid);
        }
    }
}