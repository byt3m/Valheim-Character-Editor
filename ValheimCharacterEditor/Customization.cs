using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using static ValheimCharacterEditor.ValheimEngine.Character;

namespace ValheimCharacterEditor
{
    class Customization
    {
        static public bool FirstRun = true;
        static public Character[] FoundCharacters;
        static public Character SelectedCharacter = new Character();

        static public List<Item> InventoryScratchPad = new List<Item>();
        static public HashSet<Skill> SkillScratchPad = new HashSet<Skill>();


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
        static public bool WriteCustomization ()
        {
            // Check again if game is running to avoid problems
            //if (Util.isGameRunning())
            //{
            //    MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
            //    Application.Exit();
            //}

            // Currently writting to the same .FCH file. I changed this because windows has limitations for file names and people will 
            // start using forbidden characters which will result in a crash when writting file. Also, the characters combobox now shows the character
            // names instead of file names, so there is not really a need to change the filename as the user will always see his in-game name in the GUI.
            // No need to check WriteAllBytes as it does not return any value. If fails it will go to the Form1 Try-Catch block
            // No need to check backup because it is already checked when it is done in Util.
            File.WriteAllBytes(SelectedCharacter.File, Parser.CharacterWriteData(SelectedCharacter.Data));

            return true;
        }
    }
}
