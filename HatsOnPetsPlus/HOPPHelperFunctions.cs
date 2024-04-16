using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatsOnPetsPlus
{
    internal class HOPPHelperFunctions
    {

        public class ExternalPetModData
        {
            // main vanilla types are "Dog", "Cat", "Turtle"
            public string Type { get; set; }

            // breeds are usually numbered 0 to 4, except for turtles that are 0 and 1 only
            public string BreedId { get; set; }

            public ExternalSpriteModData[] Sprites { get; set; }
        }

        public class ExternalSpriteModData
        {
            public int SpriteId { get; set; }
            public float? HatOffsetX { get; set; }
            public float? HatOffsetY { get; set; }
            public int? Direction { get; set; }
            public float? Scale { get; set; }
            public bool? Flipped { get; set; }
            public bool? Default { get; set; }
            public bool? DoNotDrawHat { get; set; }
        }

        private static IMonitor Monitor;
        private static IModHelper Helper;

        internal static void Initialize(IMonitor monitor, IModHelper helper)
        {
            Monitor = monitor;
            Helper = helper;

            PetHatsPatch.Initialize(Monitor);
            LoadCustomPetMods();
        }

        internal static void LoadCustomPetMods()
        {
            var dict = Helper.GameContent.Load<Dictionary<string, ExternalPetModData[]>>(ModEntry.modContentPath);
            Monitor.Log("HOPP Init : "+ dict.Count  +" mod(s) found", LogLevel.Trace);
            foreach (KeyValuePair<string, ExternalPetModData[]> entry in dict)
            {
                var moddedPets = entry.Value as ExternalPetModData[];
                Monitor.Log("HOPP Init : Mod " + entry.Key + " loading, " + moddedPets.Length + " modded pets found", LogLevel.Trace);
                foreach (ExternalPetModData moddedPet in moddedPets)
                {
                    PetHatsPatch.addPetToDictionnary(moddedPet);
                }
            }
        }

        internal static void Content_AssetRequested(AssetRequestedEventArgs e)
        {
            if (e.NameWithoutLocale.IsEquivalentTo(ModEntry.modContentPath))
            {
                e.LoadFrom(() => new Dictionary<string, ExternalPetModData[]>(), AssetLoadPriority.Exclusive);
            }
        }
    }
}
