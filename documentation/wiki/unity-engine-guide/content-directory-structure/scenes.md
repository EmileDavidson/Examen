# Scenes

All Files Belong In A Folder Called Scenes, Scenes files are incredibly special and it is common for every project to have its own map naming system, especially if they work with sub-scenes or streaming scenes. No matter what system of map organization is in place for the specific project, all scenes should belong in `Assets/Scenes`.

Being able to tell someone to open a specific map without having to explain where it is is a great time saver and general 'quality of life' improvement. It is common for scenes to be within sub-folders of `scenes`, such as `Scenes/Campaign1/` or `Scenes/Arenas`, but the most important thing here is that they all exist within `Assets/Scenes`.

This also simplifies the job of baking for engineers. Wrangling Scenes for a build process can be extremely frustrating if they have to dig through arbitrary folders for them. If a team's Scenes are all in one place, it is much harder to accidentally not bake a map in a build. It also simplifies lighting build scripts as well QA processes.
