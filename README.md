# ValueOf

> install-package ValueOf

## What is this library

> The Smell: Primitive Obsession is using primitive data types to represent domain ideas. For example, we use a String to represent a message, an Integer to represent an amount of money, or a Struct/Dictionary/Hash to represent a specific object.
> The Fix: Typically, we introduce a ValueObject in place of the primitive data, then watch like magic as code from all over the system shows FeatureEnvySmell and wants to be on the new ValueObject. We move those methods, and everything becomes right with the world.
> - http://wiki.c2.com/?PrimitiveObsession

ValueOf lets you define ValueObject Types in a single line of code. Use them everywhere to strengthen your codebase.

```
public class EmailAddress : ValueOf<string, EmailAddress> { }

...

EmailAddress emailAddress = EmailAddress.From("foo@bar.com");

```

The ValueOf class implements `.Equals` and `.GetHashCode()` for you.

You can use C# 7 Tuples for more complex Types with multiple values:

```
    public class Address : ValueOf<(string firstLine, string secondLine, Postcode postcode), Address> { }

```

### Validation

You can add validation to your Types by overriding the `protected void Validate() { } ` method:

```
public class ValidatedClientRef : ValueOf<string, ValidatedClientRef>
{
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Value cannot be null or empty");
    }
}	

```

## See Also

If you liked this, you'll probably like another project of mine [OneOf](https://github.com/mcintyre321/OneOf) which provides Discriminated Unions for C#, allowing stronger compile time guarantees when writing branching logic.