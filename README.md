# Swords-and-Souls-Neverseen-Difficulty-Mod
A mod (relies on BepInEx and Harmony) for the videogame Swords and Souls: Neverseen, making enemies act twice per turn.

# How to install?
Installing the mod is quite simple, though it does relies on BepInEx, and Harmony. 

1 Download BepInEx https://github.com/bepinex/bepinex, head to Releases on the right, and download latest (at the time of writing that is BepInEx 5.4.23.5) (google might want to block the file as potentinally unwated,, since it is a dll. Be mindfull when you download anything. Be carefull, and and if there is any sudden big changes, be doubtfull, maybe try checking on the SoulGame reddit, if my mod gets compromised I will post it there, though obviusly for the chance for that is minimal. The file size should not be above 1MB, in fact it should be around 0.1MB, so unless I make a huge update it should not be that large)

2 You will get a .zip file. Inside it, you will find a folder named "BepInEx", and some other files. Select them, and right click -> copy (or just use ctrl+c)

3 Open steam, select Swords and Souls: Neverseen, click on the gears icon, and select Manage -> Browse local files. If you are not using steam, than simply go to the intallation location of Swords and Souls: Neverseen, and paste it in here. This is where you should see Swords & Souls Neverseen.exe

4 Start the game once, wait while it loads, than quit. This should atomaticly generate a few BepInEx files. (Or Open the BepInEx folder, and inside here creat a folder called "plugins" (Without the " around it), and paste the downloaded DifficultyMod.dll in here, than you dont have to start the game)

5 Download and paste the DifficultyMod.dll inside here (the plugins folder)

6 Start the game, and see if it works, it should. 


# What does this mod even does, and why was it created
First off, it makes enemies take two actions instead of one, making the game quite a bit harder, and more interesting, at least for me. 

I mostly created it since this game hasnt been really modded before, and wanted to see if it was possible, and if yes, how hard. Note that I am new at C#, so many of this was done by just looking at forum posts and such, so I can try to fix some issues, but since the mod is simple, I hope there should not be any other.

Lastly, should there be any new mods, either by me or someone else, they may or may not work together, though if it does not modifies handleTurn() or createWave() it should be compatible.

Future changes to this mod
I might modify some of the enemies turn actions/order, or make some changes to enemies in general, but for now, it's only the two actions. 
