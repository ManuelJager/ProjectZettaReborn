# Structure, layout, formatting rules

<details><summary>Structure</summary>
<p>

</p>
</details>

<details><summary>Layout</summary>
<p>

Members are first ordered in types, secondly in static to non-static, and the thirdly by access modifier groups.

## Type order

Types should be grouped in this order.

```
Const fields
Readonly fields
Fields
Constructors
Destructors
Delegates
Events
Enums
Interfaces
Properties
Indexers
Methods
Structs
Classes
```

## Access modifiers 

Member group should be grouped in this order.

```
Public
Protected
Internal
Private
```

## Grouping

Non const or readonly fields with the same access modifier may be grouped together.
Dont forget to group types together.


<details><summary>EG</summary>
<p>

## :x: Incorrect:

> This example breaks 3 rules.

<ol>
<li>Types are not grouped properly</li>
<li>Non-static fields were placed before the static field,</li>
<li>Fields with different access modifiers are mixed together</li>
</ol>

```csharp
public class ClassA : BaseClassA, InterfaceA
{
    public Complex fieldB;
    public Simple fieldA;
    public Complex fieldC;
    private Complex FieldD;
    
    private Complex fieldE;
    private static Simple fieldF;

    public ClassA() 
    {
    }
}
```

## :heavy_check_mark: Correct:

> This example breaks no rules

```csharp
public class ClassA : BaseClassA, InterfaceA
{
    private static Complex FieldD;

    public static Simple fieldA;
    public Complex fieldB;
    public Complex fieldC;
    
    private Complex fieldE;
    private Simple fieldF;

    public ClassA() 
    {
    }
}
```

</p>
</details>

</p>
</details>

<details><summary>Formatting</summary>
<p>

## Naming and casing

|Type|Case|Notes|Example|
|---|---|-----|---|
|Const fields|UPPER_CASE_SNAKE_CASE|none|public const float CONST_EXAMPLE = 1f;|
|Readonly fields|camelCase|none|public readonly float readonlyExample;|
|Fields|camelCase|none|public float fieldExample;|
|Constructors|PascalCase|none|public Constructor()|
|Destructors|PascalCase|none|public ~Destructor()|
|Delegates|PascalCase|Append "Delegate"|public delegate void ExampleOfDelegate()|
|Events|PascalCase|Prepend the event type|public static ExampleOfDelegate ClickF10|
|Enums|PascalCase|constant groups must also use PascalCase|enum ExampleEnum|
|Interfaces|PascalCase|Prepend a 'I'|public interface IExampleInterface|
|Properties|PascalCase|none|public int Age {get; set;}|
|Indexers|none|none|public int this[int value]|
|Methods|PascalCase|none|public void DoSomething()|
|Structs|PascalCase|none|public struct StructExample|
|Classes|PascalCase|none|public class ClassExample|

## Scopes

>A general rule for scoping is you should open and close brackets with a new line

<br>

## If statements

<p>One line if statements are not allowed.
This is for the sake of readability and standardization of scopes.</p>

<details><summary>EG:</summary>
<p>

## :x: Incorrect:

```csharp
if (true) 
    DoSomething();
```

## :heavy_check_mark: Correct:

```csharp
if (true) 
{
    DoSomething();
}
```
</p>
</details>

<br>

## Method

<p>Member methods may never use the '=>' token to define the body of the function.</p>
<details><summary>EG:</summary>
<p>

## :x: Incorrect:

```csharp
public bool MethodA() => true;

public bool MethodA() 
    => true;
```

## :heavy_check_mark: Correct:

```csharp
public bool MethodB() 
{
    return true;
}
```
</p>
</details>

## Anonymous functions

<p>Anonymous functions may use the '=>' right after the parameter list.<br>
Then open brackets as usual</p>

<details><summary>EG:</summary>
<p>

## :x: Incorrect:

```csharp
(float value1, float value2) => DoSomething(value1, value2);

(float value1, float value2) 
    => DoSomething(value1, value2);

(float value1, float value2) => { return DoSomething(value1, value2); }
```

## :heavy_check_mark: Correct:

```csharp
(float value1, float value2) =>
{
    return DoSomething(value1, value2);
}
```

</p>
</details>

</p>
</details>