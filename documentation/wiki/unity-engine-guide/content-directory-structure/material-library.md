# Graphics

{% hint style="warning" %}
In our project we only used the 'global graphics' system where all types are separated but this just makes it really hard to find connections there for if we would do it again we would switch to this system
{% endhint %}

In the root folder (assets) there should've been 2 folders for graphics&#x20;

#### 1. Assets/Graphics&#x20;

This folder should contain all items separatly with all materials, shaders, textures needed for it in one place meaning if something needs to change about a model you can find all files connected to it right there in the same folder&#x20;

<pre><code>Assets/
└── Graphics/
    ├── Charactors/
    │   └── PlayerCharacter/
    │       ├── Player.fbx 
    │       ├── Materials/ 
    │       │   ├── Body.mat
    │       │   ├── Pants.mat
    │       │   └── Skin.mat
    │       ├── Sprites/
    │       │   └── something.png
    │       ├── Animation/
    │       │   ├── playerAnimator.animator
    │       │   └── idle.anim
    │       └── Shaders/ 
    │           └── outline.mat
<strong>    └── Vechiles/
</strong>        └── Truck/
            ├── Truck.fbx 
            ├── Materials/ 
            │   ├── Front.mat
            │   ├── Back.mat
            │   └── Container.mat
            ├── Sprites/
            │   └── something.png
            └── Shaders/ 
                └── something.mat
</code></pre>

2\. Assets/GlobalGraphics

GlobalGraphics should contains graphics that do not fit in a single item but can be used for multiple items these items should not have any connections to any other asset &#x20;

```
Assets/
└── GlobalGraphics/
    ├── Materials/
    │   ├── DebugMaterial1.mat
    │   └── DebugMaterial2.mat            
    ├── Textures/
    │   └── Emojis/
    │       ├── Happy.png
    │       └── Sad.png
    └── Shaders/
        ├── outline.shader  
        └── water.shader                
```

