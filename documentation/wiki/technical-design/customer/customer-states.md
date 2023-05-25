# Customer States

The customers in the game can and do a lot so how did we manage this? we did this by adding states to the customer that handles its current state.&#x20;



### What are states?

A state is a version of the customer it can be in meaning it can behave differently in each state or have some base logic it does but can be overwritten if needed.

### The states we have

```
/// <summary>
/// All states the customer can be in
/// </summary>
public enum CustomerState
{
    Spawned,
    WalkingToEntrance,
    WalkingToProducts,
    GettingProducts,
    WalkingToCheckout,
    DroppingProducts,
    WalkingToExit,
}
```

### the logic states handled&#x20;

States are managed by the controller and have some base start, update, finish, ongrab, onrelease methods by altering these we can change its behaviour for instance wen the customer is grabbed but it's in the DroppingProcúts state it will always claim the cash register node as his node until the state changes. this makes is so we can wait until the customer is at the cash register before going to the walking to exit state as if he went back and payed for its products.&#x20;

* Start is triggered when the state 'starts'
* Update gets triggered every update
* finish is a method that can be triggered in the state itself and handles going to the next state in the list
* onGrabbed is when a player grabs the customer and drags him around the store (default method turns of movement and unblocks all nodes the customer blocked.
* onReleased is triggered when the player is no longer grabbed by the customer and asks the controller to recalculate the path when possible.&#x20;

### our scripts

all customer scripts and states can be found in github under 'assets/scripts/runtime/customer' or by clicking this link: [https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Customer](https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Customer)
