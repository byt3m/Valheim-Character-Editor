using System;
using System.Collections.Generic;

namespace ValheimCharacterEditor
{
    class ValheimEngine // This class should only be used to "store" the reverse engineered classes from the game and other required structs like Vec3.
    {
        static public String[] BeardsUI = { "No beard", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Long 2", "Short 1", "Short 2", "Short 3", "Thick 1" };
        static public String[] BeardsInternal = { "BeardNone", "Beard5", "Beard6", "Beard9", "Beard10", "Beard1", "Beard2", "Beard3", "Beard4", "Beard7", "Beard8" };
        static public String[] HairsUI = { "No hair", "Braided 1", "Braided 2", "Braided 3", "Braided 4", "Long 1", "Ponytail 1", "Ponytail 2", "Ponytail 3", "Ponytail 4", "Short 1", "Short 2", "Side Swept 1", "Side Swept 2", "Side Swept 3" };
        static public String[] HairsInternal = { "HairNone", "Hair3", "Hair11", "Hair12", "Hair13", "Hair6", "Hair1", "Hair2", "Hair4", "Hair7", "Hair5", "Hair8", "Hair9", "Hair10", "Hair14" };

        static public String NameDisallowedCharacters = "0123456789,;.:-_´¨{}][+*`^¡¿'?=)(/&¬%$·#@!|ª\\º\"'";

        public class Vector3
        {
            public float X;
            public float Y;
            public float Z;
        }

        public class Character
        {
            public string Beard = "";
            public HashSet<Biome> Biomes;
            public int Builds;
            public int Crafts;
            public int Deaths;
            public List<Food> Foods;    // digesting foods
            public string GuardianPower;
            public float GuardianPowerCooldown;
            public string Hair = "";
            public Vector3 HairColor;
            public float Hp;
            public long Id;
            public List<Item> Inventory;
            public bool IsFirstSpawn;
            public int Kills;
            public HashSet<string> KnownMaterials;
            public float MaxHp;
            public int Gender;  // 0 - male, 1 - female
            public string Name = "";
            public HashSet<string> Recipes;
            public HashSet<string> ShownTutorials;
            public HashSet<Skill> Skills;
            public Vector3 SkinColor;
            public float Stamina;
            public string StartSeed = "";
            public Dictionary<string, int> Stations = new Dictionary<string, int>();
            public Dictionary<string, string> Texts = new Dictionary<string, string>();
            public float TimeSinceDeath;
            public HashSet<string> Trophies;
            public HashSet<string> Uniques;
            public Dictionary<long, World> WorldsData = new Dictionary<long, World>();
            public int DataVersion = 0;
            public int SkillsVersion;
            public int InventoryVersion;
            public int CharacterVersion;

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
