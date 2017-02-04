# Xcom2CheatFileCreator C# csv to cheat.txt batch file for X-Com 2

Instructions:
ENABLE CONSOLE IN XCOM 2 in STEAM

1. Right click on Xcom 2
2. Select Properties
3. Under the general tab 
  1. Click on 'Set Launch Options'
  2. In the text box add: "-allowconsole -log -autodebug" without quotation marks. 
Once you're done, you can open the console from in-game using the tilde (~) key.

Using using the cheat generator


READYING THE TEMPLATE

1. Open the 'cheatfiletemplate' in any spreadsheet manager (Google Sheets works well)
  1. Since it is a text file as well, you could manage it in a text file editor as well
2. There will be two lines for soldiers already in the file. 
  1. Copy paste to add more lines for soldiers
3. Arrange the stats for the soldiers as you see fit.
Note: In order for the cheat batch file to work, the names in the csv will need to match the names of the soldiers in your save game.
4. Export the sheet as csv to your desktop

USING THE CHEAT FILE GENERATOR

1. Open the editor.
2. Set your steam drive in the text box
3. Select or unselect the option for 'the long war 2' depending on what game you are playing
4. Click 'Load CSV file' 
  1. Select the input CSV from your desktop
5. Review the soldiers in the datagrid below
  1. Edits to the soldiers here do not carry over to the cheat (yet)
6. Click Generate Cheat File
7. Save it as an easily used filename cheat.bat works well
Note: this will set all soldiers to level 2 in their respective class.

USING THE CHEAT FILE

1. Open X-Com 2
2. Open a save game
3. While in the Avenger view, tap the tilde (~) key
4. Type 'exec ' and then the cheat file name. Example: 'exec cheat.bat'
5. Review the soldiers.

Troubleshooting:

1. If the CSV file isn't loading, try reloading the CSV in a spreadsheet editor. 
  1. Compare against the template to see if there are any differences in columns
2. If soldier stats aren't applying to certain soldiers, check the spelling of their first and last names
  2. First and Last Name in the cheat file must match the names in game, otherwise the cheat file will not change their stats.
  
