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
                if (!(
                    (!flipped) ? 
                    (customPet.sprites.TryGetValue(__instance.Sprite.currentFrame, out customHatData)) : 
                    (customPet.flippedSprites.TryGetValue(__instance.Sprite.currentFrame, out customHatData))
                    )
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

        internal static void InitializeTestData()
        {
            String testData = @"0;0;20;2;1.4;
1;0;24;2;1.4;
2;0;20;2;1.4;
3;0;24;2;1.4;
4;4;20;1;1.4;
5;8;20;1;1.4;
6;4;20;1;1.4;
7;8;20;1;1.4;
8;0;20;0;1.4;
9;0;24;0;1.4;
10;0;20;0;1.4;
11;0;24;0;1.4;
12;-8;20;3;1.4;
13;-4;20;3;1.4;
14;-8;20;3;1.4;
15;-4;20;3;1.4;
16;0;20;2;1.4;
17;0;20;-1;1.4;
18;0;20;-1;1.4;
19;0;28;-1;1.4;
20;0;28;-1;1.4;
21;0;25;-1;1.4;
22;0;22;-1;1.4;
23;0;25;-1;1.4;
24;6;24;1;1.4;
25;8;26;1;1.4;
26;10;29;1;1.4;
27;10;30;1;1.4;
28;-4;29;2;1.4;
29;-4;29;2;1.4;
30;17;24;1;1.4;
31;17;24;1;1.4;
24;-6;24;3;1.4;f
25;-8;26;3;1.4;f
26;-10;29;3;1.4;f
27;-10;30;3;1.4;f
28;-4;29;2;1.4;f
29;-4;29;2;1.4;f
30;-17;24;3;1.4;f
31;-17;24;3;1.4;f";
            using (StringReader reader = new StringReader(testData))
            {
                string line;
                int spriteId = -1;
                float? hatOffsetX = null;
                float? hatOffsetY = null;
                int? direction = null;
                float? scale = null;
                bool flipped = false;
                CustomHatPositionPet testPet = new CustomHatPositionPet();
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(';');
                    // First is sprite ID
                    spriteId = int.Parse(data[0]);
                    // Second is X
                    hatOffsetX = float.Parse(data[1], CultureInfo.InvariantCulture.NumberFormat);
                    // Third is Y
                    hatOffsetY = float.Parse(data[2], CultureInfo.InvariantCulture.NumberFormat);
                    // Fourth is direction (-1 means null)
                    direction = int.Parse(data[3]);
                    direction = ((direction == -1) ? null : direction);
                    // Fifth is scale (0 means null)
                    scale = float.Parse(data[4], CultureInfo.InvariantCulture.NumberFormat);
                    scale = ((scale == 0) ? null : scale);
                    // Sixth is either nothing or f for flipped
                    flipped = (data.Length > 5 && data[5].Equals("f"));
                    
                    CustomHatPositionSprite sprite = new CustomHatPositionSprite(hatOffsetX, hatOffsetY, direction, scale);
                    if (!flipped )
                    {
                        testPet.sprites[spriteId] = sprite;
                    }
                    else
                    {
                        testPet.flippedSprites[spriteId] = sprite;
                    }
                }
                addPetToDictionnary("Cat", "3", testPet);
            }
        }
    }
}
