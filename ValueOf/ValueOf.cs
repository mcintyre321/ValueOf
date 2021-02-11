using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ValueOf
{
    [TypeDescriptionProvider(typeof(ValueTypeDescriptionProvider))]
    public class ValueOf<TValue, TThis> where TThis : ValueOf<TValue, TThis>, new()
    {
        private static readonly Func<TThis> Factory;

        /// <summary>
        /// WARNING - THIS FEATURE IS EXPERIMENTAL. I may change it to do
        /// validation in a different way.
        /// Right now, override this method, and throw any exceptions you need to.
        /// Access this.Value to check the value
        /// </summary>
        protected virtual void Validate()
        {
        }

        static ValueOf()
        {
            var ctor = typeof(TThis)
                .GetTypeInfo()
                .DeclaredConstructors
                .First();

            var argsExp = new Expression[0];
            var newExp = Expression.New(ctor, argsExp);
            var lambda = Expression.Lambda(typeof(Func<TThis>), newExp);

            Factory = (Func<TThis>)lambda.Compile();
        }

        public TValue Value { get; protected set; }

        public static TThis From(TValue item)
        {
            var x = Factory();
            x.Value = item;
            x.Validate();

            return x;
        }

        protected virtual bool Equals(ValueOf<TValue, TThis> other)
        {
            return EqualityComparer<TValue>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((ValueOf<TValue, TThis>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(Value);
        }

        public static bool operator ==(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueOf<TValue, TThis> a, ValueOf<TValue, TThis> b)
        {
            return !(a == b);
        }

        public static implicit operator TValue(ValueOf<TValue, TThis> a)
        {
            return a.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    public class ValueOfTypeConverter<TValue, TThis> : TypeConverter where TThis : ValueOf<TValue, TThis>, new()
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(TValue) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var inf = typeof(TThis).GetMethod("From", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            return inf.Invoke("From", new[] {value});
        }
    }

    public class ValueTypeDescriptor : CustomTypeDescriptor
    {
        private readonly Type objectType;

        public ValueTypeDescriptor(Type objectType)
        {
            this.objectType = objectType;
        }

        public override TypeConverter GetConverter()
        {
            var genericArg1 = objectType.BaseType.GenericTypeArguments[0];
            var converterType = typeof(ValueOfTypeConverter<,>).MakeGenericType(genericArg1, objectType);
            return (TypeConverter)Activator.CreateInstance(converterType);
        }
    }

    public class ValueTypeDescriptionProvider : TypeDescriptionProvider
    {
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            return new ValueTypeDescriptor(objectType);
        }
    }
}