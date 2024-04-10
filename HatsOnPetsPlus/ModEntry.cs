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
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            PetHatsPatch.Initialize(this.Monitor);
            var harmony = new Harmony(this.ModManifest.UniqueID);
            harmony.Patch(
                original: AccessTools.Method(typeof(StardewValley.Characters.Pet), nameof(StardewValley.Characters.Pet.drawHat)),
                prefix: new HarmonyMethod(typeof(PetHatsPatch), nameof(PetHatsPatch.DrawHatPrefix)));
        }
    }
}