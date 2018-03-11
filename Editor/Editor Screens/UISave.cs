using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Monogame_GL
{
    public class UISave : IWindow
    {
        private static UISave instance;
        private static ItemsHolder _foldersFiles;
        private static ItemsHolder _roots;
        private string _path;
        private List<string> _directories;
        private List<string> _files;
        private List<DriveInfo> _allDrives;
        private TextBlock _block;
        private Button _save;
        private Button _open;

        private TextBox _box;

        private UISave()
        {
            _foldersFiles = new ItemsHolder(new RectangleF(512, 512 + 128 - 7, (Globals.WinRenderSize.X - (2 * 512 + 96)) / 2, 128 + 64), 1, 28);
            _roots = new ItemsHolder(new RectangleF(512, 256 + 121, (Globals.WinRenderSize.X - (2 * 512 + 96)) / 2 + 512 + 64, 128 + 64), 1, 28);
            _path = "";
            _directories = new List<string>();
            _files = new List<string>();
            _allDrives = new List<DriveInfo>();
            _block = new TextBlock(new RectangleF(512, 256 - 32, (Globals.WinRenderSize.X - (2 * 512 + 96)) / 2 + 512 + 64, 512 + 96 - 7), _path);
            _allDrives.AddRange(DriveInfo.GetDrives());
            _save = new Button(new Vector2((Globals.WinRenderSize.X - (2 * 512 + 96)) / 2, 512 + 128 + 256 + 32 - 7), "Save", Save, null, null, ButtonType.small, true);
            _open = new Button(new Vector2((Globals.WinRenderSize.X - (2 * 512 + 96)) / 2 + 128 + 32, 512 + 128 + 256 + 32 - 7), "Open", null, null, null, ButtonType.small);
            _box = new TextBox("", 512, new Vector2((Globals.WinRenderSize.X - (2 * 512 + 96)) / 2, 512 + 128 + 256 - 32 - 7), textBoxType.fileName, FileNameChange);
            foreach (DriveInfo drive in _allDrives.Reverse<DriveInfo>())
            {
                if (drive.IsReady == false)
                    _allDrives.Remove(drive);
            }

            foreach (DriveInfo drive in _allDrives.Reverse<DriveInfo>())
            {
                _roots.Items.Add(new ListItemFileFolder(_allDrives.IndexOf(drive), _roots.Boundary, drive.Name, true, FileFolder.drive));
            }

            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            _roots.Items.Add(new ListItemFileFolder(_roots.Items.Count, _roots.Boundary, Directory.GetParent(appPath).FullName, false, FileFolder.folder));
            _roots.Items.Add(new ListItemFileFolder(_roots.Items.Count, _roots.Boundary, Path.GetFullPath(documentsPath), false, FileFolder.folder));
            _roots.Items.Add(new ListItemFileFolder(_roots.Items.Count, _roots.Boundary, Path.GetFullPath(desktopPath), false, FileFolder.folder));

            Search(Path.GetPathRoot(Environment.SystemDirectory));
        }

        public void Save()
        {
            if (_box.Valid == true)
                Globals.SaveJson(Editor.EditMap, _path, _box.Text);
        }

        public void GoUp(string path)
        {
            _path = Path.GetFullPath(Path.Combine(path, "..\\"));
        }

        public void FileNameChange()
        {
            foreach (ListItemFileFolder item in _foldersFiles.Items)
            {
                if (item.Name.ToUpper().StartsWith(_box.Text.ToUpper()) == true && _box.ChangedText() == true)
                {
                    _foldersFiles.ShowIndexIfExists((uint)_foldersFiles.Items.IndexOf(item));
                    break;
                }
            }

            _save.LockState(!_box.Valid);
        }

        public void Search(string directory)
        {
            _path = Path.Combine(_path, directory);

            _directories = CustomSearcher.GetDirectories(_path, "*");
            _files = CustomSearcher.GetFiles(_path, "*");

            foreach (string dir in _directories.Reverse<string>())
            {
                try
                {
                    Directory.GetDirectories(dir);
                }
                catch (Exception)
                {
                    _directories.Remove(dir);
                }
            }

            _foldersFiles.Items.Clear();

            if (Path.GetPathRoot(_path) != _path)
            {
                _foldersFiles.Items.Add(new ListItemFileFolder(0, _foldersFiles.Boundary, "…"));

                for (int x = 0; x < _directories.Count; x++)
                {
                    _foldersFiles.Items.Add(new ListItemFileFolder( x + 1, _foldersFiles.Boundary, Path.GetFileName(_directories[x])));
                }
                for (int x = 0; x < _files.Count; x++)
                {
                    _foldersFiles.Items.Add(new ListItemFileFolder(x + 1 + _directories.Count, _foldersFiles.Boundary, Path.GetFileName(_files[x]), false, FileFolder.file));
                }
            }
            else
            {
                for (int x = 0; x < _directories.Count; x++)
                {
                    _foldersFiles.Items.Add(new ListItemFileFolder(x, _foldersFiles.Boundary, Path.GetFileName(_directories[x])));
                }
            }

            _foldersFiles.Reset();
            _block.ChangeText(_path);
        }

        public void Reset()
        {
        }

        public void Update()
        {
            _roots.Update();
            _foldersFiles.Update();
            _box.Update();
            _save.Update();
            _open.Update();
            if (Path.GetPathRoot(_path) != _path)
            {
                if (_foldersFiles.Items[0].DoubleClicked() == true)
                {
                    GoUp(_path);
                    Search(_path);
                }
                else
                {
                    for (int x = 1; x < _foldersFiles.Items.Count; ++x)
                    {
                        if (_foldersFiles.Items[x].DoubleClicked() == true && (_foldersFiles.Items[x] as ListItemFileFolder)._treat == FileFolder.folder)
                        {
                            Search((_foldersFiles.Items[x] as ListItemFileFolder).Name);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < _foldersFiles.Items.Count; ++x)
                {
                    if ((_foldersFiles.Items[x] as ListItemFileFolder)._treat == FileFolder.folder && _foldersFiles.Items[x].DoubleClicked() == true)
                    {
                        Search((_foldersFiles.Items[x] as ListItemFileFolder).Name);
                        break;
                    }
                }
            }

            for (int x = 0; x < _roots.Items.Count; ++x)
            {
                if (_roots.Items[x].DoubleClicked() == true)
                {
                    _path = (_roots.Items[x] as ListItemFileFolder).Name;
                    Search("");
                    break;
                }
            }

            string textTemp = "";

            if (_foldersFiles.ChangedSelection() == true && (textTemp = (_foldersFiles.GetSelected() as ListItemFileFolder)?.Name) != "…" && _box.IsActive == false)
            {
                if (textTemp != null)
                    _box.SetText(textTemp);
                else
                    _box.SetText("");
            }
        }

        public void Draw()
        {
            _foldersFiles.Draw(Game1.Win);
            _roots.Draw(Game1.Win);
            _block.Draw();
            _save.Draw();
            _open.Draw();
            _box.Draw();
        }

        public static UISave Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UISave();
                }
                return instance;
            }
        }
    }
}