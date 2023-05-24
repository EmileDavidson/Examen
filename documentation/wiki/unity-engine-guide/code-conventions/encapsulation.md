# Encapsulation

### Fields

All fields must be private it best to have all fields private and create getters & setters to access them from outside scripts this so you can more easily add validation and or constraints without needing to change all references you already created before.&#x20;

as for fields that you want to see in the inspector instead of making them public add the \[serializedField] attribute in front of it this will give the same result without lowering the protection &#x20;

### Methods

It is a good practice to make methods private whenever possible. and protected when needed in a inherited class. Only when the method is needed by a outside class it may be made public &#x20;

{% hint style="info" %}
Note that this would be best practice but we don't force this, and we won't check this all to hard in the pull request we do check it but it doesn't matter if we skipped it on accident&#x20;
{% endhint %}

