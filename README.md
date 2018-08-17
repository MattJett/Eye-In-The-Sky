# EyeInTheSky
Eye In The Sky – GUI WPF App

DESCRIPTION
-----------
Academic Project from Spring Quarter 2018, CSCD 371 - .Net Programming with C#

App uses FileSystemWatcher object, taking a path and monitors any activity in that directory by reporting events, created, deleted, modified, and renamed out to a data table. This data table serves as an event log showing the history of events during this session. The app allows the user to export the log out to a text file or export the data to a SQLite database. This database allows for filtering of file types and loading from a database file or appending new results to an old database. This is where the user can clear the database as well. If no database is found when exporting log, database will automatically be created upon export.

Some features are not implemented yet; work in progress.

HOW TO RUN
----------
In home directory of repository, I've created a shortcut to the .exe launcher. Or you can use Visual Studio 2017 to open my .sln solution file or .csproj project files to view the code. Or just simply open the .cs or .xaml markup files in an editor to view code.

KNOWN BUGS
----------
* clicking "data base" tab before START button was pressed will cause crash.
* after stopping monitoring, if user doesn't export table to data base and clicks on "data base" tab before exporting data, data base table won't populate. Clicking back on "monitor" tab after this erases monitor event log table.

NON-FUNCTIONING FEATURES
------------------------
* "Export to Log" feature, disabled button until feature is implemented.
* "Load Database" feature, disabled button until feature is implemented.
* "Show Hidden Files" feature, checkbox non-functioning.

PLANNED DESIGN UPDATES
----------------------
Monitor tab
* Ability to switch between directory browsing and file browsing.
* Improve UX flow for user, such as removing text in Data Base tab making it easier for user to see Export to Data Base or make Data Base tab automatically export data to database table.
Data Base tab
* Improve contextual design of extension filter combo box to RUN button, making it more obvious that after extension selection, RUN button must be pressed. Currently it appears that RUN button is only relevent to Clear Data Base and Load Data Base.
* Adjust table width so user doesn't have to scroll horizontally by default.
About tab
* App title looks like it cut off, move title and version number up.
* do something about negative space on left, looks horrible.
