using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ProjectZero.Model.Collision
{
    class BoundingBox
    {
        public Vector3 min, max;
        public Plane front, back, left, right,top,bot; //Front = +Z axis, Back= -Z axis

        public BoundingBox(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;

            front = new Plane(new Vector3(min.x, min.y, max.z), Vector3.forward); 
            back = new Plane(new Vector3(min.x, min.y, min.z), Vector3.back);

            left = new Plane(new Vector3(min.x, min.y, max.z), Vector3.left);
            right = new Plane(new Vector3(max.x, min.y, max.z), Vector3.right);

            top = new Plane(new Vector3(min.x, max.y, max.z), Vector3.up);
            bot = new Plane(new Vector3(max.x, min.y, max.z), Vector3.down);
        }

        public void Render(Color color)
        {
            Vector3 fBL, fBR, fTL, fTR, bBL, bBR, bTL, bTR;
            fBL = new Vector3(min.x, min.y, max.z);
            fBR = new Vector3(max.x, min.y, max.z);
            fTL = new Vector3(min.x, max.y, max.z);
            fTR = new Vector3(max.x, max.y, max.z);

            bBL = new Vector3(min.x, min.y, min.z);
            bBR = new Vector3(max.x, min.y, min.z);
            bTL = new Vector3(min.x, max.y, min.z);
            bTR = new Vector3(max.x, max.y, min.z);
          
            //Front
            Debug.DrawLine(fBL, fBR, color);
            Debug.DrawLine(fBL, fTL, color);
            Debug.DrawLine(fBR, fTR, color);
            Debug.DrawLine(fTL, fTR, color);
            //Back 
            Debug.DrawLine(bBL, bBR, color);
            Debug.DrawLine(bBL, bTL, color);
            Debug.DrawLine(bBR, bTR, color);
            Debug.DrawLine(bTL, bTR, color);

            //Bottom 
            Debug.DrawLine(fBL, bBL, color);
            Debug.DrawLine(fBR, bBR, color);
            //Top
            Debug.DrawLine(fTR, bTR, color);
            Debug.DrawLine(fTL, bTL, color);
        }
    }
}
