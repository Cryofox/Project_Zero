using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Terrain = ProjectZero.Model.World.Terrain;
using ProjectZero.Model.Collision;
using ProjectZero.Model.Util;
class PlayerControllerMapEditor : PlayerController
{
    Terrain terrain;
    Camera camera;

    float height = 3;
    float radius = 5;
    enum Stencil
    {
        Square,
        Circle
    }

    public PlayerControllerMapEditor(Terrain terrain)
    {
        this.terrain = terrain;
        camera = Camera.main;
    }


    public void Update(float timeDelta)
    {
        //Ray from Camera
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction*100);


        //If Left mouse Button is pressed adjust height

        //Get Collision Point
        Vector3? collisionPoint = terrain.CollisionPoint(ray);

        //Get all Points within Stencil
        if (collisionPoint != null)
        {
            DebugUtil.DebugDrawCircle((Vector3)collisionPoint, radius);
            Debug.Log("CollisionPoint:" + collisionPoint);
            if (Input.GetMouseButton(0))
            {
                ApplyHeight(Stencil.Circle, (Vector3)collisionPoint);
            }
        }
    }


    void ApplyHeight(Stencil stencil, Vector3 collisionPoint)
    {
        if (stencil == Stencil.Circle)
        {
            terrain.ApplyHeight(collisionPoint, radius, height);
        }
                //List<TilePoint> tilePoints = GetListFrom(Collision, Circle, Radius);
        //for (int i = 0; i < tilePoints.Count; i++)
        //{
        //    tilePoints[i].height = height;
        //}

    }


}
