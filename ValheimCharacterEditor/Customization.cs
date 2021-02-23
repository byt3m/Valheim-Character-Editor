using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Linq;

namespace ValheimCharacterEditor
{
    class Customization
    {
        static private String[] _fchFiles;
        static public String[] Characters;
        static public bool FirstRun = true;

        static public String SelectedCharacterBeard;
        static public String SelectedCharacterHair;
        public static HairColorPreset closestPreset;

        public static Character CurrentCharacter;

        static private int _currentCharacterBeardPosition;
        static private int _currentCharacterHairPosition;
        static private int _currentCharacterHairColorPosition;

        static public String[] BeardsUi = { "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2", "Short 3", "Thick 1" };
        static public String[] BeardsInternal = { "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7", "Beard8" };
        static public String[] HairsUi = { "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2", "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3" };
        static public String[] HairsInternal = { "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5", "Hair8", "Hair9", "Hair10", "Hair14" };

        public static HashSet<HairColorPreset> HairColorPresets = new HashSet<HairColorPreset>
        {
            new HairColorPreset { Name = "Black", Red = 0.106f, Green = 0.1f, Blue = 0.075f },
            new HairColorPreset { Name = "Blonde", Red = 1f, Green = 0.71f, Blue = 0.49f },
            new HairColorPreset { Name = "Ginger", Red = 0.70f, Green = 0.34f, Blue = 0.20f },
            new HairColorPreset { Name = "Brown", Red = 0.525f, Green = 0.374f, Blue = 0.26f },
            new HairColorPreset { Name = "White", Red = 0.81f, Green = 0.75f, Blue = 0.57f },
        };
        
        public class HairColorPreset
        {
            public string Name;
            public float Red;
            public float Green;
            public float Blue;
        }

        static public void Initialize(String character)
        {
            foreach (String file in _fchFiles)
            {
                if (character == Path.GetFileNameWithoutExtension(file))
                {
                    CurrentCharacter = new Character();
                    var data = Util.ReadFileBytes(file);
                    
                    CurrentCharacter = Util.ParseCharacterData(data);
                    // TODO check if data is correct
                    
                    CurrentCharacter.FileName = character;
                    CurrentCharacter.Path = file;

                    closestPreset = FindClosestPreset(CurrentCharacter.HairColor);
                    return;
                }
            }

            MessageBox.Show("Character .FCH file not found!", "ERROR", MessageBoxButtons.OK);
            Application.Exit();
        }

        static public bool IsCorrectName(String name)
        {
            // Check content
            if (String.IsNullOrEmpty(name))
                return false;

            // Check length
            if (name.Length < 3 || name.Length > 15)
                return false;

            // Check allowed characters
            // Game way of checking valid names is complicated, this is just an approximation 
            // It doesn't have to be perfect, ex. game doesn't allow to use numbers but they work just fine
            foreach (var t in name)
            {
                if (!char.IsLetter(t) || t != ' ')
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

            _fchFiles = Directory.GetFiles(dir, "*.fch");
            Characters = new string[_fchFiles.Length];
            for (int i = 0; i < _fchFiles.Length; i++)
            {
                Characters[i] = Path.GetFileNameWithoutExtension(_fchFiles[i]);
            }

            if (_fchFiles.Length == 0)
            {
                MessageBox.Show("No character data files found.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }
        }

        // very simple, could probably be done better in the future
        public static HairColorPreset FindClosestPreset(Vector3 color)
        {
            HairColorPreset closestPreset = HairColorPresets.First(); 
            float lowestDist = 2;   // just has to be larger than sqrt(2)
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

        static public void InternalToName()
        {
            // Get beard
            if (BeardsInternal.Contains(CurrentCharacter.Beard))
                SelectedCharacterBeard = BeardsUi[Util.FindInArrayString(BeardsInternal, CurrentCharacter.Beard)];

            // Get hair
            if (HairsInternal.Contains(CurrentCharacter.Hair))
                SelectedCharacterHair = HairsUi[Util.FindInArrayString(HairsInternal, CurrentCharacter.Hair)];
        }

        static public bool WriteCustomization ()
        {
            // Check again if game is running to avoid problems
            if (Util.isGameRunning())
            {
                MessageBox.Show("Please close Valheim before editing your character.", "ERROR", MessageBoxButtons.OK);
                Application.Exit();
            }
            String dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), @"AppData\LocalLow\IronGate\Valheim\characters");
            var characterBytes = Util.WriteCharacterData(CurrentCharacter);
            Util.WriteFileBytes(dir + "\\" +CurrentCharacter.Name + ".fch", characterBytes);

            // TODO add check to see if WriteFileBytes is successful and backups
            return true;
        }

        public class Vector3
        {
            public float X;
            public float Y;
            public float Z;
        }
        public class Character
        {
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

            public enum SkillName
            {
                None,
                Swords,
                Knives,
                Clubs,
                Polearms,
                Spears,
                Blocking,
                Axes,
                Bows,
                FireMagic,
                FrostMagic,
                Unarmed,
                Pickaxes,
                WoodCutting,
                Jump = 100,
                Sneak,
                Run,
                Swim,
                All = 999
            }

            public string Beard = "";
            public List<Biome> Biomes;
            public int Builds;
            public int Crafts;
            public int Deaths;
            public string FileName = "";
            public List<Food> Foods;
            public string GuardianPower;
            public float GuardianPowerCooldown;
            public string Hair = "";
            public Vector3 HairColor;
            public float Hp;
            public long Id;
            public List<Item> Inventory;
            public bool IsFirstSpawn;
            public int Kills;
            public List<string> KnownMaterials;
            public float MaxHp = 0;
            public int Model;
            public string Name = "";
            public string Path = "";
            public List<string> Recipes;
            public List<string> ShownTutorials;
            public List<Skill> Skills;
            public Vector3 SkinColor;
            public float Stamina;
            public string StartSeed = "";
            public Dictionary<string, int> Stations = new Dictionary<string, int>();
            public Dictionary<string, string> Texts = new Dictionary<string, string>();
            public float TimeSinceDeath;
            public List<string> Trophies;
            public List<string> Uniques;
            public Dictionary<long, World> WorldsData = new Dictionary<long, World>();
            public int DataVersion;
            public int SkillsVersion;
            public int InventoryVersion;
            public int CharacterVersion;

            public class World
            {
                public Vector3 DeathPoint = new Vector3();
                public bool HasCustomSpawnPoint;
                public bool HasDeathPoint;
                public bool HasLogoutPoint;
                public Vector3 HomePoint = new Vector3();
                public Vector3 LogoutPoint = new Vector3();
                public byte[] MapData;
                public Vector3 SpawnPoint = new Vector3();
            }

            public class Item
            {
                public long CrafterId;
                public string CrafterName;
                public float Durability;
                public bool Equipped;
                public string Name;
                public Tuple<int, int> Pos;
                public int Quality;
                public int Stack;
                public int Variant;
            }

            public class Food
            {
                public float HpLeft;
                public string Name;
                public float StaminaLeft;
            }

            public class Skill
            {
                public float Level;
                public SkillName SkillName;
                public float Something;
            }
        }
    }
}
