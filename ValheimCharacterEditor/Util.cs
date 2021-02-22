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

        static public byte[] ReadFileBytes(String file)
        {
            Byte[] bytes = File.ReadAllBytes(file);
            Thread.Sleep(100);
            return bytes;
        }

        static public void WriteFileBytes(String file, Byte[] data)
        {
            File.WriteAllBytes(file, data);
            Thread.Sleep(100);
        }

        static public bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }

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

        static public int FindInBytes (byte[] byte_array, byte[] toSearch, int Start = 0)
        {
            int index = 0;
            for (int p = Start; p < byte_array.Length; p++)
            {
                if (byte_array[p] == toSearch[index])
                {
                    index++;
                }
                else
                {
                    index = 0;
                }

                if (index == toSearch.Length)
                {
                    return p - toSearch.Length;
                }
            }

            return 0;
        }

        static public int FindInBytesReverse(byte[] byte_array, byte[] toSearch)
        {
            int index = toSearch.Length - 1;
            for (int p = byte_array.Length-1; p >= 0; p--)
            {
                if (byte_array[p] == toSearch[index])
                {
                    index--;
                }
                else
                {
                    index = toSearch.Length - 1;
                }

                if (index < 0)
                {
                    return p;
                }
            }

            return 0;
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

        static public byte[] ReadBytesArray(byte[] array, int index, int length)
        {
            byte[] result = new byte[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = array[index + i];
            }

            return result;
        }

        static public byte[] WriteBytesArray(byte[] array, int array_index, byte[] data, int data_index, int length)
        {
            for (int i = 0; i < length; i++)
            {
                array[array_index + i] = data[data_index + i];
            }

            return array;
        }
    }
}
