using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace ProjectZero.Model.Collision
{
    //A Y-Up Bounding Rect
    struct BoundingRectangle
    {
        public Vector2 min;
        public Vector2 max;
        public BoundingRectangle(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
