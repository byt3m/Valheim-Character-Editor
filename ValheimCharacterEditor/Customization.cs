using System;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace ValheimCharacterEditor
{
    class Customization
    {
        //TODO:
        //    - Write name.
        //    - Make color configurable with a slider.
        //    - Inventory editor.
        

        static private String[] FCH_files;
        static public String[] Characters;
        static public string last_backup;
        static public bool FirstRun = true;

        static private String Current_Character;
        static private String Current_Character_File;
        static private String Current_Character_Name;
        static private String Current_Character_Beard;
        static private String Current_Character_Hair;
        static private String Current_Character_HairColor;

        static public String[] Beards_UI = { "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2", "Short 3", "Thick 1" };
        static private String[] Beards_Internal = { "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7", "Beard8" };
        static public String[] Hairs_UI = { "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2", "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3" };
        static private String[] Hairs_Internal = { "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5", "Hair8", "Hair9", "Hair10", "Hair14" };
        static public String[] Hair_Colors = { "Black", "Blonde", "Ginger", "Brown", "White" };
        static private Byte[] Color_Black  = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x39, 0xF7, 0xD9, 0x3D, 0x00, 0xEF, 0xCA, 0x3D, 0xAF, 0xDB, 0x99, 0x3D };
        static private Byte[] Color_Blonde = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x4F, 0xA7, 0x35, 0x3F, 0x3C, 0x3C, 0xFC, 0x3E };
        static private Byte[] Color_Ginger = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xC4, 0xA6, 0x32, 0x3F, 0x60, 0x69, 0xAE, 0x3E, 0x55, 0xAB, 0x47, 0x3E };
        static private Byte[] Color_Brown  = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x97, 0x37, 0x06, 0x3F, 0x71, 0x53, 0xBF, 0x3E, 0xA2, 0x0F, 0x85, 0x3E };
        static private Byte[] Color_White  = { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xEA, 0xA0, 0x4E, 0x3F, 0xDA, 0x60, 0x40, 0x3F, 0xFF, 0xDA, 0x11, 0x3F };

        static public void Initialize(String Character)
        {
            Current_Character = Character;

            foreach (String file in FCH_files)
            {
                if (Character == Path.GetFileNameWithoutExtension(file))
                {
                    Current_Character_File = (file);
                    break;
                }
            }
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

        static private byte[] ReadCharacterFile()
        {
            byte[] result = { };

            foreach (String file in FCH_files)
            {
                if (Current_Character == Path.GetFileNameWithoutExtension(file))
                {
                    result = Util.ReadFileBytes(file);
                    
                    break;
                }
            }
            
            return result;
        }

        static private byte[] ReadCharacterAppearance(String Type)
        {
            byte[] character_file_bytes = ReadCharacterFile();
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
                MessageBox.Show(Type + " not found for character " + Current_Character + ". Please make sure to start a game with this character.", "ERROR", MessageBoxButtons.OK);
                return bType;
            }

            int type_length;

            if (Type.Equals("Color"))
            {
                position += character_file_bytes[position];
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

        static public String ReadCharacterName()
        {
            String name = Encoding.UTF8.GetString(ReadCharacterAppearance((String)(Current_Character.Substring(0, 1).ToUpper() + 
                Current_Character.Substring(1, Current_Character.Length - 1))));

            if (!String.IsNullOrEmpty(name))
            {
                Current_Character_Name = name;
                return name;
            }
            else
            {
                return null;
            }
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
                Current_Character_HairColor = "Black";
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

        static private bool WriteCharacterData(String Customization, String Type)
        {
            int position = 0;
            byte[] search_string;
            byte[] character_file_bytes = ReadCharacterFile();

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
                search_string = Encoding.UTF8.GetBytes(Current_Character_Name);
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
                    MessageBox.Show(Type + " not found for character " + Current_Character + ". Please make sure to start a game with this character.", "ERROR", MessageBoxButtons.OK);
                    return false;
                }
                else if (position == 0)
                {
                    break;
                }

                int current_length = character_file_bytes[position];

                // Reconstruct byte array if there is a length difference between current customization and the new one
                if (Customization.Length != current_length)
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
            }
            while (true);

            Util.WriteFileBytes(Current_Character_File, character_file_bytes);

            return true;
        }

        static public bool WriteCustomization(String Name, String Beard, String Hair, String Hair_Color)
        {
            // Make backup of current FCH file
            String backup = Util.BackupFile(FCH_files[Util.FindInArrayString(Characters, Current_Character)]);

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
            if (!String.IsNullOrEmpty(Name) && (!Name.Equals(Current_Character_Name)))
            {
                // Check name correctness (based on game behaviour)
                if ((Name.Length > 3 && Name.Length < 15) && isCorrectName(Name))
                {
                    String NewName = Name[0].ToString().ToUpper();
                    for (int i = 1; i < Name.Length; i++)
                    {
                        NewName += Name[i].ToString().ToLower();
                    }

                    if (WriteCharacterData(NewName, "Name")) // Name
                    {
                        File.Move(Current_Character_File, Path.Combine(Path.GetDirectoryName(Current_Character_File), (NewName + ".fch")));
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
                if (!WriteCharacterData(NewBeard, "Beard")) // Beard
                {
                    return false;
                }
            }
            
            String NewHair = Hairs_Internal[Util.FindInArrayString(Hairs_UI, Hair)];
            if (!NewHair.Equals(Current_Character_Hair))
            {
                if (!WriteCharacterData(NewHair, "Hair")) // Beard
                {
                    return false;
                }
            }

            if (!Hair_Color.Equals(Current_Character_HairColor))
            {
                if (!WriteCharacterData(Hair_Color, "Color")) // Hair color
                {
                    return false;
                }
            }

            return true;
        }
    }
}
