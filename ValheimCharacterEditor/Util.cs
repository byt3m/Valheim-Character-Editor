using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ValheimCharacterEditor
{
    class Util
    {
        static private string _LastBackup;
        static private string _BeforeLastBackup;

        static public bool isGameRunning()
        {
            Process[] process = Process.GetProcessesByName("valheim");

            if (process.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public bool BackupFile(String file)
        {
            String destination = Path.Combine(Path.GetDirectoryName(file), (string)(Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".backup"));
            File.Copy(file, destination, true);

            if (File.Exists(destination))
            {
                _LastBackup = destination;
                _BeforeLastBackup = file;

                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool RestoreFile()
        {
            File.Copy(_LastBackup, _BeforeLastBackup, true);

            if (File.Exists(_BeforeLastBackup))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public String OpenDirectoryDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    return null;
                }
            }            
        }

        static public int FindInArrayString(String[] array, String toSearch)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (toSearch.Equals(array[i]))
                {
                    return i;
                }
            }

            return 0;
        }

        static public String[] GetCharactersNames(Customization.Character[] Characters)
        {
            String[] names = new string[Characters.Length];

            for (int i = 0; i < Characters.Length; i++)
            {
                names[i] = Characters[i].Data.Name;
            }

            return names;
        }
    }
}
