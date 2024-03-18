using System.Linq;
using UnityEngine;

public class Vector3Helper 
{
	/// <summary>
	/// Gets the minimum x value from the specified vectors.
	/// </summary>
	public static float GetMinX(Vector3[] points)
	{
        return points.Min(point => point.x);
	}

	/// <summary>
	/// Gets the maximum x value from the specified vectors.
	/// </summary>
	public static float GetMaxX(Vector3[] points)
    {
        return points.Max(point => point.x);
    }

	/// <summary>
	/// Gets the minimum y value from the specified vectors.
	/// </summary>
	public static float GetMinY(Vector3[] points)
    {
        return points.Min(point => point.y);
    }

	/// <summary>
	/// Gets the maximum y value from the specified vectors.
	/// </summary>
	public static float GetMaxY(Vector3[] points)
    {
        return points.Max(point => point.y);
    }

	/// <summary>
	/// Gets the minimum z value from the specified vectors.
	/// </summary>
	public static float GetMinZ(Vector3[] points)
    {
        return points.Min(point => point.z);
    }

	/// <summary>
	/// Gets the maximun z value from the specified vectors.
	/// </summary>
	public static float GetMaxZ(Vector3[] points)
    {
        return points.Max(point => point.z);
    }
}
