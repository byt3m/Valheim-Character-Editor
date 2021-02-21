using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    internal class Customization
    {
        private static string[] _fchFiles;
        public static string[] Characters;
        public static string LastBackup;
        public static bool FirstRun = true;

        private static string _currentCharacterHairColor;

        private static Character _currentCharacter;

        public static string[] BeardsUi =
        {
            "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2",
            "Short 3", "Thick 1"
        };

        private static readonly string[] BeardsInternal =
        {
            "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7",
            "Beard8"
        };

        public static string[] HairsUi =
        {
            "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2",
            "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3"
        };

        private static readonly string[] HairsInternal =
        {
            "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5",
            "Hair8", "Hair9", "Hair10", "Hair14"
        };

        public static string[] HairColors = {"Black", "Blonde", "Ginger", "Brown", "White"};
        private static Character.Pos _colorBlack = new Character.Pos {X = 0.65f, Y = 0.106429f, Z = 0.075126f};

        private static readonly byte[] ColorBlack =
        {
            0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x39, 0xF7, 0xD9, 0x3D, 0x00, 0xEF,
            0xCA, 0x3D, 0xAF, 0xDB, 0x99, 0x3D
        };

        private static readonly byte[] ColorBlonde =
        {
            0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x00, 0x00, 0x80, 0x3F, 0x4F, 0xA7,
            0x35, 0x3F, 0x3C, 0x3C, 0xFC, 0x3E
        };

        private static readonly byte[] ColorGinger =
        {
            0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xC4, 0xA6, 0x32, 0x3F, 0x60, 0x69,
            0xAE, 0x3E, 0x55, 0xAB, 0x47, 0x3E
        };

        private static readonly byte[] ColorBrown =
        {
            0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x97, 0x37, 0x06, 0x3F, 0x71, 0x53,
            0xBF, 0x3E, 0xA2, 0x0F, 0x85, 0x3E
        };

        private static readonly byte[] ColorWhite =
        {
            0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0xEA, 0xA0, 0x4E, 0x3F, 0xDA, 0x60,
            0x40, 0x3F, 0xFF, 0xDA, 0x11, 0x3F
        };

        private static readonly byte[] SearchPattern =
            {0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F, 0x66, 0x66, 0x26, 0x3F};

        public static void Initialize(string character)
        {
            _currentCharacter = new Character {FileName = character};

            foreach (var file in _fchFiles)
            {
                if (character != Path.GetFileNameWithoutExtension(file)) continue;
                _currentCharacter.Path = file;
                break;
            }

            var data = Util.ReadFileBytes(_currentCharacter.Path);
            Util.ParseCharacterData(data, _currentCharacter);
        }

        public static void GetCharacters()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                @"AppData\LocalLow\IronGate\Valheim\characters");

            while (true)
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show(
                        "Directory containing character information not found. Please, point me to the directory where character \".FCH\" files are held.",
                        "ERROR", MessageBoxButtons.OK);
                    dir = Util.OpenDirectoryDialog();
                }
                else
                {
                    break;
                }

            _fchFiles = Directory.GetFiles(dir, "*.fch");
            Characters = new string[_fchFiles.Length];
            for (var i = 0; i < _fchFiles.Length; i++) Characters[i] = Path.GetFileNameWithoutExtension(_fchFiles[i]);

            if (_fchFiles.Length != 0) return;
            MessageBox.Show("No character data files found.", "ERROR", MessageBoxButtons.OK);
            Application.Exit();
        }

        private static byte[] ReadCharacterAppearance(string type)
        {
            var characterFileBytes = Util.ReadFileBytes(_currentCharacter.Path);
            byte[] bType = { };

            if (characterFileBytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return bType;
            }

            var searchString = Encoding.UTF8.GetBytes(type.Equals("Color") ? "Hair" : type);

            var position = Util.FindInBytes(characterFileBytes, searchString);

            if (position == 0)
            {
                if (type != "Beard" && type != "Hair" && type != "Color") type = "Name";
                MessageBox.Show(type + " not found for character " + _currentCharacter.FileName + ".", "ERROR",
                    MessageBoxButtons.OK);
                return bType;
            }

            int typeLength;

            if (type.Equals("Color"))
            {
                position += searchString.Length + 1;
                typeLength = 0x18;
            }
            else
            {
                typeLength = characterFileBytes[position];
            }

            bType = new byte[typeLength];

            for (var i = 0; i < bType.Length; i++) bType[i] = characterFileBytes[position + i + 1];

            return bType;
        }

        public static string ReadCharacterName()
        {
            return _currentCharacter.FileName;
        }

        public static string ReadCharacterHair()
        {
            var hair = _currentCharacter.Hair;
            return string.IsNullOrEmpty(hair) ? null : HairsUi[Util.FindInArrayString(HairsInternal, hair)];
        }

        public static string ReadCharacterBeard()
        {
            var beard = _currentCharacter.Beard;
            return string.IsNullOrEmpty(beard) ? null : BeardsUi[Util.FindInArrayString(BeardsInternal, beard)];
        }


        public static string ReadCharacterColor()
        {
            var color = ReadCharacterAppearance("Color");

            if (Util.CompareByteArrays(color, ColorBlack))
            {
                _currentCharacterHairColor = "Black";
            }
            else if (Util.CompareByteArrays(color, ColorBlonde))
            {
                _currentCharacterHairColor = "Blonde";
            }
            else if (Util.CompareByteArrays(color, ColorGinger))
            {
                _currentCharacterHairColor = "Ginger";
            }
            else if (Util.CompareByteArrays(color, ColorBrown))
            {
                _currentCharacterHairColor = "Brown";
            }
            else if (Util.CompareByteArrays(color, ColorWhite))
            {
                _currentCharacterHairColor = "White";
            }
            else
            {
                if (color.Length == 0)
                    return null;
                _currentCharacterHairColor = "Black";
            }

            return _currentCharacterHairColor;
        }

        private static byte[] GetColorBytes(string color)
        {
            switch (color)
            {
                case "Black":
                    return ColorBlack;
                case "Blonde":
                    return ColorBlonde;
                case "Ginger":
                    return ColorGinger;
                case "Brown":
                    return ColorBrown;
                case "White":
                    return ColorWhite;
                case null:
                    break;
            }

            return null;
        }

        public static bool IsCorrectName(string name)
        {
            return name.All(t => char.IsLetter(t) || t == ' ');
        }

        private static bool WriteCharacterData(string customization, string type, string filePath)
        {
            var position = 0;
            byte[] searchString;
            var characterFileBytes = Util.ReadFileBytes(_currentCharacter.Path);

            if (characterFileBytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            if (type.Equals("Color"))
                searchString = Encoding.UTF8.GetBytes("Hair");
            else if (type.Equals("Name"))
                searchString = Encoding.UTF8.GetBytes(_currentCharacter.Name);
            else
                searchString = Encoding.UTF8.GetBytes(type);

            do
            {
                position = Util.FindInBytes(characterFileBytes, searchString, position);

                if (position == 0 && !type.Equals("Name"))
                {
                    MessageBox.Show(
                        type + " not found for character " + _currentCharacter.FileName +
                        ". Please make sure to start a game with this character.", "ERROR", MessageBoxButtons.OK);
                    return false;
                }

                if (position == 0)
                {
                    break;
                }

                int currentLength = characterFileBytes[position];

                // Reconstruct byte array if there is a length difference between current customization and the new one
                if (customization.Length != currentLength && !type.Equals("Color"))
                    characterFileBytes = Util.ReconstructByteArray(characterFileBytes, currentLength,
                        customization.Length, position + 1);

                // Write data
                byte[] bCustomization;

                if (type.Equals("Color"))
                {
                    bCustomization = GetColorBytes(customization);
                    position += currentLength;
                }
                else
                {
                    bCustomization = Encoding.UTF8.GetBytes(customization);
                    characterFileBytes[position] = (byte) bCustomization.Length;
                }

                for (var i = 0; i < bCustomization.Length; i++)
                    characterFileBytes[position + i + 1] = bCustomization[i];

                position += (byte) bCustomization.Length;
            } while (type.Equals("Name"));

            Util.WriteFileBytes(filePath, characterFileBytes);

            return true;
        }

        public static bool WriteCustomization(string name, string beard, string hair, string hairColor,
            CheckBox nameCheckbox)
        {
            // Make backup of current FCH file
            var backup = Util.BackupFile(_currentCharacter.Path);

            if (string.IsNullOrEmpty(backup))
            {
                MessageBox.Show(
                    "There was an error while backing up current character data. Changes will not be applied.", "ERROR",
                    MessageBoxButtons.OK);
                return false;
            }

            LastBackup = backup;

            // if name is not null and not equal to current name proceed to write it
            if (!string.IsNullOrEmpty(name) && !name.ToLower().Equals(_currentCharacter.Name.ToLower()) &&
                nameCheckbox.Checked)
            {
                // Check name correctness (based on game behaviour)
                if (name.Length >= 3 && name.Length <= 15 && IsCorrectName(name))
                {
                    var newName = name[0].ToString().ToUpper();
                    for (var i = 1; i < name.Length; i++) newName += name[i].ToString().ToLower();

                    var newCharacterFile = Path.Combine(Path.GetDirectoryName(_currentCharacter.Path),
                        newName.ToLower() + ".fch");

                    if (File.Exists(newCharacterFile))
                    {
                        MessageBox.Show("Character " + name + " already exists.", "WARNING", MessageBoxButtons.OK);
                        return false;
                    }

                    if (WriteCharacterData(newName, "Name", newCharacterFile)) // Name
                    {
                        File.Delete(_currentCharacter.Path);
                        _currentCharacter.Path = newCharacterFile;
                        _currentCharacter.FileName = newName.ToLower();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Name must contain letters and must be between 3 and 15 characters long.",
                        "WARNING", MessageBoxButtons.OK);
                    return false;
                }
            }

            var newBeard = BeardsInternal[Util.FindInArrayString(BeardsUi, beard)];
            if (!newBeard.Equals(_currentCharacter.Beard))
                if (!WriteCharacterData(newBeard, "Beard", _currentCharacter.Path)) // Beard
                    return false;

            var newHair = HairsInternal[Util.FindInArrayString(HairsUi, hair)];
            if (!newHair.Equals(_currentCharacter.Hair))
                if (!WriteCharacterData(newHair, "Hair", _currentCharacter.Path)) // Beard
                    return false;

            if (!hairColor.Equals(_currentCharacterHairColor))
                if (!WriteCharacterData(hairColor, "Color", _currentCharacter.Path)) // Hair color
                    return false;

            return true;
        }

        public static bool RepairCharacter()
        {
            var characterFileBytes = Util.ReadFileBytes(_currentCharacter.Path);

            if (characterFileBytes.Length == 0)
            {
                MessageBox.Show("There was an error reading character's data.", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            var beard = Encoding.UTF8.GetBytes(BeardsInternal[0]);
            var hair = Encoding.UTF8.GetBytes(HairsInternal[0]);
            var bData = new byte[beard.Length + hair.Length + 2];

            bData[0] = (byte) beard.Length;
            bData[beard.Length + 1] = (byte) hair.Length;

            for (var i = 0; i < beard.Length; i++) bData[i + 1] = beard[i];

            for (var i = 0; i < hair.Length; i++) bData[i + beard.Length + 2] = hair[i];

            var position = Util.FindInBytes(characterFileBytes, SearchPattern);

            if (position == 0)
            {
                MessageBox.Show("Pattern not found. Character can not be restored", "ERROR", MessageBoxButtons.OK);
                return false;
            }

            position++;
            position -= bData.Length;

            for (var i = 0; i < bData.Length; i++) characterFileBytes[position + i] = bData[i];

            // Make backup of current FCH file
            var backup = Util.BackupFile(_currentCharacter.Path);

            if (string.IsNullOrEmpty(backup))
            {
                MessageBox.Show(
                    "There was an error while backing up current character data. Changes will not be applied.", "ERROR",
                    MessageBoxButtons.OK);
                return false;
            }

            LastBackup = backup;

            Util.WriteFileBytes(_currentCharacter.Path, characterFileBytes);

            return true;
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
            public Pos HairColor;
            public float Hp;
            public long Id;
            public List<Item> Inventory;
            public bool IsFirstSpawn;
            public int Kills;
            public List<string> KnownMaterials;
            public float MaxHp;
            public int Model;
            public string Name = "";
            public string Path = "";
            public List<string> Recipes;
            public List<string> ShownTutorials;
            public List<Skill> Skills;
            public Pos SkinColor;
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


            public class Pos
            {
                public float X;
                public float Y;
                public float Z;
            }

            public class World
            {
                public Pos DeathPoint = new Pos();
                public bool HasCustomSpawnPoint;
                public bool HasDeathPoint;
                public bool HasLogoutPoint;
                public Pos HomePoint = new Pos();
                public Pos LogoutPoint = new Pos();
                public byte[] MapData;
                public Pos SpawnPoint = new Pos();
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
