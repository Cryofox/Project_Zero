using System;

namespace AssemblyCSharp
{
	public class Terrain
	{
		private int size = 256;
		TerrainLayer[] terrainLayers;

		public Terrain ()
		{}

		public void Initialize(int size = 256)
		{
			terrainLayers = new TerrainLayer[2];
			terrainLayers [1] = new OverGround (size);
			terrainLayers [0] = new UnderGround (size);
		}
	}
}

