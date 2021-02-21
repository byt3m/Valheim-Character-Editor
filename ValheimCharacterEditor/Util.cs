using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    internal class Util
    {
        public static byte[] ReadFileBytes2(string file)
        {
            var fileStream = File.OpenRead(file);
            var binaryReader = new BinaryReader(fileStream);

            var dataSize = binaryReader.ReadInt32();
            var bytes = binaryReader.ReadBytes(dataSize);
            fileStream.Dispose();

            return bytes;
        }

        public static void ParseCharacterData(byte[] data, Customization.Character Character)
        {
            var stream = new MemoryStream(data);
            var reader = new BinaryReader(stream);
            var characterVersion = reader.ReadInt32(); // shouldn't be below 30
            Character.Kills = reader.ReadInt32();
            Character.Deaths = reader.ReadInt32();
            Character.Crafts = reader.ReadInt32();
            Character.Builds = reader.ReadInt32();
            var NumberOfWorlds = reader.ReadInt32();
            for (var i = 0; i < NumberOfWorlds; i++)
            {
                var WorldID = reader.ReadInt64();
                var world = new Customization.Character.World
                {
                    HasCustomSpawnPoint = reader.ReadBoolean(),
                    SpawnPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HasLogoutPoint = reader.ReadBoolean(),
                    LogoutPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HasDeathPoint = reader.ReadBoolean(),
                    DeathPoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    HomePoint = {X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()},
                    MapData = reader.ReadBoolean() ? ReadArray(reader, stream) : null
                };
                Character.WorldsData.Add(WorldID, world);
            }

            Character.Name = reader.ReadString();
            Character.Id = reader.ReadInt64();
            Character.StartSeed = reader.ReadString();

            if (!reader.ReadBoolean()) return;
            var dataLength = reader.ReadInt32();
            var dataVersion = reader.ReadInt32();

            Character.MaxHp = reader.ReadSingle();
            Character.Hp = reader.ReadSingle();
            Character.Stamina = reader.ReadSingle();
            Character.IsFirstSpawn = reader.ReadBoolean();
            Character.TimeSinceDeath = reader.ReadSingle();
            Character.GuardianPower = reader.ReadString();
            Character.GuardianPowerCooldown = reader.ReadSingle();
            Character.Inventory = new List<Customization.Character.Item>();

            var inventoryVersion = reader.ReadInt32();
            var numberOfItems = reader.ReadInt32();
            for (var i = 0; i < numberOfItems; i++)
            {
                var item = new Customization.Character.Item();

                item.Name = reader.ReadString();
                item.Stack = reader.ReadInt32();
                item.Durability = reader.ReadSingle();
                item.Pos = new Tuple<int, int>(reader.ReadInt32(), reader.ReadInt32());
                item.Equipped = reader.ReadBoolean();
                item.Quality = reader.ReadInt32();
                item.Variant = reader.ReadInt32();
                item.CrafterId = reader.ReadInt64();
                item.CrafterName = reader.ReadString();

                if (item.Name != "")
                    Character.Inventory.Add(item);
            }

            Character.Recipes = new List<string>();
            Character.KnownMaterials = new List<string>();
            Character.ShownTutorials = new List<string>();
            Character.Uniques = new List<string>();
            Character.Trophies = new List<string>();
            Character.Biomes = new List<Customization.Character.Biome>();

            var numberOfRecipes = reader.ReadInt32();
            for (var i = 0; i < numberOfRecipes; i++)
                Character.Recipes.Add(reader.ReadString());

            var numberOfStations = reader.ReadInt32();
            for (var i = 0; i < numberOfStations; i++)
                Character.Stations.Add(reader.ReadString(), reader.ReadInt32());

            var numberOfKnownMaterials = reader.ReadInt32();
            for (var i = 0; i < numberOfKnownMaterials; i++)
                Character.KnownMaterials.Add(reader.ReadString());

            var numberOfShownTutorials = reader.ReadInt32();
            for (var i = 0; i < numberOfShownTutorials; i++)
                Character.ShownTutorials.Add(reader.ReadString());

            var numberOfUniques = reader.ReadInt32();
            for (var i = 0; i < numberOfUniques; i++)
                Character.Uniques.Add(reader.ReadString());

            var numberOfTrophies = reader.ReadInt32();
            for (var i = 0; i < numberOfTrophies; i++)
                Character.Trophies.Add(reader.ReadString());

            var numberOfBiomes = reader.ReadInt32();
            for (var i = 0; i < numberOfBiomes; i++)
                Character.Biomes.Add((Customization.Character.Biome) reader.ReadInt32());

            var numberOfTexts = reader.ReadInt32();
            for (var i = 0; i < numberOfTexts; i++)
                Character.Texts.Add(reader.ReadString(), reader.ReadString());

            Character.Beard = reader.ReadString();
            Character.Hair = reader.ReadString();
            Character.SkinColor = new Customization.Character.Pos
            {
                X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()
            };
            Character.HairColor = new Customization.Character.Pos
            {
                X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle()
            };
            Character.Model = reader.ReadInt32();

            var numberOfConsumedFood = reader.ReadInt32();
            Character.Foods = new List<Customization.Character.Food>();
            for (var i = 0; i < numberOfConsumedFood; i++)
            {
                var food = new Customization.Character.Food
                {
                    Name = reader.ReadString(), HpLeft = reader.ReadSingle(), StaminaLeft = reader.ReadSingle()
                };
                Character.Foods.Add(food);
            }

            var skillsVersion = reader.ReadInt32();
            var numberOfSkills = reader.ReadInt32();
            Character.Skills = new List<Customization.Character.Skill>();
            for (var i = 0; i < numberOfSkills; i++)
            {
                var skill = new Customization.Character.Skill
                {
                    SkillName = (Customization.Character.SkillName) reader.ReadInt32(),
                    Level = reader.ReadSingle(),
                    Something = reader.ReadSingle()
                };
                Character.Skills.Add(skill);
            }
        }


        private static byte[] ReadArray(BinaryReader reader, MemoryStream stream)
        {
            var count = reader.ReadInt32();
            var array = new byte[count];
            var loop = 0;
            while (count > 0)
            {
                var check = stream.Read(array, loop, count);
                if (check == 0)
                    break;
                loop += check;
                count -= check;
            }

            if (loop != array.Length)
            {
                var array2 = new byte[loop];
                Buffer.BlockCopy(array, 0, array2, 0, loop);
                array = array2;
            }

            return array;
        }

        public static void WriteFileBytes(string file, byte[] data)
        {
            File.WriteAllBytes(file, data);
            Thread.Sleep(100);
        }

        public static byte[] ReconstructByteArray(byte[] array, int current_length, int new_length, int position)
        {
            byte[] new_array = { };

            // Reconstruct byte array
            new_array = new byte[array.Length + (new_length - current_length)];

            for (var i = 0; i < position; i++) new_array[i] = array[i];

            for (var i = position + current_length; i < array.Length; i++)
                new_array[i - current_length + new_length] = array[i];

            // Reconstruct FCH header
            // Header is saved in little-endian
            byte[] bHeader = {array[3], array[2], array[1], array[0]};

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bHeader);

            var iHeader = BitConverter.ToInt32(bHeader, 0);
            var bNewHeader = BitConverter.GetBytes(iHeader + (new_length - current_length));

            for (var i = 0; i < 4; i++) new_array[i] = bNewHeader[i];

            return new_array;
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length) return false;

            for (var i = 0; i < array1.Length; i++)
                if (array1[i] != array2[i])
                    return false;

            return true;
        }

        public static bool isGameRunning()
        {
            var process = Process.GetProcessesByName("valheim");

            //return process.Length != 0;
            return false;
        }

        public static string BackupFile(string file)
        {
            var destination = Path.Combine(Path.GetDirectoryName(file),
                Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".backup");
            File.Copy(file, destination, true);

            if (!File.Exists(destination)) return null;

            return destination;
        }

        public static bool RestoreFile(string file)
        {
            var destination = Path.Combine(Path.GetDirectoryName(file), Path.GetFileName(file).Split('_')[0] + ".fch");
            File.Copy(file, destination, true);

            if (!File.Exists(destination)) return true;

            return false;
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

        public static int FindInBytes(byte[] byte_array, byte[] toSearch, int Start = 0)
        {
            var index = 0;
            for (var p = Start; p < byte_array.Length; p++)
            {
                if (byte_array[p] == toSearch[index])
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
