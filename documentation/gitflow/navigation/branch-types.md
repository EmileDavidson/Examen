# Branch Types

### m**ain**

there can only be **1** main branch.

This branch you should never commit to is used for the final project ⠀⠀⠀development

### develop

there can only be **1** develop branch.

The develop branch is used for ongoing development work. It's where all changes go, and it's important that this branch never contains errors or broken code. The development branch should always have the final version of the code.

### feature/{name}

Feature branches are used to work on specific features or tasks that are separate from ongoing development work. These branches are created off of the develop branch and are merged back in once the changes are complete.

### hotfix/{name}

Hotfix branches are used to quickly fix critical issues or bugs that need to be addressed outside of the normal development process. These branches are normally created off of the master branch and are merged back in once the issue is resolved.  but in our case it can also be used for the develop branch when fixing features that became broken because of merging or another reason.

### import/{name}

Import branches are used to import work examples of this 'work' could be 3d models, sprites, etc. the work will not be added to the scene and would just be placed in the assets folder so it can be used in other branches ones it's merged to the develop
