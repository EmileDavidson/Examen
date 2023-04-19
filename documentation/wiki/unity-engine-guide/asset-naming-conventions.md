# Asset Naming Conventions

## Asset Naming Conventions

Most things are prefixed with the prefix generally being an acronym of the asset type followed by an underscore.

`[AssetTypePrefix]_[AssetName]_[Descriptor]_[OptionalVariantLetterOrNumber]`

* `AssetTypePrefix`identifies the type of Asset, refer to the table below for details.
* `AssetName` is the Asset's name.
* `Descriptor` provides additional context for the Asset, to help identify how it is used. For example, whether a texture is a normal map or an opacity map.
* `OptionalVariantLetterOrNumber` is optionally used to differentiate between multiple versions or variations of an asset.

## Asset Prefixes

This list is not exhaustive, as new features can require new Asset types. If you are using an Asset type not listed, use the existing list as a guideline for your naming convention for that Asset.

Examples based on the contentions in the table below:

* `M_Collectable`
* `T_Collectable_Normal`

#### Base Asset Name - `Prefix_BaseAssetName_Variant_Suffix`

All assets should have a _Base Asset Name_. A Base Asset Name represents a logical grouping of related assets. Any asset that is part of this logical group should follow the the standard of `Prefix_BaseAssetName_Variant_Suffix`.

Keeping the pattern `Prefix_BaseAssetName_Variant_Suffix` in mind and using common sense is generally enough to warrant good asset names. Here are some detailed rules regarding each element.

`Prefix` and `Suffix` are to be determined by the asset type through the following Asset Name Modifier tables.

`BaseAssetName` should be determined by short and easily recognizable name related to the context of this group of assets. For example, if you had a character named Wouter, all of Wouter's assets would have the `BaseAssetName` of `Wouter`.

For unique and specific variations of assets, `Variant` is either a short and easily recognizable name that represents logical grouping of assets that are a subset of an asset's base name. For example, if Wouter had multiple skins these skins should still use Wouter as the `BaseAssetName` but include a recognizable `Variant`. An 'Evil' skin would be referred to as `Wouter_Evil` and a 'Retro' skin would be referred to as `Wouter_Retro`.

For unique but generic variations of assets, `Variant` is a two digit number starting at `01`. For example, if you have an environment artist generating nondescript rocks, they would be named `Rock_01`, `Rock_02`, `Rock_03`, etc. Except for rare exceptions, you should never require a three digit variant number. If you have more than 100 assets, you should consider organizing them with different base names or using multiple variant names.

Depending on how your asset variants are made, you can chain together variant names. For example, if you are creating flooring assets for an Arch Viz project you should use the base name `Flooring` with chained variants such as `Flooring_Marble_01`, `Flooring_Maple_01`, `Flooring_Tile_Squares_01`.

**Examples**

**Character**

| Asset Type                     | Asset Name       |
| ------------------------------ | ---------------- |
| Skeletal Mesh                  | SK\_Wouter       |
| Material                       | M\_Wouter        |
| Texture (Diffuse/Albedo)       | T\_Wouter\_A     |
| Texture (Normal)               | T\_Wouter\_N     |
| Texture (2nd variant, Diffuse) | T\_Wouter\_02\_D |

**Prop**

| Asset Type               | Asset Name     |
| ------------------------ | -------------- |
| Static Mesh (01)         | SM\_Rock\_01   |
| Static Mesh (02)         | SM\_Rock\_02   |
| Static Mesh (03)         | SM\_Rock\_03   |
| Material                 | M\_Rock        |
| Material Instance (Snow) | MI\_Rock\_Snow |

## Asset Name Modifiers

When naming an asset use these tables to determine the prefix and suffix to use with an asset's Base Asset Name.

**Sections**

> * Most Common
> * Animations
> * Artificial Intelligence
> * Prefabs
> * Materials
> * Textures
> * Miscellaneous
> * Physics
> * Audio
> * User Interface
> * Effects

**Most Common**

| Asset Type      | Prefix | Suffix | Notes                                                             |
| --------------- | ------ | ------ | ----------------------------------------------------------------- |
| Level / Scene   |        |        | Should be in a folder called Scenes. e.g. `Scenes/MainMenu.unity` |
| Prefab          |        |        |                                                                   |
| Material        | M\_    |        |                                                                   |
| Static Mesh     | SM\_   |        |                                                                   |
| Texture         | T\_    |        | See Textures section for suffix                                   |
| Particle System | P\_    |        |                                                                   |
| Shader          | SH\_   |        |                                                                   |

