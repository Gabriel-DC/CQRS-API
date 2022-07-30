using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CqrsApi.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        private readonly Document _validDocument = new("39793085029");
        private readonly Document _invalidDocument = new("397930850");
        private readonly Document _invalidDocument2 = new("12345678901");
        private readonly Document _emptyDocument = new(string.Empty);

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenDocumentNumberIsEmpty()
        {
            Assert.IsFalse(_emptyDocument.IsValid);
            Assert.AreEqual(2, _emptyDocument.Notifications.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeValidWhenDocumentIsCorrectAndValid()
        {            
            Assert.IsTrue(_validDocument.IsValid);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenDocumentNumberHaveInvalidLength()
        {
            Assert.IsFalse(_invalidDocument2.IsValid);
            Assert.AreEqual("Documento inválido", _invalidDocument2.Notifications.First().ErrorMessage);
            Assert.AreEqual(1, _invalidDocument2.Notifications.Count);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void ShouldBeInvalidWhenDocumentNumberIsInvalid()
        {            
            Assert.IsFalse(_invalidDocument.IsValid);
            Assert.AreEqual("Documento inválido", _invalidDocument.Notifications.First().ErrorMessage);
            Assert.AreEqual(1, _invalidDocument.Notifications.Count);
        }
    }
}