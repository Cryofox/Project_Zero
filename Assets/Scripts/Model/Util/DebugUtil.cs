using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace ProjectZero.Model.Util
{
    class DebugUtil
    {
        public static void DebugDrawCircle(Vector3 position, float radius)
        {
            float ThetaScale = 0.01f;
            int size = (int)((1f / ThetaScale) + 1f);
            float Theta = 0f;

            Vector3? lastPosition = null, newPosition = null;
            for (int i = 0; i < size; i++)
            {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = radius * Mathf.Cos(Theta);
                float y = radius * Mathf.Sin(Theta);

                lastPosition = newPosition;
                //LineDrawer.SetPosition(i, new Vector3(x, y, 0));
                newPosition = new Vector3(x, 0, y);
                if (lastPosition != null && newPosition != null)
                {
                    Debug.DrawLine((Vector3)lastPosition + position, (Vector3)newPosition + position);
                }
            }
        }
    }
}
