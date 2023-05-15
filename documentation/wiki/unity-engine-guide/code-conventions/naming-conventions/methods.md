---
description: How to name methods
---

# Methods

In this section, we will explain all the rules we have when naming a function and some general tips to get the best name for the given methods&#x20;

## Intent

A method should clearly show there intent, is it a method that will change data? or is it a method that will return a statement? or maybe just fetching data?

```csharp
//Good examples

FetchUserDetails(); 
UpdateUserDetails(fetchedData);
HasUserDetails(); 
DisplayUserDetails();
filterLists(Filter, list); 
OnPickup(); 
OnClick(); 
CalculateStats(); 
SortLists();
GetUserDetails();
GetUserStats();
GetVideoInput();

//bad examples

HandleData(); //handles the data? does it change the data or does it handle the display? what does it handle.
UserDetails(); //UserDetails what? I don't even know what i can give as example for this it looks like you are making a new object but missing the new keyword
OnData(); //onData what? OnDataReceived? OnDataHandled? OnDataRemoved? 
Stats(); //Gets the stats? updates the stats? handles the stats? 
```

In the examples, you can easily show what you want to do by using simple keywords like 'has', 'wants', 'fetch', 'update', 'get', and 'filter'.&#x20;

When you use words like 'has' or 'wants' in a question, you get a boolean answer. Words like 'filter' and 'update' do something with the given data, and 'fetch' or 'get' returns information. So these keywords help you express your intentions clearly.

now there are some exceptions since the class may also give some context if you are in a `class UserDetails{}` names like 'fetch' and 'update' will give enough information since fetch will most likely fetch the user details and update will update the user details or a `class List{}` with method names like 'sort' or 'find' will say enough&#x20;

## abbreviated (shortened)

Method should not be shortened its better to have a bigger method name then a method name that doesn't tell you anything about what it does&#x20;

```
// good examples

GetUserStats();
GetWorldSize();

// bad examples

WSize(); //GetWorldSize();
UserSts() //GetUserStats(); 
```
