# Folder Names

These are common rules for naming any folder in the content structure.

### Always Use [PascalCase](broken-reference)

PascalCase refers to starting a name with a capital letter and then instead of using spaces, every following word also starts with a capital letter. For example, `DesertEagle`, `RocketPistol`, and `ASeriesOfWords`.

### Never Use Spaces

Never use spaces. Spaces can cause various engineering tools and batch processes to fail. Ideally, your project's root should also not contain any spaces&#x20;

### Never Use Unicode Characters And Other Symbols

If one of your game characters is named 'Zoë', its folder name should be `Zoe`. Unicode characters can be worse than Spaces for engineering tools and some parts applications don't support Unicode characters in paths either.

Related to this, if your project and your computer's user name have a Unicode character (i.e., your name is Zoë), any project in your My Documents folder will suffer. Often, moving your project to something like `D:\Project` will fix these mysterious issues.

Using other characters outside `a-z`, `A-Z`, and `0-9` such as `@`, `-`, `_`, `,`, `*`, and `#` can also lead to unexpected and hard-to-track issues on other platforms, source control, and weaker engineering tools.

### No Empty Folders

In general you should not want empty folders since this can quickly clutter your folder structure, but we will make one exception for 'root folders' meaning the root of a type think about the 'scripts' folder or the 'graphics' folder this is the only exception since it can help thinking about where to put the files instead of thinking is there already a folder? is this it? or should I create a new one?&#x20;

> one more exception in the scripts folder the 'runtime, editor, utilities' folder may also be created but empty since these should be the only files / folders in the scripts folder.

### Never name a folder assets&#x20;

_All assets are assets._

_makes sense right? So the simplest rule to follow, do not create a folder named assets._&#x20;

