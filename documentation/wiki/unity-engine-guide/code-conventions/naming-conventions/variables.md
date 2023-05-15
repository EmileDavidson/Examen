---
description: How to name variables
---

# Variables

In this section, we will explain all the rules we have when naming a Variable / Property&#x20;

## Consider Type

The most important thing is to consider type this is mostly for types like a boolean or scripts like controller, handler, manager&#x20;

```csharp
//Booleans will start with a question like word such as 'is' 'was' 'should' 
//by doing this you can clearly see things will returns either a bool 
bool isWalking = false;
bool isRunning = false;
bool isFlying = false;
bool isFetchingData = false;
bool isOrdering = false;
bool wasFlying = false;
bool wasRunning = false;
bool wasTalking = false;

//Managers, Controllers, Handlers will include there type at the end and start
//for 'what' so manager, controller, handler 
GameManager _gameManager;
WorldManager _worldManager;
LevelManager _levelManager;
CustomerHandler _customerHandler;

//integers should end with things like count or length or in(Unit)  
//but there are also worlds generaly know to be a integer like width, height, index
int playerCount = 0;
int userLengthInCentimeters = 180
int waitTimeInSeconds = 10
int index = 1;
int width = 512;
int height = 1024;

//Enumeratable elements should be show that it can be more then one item
//this is mostly done by putting an 's' behind the world 
var players = [];
var items = [];
var numbers = [];
```

> It would be next to impossible to have an example for all types but I hope this gives a good understanding that a variable should clearly show what 'type' it holds&#x20;

## abbreviated (shortened)

Variables should not be shortened it's better to have a bigger variable name since you can more easily tell for what it is and what it holds&#x20;

```csharp
// good examples

int queueIndex;
int width;
int height;

// bad examples
 
int qi; //queueIndex
int w; //width
int h; //height
```
