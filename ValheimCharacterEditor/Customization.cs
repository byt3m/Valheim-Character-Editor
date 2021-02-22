using System;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Linq;

namespace ValheimCharacterEditor
{
    class Customization
    {
        static private String[] _FCH_files;
        static public String[] Characters;
        static public bool FirstRun = true;
        //static private bool _UseSearchPattern = true;

        static public String SelectedCharacterFile;
        static public String SelectedCharacterBeard;
        static public String SelectedCharacterHair;

        static private int _CurrentCharacterBeardPosition;
        static private int _CurrentCharacterHairPosition;
        static private int _CurrentCharacterHairColorPosition;

        static public String[] Beards_UI = { "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2", "Short 3", "Thick 1" };
        static private String[] _Beards_Internal = { "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7", "Beard8" };
        static public String[] Hairs_UI = { "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2", "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3" };
        static private String[] _Hairs_Internal = { "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5", "Hair8", "Hair9", "Hair10", "Hair14" };
        static public String[] Hair_Colors = { "Black", "Blonde", "Ginger", "Brown", "White" };
        static private Byte[] _Color_Black  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x39, 0xF7, 0xD9, 0x3D, 0x00, 0xEF, 0xCA, 0x3D, 0xAF, 0xDB, 0x99, 0x3D };
        static private Byte[] _Color_Blonde =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x4F, 0xA7, 0x35, 0x3F, 0x3C, 0x3C, 0xFC, 0x3E };
        static private Byte[] _Color_Ginger =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xC4, 0xA6, 0x32, 0x3F, 0x60, 0x69, 0xAE, 0x3E, 0x55, 0xAB, 0x47, 0x3E };
        static private Byte[] _Color_Brown  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x97, 0x37, 0x06, 0x3F, 0x71, 0x53, 0xBF, 0x3E, 0xA2, 0x0F, 0x85, 0x3E };
        static private Byte[] _Color_White  =   { 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xEA, 0xA0, 0x4E, 0x3F, 0xDA, 0x60, 0x40, 0x3F, 0xFF, 0xDA, 0x11, 0x3F };
        //static private Byte[] _SearchPattern = { 0x24, 0x74, 0x75, 0x74, 0x6F, 0x72, 0x69, 0x61, 0x6C }; // $tutorial
        static public char[] NameAllowedCharacters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

        static public void Initialize(String Character)
        {
            foreach (String file in _FCH_files)
            {
                if (Character == Path.GetFileNameWithoutExtension(file))
                {
                    SelectedCharacterFile = (file);
                    return;
                }
            }

            MessageBox.Show("Character .FCH file not found!", "ERROR", MessageBoxButtons.OK);
            Application.Exit();
        }

        static public bool isCorrectName(String Name)
        {
            // Check content
            if (String.IsNullOrEmpty(Name))
                return false;

            // Check length
            if (Name.Length < 3 || Name.Length > 15)
                return false;

            // Check allowed characters
            for (int i = 0; i < Name.Length; i++)
            {
                if (!NameAllowedCharacters.Contains(Name[i]))
                    return false;
            }

            return true;
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

            _FCH_files = Directory.GetFiles(dir, "*.fch");
            Characters = new string[_FCH_files.Length];
            for (int i = 0; i < _FCH_files.Length; i++)
            {
                Characters[i] = Path.GetFileNameWithoutExtension(_FCH_files[i]);
            }

            if (_FCH_files.Length == 0)
            {
                MessageBox.Show("No character data files found.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        static public bool CheckCustomization()
        {
            // Read character file
            byte[] data = Util.ReadFileBytes(SelectedCharacterFile);

            // Check if anything was read
            if (data.Length == 0)
            {
                MessageBox.Show("Character file is empty.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            // Search keywords like "Beard" and "Hair"
            int beard = Util.FindInBytes(data, Encoding.UTF8.GetBytes("Beard"));
            int hair = Util.FindInBytes(data, Encoding.UTF8.GetBytes("Hair"));

            // If any of the keywords is not found the character file is not valid
            if (beard == 0 || hair == 0)
            {
                MessageBox.Show("This character cannot be customized because character customization data was not found.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        static public bool ReadCustomization()
        {
            int beard_length;
            byte[] beard;
            int hair_length;
            byte[] hair;

            // Read character file
            byte[] data = Util.ReadFileBytes(SelectedCharacterFile);

            // Check if anything was read
            if (data.Length == 0)
            {
                MessageBox.Show("Character file is empty.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            // Get beard
            // As beard is checked in CheckAppareance() there is no need to check if position == 0
            _CurrentCharacterBeardPosition = Util.FindInBytes(data, Encoding.UTF8.GetBytes("Beard"));
            beard_length = data[_CurrentCharacterBeardPosition];
            beard = Util.ReadBytesArray(data, _CurrentCharacterBeardPosition + 1, beard_length);

            if (_Beards_Internal.Contains(Encoding.UTF8.GetString(beard)))
                SelectedCharacterBeard = Beards_UI[Util.FindInArrayString(_Beards_Internal, Encoding.UTF8.GetString(beard))];
            else
                return false;

            // Get hair
            // As hair is checked in CheckAppareance() there is no need to check if position == 0
            _CurrentCharacterHairPosition = Util.FindInBytes(data, Encoding.UTF8.GetBytes("Hair"));
            hair_length = data[_CurrentCharacterHairPosition];
            _CurrentCharacterHairColorPosition = _CurrentCharacterHairPosition + hair_length + 1;
            hair = Util.ReadBytesArray(data, _CurrentCharacterHairPosition + 1, hair_length);

            if (_Hairs_Internal.Contains(Encoding.UTF8.GetString(hair)))
                SelectedCharacterHair = Hairs_UI[Util.FindInArrayString(_Hairs_Internal, Encoding.UTF8.GetString(hair))];
            else
                return false;

            return true;
        }

        static private byte[] _GetHairColor(String HairColor_UI)
        {
            byte[] result = { };

            if (HairColor_UI.Equals(Hair_Colors[0]))
            {
                result = _Color_Black;
            }
            else if (HairColor_UI.Equals(Hair_Colors[1]))
            {
                result = _Color_Blonde;
            }
            else if (HairColor_UI.Equals(Hair_Colors[2]))
            {
                result = _Color_Ginger;
            }
            else if (HairColor_UI.Equals(Hair_Colors[3]))
            {
                result = _Color_Brown;
            }
            else if (HairColor_UI.Equals(Hair_Colors[4]))
            {
                result = _Color_White;
            }

            return result;
        }

        static private byte[] _ReconstructCharacterFile(byte[] array, int current_length, int new_length, int position)
        {
            byte[] new_array = { };

            if (current_length == new_length)
                return array;

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

            // Reconstruct FCH header and recalculate positions
            // Header is saved in little-endian
            byte[] bHeader = { array[3], array[2], array[1], array[0] };

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bHeader);

            int iHeader = BitConverter.ToInt32(bHeader, 0);
            byte[] bNewHeader = BitConverter.GetBytes(iHeader + (new_length - current_length));

            _CurrentCharacterHairPosition += new_length - current_length;
            _CurrentCharacterHairColorPosition += new_length - current_length;

            for (int i = 0; i < 4; i++)
            {
                new_array[i] = bNewHeader[i];
            }

            return new_array;
        }

        static private bool _WriteCustomization(String Beard_UI, String Hair_UI, String HairColor_UI)
        {
            // Get selected customization data
            byte[] Beard = Encoding.UTF8.GetBytes(_Beards_Internal[Util.FindInArrayString(Beards_UI, Beard_UI)]);
            byte[] Hair = Encoding.UTF8.GetBytes(_Hairs_Internal[Util.FindInArrayString(Hairs_UI, Hair_UI)]);
            byte[] HairColor = _GetHairColor(HairColor_UI);

            // Read character file
            byte[] data = Util.ReadFileBytes(SelectedCharacterFile);

            // Check if anything was read
            if (data.Length == 0)
            {
                MessageBox.Show("Character file is empty.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            // Reconstruct data incase there is any length difference in beard and hair (hair color is always 0x18)
            data = _ReconstructCharacterFile(data, data[_CurrentCharacterBeardPosition], Beard.Length, _CurrentCharacterBeardPosition + 1);
            data = _ReconstructCharacterFile(data, data[_CurrentCharacterHairPosition], Hair.Length, _CurrentCharacterHairPosition + 1);

            // Write beard
            data[_CurrentCharacterBeardPosition] = (byte)Beard.Length;
            data = Util.WriteBytesArray(data, _CurrentCharacterBeardPosition + 1, Beard, 0, Beard.Length);

            // Write hair
            _CurrentCharacterHairPosition = _CurrentCharacterBeardPosition + Beard.Length + 1;
            data[_CurrentCharacterHairPosition] = (byte)Hair.Length;
            data = Util.WriteBytesArray(data, _CurrentCharacterHairPosition + 1, Hair, 0, Hair.Length);

            // Write hair color
            _CurrentCharacterHairColorPosition = _CurrentCharacterHairPosition + Hair.Length + 1;
            data = Util.WriteBytesArray(data, _CurrentCharacterHairColorPosition, HairColor, 0, HairColor.Length);

            // Save file
            File.WriteAllBytes(SelectedCharacterFile, data);

            return true;
        }

        static private bool _WriteName(String Name) // TODO
        {
            // Dont write a null Name
            if (String.IsNullOrEmpty(Name))
                return true;

            // WriteName

            return true;
        }

        static public bool WriteCustomization (String Beard_UI, String Hair_UI, String HairColor_UI, String Name = null)
        {
            // Check again if game is running to avoid problems
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Write the new customization
            if (_WriteName(Name) && _WriteCustomization(Beard_UI, Hair_UI, HairColor_UI))
                return true;
            else
                return false;
        }
    }
}
