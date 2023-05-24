# Class property organization

Class property should be grouped in the following order:

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

The only exception to this 'order' are events like UnityEvent these will always be the last properties in the class, this so that the inspector stays clean and organized.&#x20;

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

      
      #region getters &#x26; setters
      
      public int Balance {get; private set;}
      public string DisplayName {get; private set;} 
      
      #endregion //getters &#x26; setters
    }
}
</code></pre>

### Namespace

Use a namespace to ensure your scoping of classes/enum/interface/etc won't conflict with existing ones from other namespaces or the global namespace. The project should at minimum use the projects name for the Namespace to prevent conflicts with any imported Third Party assets.

###
