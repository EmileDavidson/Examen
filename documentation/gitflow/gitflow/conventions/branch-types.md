# Branch Types

### m**ain**

there can only be **1** main branch.

This branch you should never commit to is used for the final project ⠀⠀⠀development

### develop

there can only be **1** develop branch.

The develop branch is used for ongoing development work. It's where all changes go, and it's important that this branch never contains errors or broken code. The development branch should always have the final version of the code.

### documentation

there can only be **1** documentation branch

documentation branch is used for this gitbook and is synced we have this in a separate branch then develop since the develop branch is a protected branch an can not be merged and to

### feature/{name}

Feature branches are used to work on specific features or tasks that are separate from ongoing development work. These branches are created off of the develop branch and are merged back in once the changes are complete.

### hotfix/{name}

Hotfix branches are used to quickly fix critical issues or bugs that need to be addressed outside of the normal development process. These branches are created off of the master branch and are merged back in once the issue is resolved.&#x20;

### fix/{name}

fix branches are used to fix issues or bugs that need to be addressed. These branches are created off of the develop branch and are merged back in once the issue is resolved. these branches should not need to occur often since the features should be 'safe to import' but if needed here it is.

### import/{name}

Import branches are used to import work examples of this 'work' could be 3d models, sprites, particle effects, etc. (note that this is not limited from outside work but could also include unity particles, shaders and more) the work added is not added in any scene and would just be placed in the assets folder so it can be used in other branches ones it's merged to the develop&#x20;
