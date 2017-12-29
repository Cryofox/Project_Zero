using UnityEngine;
using UnityEditor;

// Editor Script that multiplies the scale of the current selected GameObject
using AssemblyCSharp;
using Terrain = AssemblyCSharp.Terrain;

[ExecuteInEditMode]
public class TerrainTester : EditorWindow
{
	
	  int octaves = 2;
	  int multiplier = 40;
	  float amplitude = 5f;
	  float lacunarity = 2;
	  float persistence = 0.9f;

	Game game;
	Terrain terrain;
    Texture2D rawHeightMap;

    [MenuItem("QuickTestTools/Noise Manipulator")]
    static void Init()
    {
		var window = (TerrainTester)GetWindow(typeof(TerrainTester));
        window.Show();
    }



    void OnGUI()
    {
		octaves = EditorGUILayout.IntField("octaves:", octaves);
		multiplier = EditorGUILayout.IntField("multiplier:", multiplier);
		amplitude = EditorGUILayout.FloatField("amplitude:", amplitude);  
		lacunarity = EditorGUILayout.FloatField("lacunarity:", lacunarity);
		persistence = EditorGUILayout.FloatField("persistence:", persistence);
        /*
        EditorGUILayout.ObjectField("Raw HeightMap", rawHeightMap, typeof(Texture2D), false);

        
        EditorGUILayout.LabelField("OverGround", EditorStyles.boldLabel);
        EditorGUILayout.PrefixLabel("Source Image");
        EditorGUILayout.BeginHorizontal();

        rawHeightMap= (Texture2D) EditorGUILayout.ObjectField("UnderGround",rawHeightMap, typeof(Texture2D), false);
        EditorGUILayout.ObjectField("Unit Positioning:", rawHeightMap, typeof(Texture2D), false);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("UnderGround", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField("HeightMap:", rawHeightMap, typeof(Texture2D), false);
        EditorGUILayout.ObjectField("Unit Positioning:", rawHeightMap, typeof(Texture2D), false);
        EditorGUILayout.EndHorizontal();
        
        //EditorGUILayout.ObjectField (rawHeightMap);
        GUILayout.Label (rawHeightMap);
        */

        if (GUILayout.Button("Regenerate Terrain"))
        {
			SimplexNoiseGenerator.octaves = octaves;
			SimplexNoiseGenerator.multiplier=multiplier;
			SimplexNoiseGenerator.amplitude=amplitude;
			SimplexNoiseGenerator.lacunarity=lacunarity;
			SimplexNoiseGenerator.persistence=persistence;

			Debug.Log ("Octaves:" + SimplexNoiseGenerator.octaves);
			Debug.Log ("multiplier:" + SimplexNoiseGenerator.multiplier);
			Debug.Log ("amplitude:" + SimplexNoiseGenerator.amplitude);
			Debug.Log ("lacunarity:" + SimplexNoiseGenerator.lacunarity);
			Debug.Log ("persistence:" + SimplexNoiseGenerator.persistence);

			Debug.Log ("Regenerating Terrain");
			terrain.Initialize ();
			terrain.DebugRender ();
			/*SceneView.RepaintAll();*/
        }
    }
		

	// Use this for initialization
	void OnEnable () {
		//game = GameObject.Find ("Game").GetComponent<Game> ();
		terrain = new Terrain();
	}

	// Update is called once per frame
	/*void Update () {
		//if (terrain != null) {
		//terrain.DebugRender ();
		//}
		//SceneView.RepaintAll();
		Debug.Log("Editor Update");
		SceneView.RepaintAll();
		Repaint ();
		UnityEditorInternal.InternalEditorUtility.RepaintAllViews ();
	}*/

}