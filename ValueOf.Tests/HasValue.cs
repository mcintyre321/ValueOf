using System;
using NUnit.Framework;

namespace ValueOf.Tests
{
    public class IntId : ValueOf<int, IntId> { }
    public class StringId : ValueOf<string, StringId> { }
    public class GuidId : ValueOf<Guid, GuidId> { }

    public class HasValue
    {
        [Test]
        public void HasValueWhenDefaultIntegerIdThenHasValueIsFalseAndIsEmptyIsTrue()
        {
            var id = new IntId();

            Assert.IsFalse(id.HasValue);
            Assert.IsTrue(id.IsEmpty);
        }

        [Test]
        public void HasValueWhenEmptyStringIdThenHasValueIsFalseAndIsEmptyIsTrue()
        {
            var id = new StringId();

            Assert.IsFalse(id.HasValue);
            Assert.IsTrue(id.IsEmpty);
        }

        [Test]
        public void HasValueWhenDefaultGuidIdThenHasValueIsFalseAndIsEmptyIsTrue()
        {
            var id = new GuidId();

            Assert.IsFalse(id.HasValue);
            Assert.IsTrue(id.IsEmpty);
        }
        [Test]
        public void HasValueWhenIntegerIdThenHasValueIsTrueAndIsEmptyIsFalse()
        {
            var value = new Random().Next(1, 100);
            var id = IntId.From(value);

            Assert.IsTrue(id.HasValue);
            Assert.IsFalse(id.IsEmpty);
            Assert.AreEqual(value, id.Value);
        }

        [Test]
        public void HasValueWhenStringIdThenHasValueIsTrueAndIsEmptyIsFalse()
        {
            var value = Guid.NewGuid().ToString();
            var id = StringId.From(value);

            Assert.IsTrue(id.HasValue);
            Assert.IsFalse(id.IsEmpty);
            Assert.AreEqual(value, id.Value);
        }

        [Test]
        public void HasValueWhenGuidIdThenHasValueIsTrueAndIsEmptyIsFalse()
        {
            var value = Guid.NewGuid();
            var id = GuidId.From(value);

            Assert.IsTrue(id.HasValue);
            Assert.IsFalse(id.IsEmpty);
            Assert.AreEqual(value, id.Value);
        }
    }
}
