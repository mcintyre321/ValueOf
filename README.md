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

There are two, independent ways to add validation to your Types. Both ways validate during Type creation.

#### Throw an exception during creation

You can add validation to your Types by overriding the `protected void Validate() { }` method to throw an exception when the type is created using `.From()`:

```
public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Value cannot be null or empty");
    }
}

void Main()
{
    // This will throw an ArgumentException
    var myEmailAddress = EmailAddress.From("");
}
```

#### Return false while using an `out` argument

You may also implement validation to your Types by overriding the `protected bool TryValidate() { }` method to return `false`. This will cause the `TryFrom()` creation method to return false:

```
public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override bool TryValidate()
    {
        if (string.IsNullOrWhitespace(Value))
            return false;
    }
}

void Main()
{
    if (!EmailAddress.TryFrom("", out var myEmailAddress)
    {
        Console.WriteLine("Invalid email address");
    }
}
```

#### Validation best practices

It is recommended to override both `Validate` and `TryValidate` with the same failure cases, if you choose to implement validation at all.

For example:

```
public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override void Validate()
    {
        if (!IsValidEmailAddress(Value))
            throw new ArgumentException("Invalid email address");
    }

    protected override bool TryValidate()
    {
        return IsValidEmailAddress(Value);
    }

    // Prevent circular references from Validate and TryValidate by
    // breaking out the logic into a separate method
    public static bool IsValidEmailAddress(string value)
    {
        return !string.IsNullOrWhitespace(value)
    }
}
```

If you prefer one method over the other, then consider at least overriding the alternative method to always fail. This will prevent someone from creating an invalid Type using the creation method you did not override.

For example:

```
public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override void Validate()
    {
        throw new NotSupportedException("Use TryValidate() and TryFrom() instead");
    }

    protected override bool TryValidate()
    {
        return !string.IsNullOrWhitespace(Value)
    }
}
```

or

```
public class EmailAddress : ValueOf<string, EmailAddress>
{
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new ArgumentException("Value cannot be null or empty");
    }

    protected override bool TryValidate()
    {
        return false; // Use Validate() and From() instead
    }
}
```

## See Also

If you liked this, you'll probably like another project of mine [OneOf](https://github.com/mcintyre321/OneOf) which provides Discriminated Unions for C#, allowing stronger compile time guarantees when writing branching logic.