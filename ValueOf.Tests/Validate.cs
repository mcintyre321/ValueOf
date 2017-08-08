using System;
using NUnit.Framework;

namespace ValueOf.Tests
{
    public class ValidatedClientRef : ValueOf<string, ValidatedClientRef>
    {
        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new ArgumentException("Value cannot be null or empty");
        }
    }

    public class Validation
    {
        [Test]
        public void SingleValuedExample()
        {
            Assert.Throws<ArgumentException>(() => ValidatedClientRef.From(""), "Value cannot be null or empty");
        }
         
    }
}

