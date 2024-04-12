using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HatsOnPetsPlus
{
    internal class PetHatsPatch
    {

        private static IMonitor Monitor;
        public static Dictionary<Tuple<string, string>, CustomHatPositionPet> customPetsDict = new Dictionary<Tuple<string, string>, CustomHatPositionPet>();

        internal static void Initialize(IMonitor monitor)
        {
            Monitor = monitor;
        }

        public static void addPetToDictionnary(string petType, string petBreed, CustomHatPositionPet pet)
        {
            customPetsDict[new Tuple<string, string>(petType, petBreed)] = pet;
        }

        internal static bool DrawHatPrefix(SpriteBatch b, Vector2 shake, StardewValley.Characters.Pet __instance)
        {
            try
            {
                Monitor.Log("Entered draw hat prefix function", LogLevel.Debug);

                if (__instance.hat.Value == null)
                {
                    // This could return false since the original codes will also return without doing anything in this case, but I think it's best to let the vanilla code run
                    // in case something changes in the vanilla game in the future
                    return true;
                }

                // Check if the pet has custom hat data, if not default to vanilla logic

                Tuple<string, string> petTypeAndBreed = new Tuple<string, string>(__instance.petType, __instance.whichBreed);
                CustomHatPositionPet customPet;
                if (!customPetsDict.TryGetValue(petTypeAndBreed, out customPet))
                {
                    Monitor.Log("No modded data found for this pet ["+ petTypeAndBreed +"], defaulting to vanilla logic", LogLevel.Debug);
                    return true;
                }

                // Check if the specific sprite has custom data, if not default to vanilla logic  <-- Debatable if it should work like that

                bool flipped = __instance.flip || (__instance.sprite.Value.CurrentAnimation != null && __instance.sprite.Value.CurrentAnimation[__instance.sprite.Value.currentAnimationIndex].flip);
                CustomHatPositionSprite customHatData;
                if (!
                    customPet.sprites.TryGetValue(new Tuple<int, bool>(__instance.Sprite.currentFrame, flipped), out customHatData)
                   )
                {
                    Monitor.Log("No modded data found for this sprite on this otherwise custom pet, defaulting to vanilla logic", LogLevel.Debug);
                    return true;
                }

                Monitor.Log("Modded data found for this pet and sprite, using custom logic", LogLevel.Debug);

                Vector2 hatOffset = Vector2.Zero;
                hatOffset *= 4f;
                if (hatOffset.X <= -100f)
                {
                    return true;
                }
                float horse_draw_layer = Math.Max(0f, __instance.isSleepingOnFarmerBed.Value ? (((float)__instance.StandingPixel.Y + 112f) / 10000f) : ((float)__instance.StandingPixel.Y / 10000f));
                hatOffset.X = -2f;
                hatOffset.Y = -24f;
                horse_draw_layer += 1E-07f;
                int direction = 2;
                // flipped boolean is initialized earlier
                float scale = 1.3333334f;

                // Adjust hat placement, direction and scale with custom data when provided
                hatOffset.X += customHatData.hatOffsetX.HasValue ? customHatData.hatOffsetX.Value : 0;
                hatOffset.Y += customHatData.hatOffsetY.HasValue ? customHatData.hatOffsetY.Value : 0;
                direction = customHatData.direction.HasValue ? customHatData.direction.Value : direction;
                scale = customHatData.scale.HasValue ? customHatData.scale.Value : scale;

                hatOffset += shake; 

                // Not sure if the following lines from the vanilla logic should be implemented or not, needs testing
                // In my opinion, it makes it more confusing so I'll leave it commented for now
                //if (flipped)
                //{
                //    hatOffset.X -= 4f;
                //}

                __instance.hat.Value.draw(b, __instance.getLocalPosition(Game1.viewport) + hatOffset + new Vector2(30f, -42f), scale, 1f, horse_draw_layer, direction, useAnimalTexture: true);

                return false;
            }
            catch(Exception ex)
            {
                Monitor.Log($"Failed in {nameof(DrawHatPrefix)}:\n{ex}", LogLevel.Error);
                return true; // run original logic in case of exception
            }
            
        }

    }
}
