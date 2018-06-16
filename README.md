# Xcom2CheatFileCreator C# csv to cheat.txt batch file for X-Com 2

Instructions:

## ENABLE CONSOLE IN XCOM 2 in STEAM

1. Right click on Xcom 2
2. Select Properties
3. Under the general tab 
  1. Click on 'Set Launch Options'
  2. In the text box add: "-allowconsole -log -autodebug" without quotation marks. 
Once you're done, you can open the console from in-game using the tilde (~) key.

## READYING THE TEMPLATE (Optional)
1. Open the 'cheatfiletemplate' in any spreadsheet manager (Google Sheets works well)
  1. Since it is a text file as well, you could manage it in a text file editor as well
2. There will be two lines for soldiers already in the file. 
  1. Copy paste to add more lines for soldiers
3. Arrange the stats for the soldiers as you see fit.
Note: In order for the cheat batch file to work, the names in the csv will need to match the names of the soldiers in your save game.
4. Export the sheet as csv to your desktop

## USING THE CHEAT FILE GENERATOR
1. Open the editor.
2. Set game type 
  1. Set your steam drive in the text box for xcom 2
  2. If playing 'Xcom 2 - Wrath of the Chosen, click the 'War Of The Chosen File Location' checkbox
  3. Select or unselect the option for 'the long war 2' depending on what game you are playing
    1. This adds the prefix 'LWS_' before the class names, except for the Psionic class
  4. If using 'Richards Classes' mod, click the checkbox
4. Click 'Load CSV file' 
  1. Select the input CSV from your desktop
5. Review the soldiers in the datagrid below
  1. Values can be edited in the grid - See 'SOLDIER EDITING' below
6. Click Generate Cheat File
7. Save it as an easily used filename - cheat.txt works well
Note: this will set all soldiers to level 2 in their respective class.

## SOLDIER EDITING
1. Soldier Attributes can now be edited in the datagrid
2. Soldier class can be set via the dropdown and button
   1. Select a soldier
   2. Select a class
   3. Hit 'Change Soldier Class'
   4. Soldier class can be changed in the data by typing if needed
3. Soldiers can be deleted by selecting a soldier in the grid and clicking the delete button
4. The add soldier button adds a new soldier at the end of the list of soldiers in the grid

## USING THE CHEAT FILE
1. Open X-Com 2
2. Open a save game
3. While in the Avenger view, tap the tilde (~) key
4. Type 'exec ' and then the cheat file name. Example: 'exec cheat.txt'
5. Review the soldiers

## SETTINGS
1. General Program settings
   1. The Xcom2 Exe folder location can be set here as well
   2. Either type the full path of the exe on your machine
   3. Or browse to it using the folder browse button
      1. This absolutely MUST be the directory with the 'Win32' and 'Win64' folders
   4. The 'Adjust Attributes By Class' does ... something, only involving the Hacking class
   5. The folder will be set once the settings window is closed  
   6. The 'Override Xcom2 Location' checkbox will need to be checked in order to use the newly set location
   7. The folder settings will not be saved for later use unless "Export Settings CSV" is clicked 

2. Classes can be edited from the settings control - Class tab
   1. To add new classes, add the class name as referenced in the mod and click the "Add Class" button
      1. If the name of the class does not match the name of the class in the mod, the class change won't work
   2. To delete a class, select the class in the list and click the "Delete Class" button
   3. The changed class list will be passed back to the main control on closing the settings page
   4. If the class list needs to be reset to the base classes, click 'reset class'
   5. The classes will not be saved for later use unless "Export Class CSV" is clicked 
  

## Troubleshooting:

1. If the CSV file isn't loading, try reloading the CSV in a spreadsheet editor. 
   1. Compare against the template to see if there are any differences in columns
2. If soldier stats aren't applying to certain soldiers, check the spelling of their first and last names
   2. First and Last Name in the cheat file must match the names in game, otherwise the cheat file will not change their stats.
