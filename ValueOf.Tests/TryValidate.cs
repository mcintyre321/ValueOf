using NUnit.Framework;

namespace ValueOf.Tests
{
    public class TryValidateClientRef : ValueOf<string, TryValidateClientRef>
    {
        protected override bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace(Value);
        }
    }

    public class TryValidation
    {
        [Test]
        public void TryValidateReturnsFalse()
        {
            bool isValid = TryValidateClientRef.TryFrom("", out TryValidateClientRef valueObject);

            Assert.IsFalse(isValid);
            Assert.IsNull(valueObject);
        }

        [Test]
        public void TryValidateReturnsTrue()
        {
            bool isValid = TryValidateClientRef.TryFrom("something", out TryValidateClientRef valueObject);

            Assert.IsTrue(isValid);
            Assert.IsNotNull(valueObject);
            Assert.AreEqual("something", valueObject.Value);
        }
    }
}
