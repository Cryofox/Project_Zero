using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using ProjectZero.Model.Util;
using ProjectZero.Model.Collision;

namespace ProjectZero.Model.World
{
    class Terrain
    {
        public readonly static float tileSize = 2;
        int size; //Multiple of 60

       


        public Terrain(int size)
        {
            this.size = size;
            Vector2 min = new Vector2(0, 0);
            Vector2 max = new Vector2(size, size) * tileSize;
            boundingRectangle = new BoundingRectangle(min, max);
        }

        public void Init()
        {
            System.Random rnd = new System.Random();
            //Tiles Generated
            //tiles = new Tile[size][];
            points = new TilePoint[size + 1][];
            for (int x = 0; x < size+1; x++)
            {
                points[x] = new TilePoint[size+1];
                for (int y = 0; y < size+1; y++)
                {
                    points[x][y] = new TilePoint();
                    points[x][y].height = (float)rnd.NextDouble()*2f;
                }
            }
            
        }

        //The Raw Tile Data used to Generate the Map 1x1 in size
        TilePoint[][] points; //Point contains Type + Height Information


        public void RenderGridPoints()
    {
        float third = tileSize / 10;
        Vector3 thirdX = new Vector3(third, 0, 0);
        Vector3 thirdY = new Vector3(0, 0, third);


        for (int x = 0; x < points.Length; x++)
        {
            for (int y = 0; y < points[x].Length; y++)
            {
                Vector3 tilePosition = new Vector3(x * tileSize, points[x][y].height, y * tileSize);
                /*Debug.DrawLine(tilePosition, tilePosition + thirdX);
                Debug.DrawLine(tilePosition, tilePosition - thirdX);
                Debug.DrawLine(tilePosition, tilePosition + thirdY);
                Debug.DrawLine(tilePosition, tilePosition - thirdY);*/
                DebugDrawCircle(tilePosition,third);
            }
        }
    }

        //Chunks Exist to Simplify the Rendering Process
        //Chunks offer no benefit other than batching tiles for rendering.
        Chunk[][] chunks;
        int chunkDivision = 1; //Min 1, Max = TileCount
        //Tile[][] tiles; //Tiles = Gridcells in the world
        public void InitializeChunks(int chunkDivision)
        {
            //Constrain Chunk into 1->Size Max
            chunkDivision = Math.Min(Math.Max(1, chunkDivision), size);
            this.chunkDivision = chunkDivision;
            int tilesPerChunk = size / chunkDivision;
            chunks = new Chunk[chunkDivision][];
            Chunk chunk;
            //Create Chunks
            for (int x = 0; x < chunkDivision; x++)
            {
                chunks[x] = new Chunk[chunkDivision];
                for (int y = 0; y < chunkDivision; y++)
                {
                    //Points passed as Reference so Terrain manages Points and Chunks utilize.
                    chunk = new Chunk(x * tilesPerChunk, y * tilesPerChunk, tilesPerChunk, ref points);
                    chunk.GenerateChunk();
                    chunks[x][y] = chunk;
                }
            }
        }


        void DebugDrawCircle(Vector3 offset, float radius)
        {
            float ThetaScale = 0.01f;
            int size = (int)((1f / ThetaScale) + 1f);
            float Theta = 0f;

            Vector3? lastPosition = null, newPosition = null;
            for (int i = 0; i < size; i++)
            {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = radius * Mathf.Cos(Theta);
                float y = radius * Mathf.Sin(Theta);

                lastPosition = newPosition;
                //LineDrawer.SetPosition(i, new Vector3(x, y, 0));
                newPosition = new Vector3(x, 0, y);
                if (lastPosition != null && newPosition != null)
                {
                    Debug.DrawLine((Vector3)lastPosition + offset, (Vector3)newPosition + offset);
                }
            }
        }
        public void RenderGridLines()
        {
            Vector3 p1, p2;
            //Draw Horizontal Lines
            for (int y = 0; y < points.Length; y++)
            {
                for (int x = 1; x < points.Length; x++)
                {
                    float h1 = points[x - 1][y].height;
                    float h2 = points[x][y].height;
                    float x1 = (x - 1) * tileSize;
                    float x2 = (x) * tileSize;
                    float y12 = y * tileSize;
                    p1 = new Vector3(x1, h1, y12);
                    p2 = new Vector3(x2, h2, y12);
                    Debug.DrawLine(p1, p2);
                }
            }
            //Draw Vertical Lines
            for (int x = 0; x < points.Length; x++)
            {
                for (int y = 1; y < points.Length; y++)
                {
                    float h1 = points[x][y - 1].height;
                    float h2 = points[x][y].height;
                    float y1 = (y - 1) * tileSize;
                    float y2 = (y) * tileSize;
                    float x12 = x * tileSize;
                    p1 = new Vector3(x12, h1, y1);
                    p2 = new Vector3(x12, h2, y2);
                    Debug.DrawLine(p1, p2);
                }
            }
        }

