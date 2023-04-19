# Do Not Create Folders Called Assets or AssetTypes

#### Creating a folder named Assets is redundant.

_All assets are assets._

Creating a folder named Meshes, Textures, or Materials is redundant. All asset names are named with their asset type in mind. These folders offer only redundant information and the use of these folders can easily be replaced with the robust and easy to use filtering system the Content Browser provides.

Want to view only static mesh in Environment/Rocks/? Simply turn on the Static Mesh filter. If all assets are named correctly, they will also be sorted in alphabetical order regardless of prefixes. Want to view both static meshes and skeletal meshes? Simply turn on both filters. this eliminates the need to potentially have to Control-Click select two folders in the Content Browser's tree view.

This also extends the full path name of an asset for very little benefit. The SM\_ prefix for a static mesh is only three characters, whereas Meshes/ is seven characters.

Not doing this also prevents the inevitability of someone putting a static mesh or a texture in a Materials folder.
