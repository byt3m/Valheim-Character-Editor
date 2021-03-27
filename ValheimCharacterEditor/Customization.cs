using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

namespace ValheimCharacterEditor
{
    class Customization
    {
        static public bool FirstRun = true;
        static public Character[] FoundCharacters;
        static public Character SelectedCharacter = new Character();

        public class Character
        {
            public String File;
            public ValheimEngine.Character Data;
        }

        static public void Initialize(String name)
        {
            foreach (Character character in FoundCharacters)
            {
                if (name == character.Data.Name)
                {
                    SelectedCharacter.Data = character.Data;
                    SelectedCharacter.File = character.File;
                    return;
                }
            }

            MessageBox.Show("Selected character not found.", "ERROR", MessageBoxButtons.OK);
            Application.Exit();
        }

        static public int GenderUItoInternal(String gender)
        {
            if (gender.Equals("Male"))
                return 0;
            else
                return 1;
        }

        static public String GenderInternaltoUI(int gender)
        {
            if (gender == 0)
                return "Male";
            else
                return "Female";
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

            String[] fchFiles = Directory.GetFiles(dir, "*.fch");
            if (fchFiles.Length == 0)
            {
                MessageBox.Show("No character data files found.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Create a Customization.Character class for each identified FCH file and read everything
            FoundCharacters = new Character[fchFiles.Length];
            for (int i = 0; i < fchFiles.Length; i++)
            {
                FoundCharacters[i] = new Character
                {
                    File = fchFiles[i], Data = Parser.CharacterReadData(fchFiles[i])
                };
            }
            GC.Collect();   // is it really that bad?
        }

        static private String RemoveInvalidFileNameChars (String file_name)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();
            String result = "";
            bool addChar = true;

            foreach (char chr in file_name)
            {
                foreach (char invalidchr in invalidFileChars)
                {
                    if (chr == invalidchr)
                        addChar = false;
                }

                if (addChar)
                    result += chr;
                else
                    addChar = true;
            }

            return result;
        }

        static public bool WriteCustomization()
        {
            // Check again if game is running to avoid problems
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

            // Build new file name in case name changed
            String newFileName = RemoveInvalidFileNameChars(SelectedCharacter.Data.Name) + ".fch";
            SelectedCharacter.File = Path.Combine(Path.GetDirectoryName(SelectedCharacter.File), newFileName);

            // Write new file
            File.WriteAllBytes(SelectedCharacter.File, Parser.CharacterWriteData(SelectedCharacter.Data));

            return true;
        }
    }
}
