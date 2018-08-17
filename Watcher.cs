using System;
using System.IO;

namespace EyeInTheSky
{
    class Watcher
    {
        // FIELD
        private String _path;
        private FileSystemWatcher _watcher;
        private MainWindow _window;

        // PROPERTY
        public string DirToWatch { get => _path; set => _path = value; }

        // CONSTRUCTOR
        public Watcher(MainWindow window)
        {
            _watcher = new FileSystemWatcher();
            _window = window;

            // ADD: event handlers to watcher.
            _watcher.Changed += new FileSystemEventHandler(OnChanged);
            _watcher.Created += new FileSystemEventHandler(OnChanged);
            _watcher.Deleted += new FileSystemEventHandler(OnChanged);
            _watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // WATCH: Watch for changes in LastAccess and LastWrite times, and the renaming of files or directories.
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.Filter = "*";

            _watcher.IncludeSubdirectories = true;
        }


        // PRIMARY METHOD THAT RUNS A WATCHER THAT WATCHERS FOR EVENTS
        public void Start(string path, string extension)
        {
            _watcher.Path = path;
            _watcher.Filter = extension;

            if (!(_watcher.EnableRaisingEvents))
            {
                // START: Begin watching the file/directory entered including its subdirectories...
                _watcher.EnableRaisingEvents = true;
            }
            else
            {
                // TODO: show messagebox to me to know its still running
            }
            // TODO: how to keep this open and running....?
        }

        public void Stop()
        {
            if (_watcher.EnableRaisingEvents)
            {
                _watcher.EnableRaisingEvents = false;
            }
        }


        // METHODS //
        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // To Screen--
            Console.WriteLine("\nName: {0}  @  Path: {1}  |  Event: {2}  |  Date: {3}\n", e.Name, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
            // To Log--
            StreamWriter sw = null;
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\log.txt"))
            {
                using (sw = File.CreateText(Directory.GetCurrentDirectory() + "\\log.txt"))
                {
                    sw.WriteLine("\n====== MONITORING: {0} =======", e.FullPath);
                    sw.WriteLine("");
                    sw.WriteLine("------------- EVENT: {0} -------------", e.ChangeType.ToString());
                    sw.WriteLine("\nName: {0}  @  Path: {1}  |  Event: {2}  |  Date: {3}", e.Name, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
                }
            }
            using (sw = File.AppendText(Directory.GetCurrentDirectory() + "\\log.txt"))
            {
                sw.WriteLine("");
                sw.WriteLine("------------- NEW EVENT: {0} -------------", e.ChangeType.ToString());
                sw.WriteLine("\nName: {0}  @  Path: {1}  |  Event: {2}  |  Date: {3}", e.Name, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
            }
            sw.Close();
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // To Screen--
            Console.WriteLine("\nName: {0}  @  OldPath: {1}  -->  NewPath: {2}  |  Event: {3}  |  Date: {4}\n", e.Name, e.OldFullPath, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
            // To Log--
            StreamWriter sw = null;
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\log.txt"))
            {
                using (sw = File.CreateText(Directory.GetCurrentDirectory() + "\\log.txt"))
                {
                    sw.WriteLine("\n====== MONITORING: {0} =======", e.FullPath);
                    sw.WriteLine("");
                    sw.WriteLine("------------- EVENT: {0} -------------", e.ChangeType.ToString());
                    sw.WriteLine("\nName: {0}  @  OldPath: {1}  -->  NewPath: {2}  |  Event: {3}  |  Date: {4}", e.Name, e.OldFullPath, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
                }
            }
            using (sw = File.AppendText(Directory.GetCurrentDirectory() + "\\log.txt"))
            {
                sw.WriteLine("");
                sw.WriteLine("------------- NEW EVENT: {0} -------------", e.ChangeType.ToString());
                sw.WriteLine("\nName: {0}  @  OldPath: {1}  -->  NewPath: {2}  |  Event: {3}  |  Date: {4}", e.Name, e.OldFullPath, e.FullPath, e.ChangeType, File.GetLastWriteTime(e.FullPath));
            }
            sw.Close();
        }
    }
}
