using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectZero.Assets.Scripts.Model.World
{
    enum TileType {
        Grass,
        Dirt,
        Rock
    }
    struct TilePoint
    {
        public float height;
        public TileType type;
        /*public Tile(int height=0, TileType type = TileType.Grass)
        {
             this.height = height;
             this.type = type;
        }*/
    }
}
