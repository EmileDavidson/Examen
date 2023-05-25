# Emile

> Just a quick note I've worked on more than just this. but these are the ones most worth mentioning.

## 1. Customers&#x20;

### 1.1 PathFinding + Grid

Technical Design:

{% content-ref url="../technical-design/customer/customer-pathing.md" %}
[customer-pathing.md](../technical-design/customer/customer-pathing.md)
{% endcontent-ref %}

Github PathFinding:

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Grid/GridPathFinding" %}

Github Grid:

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Grid" %}

#### grid

Grid is a class that allows us to create 3d / 2d grids in the world it also has some gizmos that allows us to few the grid and see if it is at the right position by giving and offset (offset is from 0,0) or by setting the cell size it works stand alone and does not require any other scripts outside of the grid folder&#x20;

<figure><img src="../../.gitbook/assets/Unity_CUreZS8Beh.png" alt=""><figcaption></figcaption></figure>

class diagram:

```mermaid

classDiagram
    class GridHelper {
        + Grid3dLoop(width, height, depth, callback)
    }

    class GridNode {
        - IsBlocked: bool
        - IsTempBlocked: bool
        - IsLocationNode: bool
        - GridPosition: Vector3Int
        - Index: int
        - TempBlockedBy: List<string>
        + GridNode(index, x, y, z, blockedNode)
        + SetBlocked(doBlock)
        + SetTempBlock(doTempBlock, id)
        + SetLocationNode(isLocationNode)
        + IsBlockedBy(id)
    }

      class MyGrid {
        - Vector3 pivotPoint
        - int nodeSize
        - Vector3Int gridSize
        - List<GridNode> nodes
        - UnityEvent onResetGrid
        - UnityEvent onGridChanged
        - UnityEvent<GridNode> onGridChangedWithNode
        + Width: int
        + Height: int
        + Depth: int
        + PivotPoint: Vector3
        + ShouldGenerate(): bool
        + GenerateGrid(): MyGrid
        + ResetGrid(): MyGrid
        + GetNodeByIndex(index: int): GridNode
        + GetNodeByPosition(position: Vector3Int): GridNode
        + GetDirectNeighbourList(currentNode: GridNode): List<GridNode>
        + GetTotalNodeCount(): int
    }
    GridHelper --> GridNode
    MyGrid --> GridNode
    GridHelper --> MyGrid

```

#### PathFinding

Pathfinding is an extension from grid it allows us to create or calculate paths on the grid and returns a Path object (a list of grid nodes) this can than be used by anyone that knows of it for whatever it wants.  &#x20;

<figure><img src="../../.gitbook/assets/Unity_16BQIWAUJp.gif" alt=""><figcaption></figcaption></figure>

class diagram:

```mermaid

classDiagram
    class GridNode {
        - int Index
        - int X
        - int Y
        - int Z
        - bool Blocked
        - bool TempBlocked
        - bool Endpoint
        - bool Walkable
    }

    class Path {
        - List<int> PathNodes
        - int CurrentIndex
        - GridNode StartNode
        - GridNode EndNode
        - bool DestinationReached
        + GetNextNodeIndex(): int
        + Reset(): void
        + Copy(): Path
    }

    class MyGrid {
        - Vector3 pivotPoint
        - int nodeSize
        - Vector3Int gridSize
        - List<GridNode> nodes
        - UnityEvent onResetGrid
        - UnityEvent onGridChanged
        - UnityEvent<GridNode> onGridChangedWithNode
        + Width: int
        + Height: int
        + Depth: int
        + PivotPoint: Vector3
        + ShouldGenerate(): bool
        + GenerateGrid(): MyGrid
        + ResetGrid(): MyGrid
        + GetNodeByIndex(index: int): GridNode
        + GetNodeByPosition(position: Vector3Int): GridNode
        + GetDirectNeighbourList(currentNode: GridNode): List<GridNode>
        + GetTotalNodeCount(): int
    }

    class PathFinding {
        - MyGrid grid
        - GridNode startNode
        - GridNode endNode
        - List<GridNode> openList
        - List<GridNode> closedList
        - Dictionary<GridNode, PathFindingCost> nodeCosts
        - Dictionary<GridNode, GridNode> cameFrom
        + FindPath(start: GridNode, end: GridNode): Path
        + CalculatePathCost(node: GridNode, cameFromNode: GridNode): PathFindingCost
        + CalculateHeuristicCost(node: GridNode): int
        + GetPath(): Path
        + Clear(): void
    }

    GridNode --> Path
    Path --> GridNode
    MyGrid --> GridNode
    MyGrid --> Path
    PathFinding --> MyGrid
    PathFinding --> GridNode

```

