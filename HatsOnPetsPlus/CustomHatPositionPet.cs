using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatsOnPetsPlus
{
    internal class CustomHatPositionPet
    {
        // The key is construted as a Tuple<int, bool> :
        // The first part (int) is the sprite ID (0 for the top left sprite on the sprite sheet, then its numbered left to right and top to bottom)
        // The second part of the key (bool) is the "flipped" value of the sprite : true is flipped, false isn't 
        internal Dictionary<Tuple<int, bool>, CustomHatPositionSprite> sprites = new Dictionary<Tuple<int, bool>, CustomHatPositionSprite>();
    }
}
