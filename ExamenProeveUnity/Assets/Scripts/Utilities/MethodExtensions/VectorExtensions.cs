using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class VectorExtensions
    {
        /// <summary>
        /// gets closest point from given list of positions
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>the closest point</returns>
        public static Vector3 GetClosest(this Vector3 vector3, IEnumerable<Vector3> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector3.Distance(trans, vector3)).First();
        }

        /// <summary>
        /// gets furthest point from given list of positions
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>the furthest point</returns>
        public static Vector3 GetFurthest(this Vector3 vector3, IEnumerable<Vector3> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector3.Distance(trans, vector3)).Last();
        }


        /// <summary>
        /// gets closest point from given list of positions
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>the closest point</returns>
        public static Vector2 GetClosest(this Vector2 vector2, IEnumerable<Vector2> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector2.Distance(trans, vector2)).First();
        }

        /// <summary>
        /// gets furthest point from given list of positions
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>the furthest point</returns>
        public static Vector2 GetFurthest(this Vector2 vector3, IEnumerable<Vector3> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector2.Distance(trans, vector3)).Last();
        }

        /// <summary>
        /// Get the list of positions ordered by closest to furthest  
        /// </summary>
        /// <param name="vector3"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>list of ordered position by distance</returns>
        public static Vector3[] GetPositionsByDistance(this Vector3 vector3, IEnumerable<Vector3> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector3.Distance(trans, vector3)).ToArray();
        }

        /// <summary>
        /// Get the list of positions ordered by closest to furthest  
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="otherPositions">list of other positions</param>
        /// <returns>list of ordered position by distance</returns>
        public static Vector2[] GetPositionsByDistance(this Vector2 vector2, IEnumerable<Vector2> otherPositions)
        {
            return otherPositions.OrderBy(trans => Vector2.Distance(trans, vector2)).ToArray();
        }

        /// <summary>
        /// checks if the vector is 0 
        /// </summary>
        /// <param name="dir">the vector</param>
        /// <returns>return true or false</returns>
        public static bool IsLengthZero(this Vector3 dir)
        {
            return dir.sqrMagnitude == 0;
        }

        /// <summary>
        /// checks if the vector is 0 
        /// </summary>
        /// <param name="dir">the vector</param>
        /// <returns>return true or false</returns>
        public static bool IsLengthZero(this Vector2 dir)
        {
            return dir.sqrMagnitude == 0;
        }

        /// <summary>
        /// Check if the vector is pointing in the same direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsSameDirectionAs(this Vector3 dir, Vector3 otherVector, float treshold = 0.1f)
        {
            return Vector3.Dot(dir.normalized, otherVector.normalized) > 1 - treshold;
        }


        /// <summary>
        /// Check if the vector is pointing in the same direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsSameDirectionAs(this Vector2 dir, Vector3 otherVector, float treshold = 0.1f)
        {
            return Vector3.Dot(dir.normalized, otherVector.normalized) > 1 - treshold;
        }

        /// <summary>
        /// Check if the vector is pointing in the opposite direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsOppositeDirectionAs(this Vector3 dir, Vector3 otherVector, float treshold = 0.1f)
        {
            return Vector3.Dot(dir.normalized, otherVector.normalized) < -1 + treshold;
        }

        /// <summary>
        /// Check if the vector is pointing in the opposite direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsOppositeDirectionAs(this Vector2 dir, Vector2 otherVector, float treshold = 0.1f)
        {
            return Vector3.Dot(dir.normalized, otherVector.normalized) < -1 + treshold;
        }

        /// <summary>
        /// Check if the vector is pointing in the same OR opposite direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsParallelDirectionAs(this Vector3 dir, Vector3 otherVector, float treshold = 0.1f)
        {
            return Mathf.Abs(Vector3.Dot(dir.normalized, otherVector.normalized)) > 1 - treshold;
        }

        /// <summary>
        /// Check if the vector is pointing in the same OR opposite direction as otherVector
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="otherVector"></param>
        /// <param name="treshold">The treshold to allow a margin</param>
        /// <returns></returns>
        public static bool IsParallelDirectionAs(this Vector2 dir, Vector2 otherVector, float treshold = 0.1f)
        {
            return Mathf.Abs(Vector3.Dot(dir.normalized, otherVector.normalized)) > 1 - treshold;
        }

        /// <summary>
        /// Checks if the direction.sqrMagnitude equals to 0
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static bool IsDirectingTowards(this Vector2 dir)
        {
            return dir.sqrMagnitude == 0;
        }

        /// <summary>
        /// The OffSetToDirectionXY method calculates a midpoint between two Vector2 values,
        /// calculates the direction from this midpoint to a third Vector2 value,
        /// offsets the third Vector2 value in the opposite direction,
        /// and returns a tuple of the offset Vector2 and the midpoint Vector2.
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static (Vector2, Vector2) OffSetToDirectionXY(this Vector2 vec, Vector2 first, Vector2 second,
            float offset)
        {
            Vector2 pointBetween = Vector3.Lerp(first, second, .5f);
            Vector2 dirToCurrent = pointBetween.Direction(vec);
            return (dirToCurrent * -offset + vec, pointBetween);
        }

        /// <summary>
        /// gets the direction vector to given target.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="targetVector"></param>
        /// <returns></returns>
        public static Vector2 Direction(this Vector2 v, Vector2 targetVector)
        {
            Vector2 dir = v - targetVector;
            return dir;
        }

        /// <summary>
        /// The LengthSquare method calculates the square of the distance between two Vector2 values vec1 and vec2.
        /// It does this by finding the difference between the x values of the two Vector2 and the difference between the y values,
        /// and then returning the sum of the squares of these differences.
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static float LengthSquare(this Vector2 vec1, Vector2 vec2)
        {
            float xDiff = vec1.x - vec2.x;
            float yDiff = vec2.y - vec1.y;
            return xDiff * xDiff + yDiff * yDiff;
        }
    }
}