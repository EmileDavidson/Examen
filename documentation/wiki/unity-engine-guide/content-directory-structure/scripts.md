# Scripts

### All scripts must be under the scripts folder

All script files should be under "Assets/scripts/" There can not be any other place with a script file except for libraries (packages) these may contain script files but only the scripts from the library self and not self-created scripts.&#x20;

### 3 Script types (only 3 folders in assets/scripts)

The scripts folder may only contain 3 folders and nothing else no scripts, no other folders no nothing just 3 folders these folders are&#x20;

1.  **Runtime**

    contains all runtime scripts and only the runtime scripts&#x20;
2.  **Editor**

    contains all editor scripts and only editor scripts
3.  **Utilities**

    may contain editor scripts and runtime scripts but is more like a tools folder where all scripts may not be linked to any script in the runtime or editor folder and if possible not connected to another script in the utilities folder except for if its a big system that needs more classes to work but this will be moved to a separate folder where only the items in the folder may use eachother&#x20;
