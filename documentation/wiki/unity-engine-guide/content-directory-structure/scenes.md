# Scenes

### All .scene files must be under scenes folder

All scene files should be under "Assets/scenes" there can not be any other place with a scene file except for library example scene but these should generaly not be included when importating or removed after finishing the library.&#x20;

## Folder structure

Depending on the type of game you make the structure can change but we did it the following:

```
Assets/
└── Scenes/
    ├── MainScene.scene //always the first scene the user goes to. 
    ├── Levels/   
    │   ├── Level1.scene 
    │   └── Level2.scene 
    └── Testing/   
        ├── fvxTest.scene 
        └── {scriptName}Test.scene

```



### Scene Naming

For sene naming we don't really have hard rules but a good way of doing it can be seen in the folder structure section&#x20;

1. first scene is called mainScene.scene
2. level scenes have a index indicating the number (first level, second level, etc..)
3. test scenes end with the word test&#x20;
