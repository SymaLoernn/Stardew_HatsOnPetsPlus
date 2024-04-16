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
        IModHelper helper;
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.helper = helper;
            helper.Events.Content.AssetRequested += this.OnAssetRequested;

            helper.Events.GameLoop.SaveLoaded += this.OnSaveLoaded;

            var harmony = new Harmony(this.ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.Pet), nameof(StardewValley.Characters.Pet.drawHat)),
                prefix: new HarmonyMethod(typeof(PetHatsPatch), nameof(PetHatsPatch.DrawHatPrefix)));
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.Pet), nameof(StardewValley.Characters.Pet.checkAction)),
                postfix: new HarmonyMethod(typeof(PetHatsPatch), nameof(PetHatsPatch.CheckActionPostfix)));
        }

        private void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
        {
            HOPPHelperFunctions.Initialize(this.Monitor, helper);
        }

        private void OnAssetRequested(object sender, AssetRequestedEventArgs e)
        {
            HOPPHelperFunctions.Content_AssetRequested(e);
        }
    }
}