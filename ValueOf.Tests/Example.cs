using NUnit.Framework;

namespace ValueOf.Tests
{
    public class ClientRef : ValueOf<string, ClientRef> { }
    public class Postcode : ValueOf<string, Postcode> { }
    public class Address : ValueOf<(string firstLine, string secondLine, Postcode postcode), Address> { }

    public class Example
    {
        [Test]
        public void SingleValuedExample()
        {
            ClientRef clientRef1 = ClientRef.From("ASDF12345");
            ClientRef clientRef2 = ClientRef.From("ASDF12345");
            Assert.AreEqual(clientRef1, clientRef2);
            Assert.AreEqual(clientRef1.GetHashCode(), clientRef2.GetHashCode());

            ClientRef clientRef3 = ClientRef.From("QWER98765");
            Assert.AreNotEqual(clientRef1, clientRef3);
            Assert.AreNotEqual(clientRef1.GetHashCode(), clientRef3.GetHashCode());
        }

        [Test]
        public void ValueTupleValuedExample()
        {
            Address address1 = Address.From(("16 Food Street", "London", Postcode.From("N1 1LT")));
            Address address2 = Address.From(("16 Food Street", "London", Postcode.From("N1 1LT")));
            Assert.AreEqual(address1, address2);
            Assert.AreEqual(address1.GetHashCode(), address2.GetHashCode());

            Address address3 = Address.From(("17 Food Street", "London", Postcode.From("N1 1LT")));
            Assert.AreNotEqual(address1, address3);
            Assert.AreNotEqual(address1.GetHashCode(), address3.GetHashCode());
        }
    }
}
