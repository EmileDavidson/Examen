# Extract methods

Extraction is trying to create a method for each piece of code this is helpful for 3 things it may limit nesting since a nesting of 3 can be converted to a simple method call, besides limiting nesting it also makes the code a lot easier to read and re-usable&#x20;

### Extraction for readability

In unity we have some main methods (onEnable, onDisable, awake, start, update) and instead of placing all code in these methods its better to extract all logic in separate logic there are 3 ways this will increase readability&#x20;

1. By extracting a method you get a simple method name to read like "HandleMovement", "HandleFireButtonClicked" rather than a block of code where you need to read all of it&#x20;
2. methods are more likely to only contain code needed for its function if you have all code under another the chance of putting some logic that is not needed in the code block since the code beneath it needs it is a lot higher.&#x20;
3. Logic don't need to be nested if you have all logic under another you can't return early since you might have wanted to run the Jump logic but not the Move logic by converting it to a method you can just return the method when needed

### Extraction for never nesting&#x20;

Extracting code in its own method can reduce the amount of brackets drasticly wel... it doesn't remove them but it separates so you can have max of 3 brackets per method a good example would be when you want a 3d loop&#x20;

```
public static void CreateGrid(int width, int height, int depth, Action<int, Vector3Int> callback)
{
    int cellIndex = 0;
    for (var gridZ = 0; gridZ < depth; gridZ++)
    {
        for (var gridY = 0; gridY < height; gridY++)
        {
            for (var gridX = 0; gridX < width; gridX++)
            {
                GridNode node = new GridNode(index, gridPos.x, gridPos.y, gridPos.z);
                nodes.Add(node);   
                cellIndex++;
            }
        }
    }
}
```

can be converted to&#x20;

```csharp
public static void CreateGrid(int width, int height, int depth, Action<int, Vector3Int> callback)
    GridHelper.Grid3dLoop(Width, Height, Depth, (index, gridPos) =>
    {
        GridNode node = new GridNode(index, gridPos.x, gridPos.y, gridPos.z);
        nodes.Add(node);
    });
}

//some class you almost never need to update so you won't see it 
public class GridHelper
    {
        public static void Grid3dLoop(int width, int height, int depth, Action<int, Vector3Int> callback)
        {//nesting1
            int cellIndex = 0;
            for (var gridZ = 0; gridZ < depth; gridZ++)
            {//nesting 2
                for (var gridY = 0; gridY < height; gridY++)
                {//nesting3
                    for (var gridX = 0; gridX < width; gridX++)
                    {//nesting 4
                        callback.Invoke(cellIndex, new Vector3Int(gridX, gridY, gridZ));
                        cellIndex++;
                        
                        //this is the limit we don't want to see any more nesting here.
                    }
                }
            }
        }
    }
```

### Extraction for re-usability&#x20;

As you might've seen in the example in nesting by converting code in to methods we can re-use it without having to re-write each line every time. as can seen in the example each time we want to do a 3d loop we only need to call one method that gives us (index, x, y, z) values and can be used like the a for loop except you can't return to stop the loop&#x20;

```csharp
public static void CreateGrid(int width, int height, int depth, Action<int, Vector3Int> callback)
    //reused 3d loop
    GridHelper.Grid3dLoop(Width, Height, Depth, (index, gridPos) =>
    {
        GridNode node = new GridNode(index, gridPos.x, gridPos.y, gridPos.z);
        nodes.Add(node);
    });
}

public class GridHelper
    {
        public static void Grid3dLoop(int width, int height, int depth, Action<int, Vector3Int> callback)
        {
            int cellIndex = 0;
            for (var gridZ = 0; gridZ < depth; gridZ++)
            {
                for (var gridY = 0; gridY < height; gridY++)
                {
                    for (var gridX = 0; gridX < width; gridX++)
                    {
                        callback.Invoke(cellIndex, new Vector3Int(gridX, gridY, gridZ));
                        cellIndex++;
                    }
                }
            }
        }
    } 
```
