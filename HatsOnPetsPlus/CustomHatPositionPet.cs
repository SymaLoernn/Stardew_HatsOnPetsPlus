using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatsOnPetsPlus
{
    internal class CustomHatPositionPet
    {
        internal Dictionary<int, CustomHatPositionSprite> sprites = new Dictionary<int, CustomHatPositionSprite>();
        internal Dictionary<int, CustomHatPositionSprite> flippedSprites = new Dictionary<int, CustomHatPositionSprite>();
    }
}
