using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AssemblyCSharp;
using Terrain = AssemblyCSharp.Terrain;

public class Game : MonoBehaviour {
	Terrain terrain;
	// Use this for initialization
	void Start () {
		terrain = new Terrain ();
		terrain.Initialize ();

	}
	
	// Update is called once per frame
	void Update () {
		terrain.DebugRender ();
	}
}
