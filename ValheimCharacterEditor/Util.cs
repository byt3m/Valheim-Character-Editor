using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace ValheimCharacterEditor
{
    class Util
    {
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

        static public byte[] ReconstructByteArray (byte[] array, int current_length, int new_length, int position)
        {
            byte[] new_array = { };

            // Reconstruct byte array
            new_array = new byte[array.Length + (new_length - current_length)];

            for (int i = 0; i < position; i++)
            {
                new_array[i] = array[i];
            }

            for (int i = position + current_length; i < array.Length; i++)
            {
                new_array[i - current_length + new_length] = array[i];
            }

            // Reconstruct FCH header
            // Header is saved in little-endian
            byte[] bHeader = { array[3], array[2], array[1], array[0] };

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bHeader);

            int iHeader = BitConverter.ToInt32(bHeader, 0);
            byte[] bNewHeader = BitConverter.GetBytes(iHeader + (new_length - current_length));

            for (int i = 0; i < 4; i++)
            {
                new_array[i] = bNewHeader[i];
            }                      

            return new_array;
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

        static public String BackupFile(String file)
        {
            String destination = Path.Combine(Path.GetDirectoryName(file), (string)(Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".backup"));
            File.Copy(file, destination, true);

            if (!File.Exists(destination))
            {
                return null;
            }

            return destination;
        }

        static public bool RestoreFile(String file)
        {
            String destination = Path.Combine(Path.GetDirectoryName(file), (string)(Path.GetFileName(file).Split('_')[0] + ".fch"));
            File.Copy(file, destination, true);

            if (!File.Exists(destination))
            {
                return true;
            }

            return false;
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
    }
}
