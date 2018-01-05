using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectZero.Assets.Scripts.Model.World
{
    class Chunk
    {
        readonly TilePoint[][] tilePointMap;
        int startX, startY, size;
        public Chunk(int startX, int startY, int size, ref TilePoint[][] tilePointMap)
        {
            this.tilePointMap = tilePointMap;
            this.startX = startX;
            this.startY = startY;
            this.size = size;
        }

    }
}
