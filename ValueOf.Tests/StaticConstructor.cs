using System;
using System.Diagnostics;

using NUnit.Framework;

namespace ValueOf.Tests
{
    class StaticConstructor
    {
        [Test]
        public void ClassesDerivedFromValueOfCanHaveStaticConstructor()
        {
            Assert.DoesNotThrow(() => ValueWithStaticConstructor.From(42));
        }
    }

    public class ValueWithStaticConstructor : ValueOf<int, ValueWithStaticConstructor>
    {
        static ValueWithStaticConstructor()
        {
            Debug.WriteLine("pretending this needs to be here");
        }
    }
}
