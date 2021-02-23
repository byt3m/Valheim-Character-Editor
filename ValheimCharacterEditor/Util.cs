using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Threading;

namespace ValheimCharacterEditor
{
    class Util
    {
        static private string _LastBackup;
        static private string _BeforeLastBackup;

        static public byte[] ReadFileBytes(String file)
        {
            Byte[] bytes = File.ReadAllBytes(file);
            Thread.Sleep(100);
            return bytes;
        }

        static public void WriteFileBytes(String file, Byte[] data)
        {
            File.WriteAllBytes(file, data);
            Thread.Sleep(100);
        }

        public static Customization.Character ParseCharacterData(byte[] data)
        {
            var character = new Customization.Character();
            var stream = new MemoryStream(data);
            var reader = new BinaryReader(stream);
            var fileSize = reader.ReadInt32();
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
                    SpawnPoint = { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() },
                    HasLogoutPoint = reader.ReadBoolean(),
                    LogoutPoint = { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() },
                    HasDeathPoint = reader.ReadBoolean(),
                    DeathPoint = { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() },
                    HomePoint = { X = reader.ReadSingle(), Y = reader.ReadSingle(), Z = reader.ReadSingle() },
                };
                if (reader.ReadBoolean())
                {
                    var mapDataLength = reader.ReadInt32();
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

            if (!reader.ReadBoolean()) return character;
            
            var dataLength = reader.ReadInt32(); // needs to be recalculated in case anything changes, strings
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
            character.SkinColor = new Customization.Vector3
            {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle(),
                Z = reader.ReadSingle()
            };
            character.HairColor = new Customization.Vector3
            {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle(),
                Z = reader.ReadSingle()
            };
            character.Model = reader.ReadInt32();

            var numberOfConsumedFood = reader.ReadInt32();
            character.Foods = new List<Customization.Character.Food>();
            for (var i = 0; i < numberOfConsumedFood; i++)
            {
                var food = new Customization.Character.Food
                {
                    Name = reader.ReadString(),
                    HpLeft = reader.ReadSingle(),
                    StaminaLeft = reader.ReadSingle()
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

            return character;
        }

        public static byte[] WriteCharacterData(Customization.Character character)
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write(character.CharacterVersion);
            writer.Write(character.Kills);
            writer.Write(character.Deaths);
            writer.Write(character.Crafts);
            writer.Write(character.Builds);
            writer.Write(character.WorldsData.Count);
            foreach (var world in character.WorldsData)
            {
                writer.Write(world.Key);
                writer.Write(world.Value.HasCustomSpawnPoint);
                writer.Write(world.Value.SpawnPoint.X);
                writer.Write(world.Value.SpawnPoint.Y);
                writer.Write(world.Value.SpawnPoint.Z);
                writer.Write(world.Value.HasLogoutPoint);
                writer.Write(world.Value.LogoutPoint.X);
                writer.Write(world.Value.LogoutPoint.Y);
                writer.Write(world.Value.LogoutPoint.Z);
                writer.Write(world.Value.HasDeathPoint);
                writer.Write(world.Value.DeathPoint.X);
                writer.Write(world.Value.DeathPoint.Y);
                writer.Write(world.Value.DeathPoint.Z);
                writer.Write(world.Value.HomePoint.X);
                writer.Write(world.Value.HomePoint.Y);
                writer.Write(world.Value.HomePoint.Z);
                writer.Write(world.Value.MapData != null);
                writer.Write(character.WorldsData[world.Key].MapData.Length);
                if (world.Value.MapData != null)
                    writer.Write(world.Value.MapData);
            }
            writer.Write(character.Name);
            writer.Write(character.Id);
            writer.Write(character.StartSeed);
            writer.Write(character.MaxHp != 0);


            //player data
            //need to turn data into hex, calculate size and put it before actual data
            var stream2 = new MemoryStream();
            var writer2 = new BinaryWriter(stream2);
            if (character.MaxHp != 0)
            {
                writer2.Write(character.DataVersion);
                writer2.Write(character.MaxHp);
                writer2.Write(character.Hp);
                writer2.Write(character.Stamina);
                writer2.Write(character.IsFirstSpawn);
                writer2.Write(character.TimeSinceDeath);
                writer2.Write(character.GuardianPower);
                writer2.Write(character.GuardianPowerCooldown);
                writer2.Write(character.InventoryVersion);
                writer2.Write(character.Inventory.Count);
                foreach (var item in character.Inventory)
                {
                    writer2.Write(item.Name);
                    writer2.Write(item.Stack);
                    writer2.Write(item.Durability);
                    writer2.Write(item.Pos.Item1);
                    writer2.Write(item.Pos.Item2);
                    writer2.Write(item.Equipped);
                    writer2.Write(item.Quality);
                    writer2.Write(item.Variant);
                    writer2.Write(item.CrafterId);
                    writer2.Write(item.CrafterName);
                }

                writer2.Write(character.Recipes.Count);
                foreach (var recipe in character.Recipes)
                    writer2.Write(recipe);
                writer2.Write(character.Stations.Count);
                foreach (var station in character.Stations)
                {
                    writer2.Write(station.Key);
                    writer2.Write(station.Value);
                }

                writer2.Write(character.KnownMaterials.Count);
                foreach (var material in character.KnownMaterials)
                    writer2.Write(material);
                writer2.Write(character.ShownTutorials.Count);
                foreach (var tutorial in character.ShownTutorials)
                    writer2.Write(tutorial);
                writer2.Write(character.Uniques.Count);
                foreach (var unique in character.Uniques)
                    writer2.Write(unique);
                writer2.Write(character.Trophies.Count);
                foreach (var trophy in character.Trophies)
                    writer2.Write(trophy);
                writer2.Write(character.Biomes.Count);
                foreach (var biome in character.Biomes)
                    writer2.Write((int)biome);
                writer2.Write(character.Texts.Count);
                foreach (var text in character.Texts)
                {
                    writer2.Write(text.Key);
                    writer2.Write(text.Value);
                }

                writer2.Write(character.Beard);
                writer2.Write(character.Hair);
                writer2.Write(character.SkinColor.X);
                writer2.Write(character.SkinColor.Y);
                writer2.Write(character.SkinColor.Z);
                writer2.Write(character.HairColor.X);
                writer2.Write(character.HairColor.Y);
                writer2.Write(character.HairColor.Z);
                writer2.Write(character.Model);
                writer2.Write(character.Foods.Count);
                foreach (var food in character.Foods)
                {
                    writer2.Write(food.Name);
                    writer2.Write(food.HpLeft);
                    writer2.Write(food.StaminaLeft);
                }

                writer2.Write(character.SkillsVersion);
                writer2.Write(character.Skills.Count);
                foreach (var skill in character.Skills)
                {
                    writer2.Write((int)skill.SkillName);
                    writer2.Write(skill.Level);
                    writer2.Write(skill.Something);
                }
            }

            writer2.Flush();
            stream2.Flush();
            byte[] playerData = stream2.ToArray();

            //calc size
            writer.Write(playerData.Length);

            writer.Flush();
            stream.Flush();
            byte[] data = stream.ToArray();
            byte[] final = data.Concat(playerData).ToArray();
            byte[] length = BitConverter.GetBytes(final.Length);
            byte[] hashLength = BitConverter.GetBytes(64); // 512/8
            byte[] hash = SHA512.Create().ComputeHash(final);
            return length.Concat(final).ToArray().Concat(hashLength).ToArray().Concat(hash).ToArray();
        }

        static public bool isGameRunning()
        {
            Process[] process = Process.GetProcessesByName("valheim");

            if (process.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static public bool BackupFile(String file)
        {
            String destination = Path.Combine(Path.GetDirectoryName(file), (string)(Path.GetFileNameWithoutExtension(file) + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".backup"));
            File.Copy(file, destination, true);

            if (File.Exists(destination))
            {
                _LastBackup = destination;
                _BeforeLastBackup = file;

                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool RestoreFile()
        {
            File.Copy(_LastBackup, _BeforeLastBackup, true);

            if (File.Exists(_BeforeLastBackup))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public String OpenDirectoryDialog()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
                else
                {
                    return null;
                }
            }            
        }

        static public int FindInArrayString(String[] array, String toSearch)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (toSearch.Equals(array[i]))
                {
                    return i;
                }
            }

            return 0;
        }

        static public byte[] WriteBytesArray(byte[] array, int array_index, byte[] data, int data_index, int length)
        {
            for (int i = 0; i < length; i++)
            {
                array[array_index + i] = data[data_index + i];
            }

            return array;
        }
    }
}
