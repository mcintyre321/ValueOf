using NUnit.Framework;
using System.Collections.Generic;

namespace ValueOf.Tests
{

    public class CaseInsensitiveClientRef : ValueOf<string, CaseInsensitiveClientRef>
    {
        protected override bool Equals(ValueOf<string, CaseInsensitiveClientRef> other)
        {
            return EqualityComparer<string>.Default.Equals(Value.ToLower(), other.Value.ToLower());
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(Value.ToLower());
        }
    }

    public class Equals
    {
        [Test]
        public void CaseInsensitiveEquals()
        {
            CaseInsensitiveClientRef clientRef1 = CaseInsensitiveClientRef.From("ASDF12345");
            CaseInsensitiveClientRef clientRef2 = CaseInsensitiveClientRef.From("asdf12345");
            Assert.AreEqual(clientRef1, clientRef2);
            Assert.AreEqual(clientRef1.GetHashCode(), clientRef2.GetHashCode());
            Assert.IsTrue(clientRef1 == clientRef2);
            Assert.IsTrue(clientRef1.Value == "ASDF12345");

            CaseInsensitiveClientRef clientRef3 = CaseInsensitiveClientRef.From("QWER98765");
            Assert.AreNotEqual(clientRef1, clientRef3);
            Assert.AreNotEqual(clientRef1.GetHashCode(), clientRef3.GetHashCode());
            Assert.IsFalse(clientRef1 == clientRef3);
        }
    }
}
