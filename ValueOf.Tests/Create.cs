using NUnit.Framework;

namespace ValueOf.Tests
{
    public class ProductId : ValueOf<string, ProductId>
    {
        protected override string Create(string item)
        {
            return item?.ToLower();
        }
    }

    public class Create
    {
        [Test]
        public void CreateFactory()
        {
            Assert.AreEqual("asdf12345", ProductId.From("ASDF12345").Value);
        }
    }
}
