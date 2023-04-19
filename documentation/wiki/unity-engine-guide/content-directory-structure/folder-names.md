# Folder Names

These are common rules for naming any folder in the content structure.

### Always Use [PascalCase](broken-reference)

PascalCase refers to starting a name with a capital letter and then instead of using spaces, every following word also starts with a capital letter. For example, `DesertEagle`, `RocketPistol`, and `ASeriesOfWords`.

### Never Use Spaces

Never use spaces. Spaces can cause various engineering tools and batch processes to fail. Ideally, your project's root should also not contains any spaces and is located somewhere such as `D:\Project` instead of `C:\Users\My Name\My Documents\Unity Projects`.

### Never Use Unicode Characters And Other Symbols

If one of your game characters is named 'Zoë', its folder name should be `Zoe`. Unicode characters can be worse than Spaces for engineering tools and some parts applications don't support Unicode characters in paths either.

Related to this, if your project and your computer's user name has a Unicode character (i.e. your name is `Zoë`), any project located in your `My Documents` folder will suffer from this issue. Often simply moving your project to something like `D:\Project` will fix these mysterious issues.

Using other characters outside `a-z`, `A-Z`, and `0-9` such as `@`, `-`, `_`, `,`, `*`, and `#` can also lead to unexpected and hard-to-track issues on other platforms, source control, and weaker engineering tools.

### No Empty Folders

There simply shouldn't be any empty folders. They clutter the content browser.

If you find that the content browser has an empty folder you can't delete, you should perform the following:

1. Be sure you're using source control.
2. Navigate to the folder on-disk and delete the assets inside.
3. Close the editor.
4. Make sure your source control state is in sync (i.e. if using Perforce, run a Reconcile Offline Work on your content directory)
5. Open the editor. Confirm everything still works as expected. If it doesn't, revert, figure out what went wrong, and try again.
6. Ensure the folder is now gone.
7. Submit changes to source control.
