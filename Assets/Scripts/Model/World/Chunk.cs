using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ProjectZero.Assets.Scripts.Model.World
{
    class Chunk
    {
        static Transform terrainTransform;

        readonly TilePoint[][] tilePointMap;
        int startX, startY, size;
        public Chunk(int startX, int startY, int size, ref TilePoint[][] tilePointMap)
        {
            this.tilePointMap = tilePointMap;
            this.startX = startX;
            this.startY = startY;
            this.size = size;

            triangles = new List<int>();
            vertices = new List<Vector3>();
           
            //Set this Value if it's empty
            if (terrainTransform == null)
            {
                terrainTransform= GameObject.Find("World/Terrain").transform;
            }
        }

        //Mesh Attributes
        List<int> triangles;
        List<Vector3> vertices;
        List<Vector2> uvs;
        Mesh mesh;
        Material terrainMaterial;

        //Unity Specific
        GameObject prefab; //What's rendered in the Engine
        MeshFilter meshFilter;
        MeshRenderer meshRenderer;



        public void GenerateChunk()
        {
            if (prefab == null)
            {
                CreatePrefab();
            }
            //Clear List
            vertices.Clear();
            triangles.Clear();

            //Create Vertex + Triangle List
            Vector3 p1, p2, p3, p4;
            for (int x = startX, i = 0; i < size; x++, i++)
            {
                for (int y = startY, j = 0; j < size; y++, j++)
                {
                    //Clockwise winding order

                    //A=BL, B=TL, C=TR, D=BR
                    p1 = new Vector3(x*Terrain.tileSize, tilePointMap[x][y].height, y*Terrain.tileSize);
                    p2 = new Vector3(x * Terrain.tileSize, tilePointMap[x][y+1].height, (y+1) * Terrain.tileSize);
                    p3 = new Vector3((x + 1) * Terrain.tileSize, tilePointMap[x+1][y+1].height, (y + 1) * Terrain.tileSize);
                    p4 = new Vector3((x + 1) * Terrain.tileSize, tilePointMap[x + 1][y].height, y * Terrain.tileSize);

                    AddQuad(p1, p2, p3, p4);
                }
            }
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
        }


        void CreatePrefab()
        {
            prefab = new GameObject();
            prefab.name = "Chunk["+startX+"]["+startY+"]";
            //Unity Specific one time use.
            prefab.transform.parent = terrainTransform; 
            meshRenderer = prefab.AddComponent<MeshRenderer>();
            meshFilter = prefab.AddComponent<MeshFilter>();
            mesh = new Mesh();

            meshFilter.mesh = mesh;
        }



        void AddTriangle(Vector3 a, Vector3 b, Vector3 c)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);
        }
        void AddQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);
            vertices.Add(d);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);
        }

        private void AddPentagon(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Vector3 e)
        {
            int vertexIndex = vertices.Count;
            vertices.Add(a);
            vertices.Add(b);
            vertices.Add(c);
            vertices.Add(d);
            vertices.Add(e);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 1);
            triangles.Add(vertexIndex + 2);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 2);
            triangles.Add(vertexIndex + 3);

            triangles.Add(vertexIndex);
            triangles.Add(vertexIndex + 3);
            triangles.Add(vertexIndex + 4);
        }
    }
}
