﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using static ValheimCharacterEditor.ValheimEngine.Character;
using System.Text;

namespace ValheimCharacterEditor
{
    class Util
    {
        static private string _LastBackup;
        static private string _BeforeLastBackup;

        static public bool isGameRunning()
        {
            Process[] process = Process.GetProcessesByName("valheim");

            return process.Length != 0;
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

            return false;
        }

        static public bool RestoreFile()
        {
            File.Copy(_LastBackup, _BeforeLastBackup, true);

            return File.Exists(_BeforeLastBackup);
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

                return null;
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

        static public ValheimEngine.Vector3 ColorToVec3(System.Drawing.Color Color)
        {
            return new ValheimEngine.Vector3
            {
                X = Color.R / 255.0F,
                Y = Color.G / 255.0F,
                Z = Color.B / 255.0F
            };
        }

        static public System.Drawing.Color Vec3ToColor(ValheimEngine.Vector3 color)
        {
            return System.Drawing.Color.FromArgb(255, (int)(color.X * 255), (int)(color.Y * 255), (int)(color.Z * 255));
        }

        static public void LoadInventory(ref List<Item> inventory, ref DataGridView dataView)
        {
            dataView.Rows.Clear();
            Customization.InventoryScratchPad.Clear();

            Customization.InventoryScratchPad.AddRange(inventory);
            foreach (var item in inventory)
            {
                dataView.Rows.Add(
                    new string[] { item.Name, item.Stack.ToString() }
                );
            }
        }

        static public void BuildInventoryPrintList(ref List<Item> inventory, ref StringBuilder sb)
        {
            sb.Clear();
            foreach( var item in inventory )
            {
                sb.Append("\t\t ");
                sb.Append(item.Stack);
                sb.Append(" ");
                sb.Append(item.Name);
                sb.Append("\n");
            }
        }
    }
}
