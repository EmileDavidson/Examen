# Platform specific code

Platform-specific code should always be abstracted and implemented in platform-specific source files in appropriately named subdirectories, for example:

```csharp
Assets/Scripts/Runtime/[PLATFORM]/[PLATFORM]Memory.cs
```

In general, you should avoid adding any uses of PLATFORM\_\[PLATFORM] (e.g PLATFORM\_XBOXONE) to code outside of a directory named \[PLATFORM].

Instead, use interfaces to create a more abstract system. This allows the code to use the interface functions and we can compile platform specific code for each platform.

```csharp
public interface IMicrophoneListener() {
    public float GetMicrophone();
}
```

Platform specific code can the create classes that extend this interface and use platform specific code the return the value.

for platform specific code it's usefull to use Assembly Definitions as you can decide which assembly definition compiles in each platform.

In some cases you need to add platform-specific code in the same class. This can be achieved using

```csharp
private void SomeMethode() {
#if (UNITY_EDITOR)
        Debug.log("The is ran in the editor");
#elseif
        Debug.Log("This does not run in the editor");
#endif
}
```
