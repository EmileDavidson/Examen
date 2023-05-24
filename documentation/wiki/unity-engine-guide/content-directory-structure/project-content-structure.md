# Project content structure

<pre><code>Assets
├── Graphics or Art
│   ├── Models or 3d 
│   ├── Textures
│   ├── Sprites
│   ├── Meshes
│   └── Shaders
│
├── Scenes                     
│    ├── MainMenu.scene            
│    ├── world1                  
│    │    ├── Level1.scene
│    │    └── Level2.scene
│    ├── world2                  
│    │    ├── Level1.scene
│    │    └── Level2.scene
│    └── Testing              &#x3C;-- Testing scenes scene naming should end with 'test'
│         ├── VFXTest.scene
│         └── PlayerMovementTest.scene
│
├── Animators
│    ├── AC_Character
<strong>│    └── AC_Customer
</strong>│
├── Lighting     
├── ScriptableObjects
│
├── MaterialLibrary            
│    ├── Debug
│    └── Shaders
│
├── Prefabs                
│
├── Scripts
│    ├── Runtime          
│    │    ├── Player
│    │    ├── Inventory
│    │    ├── Interaction
│    │    └── Enviroment 
│    ├── Editor           
│    │    ├── Player
│    │    ├── Inventory
│    │    └── Interaction
│    └── Utilities         
│
├── Sound                     
│    ├── Characters
│    ├── Vehicles
│    │    └── TieFighter
│    │    └── Abilities
│    │    └── Afterburners
│    └── Weapons
│
├── Fonts
├── ThirdParty
└── TextMesh Pro

</code></pre>

{% hint style="warning" %}
If we would do it again we we make a big change to all materials, textures, fonts, animators read more about it [here](material-library.md)&#x20;
{% endhint %}