### 1.2 States

Technical design:&#x20;

{% content-ref url="../technical-design/customer/customer-states.md" %}
[customer-states.md](../technical-design/customer/customer-states.md)
{% endcontent-ref %}

Github:

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Customer/CustomerStates" %}

#### Customer States

The customers are a vital part of the game but they also have a lot of different states they can be in, so I created a sort 'statemachine' that triggers state logic giving us the ability make a lot of states for the customers and having them do different things depending on the situation&#x20;

state diagram:

```mermaid
stateDiagram
    [*] --> Spawned
    Spawned --> WalkingToEntrance
    WalkingToEntrance --> WalkingToProducts
    WalkingToProducts --> GettingProducts
    GettingProducts --> WalkingToCheckout
    WalkingToCheckout --> DroppingProducts
    DroppingProducts --> WalkingToExit
    WalkingToExit --> [*]
```

## 2. Player

### 2.1 PlayerInputActions + Connecting (coop)&#x20;

Github (PlayerInputManagerExtensions):

{% embed url="https://github.com/EmileDavidson/Examen/blob/develop/ExamenProeveUnity/Assets/Scripts/Runtime/Player/PlayerInputManagerExtension.cs" %}

Github (Action map):

{% embed url="https://github.com/EmileDavidson/Examen/blob/develop/ExamenProeveUnity/Assets/PlayerControls.inputactions" %}

Github (ResolverIterator):&#x20;

{% embed url="https://github.com/EmileDavidson/Examen/blob/develop/ExamenProeveUnity/Assets/Scripts/Utilities/Other/Runtime/ResolverIteratorChanger.cs" %}

#### PlayerInputActions

PlayerInputActions is just the ActionMap created by unitys latest input system nothing all to big but a big part in our coop games since it allowed us to link controllers or keyboards to a input script so we could have multiple players.

<figure><img src="../../.gitbook/assets/Photoshop_J0AwyhblU1.png" alt=""><figcaption></figcaption></figure>

#### PlayerInputManagerExtensions

We used the PlayerInputManager to handle devices connecting and spawning a prefab but unity doesn't allow for spawn locations or multiple prefabs so I created the PlayerInputManagerExtensions that does 2 things it allows us to spawn multiple player prefabs and allows us to give spawn locations&#x20;

as for why I created a script instead of inheriting PlayerInputManager is simple the inspector was custom drawn for the PlayerInputManager so it looked awful if I used inherting&#x20;

### 2.2 PlayerAnimation

Technical Design:

{% content-ref url="../technical-design/player/animations.md" %}
[animations.md](../technical-design/player/animations.md)
{% endcontent-ref %}

#### PlayerAnimations (ragdoll with animator)&#x20;

Since we used a ragdoll player we couldn't simply use the animator to animate the player so a solution to this is creating a copy of the player and copy the rotation from that copy to the ragdoll target rotation, allowing us to the animator to animate ragdoll&#x20;

### 2.3 ResolverIterator

The ResolerIterator on its own is nothing special but in this case its now about the script but more about its function. so.. the players had a lot of problems mostly with stretching when moving to fast or when grabbing a object that is to heavy or having force to the opposite direction you want to walk and we had a hard time fixing it and than we found out we could just update the rigidbody ResolverIterator to a higher number and this fixed the issue!&#x20;

so its not about the script but all the hard work we put in to fixing the ragdoll stretching even tough the solution was so simple.&#x20;



## 3. Tools

### 3.1 Method Extensions

Github:

{% embed url="https://github.com/EmileDavidson/Examen/tree/develop/ExamenProeveUnity/Assets/Scripts/Utilities/MethodExtensions" %}

#### Method Extension

We added a ton of method extensions previously created or even new ones during the development this greatly sped up the process of creating certain scripts one method extension is not worth mentioning but the amount we have sure is!

### 3.2 MonoSingleton

Github:

{% embed url="https://github.com/EmileDavidson/Examen/blob/develop/ExamenProeveUnity/Assets/Scripts/Utilities/Other/Runtime/MonoSingleton.cs" %}

#### MonoSingleton

A powerful tool to have but also a great risk and " a lot " of work to set up each time you want to make a singleton, so I created the monosingleton that can be used instead of a monobehaviour and its only use is making a singleton from a monobehaviour so you can think about how to set up the script instead of how to make the singleton&#x20;











