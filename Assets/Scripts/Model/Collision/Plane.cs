using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace ProjectZero.Model.Collision
{
    struct Plane
    {
        public Vector3 normal;
        public Vector3 position;
        public Plane(Vector3 a, Vector3 b, Vector3 c)
        {
            position = a;
            Vector3 lineA = a - b;
            Vector3 lineB = a - c;

            //Cross product = Perpendicular
            normal =Vector3.Cross(lineA, lineB).normalized;
        }
        public Plane(Vector3 position, Vector3 direction)
        {
            normal = direction.normalized;
            this.position = position;
        }
    }
}
