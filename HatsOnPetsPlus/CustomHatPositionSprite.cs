using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatsOnPetsPlus
{
    internal class CustomHatPositionSprite
    {
        internal float? hatOffsetX = null;
        internal float? hatOffsetY = null;
        internal int? direction = null;
        internal float? scale = null;

        public CustomHatPositionSprite(float? hatOffsetX, float? hatOffsetY, int? direction, float? scale)
        {
            this.hatOffsetX = hatOffsetX;
            this.hatOffsetY = hatOffsetY;
            this.direction = direction;
            this.scale = scale;
        }
    }
}
