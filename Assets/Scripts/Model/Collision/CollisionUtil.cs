using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ProjectZero.Model.World;
using UnityEngine;

using Terrain = ProjectZero.Model.World.Terrain;
using Plane = ProjectZero.Model.Collision.Plane;
namespace ProjectZero.Model.Collision
{
    //This util is used to determine whether 2 objects collide or not
    //Can be also used to determine collision point
    class CollisionUtil
    {


        public static bool Collides(Vector2 colliderPosition, float range, Vector2 objectPosition)
        {
            float distance = Vector2.Distance(colliderPosition, objectPosition);
            return distance <= range;
        }
        public static bool Collides(Vector3 position, float radius, BoundingBox boundingBox)
        {
            return false;
        }




        //This should not be called once the Spatial Partitioner is in place

            /// <summary>
            /// Null if no Point found
            /// </summary>
            /// <param name="ray"></param>
            /// <param name="boundingBox"></param>
            /// <returns></returns>
        public static Vector3? CollisionPoint(Ray ray, BoundingBox boundingBox)
        {
            Vector3 position;

            position = CollisionPoint(ray, boundingBox.top);
            if ((position.x >= boundingBox.min.x && position.x <= boundingBox.max.x) &&
               (position.z >= boundingBox.min.z && position.z <= boundingBox.max.z))
                return position;
            position = CollisionPoint(ray, boundingBox.bot);
            if ((position.x >= boundingBox.min.x && position.x <= boundingBox.max.x) &&
                  (position.z >= boundingBox.min.z && position.z <= boundingBox.max.z))
                return position;
            position = CollisionPoint(ray, boundingBox.front);
            if ((position.x >= boundingBox.min.x && position.x <= boundingBox.max.x) &&
              (position.y>= boundingBox.min.y&& position.y <= boundingBox.max.y))
                return position;
            position = CollisionPoint(ray, boundingBox.back);
            if ((position.x >= boundingBox.min.x && position.x <= boundingBox.max.x) &&
              (position.y >= boundingBox.min.y && position.y <= boundingBox.max.y))
                return position;
            position = CollisionPoint(ray, boundingBox.left);
            if (
               (position.y >= boundingBox.min.y && position.y <= boundingBox.max.y) &&
               (position.z >= boundingBox.min.z && position.z <= boundingBox.max.z))
                return position;
            position = CollisionPoint(ray, boundingBox.right);
            if (
                  (position.y >= boundingBox.min.y && position.y <= boundingBox.max.y) &&
                  (position.z >= boundingBox.min.z && position.z <= boundingBox.max.z))
                return position;
            return null;
        }

        //For reference
        //https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-plane-and-ray-disk-intersection
        public static Vector3 CollisionPoint(Ray ray, Plane plane)
        {
            ray.direction.Normalize();

            Vector3 collisionPoint;

            float distance = Vector3.Dot((plane.position - ray.origin), plane.normal)/ Vector3.Dot(ray.direction, plane.normal);

            collisionPoint = ray.origin + ray.direction * distance;
            return collisionPoint;               
        }



    }
}
