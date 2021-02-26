using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ValheimCharacterEditor
{
    class Parser
    {
        public static ByteAccess LoadDataFromPath(string path)
        {
            using (var fileStream = File.OpenRead(path))
            {
                var reader = new BinaryReader(fileStream);
                var dataLength = reader.ReadInt32();
                var data = reader.ReadBytes(dataLength);
                //var hashLength = reader.ReadInt32();
                //reader.ReadBytes(hashLength);   // hash is unused, why bother?
                return new ByteAccess(data);
            }
        }

        // TODO check if data is correct/makes sense
        static public ValheimEngine.Character CharacterReadData(string path)
        {
            // Read header
            var character = new ValheimEngine.Character();
            var byteAccess = LoadDataFromPath(path);
            if (byteAccess.Length() == 0)
            {
                MessageBox.Show("Could not read file.", "ERROR", MessageBoxButtons.OK);
            }
            character.CharacterVersion = byteAccess.ReadInt32(); // shouldn't be below 30
            character.Kills = byteAccess.ReadInt32();
            character.Deaths = byteAccess.ReadInt32();
            character.Crafts = byteAccess.ReadInt32();
            character.Builds = byteAccess.ReadInt32();
            var numberOfWorlds = byteAccess.ReadInt32();
            // Read worlds information
            for (var i = 0; i < numberOfWorlds; i++)
            {
                var worldId = byteAccess.ReadInt64();
                var world = new ValheimEngine.Character.World
                {
                    HasCustomSpawnPoint = byteAccess.ReadBoolean(),
                    SpawnPoint = byteAccess.ReadVector3(),
                    HasLogoutPoint = byteAccess.ReadBoolean(),
                    LogoutPoint = byteAccess.ReadVector3(),
                    HasDeathPoint = byteAccess.ReadBoolean(),
                    DeathPoint = byteAccess.ReadVector3(),
                    HomePoint = byteAccess.ReadVector3(),
                };
                if (byteAccess.ReadBoolean())
                {
                    world.MapData = byteAccess.ReadBytes();
                }

                character.WorldsData.Add(worldId, world);
            }

            // Read main character info
            character.Name = byteAccess.ReadString();
            character.Id = byteAccess.ReadInt64();
            character.StartSeed = byteAccess.ReadString();

            // Check if character has more player data
            // Should be false for new characters
            if (!byteAccess.ReadBoolean()) return character;

            var dataLength = byteAccess.ReadInt32();
            character.DataVersion = byteAccess.ReadInt32();

            character.MaxHp = byteAccess.ReadSingle();
            character.Hp = byteAccess.ReadSingle();
            character.Stamina = byteAccess.ReadSingle();
            character.IsFirstSpawn = byteAccess.ReadBoolean();
            character.TimeSinceDeath = byteAccess.ReadSingle();
            character.GuardianPower = byteAccess.ReadString();
            character.GuardianPowerCooldown = byteAccess.ReadSingle();

            // Read inventory info
            character.Inventory = new List<ValheimEngine.Character.Item>();
            character.InventoryVersion = byteAccess.ReadInt32();
            var numberOfItems = byteAccess.ReadInt32();

            for (var i = 0; i < numberOfItems; i++)
            {
                var item = new ValheimEngine.Character.Item
                {
                    Name = byteAccess.ReadString(),
                    Stack = byteAccess.ReadInt32(),
                    Durability = byteAccess.ReadSingle(),
                    Pos = new Tuple<int, int>(byteAccess.ReadInt32(), byteAccess.ReadInt32()),
                    Equipped = byteAccess.ReadBoolean(),
                    Quality = byteAccess.ReadInt32(),
                    Variant = byteAccess.ReadInt32(),
                    CrafterId = byteAccess.ReadInt64(),
                    CrafterName = byteAccess.ReadString()
                };

                if (item.Name != "")
                    character.Inventory.Add(item);
            }

            // Read character info like recipes, trophies, tutorials, etc
            character.Recipes = new HashSet<string>();
            character.KnownMaterials = new HashSet<string>();
            character.ShownTutorials = new HashSet<string>();
            character.Uniques = new HashSet<string>();
            character.Trophies = new HashSet<string>();
            character.Biomes = new HashSet<ValheimEngine.Character.Biome>();

            var numberOfRecipes = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfRecipes; i++)
                character.Recipes.Add(byteAccess.ReadString());

            var numberOfStations = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfStations; i++)
                character.Stations.Add(byteAccess.ReadString(), byteAccess.ReadInt32());

            var numberOfKnownMaterials = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfKnownMaterials; i++)
                character.KnownMaterials.Add(byteAccess.ReadString());

            var numberOfShownTutorials = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfShownTutorials; i++)
                character.ShownTutorials.Add(byteAccess.ReadString());

            var numberOfUniques = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfUniques; i++)
                character.Uniques.Add(byteAccess.ReadString());

            var numberOfTrophies = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfTrophies; i++)
                character.Trophies.Add(byteAccess.ReadString());

            var numberOfBiomes = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfBiomes; i++)
                character.Biomes.Add((ValheimEngine.Character.Biome)byteAccess.ReadInt32());

            var numberOfTexts = byteAccess.ReadInt32();
            for (var i = 0; i < numberOfTexts; i++)
                character.Texts.Add(byteAccess.ReadString(), byteAccess.ReadString());

            // Read character appearance
            character.Beard = byteAccess.ReadString();
            character.Hair = byteAccess.ReadString();
            character.SkinColor = byteAccess.ReadVector3();
            character.HairColor = byteAccess.ReadVector3();
            character.Gender = byteAccess.ReadInt32();

            // Read character state like food consumed, skills, etc.
            var numberOfConsumedFood = byteAccess.ReadInt32();
            character.Foods = new List<ValheimEngine.Character.Food>();
            for (var i = 0; i < numberOfConsumedFood; i++)
            {
                var food = new ValheimEngine.Character.Food
                {
                    Name = byteAccess.ReadString(),
                    HpLeft = byteAccess.ReadSingle(),
                    StaminaLeft = byteAccess.ReadSingle()
                };
                character.Foods.Add(food);
            }

            character.SkillsVersion = byteAccess.ReadInt32();
            var numberOfSkills = byteAccess.ReadInt32();
            character.Skills = new HashSet<ValheimEngine.Character.Skill>();
            for (var i = 0; i < numberOfSkills; i++)
            {
                var skill = new ValheimEngine.Character.Skill
                {
                    SkillName = (ValheimEngine.Character.SkillName)byteAccess.ReadInt32(),
                    Level = byteAccess.ReadSingle(),
                    Something = byteAccess.ReadSingle()
                };
                character.Skills.Add(skill);
            }

            return character;
        }

        static public byte[] CharacterWriteData(ValheimEngine.Character character)
        {
            var byteAccess = new ByteAccess();

            byteAccess.Write(character.CharacterVersion);
            byteAccess.Write(character.Kills);
            byteAccess.Write(character.Deaths);
            byteAccess.Write(character.Crafts);
            byteAccess.Write(character.Builds);
            byteAccess.Write(character.WorldsData.Count);
            foreach (var world in character.WorldsData)
            {
                byteAccess.Write(world.Key);
                byteAccess.Write(world.Value.HasCustomSpawnPoint);
                byteAccess.Write(world.Value.SpawnPoint);
                byteAccess.Write(world.Value.HasLogoutPoint);
                byteAccess.Write(world.Value.LogoutPoint);
                byteAccess.Write(world.Value.HasDeathPoint);
                byteAccess.Write(world.Value.DeathPoint);
                byteAccess.Write(world.Value.HomePoint);
                byteAccess.Write(world.Value.MapData != null);
                if (world.Value.MapData != null)
                    byteAccess.Write(world.Value.MapData);
            }
            byteAccess.Write(character.Name);
            byteAccess.Write(character.Id);
            byteAccess.Write(character.StartSeed);
            byteAccess.Write(character.DataVersion != 0);   // didn't save the check so have to create one

            //player data
            //need to turn data into hex, calculate size and put it before actual data
            var byteAccess2 = new ByteAccess();
            if (character.MaxHp != 0)
            {
                byteAccess2.Write(character.DataVersion);
                byteAccess2.Write(character.MaxHp);
                byteAccess2.Write(character.Hp);
                byteAccess2.Write(character.Stamina);
                byteAccess2.Write(character.IsFirstSpawn);
                byteAccess2.Write(character.TimeSinceDeath);
                byteAccess2.Write(character.GuardianPower);
                byteAccess2.Write(character.GuardianPowerCooldown);
                byteAccess2.Write(character.InventoryVersion);
                byteAccess2.Write(character.Inventory.Count);

                foreach (var item in character.Inventory)
                {
                    byteAccess2.Write(item.Name);
                    byteAccess2.Write(item.Stack);
                    byteAccess2.Write(item.Durability);
                    byteAccess2.Write(item.Pos.Item1);
                    byteAccess2.Write(item.Pos.Item2);
                    byteAccess2.Write(item.Equipped);
                    byteAccess2.Write(item.Quality);
                    byteAccess2.Write(item.Variant);
                    byteAccess2.Write(item.CrafterId);
                    byteAccess2.Write(item.CrafterName);
                }

                byteAccess2.Write(character.Recipes.Count);
                foreach (var recipe in character.Recipes)
                    byteAccess2.Write(recipe);

                byteAccess2.Write(character.Stations.Count);
                foreach (var station in character.Stations)
                {
                    byteAccess2.Write(station.Key);
                    byteAccess2.Write(station.Value);
                }

                byteAccess2.Write(character.KnownMaterials.Count);
                foreach (var material in character.KnownMaterials)
                    byteAccess2.Write(material);

                byteAccess2.Write(character.ShownTutorials.Count);
                foreach (var tutorial in character.ShownTutorials)
                    byteAccess2.Write(tutorial);

                byteAccess2.Write(character.Uniques.Count);
                foreach (var unique in character.Uniques)
                    byteAccess2.Write(unique);

                byteAccess2.Write(character.Trophies.Count);
                foreach (var trophy in character.Trophies)
                    byteAccess2.Write(trophy);

                byteAccess2.Write(character.Biomes.Count);
                foreach (var biome in character.Biomes)
                    byteAccess2.Write((int)biome);

                byteAccess2.Write(character.Texts.Count);
                foreach (var text in character.Texts)
                {
                    byteAccess2.Write(text.Key);
                    byteAccess2.Write(text.Value);
                }

                byteAccess2.Write(character.Beard);
                byteAccess2.Write(character.Hair);
                byteAccess2.Write(character.SkinColor);
                byteAccess2.Write(character.HairColor);
                byteAccess2.Write(character.Gender);
                byteAccess2.Write(character.Foods.Count);
                foreach (var food in character.Foods)
                {
                    byteAccess2.Write(food.Name);
                    byteAccess2.Write(food.HpLeft);
                    byteAccess2.Write(food.StaminaLeft);
                }

                byteAccess2.Write(character.SkillsVersion);
                byteAccess2.Write(character.Skills.Count);
                foreach (var skill in character.Skills)
                {
                    byteAccess2.Write((int)skill.SkillName);
                    byteAccess2.Write(skill.Level);
                    byteAccess2.Write(skill.Something);
                }
            }
            byte[] playerData = byteAccess2.ToArray();

            // order:
            // length of (playerData + data) + data + playerData + length of hash + hash
            byteAccess.Write(playerData.Length);
            byte[] data = byteAccess.ToArray();
            byte[] final = data.Concat(playerData).ToArray();
            byte[] length = BitConverter.GetBytes(final.Length);
            byte[] hashLength = BitConverter.GetBytes(64); // 512/8
            byte[] hash = SHA512.Create().ComputeHash(final);   // Unused but want to keep it
            byteAccess.Clear();
            byteAccess2.Clear();

            return length.Concat(final).ToArray().Concat(hashLength).ToArray().Concat(hash).ToArray();
        }
    }
}
