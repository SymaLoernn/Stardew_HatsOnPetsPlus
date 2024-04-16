﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HatsOnPetsPlus
{
    internal class SpriteData
    {
        internal float? hatOffsetX = null;
        internal float? hatOffsetY = null;
        internal int? direction = null;
        internal float? scale = null;
        internal bool? doNotDraw = null;

        public SpriteData(float? hatOffsetX, float? hatOffsetY, int? direction, float? scale, bool? doNotDraw)
        {
            this.hatOffsetX = hatOffsetX;
            this.hatOffsetY = hatOffsetY;
            this.direction = direction;
            this.scale = scale;
            this.doNotDraw = doNotDraw;
        }
    }
}
