using System;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace HatsOnPetsPlus
{
    /// <summary>The mod entry point.</summary>
    internal sealed class ModEntry : Mod
    {
        public const string modContentPath = "Syma.HatsOnPetsPlus/CustomPetData";
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            HOPPHelperFunctions.Initialize(this.Monitor, helper);

            // Test data for Goomy
            // HOPPHelperFunctions.InitializeTestData();

            var harmony = new Harmony(this.ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.Pet), nameof(StardewValley.Characters.Pet.drawHat)),
                prefix: new HarmonyMethod(typeof(PetHatsPatch), nameof(PetHatsPatch.DrawHatPrefix)));
        }

        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            HOPPHelperFunctions.Content_AssetRequested(e);
        }
    }
}