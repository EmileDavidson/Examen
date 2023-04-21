# Encapsulation

Enforce encapsulation with the protection keywords. it's best to have everything private and access them using a getter and or setter if you need them in derived classes make them protected rather than private same goes for methods \
\
if you need to access the property in the inspector use \[serializedField] instead of making it public. and if you use `public int Balance {get; private set;} = 0`  use \[Field: SerializedField] to display it in the inspector and try to keep the set private in all instances.&#x20;

{% hint style="info" %}
Note that this would be best practice but there are some instances where you really want something public this is up to the developer but choose wisely and use them sparingly&#x20;
{% endhint %}

