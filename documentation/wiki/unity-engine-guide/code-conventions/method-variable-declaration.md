# method variable declaration

### Where do I declare variables?

When creating a method its almost certain you will create some local variables used inside of the method for this we have one rule do it at the top of the method (excluding for loop declarations etc..) this makes it so by reading a method I can see all local variables at once and I don't have to look if its a local variable, static variable or any other time of variable somewhere hidden since all variables are either at the top of class or top of the methods&#x20;

```csharp
public void FetchData(){
    int page = 0;
    int accountId = AccountData.Instance.id;
    int accountToken = AccountData.Instance.token;
    
    //now the logic we want to run 
}
```

### But what if i need to validate?

If you want to validate the variables you can do that between declaration but yes there is a but these validations may only be return statements or if null set to this statements and may not contain any nesting. (except if needed but chances are low)

also if you take a look at the example by adding adding validations the code may become cluttert so if possible try doing the validation after the variable declaration or try using operators like the ?? or ??= or ?

```csharp
//the ?? operator means if left is null use right 
//if tryGetIcon() is null it will use defaultIcon();
icon = tryGetIcon() ?? defaultIcon();

//the ??= operator is almost the same but is used on already declared values
//if icon is null it will be set to defaultIcon();
icon ??= defaultIcon(); 

//the ? operator stops the next code if the value is null (can not be used on everything)
//in the example if instance is null handleCreation will not trigger
AccountData.Instance?.HandleCreation();
```

```csharp
//good example
public void FetchData(){
    int page = 0;
    
    if(AccountData.Instance == null){
        CreateAccountData();
    }
    
    int accountId = AccountData.Instance.id;
    int accountToken = AccountData.Instance.token;
    
    //now the logic we want to run 
}
```

```csharp
//another good example
public void FetchData(){
    int page = 0;
    int accountId = AccountData.Instance.getId() ?? -1;    
    int accountToken = AccountData.Instance.getToken() ?? -1;
    
    if(accountId == -1 || accountToken == -1){
        return;
    }
    //now the logic we want to run 
}
```
