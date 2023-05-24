# Code comments

### Comments or Summary

Summary is used to give some more information about what the whole function does and what the parameters are used for it can also be used to give some examples on how to use the method, if you have some callbacks, or some unusual code

Comments are used to give more information about a certain part of a method this can be really useful when there is a big mathematical formula or some weird code that is hard to understand by reading, a comment can help to clarify to code without spending hours deciphering&#x20;

in general we mostly want to use summary since most IDE's will have some visuals when hovering over a method where the summary is displayed this helps if you are not sure what the function does and you don't want your IDE cluttered with opened files since you wanted to see a method logic&#x20;

### Functions Should Have A Summary

Best practice would be to give all methods a summary but it would take forever to do so we don't force it and we don't check on it when doing a pull request&#x20;

```csharp
/// <summary>
/// Fires the gun if possible, and removes a bullet from the [bulletCount]
/// </summary>
public void Fire()
{
}
```

### Comment styling

* Comments are ALWAYS above the code you are commenting for not under, or next to it but above it.
* Comments should follow the normal grammer practice so start with a Capalized character and the use of . and , would be appreciated.
* use the // version of a comment except for big comments these may use the /\* \*/ version so you can put it on multiple lines &#x20;

```csharp
class ClassName{
    ///<Summary>
    /// returns the end score in 0 to 1 value 
    /// where 0 stands for the player getting 0 points 
    /// and 1 stands for the player getting max points 
    ///</Summary>
    private float CalculateNormalizedEndScore(){
        //comment examplaing the math behind it 
        //this is not needed in the summary since they don't need to know how it works just that it works.
        double endScore = (initialScore * 3.14) + Math.Sqrt(age) - Math.Pow(favoriteNumber, 2);
        double normalizedEndScore = (endScore - 0) / (10 - 0); 
        normalizedEndScore = Math.Max(0, Math.Min(1, normalizedEndScore));

        return normalizedEndScore;
    }
}
```

### The biggest downfall of comments and summary

One big problem you see with comments and summary is that it doesn't update often even if the code changed and overtime the comment can be way of from what the code actually does&#x20;

the simplest way to prevent this is to read / change the comments / summary each time you make a change but this unfortunaly doesn't happen all the time&#x20;

so its best to not always believe a comment but rather use it as a helping hand to understand the logic&#x20;
