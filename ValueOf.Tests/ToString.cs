using NUnit.Framework;

namespace ValueOf.Tests
{
    public class ToString
    {
        [Test]
        public void ToStringReturnsValueToStringForSingleValuedObjects()
        {
            ClientRef clientRef1 = ClientRef.From("ASDF12345");
            
            Assert.AreEqual(clientRef1.Value, clientRef1.ToString());
        }

        [Test]
        public void ToStringReturnsValueOfTupleForTupleValuedObjects()
        {
            Address address1 = Address.From(("16 Food Street", "London", Postcode.From("N1 1LT")));
            
            Assert.AreEqual(address1.Value.ToString(), address1.ToString());

        }
    }
}
