using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AssemblyCSharp
{
	public class DebugUtil  {

		public static void DrawLine(Vector3 a, Vector3 b)
		{
			Debug.DrawLine (a, b,Color.white);
		}
		public static void DrawLine(Vector3 a, Vector3 b, Color color)
		{
			Debug.DrawLine (a, b,color,0);
		}

		public static void DrawCircle (Vector3 center, float radius, Color? color = null, float duration = 0, bool depthTest = false)
	  	{
		  //float degRad = Mathf.PI / 180;
		       for(float theta = 0.0f; theta < (2*Mathf.PI); theta += 0.2f)
		       {
		         Vector3 ci = (new Vector3(Mathf.Cos(theta) * radius + center.x, Mathf.Sin(theta) * radius + center.y, center.z));
				 DrawLine (ci, ci + new Vector3 (0, 0.02f, 0));
		       }
	  	}
	}
}