using UnityEngine;


    /// <summary>
    /// Abstraction Wrapper for Unit's Vector3 class. This is temporary until the Functionality of this class is replaced entirely with functionality that
    /// mimics Unit's behaviour (Needed for porting outside of Unity)
    /// </summary>
    struct Vector3D
    {
        Vector3 unityVector;
        Vector3D(int x, int y, int z)
        {
            unityVector = new Vector3(x, y, z);
        }
        Vector3D(float x, float y, float z)
        {
            unityVector = new Vector3(x, y, z);
        }

        public float x { get { return unityVector.x; } set { unityVector.x = value; } }
        public float y { get { return unityVector.y; } set { unityVector.y = value; } }
        public float z { get { return unityVector.z; } set { unityVector.z = value; } }
       
         
    }

