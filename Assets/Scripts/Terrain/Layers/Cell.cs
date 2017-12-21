using System;

namespace AssemblyCSharp
{
	public class Cell{
		/*
			CellTypes:
			//The Floor of The Cell			
				OverGround:
					Grass, Rock, Sand, 
				UnderGround:
					Rock

			//For Water: Water is a Body Mesh(not a cellType).
				Layers are NOT flat, they are bumpy and have different Height depths.				
					This is how water Volumes can grow and become a threat if not dealt with
		*/
		 
		float centroidHeight=0;
		public Cell ()
		{
		}


		public void setHeight(float height)
		{
			this.centroidHeight = height;
		}

		public float getHeight()
		{
			return this.centroidHeight;
		}



	}
}

