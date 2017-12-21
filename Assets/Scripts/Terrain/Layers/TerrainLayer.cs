using System;
using UnityEngine; //Vector

namespace AssemblyCSharp
{
	public class TerrainLayer
	{
		int planeElevation = 0;

		Cell[][] cells;
		const int cellSize    = 10; //Divisible by 2!
		const int halfCellSize= 5;
		int mapSize;


		public TerrainLayer (int size, int planeElevation)
		{
			float blue= 0, cyan= 0, yellow= 0, green= 0, white = 0;

			this.mapSize = size;
			SimplexNoiseGenerator noiseGen = new SimplexNoiseGenerator ();

			this.planeElevation = planeElevation;
			float height;
			//ToDo: Use References/Pointers to instantiate 1 "Grass" Tile, and point all cells in array to Tile.
			cells = new Cell[size][];
			for (int x = 0; x < size; ++x) {
				cells [x] = new Cell[256];
				for (int y = 0; y < size; ++y) {
					//Todo: Augment Cell Height
					cells [x] [y] = new Cell ();

					int octaves = 2;
					int multiplier = 40;
					float amplitude = 5f;
					float lacunarity = 2;
					float persistence = 0.9f;
					height =  noiseGen.coherentNoise (x, planeElevation, y,octaves,multiplier,amplitude,lacunarity,persistence);
					height *= 10;

					if (height < 2) {
						blue++;
					} else if (height < 4) {
						cyan++;
					} else if (height < 6) {
						yellow++;
					} else if (height < 8) {
						green++;
					} else
						white++;

					//Debug.Log ("Noise Height:" + height);
					cells [x] [y].setHeight (height);
				}
			}
			Debug.Log(""+blue+" Blue Tiles = "+ (blue/ (size*size)) *100+"%");
			Debug.Log(""+cyan+" cyan Tiles = "+ (cyan/ (size*size)) *100+"%");
			Debug.Log(""+yellow+" yellow Tiles = "+ (yellow/ (size*size))*100+"%");
			Debug.Log(""+green+" green Tiles = "+ (green/ (size*size))*100+"%");
			Debug.Log(""+white+" white Tiles = "+ (white/ (size*size))*100+"%");
		}






		public void DebugRender()
		{
			
			Vector3 a, b, c, d;
			a.y = b.y = c.y = d.y = planeElevation;

			for (int x = 0; x < mapSize; ++x) {
				for (int y = 0; y < mapSize; ++y) {
					Vector3 worldCoordinates = CellToWorld (x, y);

					a.x = c.x = worldCoordinates.x - halfCellSize;
					b.x = d.x = worldCoordinates.x + halfCellSize;

					a.z = b.z =  worldCoordinates.z + halfCellSize;
					c.z = d.z =  worldCoordinates.z - halfCellSize;


					a.y = b.y = c.y = d.y = worldCoordinates.y;

					Color color = Color.white;
					float cellHeight = cells [x] [y].getHeight ();
					if (cellHeight < 2) {
						color = Color.blue;
					} else if (cellHeight < 4) {
						color = Color.cyan;
					} else if (cellHeight < 6) {
						color = Color.yellow;
					} else if (cellHeight < 8) {
						color = Color.green;
					} else
						color = Color.white;



					DebugUtil.DrawLine (a, b,color);
					DebugUtil.DrawLine (c, d,color);
					DebugUtil.DrawLine (a, c,color);
					DebugUtil.DrawLine (b, d,color);
				}

			}
		}



		Vector3 CellToWorld(Vector2 coord)
		{
			return CellToWorld((int)coord.x,(int)coord.y);
		}

		Vector3 CellToWorld(int x, int y)
		{
			Vector3 worldPosition = new Vector3 ();
			worldPosition.x = x * cellSize + halfCellSize;
			worldPosition.z = y * cellSize + halfCellSize;
			worldPosition.y = cells [x] [y].getHeight() + planeElevation;
			
			return worldPosition;
		}



	}
}

