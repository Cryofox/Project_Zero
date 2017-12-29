using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssemblyCSharp;
using Terrain = AssemblyCSharp.Terrain;


public class Game : MonoBehaviour {
	
	 Terrain terrain = null;


	// Use this for initialization
	void Start () {
		SimplexNoiseGenerator noiseGen = new SimplexNoiseGenerator ();
		terrain = new Terrain ();
		terrain.Initialize ();

	}

	// Update is called once per frame
	void Update () {
		terrain.DebugRender ();
	}

	public void ForceUpdate()
	{
		if (terrain == null) {
			terrain = new Terrain ();
		}
		terrain.Initialize ();
	}


}
