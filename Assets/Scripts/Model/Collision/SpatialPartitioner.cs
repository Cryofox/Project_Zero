using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectZero.Assets.Scripts.Model.Collision
{
    //This Class is to be used in conjunction with the Collision util for narrowing down the
    //comparisons between objects.

     /*
      * Camera Cast:
      *       Compare Objects -> Ray
      *       Compare Terrain -> Ray
      */ 
    class SpatialPartitioner
    {

        private static SpatialPartitioner instance;
        public static SpatialPartitioner Instance
        {
            get
            {
                if (instance == null)
                { instance = new SpatialPartitioner(); }
                return instance;
            }
        }
        private SpatialPartitioner()
        {

        }


        public void Clear() { }

        //Bounding Box size from 0,0,0
        public void Initialize(int size)
        {

        }
    }
}
