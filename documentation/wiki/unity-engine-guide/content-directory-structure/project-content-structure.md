# Project content structure

<pre><code>Assets
├── Graphics or Art
│   ├── Models or 3d 
│   ├── Textures
│   ├── Sprites
│   ├── Meshes
│   ├── Shaders
│
├── Scenes                     
│    ├── MainMenu.scene            
│    ├── world1                  
│    │    ├── Level1.scene
│    │    └── Level2.scene
│    ├── world2                  
│    │    ├── Level1.scene
│    │    └── Level2.scene
│    └── Testing                  &#x3C;-- Testing scenes scene naming should end with 'test'
│         ├── VFXTest.scene
│         └── PlayerMovementTest.scene
│
├── Animators
│    ├── AC_Character
<strong>│    ├── AC_Customer
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
│    ├── Runtime                  &#x3C;- All scripts that are runtime scripts aka monoBehaviours 
│    │    ├── Player
│    │    ├── Inventory
│    │    ├── Interaction
│    │    └── Enviroment 
│    ├── Editor                   &#x3C;-- All scripts that are used in the editor 
│    │    ├── Player
│    │    ├── Inventory
│    │    └── Interaction
│    └── Utilities                &#x3C;-- All scripts that are neither used for runtime or Editor but can be used for both.
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
