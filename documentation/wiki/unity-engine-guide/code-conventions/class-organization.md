# Class Organization

Classes should be organized with the reader in mind rather than the writer. Since most readers will be using the public interface of the class

Scripts should only have on public type (class, struct etc..), although multiple internal classes are allowed.

Script file name should be the name of the public class in the file.

Organize namespaces with a clearly defined structure,

Class members should be alphabetized, and grouped into sections:

* Constant Fields
* Static Fields
* Fields
* Events / Delegates
* Constructors
* LifeCycle Methods (Awake, OnEnable, OnDisable, OnDestroy)
* Public Methods
* Private Methods
* Nested types
* Getters & Setters

Within each of these groups order by access:

* serializedFields
* public
* internal
* protected
* private

The only exception to this 'order' are events like UnityEvent this so that the inspector stays clean and organized.

<pre class="language-csharp"><code class="lang-csharp">namespace ProjectName
{
	/// &#x3C;summary>  
	/// Brief summary of what the class does
	/// &#x3C;/summary>
    public class Account
    {
      //first const values
      public const string AccountKey = "key"
      public const string AccountName = "Name"

      //second static values
      public static Account Instance;
      public static bool InstanceExists;
      
      /* now on order of access */
      [serializedField] public RefenceClass1 refenceClass1;
      [serializedField] public ReferenceClass2 RefenceClass2;
      
      public string name;
      public string displayName;
      
      protected List&#x3C;valueType> protectedList = new();
      protected EnumType accountType;
      
      private int balance;
      private string balanceDisplay;

      
      // UnityEvents should always be defined last. 
      public UnityEvent onBalanceAdded = new();
      public UnityEvent onBalanceRemoved = new();
      public UnityEvent onBalanceChanged = new();
           
      //event method 
      public onEnable(){}
      public onDisable(){}
      public Awake(){}
      public Start(){}
      public Update(){}
<strong>      public FixedUpdate(){}
</strong><strong>      
</strong><strong>      //custom event methods
</strong><strong>      public onInput(){}
</strong>      public onInput1(){}
      
      //methods order by access
      public void AddBalance(){}
      public void RemoveBalance(){}
    
      protected void protectedMethod(){}
      protected void protectedMethod1(){}
      
      private void UpdateBalanceDisplay(){}
      private void privateMethod(){}

      //getters &#x26; setters always nice to see a region :)
      #region getters &#x26; setters
      
      public int Balance {get; private set;}
      public string DisplayName {get; private set;} 
      
      #endregion //getters &#x26; setters
    }
}
</code></pre>

### Namespace

Use a namespace to ensure your scoping of classes/enum/interface/etc won't conflict with existing ones from other namespaces or the global namespace. The project should at minimum use the projects name for the Namespace to prevent conflicts with any imported Third Party assets.

### Functions Should Have A Summary

All functions should have a summary is a hard statement in general it would be great if all methods would have a summary but its not mandetory except for public methods&#x20;

```csharp
/// <summary>
/// Fires the gun if the gun is loaded and removes a bullet from the [bulletCount] 
/// </summary>
public void Fire()
{
}
```

### Commenting

In general we don't want comments a method name should clearly show what should happen and the summary would also amplify that effect but in the case that the method contains some 'weird' code and or hard to understand code you can write a small comment explaining it a bit without going to indept we also don't want a comment for everything if you normalize a vector by doing .normalize I don't want to see a comment above it saying " //normalizing the vector" since by reading the code this is already clear.&#x20;

### Comment style

* All comments must be on top of the code you want to comment not next or under it.&#x20;
* Comments should follow the normal grammer practice so start with a Capalized character and the use of . and , would be appreciated.
* comments should end with a period.&#x20;
* use a space between the // and your actual comment as seen in the example.
* the // version of a comment starter should be used in most situations but long comments with more than one line may also use the /\* \*/ version of a comment&#x20;

```csharp
        // Sample comment above a variable.
        private int _myInt = 5;
```

### Spacing

Do use a single space after a comma between function arguments.

```
private void Method(arg1, arg2, arg3)
{}
```

* Do not use a space after the parenthesis and function arguments.
* Do not use spaces between a function name and parenthesis.
* Do not use spaces inside brackets.