**3D Models (FBX Files)**

PascalCase

| Asset Type  | Prefix | Suffix | Notes |
| ----------- | ------ | ------ | ----- |
| Static Mesh | SM\_   |        |       |

**3d Models (3ds Max)**

All meshes in 3ds Max are lowercase to differentiate them from their FBX export.

| Asset Type | Prefix | Suffix       | Notes                                   |
| ---------- | ------ | ------------ | --------------------------------------- |
| Mesh       |        | \_mesh\_lod0 | Only use LOD suffix if model uses LOD's |

**4.2.2 Animations**

| Asset Type           | Prefix | Suffix | Notes |
| -------------------- | ------ | ------ | ----- |
| Animation Clip       | A\_    |        |       |
| Animation Controller | AC\_   |        |       |
| Morph Target         | MT\_   |        |       |

**Prefabs**

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Prefab            |        |        |       |
| Prefab Instance   | I      |        |       |
| Scriptable Object |        |        |       |

**Materials**

| Asset Type        | Prefix | Suffix | Notes |
| ----------------- | ------ | ------ | ----- |
| Material          | M\_    |        |       |
| Physical Material | PM\_   |        |       |

**Textures**

| Asset Type                          | Prefix | Suffix | Notes                         |
| ----------------------------------- | ------ | ------ | ----------------------------- |
| Texture                             | T\_    |        |                               |
| Texture (Diffuse/Albedo/Base Color) | T\_    | \_A    |                               |
| Texture (Normal)                    | T\_    | \_N    |                               |
| Texture (Roughness)                 | T\_    | \_R    |                               |
| Texture (Alpha/Opacity)             | T\_    | \_O    |                               |
| Texture (Ambient Occlusion)         | T\_    | \_AO   |                               |
| Texture (Bump)                      | T\_    | \_B    |                               |
| Texture (Height)                    | T\_    | \_H    |                               |
| Texture (Emissive)                  | T\_    | \_E    |                               |
| Texture (Mask)                      | T\_    | \_M    |                               |
| Texture (Specular)                  | T\_    | \_S    |                               |
| Texture (Packed)                    | T\_    | \_\*   | See notes below about packing |

**Texture Packing**

It is common practice to pack multiple layers of texture data into one texture. An example of this is packing Metallic, Roughness, Ambient Occlusion together as the Red, Green, and Blue channels of a texture respectively. To determine the suffix, simply stack the given suffix letters from above together, e.g. `_ORM`.

> It is generally acceptable to include an Alpha/Opacity layer in your Diffuse/Albedo's alpha channel and as this is common practice, adding `A` to the `_D` suffix is optional.

Packing 4 channels of data into a texture (RGBA) is not recommended except for an Alpha/Opacity mask in the Diffuse/Albedo's alpha channel as a texture with an alpha channel incurs more overhead than one without.

**Miscellaneous**

| Asset Type                            | Prefix | Suffix | Notes |
| ------------------------------------- | ------ | ------ | ----- |
| High Definition Render Pipeline Asset | HDRP\_ |        |       |
| Universal Render Pipeline Asset       | URP\_  |        |       |
| Post Process Volume Profile           | PP\_   |        |       |

**Audio**

| Asset Type     | Prefix | Suffix | Notes |
| -------------- | ------ | ------ | ----- |
| Sound Clip     | S\_    |        |       |
| Sound Cue      | SC\_   |        |       |
| Audio Mixer    | MIX\_  |        |       |
| Dialogue Voice | DV\_   |        |       |

**4.2.10 User Interface**

| Asset Type       | Prefix | Suffix | Notes |
| ---------------- | ------ | ------ | ----- |
| Font             | Font\_ |        |       |
| Texture (Sprite) | T\_    |        |       |

**4.2.11 Effects**

| Asset Type             | Prefix | Suffix | Notes |
| ---------------------- | ------ | ------ | ----- |
| Post Processing Volume | PP\_   |        |       |
| Particle System        | P\_    |        |       |