        public void RenderBoundingBoxes()
        {
            for (int x = 0; x < chunkDivision; x++)
            {
                for (int y = 0; y < chunkDivision; y++)
                {
                    chunks[x][y].boundingBox.Render(Color.blue);
                }
            }

        }

        public void ApplyHeight(Vector3 collisionPoint3D, float radius, float height)
        {
            float colPointX = collisionPoint3D.x;
            float colPointY = collisionPoint3D.z;

            //Convert to Local Grid coordinates
            colPointX =(float)( (int)colPointX / tileSize);
            colPointY =(float)( (int)colPointY / tileSize);
            radius /= tileSize;

            Vector2 collisionPoint = new Vector2(colPointX, colPointY);
            Vector2 samplePoint;
            
            //Modify all Points within Range
            for (int x = Math.Max(0,(int)(colPointX - radius)); x < Math.Min(size+1,colPointX + radius); x++)
            {
                for (int y = Math.Max(0, (int)(colPointY - radius)); y < Math.Min(size + 1, colPointY + radius); y++)
                {
                    samplePoint.x = x;
                    samplePoint.y = y;
                    if (Vector2.Distance(samplePoint, collisionPoint) <= radius)
                    {
                        points[x][y].height = height;
                    }
                }
            }
            //Chunks are
            int tilesPerChunk = size / chunkDivision;

            int startChunkIndexX = Math.Min(chunks.Length-1, Math.Max(0,((int)(colPointX - radius) / tilesPerChunk)-1));
            int startChunkIndexY = Math.Min(chunks.Length-1, Math.Max(0,((int)(colPointY - radius) / tilesPerChunk)-1));

            int endChunkIndexX = Math.Min(chunks.Length - 1, Math.Max(0, (int)(colPointX + radius) / tilesPerChunk));
            int endChunkIndexY = Math.Min(chunks.Length - 1, Math.Max(0, (int)(colPointY + radius) / tilesPerChunk));

            //Regenerate Affected Chunks
            for (int x = startChunkIndexX; x< chunks.Length; x++)
            {
                for (int y = startChunkIndexY; y < chunks.Length; y++)
                {
                    //if(CollisionUtil.Collides(collisionPoint,))
                    //if (CollisionUtil.Collides(collisionPoint3D, radius, chunks[x][y]))
                    //{
                        chunks[x][y].GenerateChunk();//Recreate this Chunk
                    //}
                }
            }
        }

        BoundingRectangle boundingRectangle;
        public Vector3? CollisionPoint(Ray ray)
        {
            Vector3? collisionPoint= null;

            for (int x = 0; x < chunkDivision; x++)
            {
                for (int y = 0; y < chunkDivision; y++)
                {
                    collisionPoint = CollisionUtil.CollisionPoint(ray, chunks[x][y].boundingBox);
                    //Debug.Log("Chunk BB:" + chunks[x][y].boundingBox.min + " max:" + chunks[x][y].boundingBox.max); 
                    if (collisionPoint != null)
                        return collisionPoint;
                }
            }
            return collisionPoint;
        }
    }
}
