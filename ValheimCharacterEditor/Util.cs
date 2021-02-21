using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    internal class Util
    {
        public static byte[] ReadFileBytes(string file)
        {
            var fileStream = File.OpenRead(file);
            var binaryReader = new BinaryReader(fileStream);

            var dataSize = binaryReader.ReadInt32();
            var bytes = binaryReader.ReadBytes(dataSize);
            fileStream.Dispose();

            return bytes;
        }

        public static void ParseCharacterData(byte[] data, Customization.Character character)
        {
            var stream = new MemoryStream(data);
            var reader = new BinaryReader(stream);
            character.CharacterVersion = reader.ReadInt32(); // shouldn't be below 30
            character.Kills = reader.ReadInt32();
            character.Deaths = reader.ReadInt32();
            character.Crafts = reader.ReadInt32();
            character.Builds = reader.ReadInt32();
            var numberOfWorlds = reader.ReadInt32();
            for (var i = 0; i < numberOfWorlds; i++)
            {
                var worldId = reader.ReadInt64();
                var world = new Customization.Character.World
                {
                    HasCustomSpawnPoint = reader.ReadBoolean(),
                    SpawnPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HasLogoutPoint = reader.ReadBoolean(),
                    LogoutPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HasDeathPoint = reader.ReadBoolean(),
                    DeathPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HomePoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                };
                if (reader.ReadBoolean())
                {
                    var mapDataLength = reader.ReadInt32();     //character.WorldsData[worldId].MapData.Length;
                    var array = new byte[mapDataLength];
                    var loop = 0;
                    while (mapDataLength > 0)
                    {
                        var check = stream.Read(array, loop, mapDataLength);
                        if (check == 0)
                            break;
                        loop += check;
                        mapDataLength -= check;
                    }

                    if (loop != array.Length)
                    {
                        var array2 = new byte[loop];
                        Buffer.BlockCopy(array, 0, array2, 0, loop);
                        array = array2;
                    }
                    world.MapData = array;
                }

                character.WorldsData.Add(worldId, world);
            }

            character.Name = reader.ReadString();
            character.Id = reader.ReadInt64();
            character.StartSeed = reader.ReadString();

            if (!reader.ReadBoolean()) return;
            var dataLength = reader.ReadInt32();     // needs to be recalculated in case anything changes, strings
            character.DataVersion = reader.ReadInt32();

            character.MaxHp = reader.ReadSingle();
            character.Hp = reader.ReadSingle();
            character.Stamina = reader.ReadSingle();
            character.IsFirstSpawn = reader.ReadBoolean();
            character.TimeSinceDeath = reader.ReadSingle();
            character.GuardianPower = reader.ReadString();
            character.GuardianPowerCooldown = reader.ReadSingle();
            character.Inventory = new List<Customization.Character.Item>();

            character.InventoryVersion = reader.ReadInt32();
            var numberOfItems = reader.ReadInt32();
            for (var i = 0; i < numberOfItems; i++)
            {
                var item = new Customization.Character.Item
                {
                    Name = reader.ReadString(),
                    Stack = reader.ReadInt32(),
                    Durability = reader.ReadSingle(),
                    Pos = new Tuple<int, int>(reader.ReadInt32(), reader.ReadInt32()),
                    Equipped = reader.ReadBoolean(),
                    Quality = reader.ReadInt32(),
                    Variant = reader.ReadInt32(),
                    CrafterId = reader.ReadInt64(),
                    CrafterName = reader.ReadString()
                };

                if (item.Name != "")
                    character.Inventory.Add(item);
            }

            character.Recipes = new List<string>();
            character.KnownMaterials = new List<string>();
            character.ShownTutorials = new List<string>();
            character.Uniques = new List<string>();
            character.Trophies = new List<string>();
            character.Biomes = new List<Customization.Character.Biome>();

            var numberOfRecipes = reader.ReadInt32();
            for (var i = 0; i < numberOfRecipes; i++)
                character.Recipes.Add(reader.ReadString());

            var numberOfStations = reader.ReadInt32();
            for (var i = 0; i < numberOfStations; i++)
                character.Stations.Add(reader.ReadString(), reader.ReadInt32());

            var numberOfKnownMaterials = reader.ReadInt32();
            for (var i = 0; i < numberOfKnownMaterials; i++)
                character.KnownMaterials.Add(reader.ReadString());

            var numberOfShownTutorials = reader.ReadInt32();
            for (var i = 0; i < numberOfShownTutorials; i++)
                character.ShownTutorials.Add(reader.ReadString());

            var numberOfUniques = reader.ReadInt32();
            for (var i = 0; i < numberOfUniques; i++)
                character.Uniques.Add(reader.ReadString());

            var numberOfTrophies = reader.ReadInt32();
            for (var i = 0; i < numberOfTrophies; i++)
                character.Trophies.Add(reader.ReadString());

            var numberOfBiomes = reader.ReadInt32();
            for (var i = 0; i < numberOfBiomes; i++)
                character.Biomes.Add((Customization.Character.Biome) reader.ReadInt32());

            var numberOfTexts = reader.ReadInt32();
            for (var i = 0; i < numberOfTexts; i++)
                character.Texts.Add(reader.ReadString(), reader.ReadString());

            character.Beard = reader.ReadString();
            character.Hair = reader.ReadString();
            character.SkinColor = new Customization.Character.Pos
            {
                X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()
            };
            character.HairColor = new Customization.Character.Pos
            {
                X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()
            };
            character.Model = reader.ReadInt32();

            var numberOfConsumedFood = reader.ReadInt32();
            character.Foods = new List<Customization.Character.Food>();
            for (var i = 0; i < numberOfConsumedFood; i++)
            {
                var food = new Customization.Character.Food
                {
                    Name = reader.ReadString(), HpLeft = reader.ReadSingle(), StaminaLeft = reader.ReadSingle()
                };
                character.Foods.Add(food);
            }

            character.SkillsVersion = reader.ReadInt32();
            var numberOfSkills = reader.ReadInt32();
            character.Skills = new List<Customization.Character.Skill>();
            for (var i = 0; i < numberOfSkills; i++)
            {
                var skill = new Customization.Character.Skill
                {
                    SkillName = (Customization.Character.SkillName) reader.ReadInt32(),
                    Level = reader.ReadSingle(),
                    Something = reader.ReadSingle()
                };
                character.Skills.Add(skill);
            }
        }

        
        
        public static void WriteFileBytes(string file, byte[] data)
        {
            File.WriteAllBytes(file, data);
            Thread.Sleep(100);
        }

        public static byte[] ReconstructByteArray(byte[] array, int currentLength, int newLength, int position)
        {
            // Reconstruct byte array
            byte[] newArray = new byte[array.Length + (newLength - currentLength)];

            for (var i = 0; i < position; i++) newArray[i] = array[i];

            for (var i = position + currentLength; i < array.Length; i++)
                newArray[i - currentLength + newLength] = array[i];

            // Reconstruct FCH header
            // Header is saved in little-endian
            byte[] bHeader = {array[3], array[2], array[1], array[0]};

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bHeader);

            var iHeader = BitConverter.ToInt32(bHeader, 0);
            var bNewHeader = BitConverter.GetBytes(iHeader + (newLength - currentLength));

            for (var i = 0; i < 4; i++) newArray[i] = bNewHeader[i];

            return newArray;
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;

            return !array1.Where((t, i) => t != array2[i]).Any();
        }

        public static bool IsGameRunning()
        {
            //var process = Process.GetProcessesByName("valheim");
            //return process.Length != 0;
            return false;
        }

        public static string BackupFile(string file)
        {
            var destination = Path.Combine(Path.GetDirectoryName(file),
                Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".backup");
            File.Copy(file, destination, true);

            return !File.Exists(destination) ? null : destination;
        }

        public static bool RestoreFile(string file)
        {
            var destination = Path.Combine(Path.GetDirectoryName(file), Path.GetFileName(file).Split('_')[0] + ".fch");
            File.Copy(file, destination, true);

            return !File.Exists(destination);
        }

        public static string OpenDirectoryDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    return fbd.SelectedPath;
                return null;
            }
        }

        public static int FindInBytes(byte[] byteArray, byte[] toSearch, int start = 0)
        {
            var index = 0;
            for (var p = start; p < byteArray.Length; p++)
            {
                if (byteArray[p] == toSearch[index])
                    index++;
                else
                    index = 0;

                if (index == toSearch.Length) return p - toSearch.Length;
            }

            return 0;
        }

        public static int FindInArrayString(string[] array, string toSearch)
        {
            for (var i = 0; i < array.Length; i++)
                if (toSearch.Equals(array[i]))
                    return i;

            return 0;
        }
    }
}
