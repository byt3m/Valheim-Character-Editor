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
             
        static public HashSet<ColorPreset> HairColorPresets = new HashSet<ColorPreset>
        {
            new ColorPreset { Name = "Black", Red = 0.106f, Green = 0.1f, Blue = 0.075f },
            new ColorPreset { Name = "Blonde", Red = 1f, Green = 0.71f, Blue = 0.49f },
            new ColorPreset { Name = "Ginger", Red = 0.70f, Green = 0.34f, Blue = 0.20f },
            new ColorPreset { Name = "Brown", Red = 0.525f, Green = 0.374f, Blue = 0.26f },
            new ColorPreset { Name = "White", Red = 0.81f, Green = 0.75f, Blue = 0.57f },
        };

        public class Character
        {
            public String File;
            public ValheimEngine.Character Data;
            public ColorPreset ColorPreset;
        }

        public class ColorPreset
        {
            public string Name;
            public float Red;
            public float Green;
            public float Blue;
        }

        static public void Initialize(String name)
        {
            foreach (Character character in FoundCharacters)
            {
                if (name == character.Data.Name)
                {
                    SelectedCharacter.Data = character.Data;
                    SelectedCharacter.File = character.File;
                    SelectedCharacter.ColorPreset = FindClosestPreset(SelectedCharacter.Data.HairColor);
                    return;
                }
            }

            MessageBox.Show("Selected character not found.", "ERROR", MessageBoxButtons.OK);
            Application.Exit();
        }

        static public ValheimEngine.Vector3 GetHairColor(int index)
        {
            return new ValheimEngine.Vector3
            {
                X = HairColorPresets.ElementAt(index).Red,
                Y = HairColorPresets.ElementAt(index).Green,
                Z = HairColorPresets.ElementAt(index).Blue,
            };
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
        public static ColorPreset FindClosestPreset(ValheimEngine.Vector3 color)
        {
            ColorPreset closestPreset = HairColorPresets.First(); 
            float lowestDist = 2;   // just has to be larger than sqrt(3)
            foreach (var preset in HairColorPresets)
            {
                // distance between points in 3d space
                float distance = Math.Abs(preset.Red * preset.Green * preset.Blue - color.X * color.Y * color.Z);
                if (distance <= lowestDist)
                {
                    lowestDist = distance;
                    closestPreset = preset;
                }
            }

            return closestPreset;
        }

        static public bool WriteCustomization ()
        {
            // Check again if game is running to avoid problems
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }

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
