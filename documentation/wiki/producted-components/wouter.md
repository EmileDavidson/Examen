# Wouter

### What did I work on:

> This is a list of things I've worked on but we also did a lot of pair programming so I might have missed some scripts&#x20;

\-     [   Initial setup for customer pathfinding](https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Customer)

\-       [ Player ragdoll](../technical-design/player/ragdoll.md)

\-        Player grabbing

\-        Shelves and Checkout

\-       [ Game UI renderers](https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/UserInterfaces)

\-        Product ordering system

\-       [ Product Scriptable Object](https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Utilities/ScriptableObjects)

&#x20;

### Player

#### Player Ragdoll

[\[technical docs link\]](../technical-design/player/ragdoll.md)

The player ragdoll was added by using a rigged player model and adding

configurable joints to them as well as colliders.

![](<../../.gitbook/assets/player\_ragdoll (1).JPG>)

#### Player Grabbing

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Player" %}

&#x20;The player is able to use the left and right trigger on the controller

to grab products, boxes and other things with their left and right hand.

![](../../.gitbook/assets/player\_grab\_game.JPG)

&#x20;![](../../.gitbook/assets/player\_1\_grab.JPG)

![](<../../.gitbook/assets/player\_2\_grab (1).JPG>)

### Products

#### Product Ordering

The player can order products from a screen within the scene.

They are able to cycle through the different products and order them

by pressing the interact button. These order will appear on a different screen

showing what the truck will bring once it arrives

![](../../.gitbook/assets/product\_ordering.gif)&#x20;

![](../../.gitbook/assets/ordering\_screen.gif)

### Environment

#### Shelf & Cash Register

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Environment" %}

&#x20;The players are able to restock the shelves and the customers are able to grab items from it.

These actions will update a display that is shown above the shelf

![](../../.gitbook/assets/product\_shelf.JPG)

&#x20;

These products can be brought to the checkout and be scanned there by the player

![](../../.gitbook/assets/customer\_checkout.JPG)
