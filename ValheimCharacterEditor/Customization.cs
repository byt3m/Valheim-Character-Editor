using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace ValheimCharacterEditor
{
    class Customization
    {
        static private String[] FCH_files;
        static public String[] Characters;
        static public string last_backup;
        static public bool FirstRun = true;
        
        static private String Current_Character_Beard;
        static private String Current_Character_Hair;
        static private String Current_Character_HairColor;

        public class Character
        {
            public string FileName = "";
            public string Path = "";
            public string Name = "";
            public long Id;
            public string StartSeed = "";
            public Dictionary<long, World> WorldsData = new Dictionary<long, World>();
            public int Kills;
            public int Deaths;
            public int Crafts;
            public int Builds;
            public float MaxHp;
            public float Hp;
            public float Stamina;
            public bool IsFirstSpawn;
            public float TimeSinceDeath;
            public string GuardianPower;
            public float GuardianPowerCooldown;
            public List<Item> Inventory;
            public List<string> Recipes;
            public Dictionary<string, int> Stations = new Dictionary<string, int>();
            public List<string> KnownMaterials;
            public List<string> ShownTutorials;
            public List<string> Uniques;
            public List<string> Trophies;
            public List<Biome> Biomes;
            public Dictionary<string, string> Texts = new Dictionary<string, string>();
            public string Beard = "";
            public string Hair = "";
            public pos SkinColor;
            public pos HairColor;
            public int Model;

            public class pos
            {
                public float x = 0;
                public float y = 0;
                public float z = 0;
            }

            public class World
            {
                public pos SpawnPoint = new pos();
                public bool HasCustomSpawnPoint;
                public pos LogoutPoint = new pos();
                public bool HasLogoutPoint;
                public pos DeathPoint = new pos();
                public bool HasDeathPoint;
                public pos HomePoint = new pos();
                public byte[] MapData;
            }

            public class Item
            {
                public string Name;
                public int Stack;
                public float Durability;
                public Tuple<int, int> Pos;
                public bool Equipped;
                public int Quality;
                public int Variant;
                public long CrafterId;
                public string CrafterName;
            }

            public enum Biome
            {
                None,
                Meadows,
                Swamp,
                Mountain = 4,
                BlackForest = 8,
                Plains = 16,
                AshLands = 32,
                DeepNorth = 64,
                Ocean = 256,
                Mistlands = 512,
                BiomesMax
            }
        }

        private static Character CurrentCharacter = new Character();

        static public String[] Beards_UI = { "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2", "Short 3", "Thick 1" };
        static private String[] Beards_Internal = { "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7", "Beard8" };
        static public String[] Hairs_UI = { "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2", "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3" };
        static private String[] Hairs_Internal = { "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5", "Hair8", "Hair9", "Hair10", "Hair14" };
        static public String[] Hair_Colors = { "Black", "Blonde", "Ginger", "Brown", "White" };
        static private Byte[] Color_Black  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x39, 0xF7, 0xD9, 0x3D, 0x00, 0xEF, 0xCA, 0x3D, 0xAF, 0xDB, 0x99, 0x3D };
        static private Byte[] Color_Blonde =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x4F, 0xA7, 0x35, 0x3F, 0x3C, 0x3C, 0xFC, 0x3E };
        static private Byte[] Color_Ginger =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xC4, 0xA6, 0x32, 0x3F, 0x60, 0x69, 0xAE, 0x3E, 0x55, 0xAB, 0x47, 0x3E };
        static private Byte[] Color_Brown  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x97, 0x37, 0x06, 0x3F, 0x71, 0x53, 0xBF, 0x3E, 0xA2, 0x0F, 0x85, 0x3E };
        static private Byte[] Color_White  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xEA, 0xA0, 0x4E, 0x3F, 0xDA, 0x60, 0x40, 0x3F, 0xFF, 0xDA, 0x11, 0x3F };
        static private Byte[] Search_Pattern = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F };
        static public char[] NameAllowedCharacters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static public void Initialize(String Character)
        {
            CurrentCharacter.FileName = Character;

            foreach (String file in FCH_files)
            {
                if (Character != Path.GetFileNameWithoutExtension(file)) continue;
                CurrentCharacter.Path = file;
                break;
            }

            byte[] data = Util.ReadFileBytes2(CurrentCharacter.Path);
            Util.ParseCharacterData(data, CurrentCharacter);
        }

        static public void GetCharacters()
        {
            String dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\LocalLow\IronGate\Valheim\characters");

            while (true)
            {
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show("Directory containing character information not found. Please, point me to the directory where character \".FCH\" files are held.", "ERROR", MessageBoxButtons.OK);
                    dir = Util.OpenDirectoryDialog();
                }
                else
                {
                    break;
                }
            }

            FCH_files = Directory.GetFiles(dir, "*.fch");
            Characters = new string[FCH_files.Length];
            for (int i = 0; i < FCH_files.Length; i++)
            {
                Characters[i] = Path.GetFileNameWithoutExtension(FCH_files[i]);
            }

            if (FCH_files.Length == 0)
            {
                MessageBox.Show("No character data files found.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        static private byte[] ReadCharacterAppearance(String Type)
        {
            byte[] character_file_bytes = Util.ReadFileBytes(CurrentCharacter.Path);
            byte[] bType = { };

            if (character_file_bytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return bType;
            }

            byte[] search_string;

            if (Type.Equals("Color"))
            {
                search_string = Encoding.UTF8.GetBytes("Hair");
            }
            else
            {
                search_string = Encoding.UTF8.GetBytes(Type);
            }

            int position = Util.FindInBytes(character_file_bytes, search_string);

            if (position == 0)
            {
                if (Type != "Beard" && Type != "Hair" && Type != "Color")
                {
                    Type = "Name";
                }
                MessageBox.Show(Type + " not found for character " + CurrentCharacter.FileName + ".", "ERROR", MessageBoxButtons.OK);
                return bType;
            }

            int type_length;

            if (Type.Equals("Color"))
            {
                position += search_string.Length + 1;
                type_length = 0x18;
            }
            else
            {
                type_length = character_file_bytes[position];
            }
            
            bType = new byte[type_length];

            for (int i = 0; i < bType.Length; i++)
            {
                bType[i] = character_file_bytes[position + i + 1];
            }

            return bType;
        }

        static private String TransformName(String name)
        {
            String[] Name = name.Split(' ');

            for(int i = 0; i < Name.Length; i++)
            {
                Name[i] = (String)(Name[i].Substring(0, 1).ToUpper() + Name[i].Substring(1, Name[i].Length - 1));
            }

            return String.Join(" ", Name);
        }

        static public String ReadCharacterName()
        {
            String name = Encoding.UTF8.GetString(ReadCharacterAppearance(TransformName(CurrentCharacter.FileName)));

            if (String.IsNullOrEmpty(name)) return null;
            CurrentCharacter.FileName = name;
            return name;
        }

        static public String ReadCharacterHair()
        {
            String hair = Encoding.UTF8.GetString(ReadCharacterAppearance("Hair"));

            if (!String.IsNullOrEmpty(hair))
            {
                Current_Character_Hair = hair;
                return Hairs_UI[Util.FindInArrayString(Hairs_Internal, hair)];
            }
            else
            {
                return null;
            }
        }

        static public String ReadCharacterBeard()
        {
            String beard = Encoding.UTF8.GetString(ReadCharacterAppearance("Beard"));

            if (!String.IsNullOrEmpty(beard))
            {
                Current_Character_Beard = beard;
                return Beards_UI[Util.FindInArrayString(Beards_Internal, beard)];
            }
            else
            {
                return null;
            }
        }



        static public String ReadCharacterColor()
        {
            byte[] color = ReadCharacterAppearance("Color");

            if (Util.CompareByteArrays(color, Color_Black))
            {
                Current_Character_HairColor = "Black";
            }
            else if (Util.CompareByteArrays(color, Color_Blonde))
            {
                Current_Character_HairColor = "Blonde";
            }
            else if (Util.CompareByteArrays(color, Color_Ginger))
            {
                Current_Character_HairColor = "Ginger";
            }
            else if (Util.CompareByteArrays(color, Color_Brown))
            {
                Current_Character_HairColor = "Brown";
            }
            else if (Util.CompareByteArrays(color, Color_White))
            {
                Current_Character_HairColor = "White";
            }
            else
            {
                if (color.Length == 0)
                {
                    return null;
                }
                else
                {
                    Current_Character_HairColor = "Black";
                }
            }

            return Current_Character_HairColor;
        }

        static private byte[] GetColorBytes(String Color)
        {
            switch (Color)
            {
                case "Black":
                    return Color_Black;
                case "Blonde":
                    return Color_Blonde;
                case "Ginger":
                    return Color_Ginger;
                case "Brown":
                    return Color_Brown;
                case "White":
                    return Color_White;
                case null:
                    break;
            }

            return null;
        }

        static private bool isCorrectName(String Name)
        {
            for (int i = 0; i < Name.Length; i++)
            {
                if (!Char.IsLetter(Name[i]))
                {
                    return false;
                }
            }

            return true;
        }

        static private bool WriteCharacterData(String Customization, String Type, String file_path)
        {
            int position = 0;
            byte[] search_string;
            byte[] character_file_bytes = Util.ReadFileBytes(CurrentCharacter.Path);

            if (character_file_bytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            if (Type.Equals("Color"))
            {
                 search_string = Encoding.UTF8.GetBytes("Hair");
            }
            else if (Type.Equals("Name"))
            {
                search_string = Encoding.UTF8.GetBytes(CurrentCharacter.Name);
            }
            else
            {
                search_string = Encoding.UTF8.GetBytes(Type);
            }

            do
            {
                position = Util.FindInBytes(character_file_bytes, search_string, position);

                if (position == 0 && !Type.Equals("Name"))
                {
                    MessageBox.Show(Type + " not found for character " + CurrentCharacter.FileName + ". Please make sure to start a game with this character.", "ERROR", MessageBoxButtons.OK);
                    return false;
                }
                else if (position == 0)
                {
                    break;
                }

                int current_length = character_file_bytes[position];

                // Reconstruct byte array if there is a length difference between current customization and the new one
                if (Customization.Length != current_length && !Type.Equals("Color"))
                {
                    character_file_bytes = Util.ReconstructByteArray(character_file_bytes, current_length, Customization.Length, position + 1);
                }

                // Write data
                byte[] bCustomization;

                if (Type.Equals("Color"))
                {
                    bCustomization = GetColorBytes(Customization);
                    position += current_length;
                }
                else
                {
                    bCustomization = Encoding.UTF8.GetBytes(Customization);
                    character_file_bytes[position] = (byte)bCustomization.Length;
                }

                for (int i = 0; i < bCustomization.Length; i++)
                {
                    character_file_bytes[position + i + 1] = (byte)bCustomization[i];
                }

                position += (byte)bCustomization.Length;
            }
            while (Type.Equals("Name"));

            Util.WriteFileBytes(file_path, character_file_bytes);

            return true;
        }

        static public bool WriteCustomization(String Name, String Beard, String Hair, String Hair_Color, System.Windows.Forms.CheckBox NameCheckbox)
        {
            // Make backup of current FCH file
            String backup = Util.BackupFile(CurrentCharacter.Path);

            if (String.IsNullOrEmpty(backup))
            {
                MessageBox.Show("There was an error while backing up current character data. Changes will not be applied.", "ERROR", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                last_backup = backup;
            }

            // if name is not null and not equal to current name proceed to write it
            if (!String.IsNullOrEmpty(Name) && (!Name.ToLower().Equals(CurrentCharacter.Name.ToLower())) && NameCheckbox.Checked)
            {
                // Check name correctness (based on game behaviour)
                if ((Name.Length >= 3 && Name.Length <= 15) && isCorrectName(Name))
                {
                    String NewName = Name[0].ToString().ToUpper();
                    for (int i = 1; i < Name.Length; i++)
                    {
                        NewName += Name[i].ToString().ToLower();
                    }

                    String New_Character_File = Path.Combine(Path.GetDirectoryName(CurrentCharacter.Path), (NewName.ToLower() + ".fch"));

                    if (File.Exists(New_Character_File))
                    {
                        MessageBox.Show("Character " + Name + " already exists.", "WARNING", MessageBoxButtons.OK);
                        return false;
                    }

                    if (WriteCharacterData(NewName, "Name", New_Character_File)) // Name
                    {
                        File.Delete(CurrentCharacter.Path);
                        CurrentCharacter.Path = New_Character_File;
                        CurrentCharacter.FileName = NewName.ToLower();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Name must contain letters and must be between 3 and 15 characters long.", "WARNING", MessageBoxButtons.OK);
                    return false;
                }
            }

            String NewBeard = Beards_Internal[Util.FindInArrayString(Beards_UI, Beard)];
            if (!NewBeard.Equals(Current_Character_Beard))
            {
                if (!WriteCharacterData(NewBeard, "Beard", CurrentCharacter.Path)) // Beard
                {
                    return false;
                }
            }
            
            String NewHair = Hairs_Internal[Util.FindInArrayString(Hairs_UI, Hair)];
            if (!NewHair.Equals(Current_Character_Hair))
            {
                if (!WriteCharacterData(NewHair, "Hair", CurrentCharacter.Path)) // Beard
                {
                    return false;
                }
            }

            if (!Hair_Color.Equals(Current_Character_HairColor))
            {
                if (!WriteCharacterData(Hair_Color, "Color", CurrentCharacter.Path)) // Hair color
                {
                    return false;
                }
            }

            return true;
        }

        static public bool RepairCharacter()
        {
            byte[] character_file_bytes = Util.ReadFileBytes(CurrentCharacter.Path);

            if (character_file_bytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            byte[] Beard = Encoding.UTF8.GetBytes(Beards_Internal[0]);
            byte[] Hair = Encoding.UTF8.GetBytes(Hairs_Internal[0]);
            byte[] bData = new byte[Beard.Length + Hair.Length + 2];

            bData[0] = (byte)Beard.Length;
            bData[Beard.Length+1] = (byte)Hair.Length;

            for (int i = 0; i < Beard.Length; i++)
            {
                bData[i + 1] = Beard[i];
            }

            for (int i = 0; i < Hair.Length; i++)
            {
                bData[i + Beard.Length + 2] = Hair[i];
            }

            int position = Util.FindInBytes(character_file_bytes, Search_Pattern);

            if (position == 0)
            {
                MessageBox.Show("Pattern not found. Character can not be restored", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            position++;
            position -= bData.Length;

            for (int i = 0; i < bData.Length; i++)
            {
                character_file_bytes[position + i] = bData[i];
            }

            // Make backup of current FCH file
            String backup = Util.BackupFile(CurrentCharacter.Path);

            if (String.IsNullOrEmpty(backup))
            {
                MessageBox.Show("There was an error while backing up current character data. Changes will not be applied.", "ERROR", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                last_backup = backup;
            }

            Util.WriteFileBytes(CurrentCharacter.Path, character_file_bytes);

            return true;
        }
    }
}
