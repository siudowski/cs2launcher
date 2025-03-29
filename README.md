# CS 2 Stretched Launcher

A simple console app that wraps [this wonderful script that allows the game to be played in windowed-stretched mode](https://www.reddit.com/r/GlobalOffensive/comments/2o51wv/multiple_monitors_and_43_stretched_resolution/).
Since the script isn't an .exe, Windows 11 won't let you pin it to your Start Menu or Taskbar.
This app solves that by creating a wrapper so you can launch the script just like any other program - it's for all of you that like clean desktops and prefer playing CS2 in windowed stretched mode.
The script installer is provided alongside the .exe in the release.

# Configuration
On first launch the program will ask you for the path to the folder where the script is installed.
It expects a `\scripts` folder which contains `csgo_sl_hide.vbs` file, which won't work without `csgo_sl.bat` and `QRes.exe` files.
Navigate to the installation directory (by default it's `C:\Program Files (x86)\CSGO Stretched Launcher\scripts`, [copy the path](https://www.youtube.com/watch?v=LGxUZdwAkGM), paste it into the terminal when prompted and you're done.
It's almost plug & play.

# Notice
Be wary that it will save the path to `HKEY_CURRENT_USER\Environment` under `CS2Launcher_Path` name.
Deleting .exe file from your system or uninstalling the script will not automatically remove this environment variable.
While environment variables are harmless, you might want to know about it if you're particular about system cleanliness.
Also excuse the 70MB file size but it's just .NET doing its self-contained-exe things (basically bundles .NET runtime inside) and I wanted it to be as simple to use as possible.
Launching the game through Steam or in any other way will not execute neither this app or the script.

For resolution configuration refer to the [original guide](https://www.reddit.com/r/GlobalOffensive/comments/2o51wv/multiple_monitors_and_43_stretched_resolution/).
