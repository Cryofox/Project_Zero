using System;

namespace AssemblyCSharp
{
	public class TerrainLayer
	{
		int planeElevation = 0;
		Cell[][] cells;


		public TerrainLayer (int size, int planeElevation)
		{
			this.planeElevation = planeElevation;
			//ToDo: Use References/Pointers to instantiate 1 "Grass" Tile, and point all cells in array to Tile.
			cells = new Cell[size][];
			for (int x = 0; x < size; ++x) {
				cells [x] = new Cell[256];

				for (int y = 0; y < size; ++y) {
					cells [x] [y] = new Cell ();
				}
			}
		}


		public void DebugRender()
		{
			

		}
	}
}

