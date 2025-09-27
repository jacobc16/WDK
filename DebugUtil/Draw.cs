using UnityEngine;

namespace WDK.DebugUtil
{
	/// <summary>
	///     This requires a DebugManager in the scene to work.
	/// </summary>
	public static class Draw
	{
		/// <summary>
		///     Draw a line in the scene view.
		/// </summary>
		/// <param name="start">The start position of the line.</param>
		/// <param name="end">The end position of the line.</param>
		/// <param name="color">The color of the line.</param>
		/// <param name="duration">The duration the line will be visible for.</param>
		public static void Line(Vector3 start, Vector3 end, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Line,
				Start = start,
				End = end,
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a ray in the scene view.
		/// </summary>
		/// <param name="start">The start position of the ray.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="color">The color of the ray.</param>
		/// <param name="duration">The duration the ray will be visible for.</param>
		public static void Ray(Vector3 start, Vector3 direction, Color color, float duration = 5f)
		{
			Line(start, start + direction, color, duration);
		}

		/// <summary>
		///     Draw a ray in the scene view.
		/// </summary>
		/// <param name="ray">The ray to draw.</param>
		/// <param name="color">The color of the ray.</param>
		/// <param name="duration">The duration the ray will be visible for.</param>
		public static void Ray(Ray ray, Color color, float duration = 5f)
		{
			Ray(ray.origin, ray.direction, color, duration);
		}

		/// <summary>
		///     Draw a box in the scene view.
		/// </summary>
		/// <param name="center">The center of the box.</param>
		/// <param name="size">The size of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">The duration the box will be visible for.</param>
		public static void Box(Vector3 center, Vector3 size, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Box,
				Center = center,
				Size = size,
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a wireframe box in the scene view.
		/// </summary>
		/// <param name="center">The center of the box.</param>
		/// <param name="size">The size of the box.</param>
		/// <param name="color">The color of the box.</param>
		/// <param name="duration">The duration the box will be visible for.</param>
		public static void WireBox(Vector3 center, Vector3 size, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.WireBox,
				Center = center,
				Size = size,
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a sphere in the scene view.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="color">The color of the sphere.</param>
		/// <param name="duration">The duration the sphere will be visible for.</param>
		public static void Sphere(Vector3 center, float radius, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Sphere,
				Center = center,
				Size = new Vector3(radius * 2, radius * 2, radius * 2),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a wireframe sphere in the scene view.
		/// </summary>
		/// <param name="center">The center of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="color">The color of the sphere.</param>
		/// <param name="duration">The duration the sphere will be visible for.</param>
		public static void WireSphere(Vector3 center, float radius, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.WireSphere,
				Center = center,
				Size = new Vector3(radius * 2, radius * 2, radius * 2),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a circle in the scene view.
		/// </summary>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="direction">The direction the circle is facing.</param>
		/// <param name="color">The color of the circle.</param>
		/// <param name="duration">The duration the circle will be visible for.</param>
		public static void Circle(Vector3 center, float radius, Vector3 direction, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Circle,
				Center = center,
				Direction = direction,
				Size = new Vector3(radius * 2, radius * 2, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a wireframe circle in the scene view.
		/// </summary>
		/// <param name="center">The center of the circle.</param>
		/// <param name="radius">The radius of the circle.</param>
		/// <param name="direction">The direction the circle is facing.</param>
		/// <param name="color">The color of the circle.</param>
		/// <param name="duration">The duration the circle will be visible for.</param>
		public static void WireCircle(Vector3 center, float radius, Vector3 direction, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.WireCircle,
				Center = center,
				Direction = direction,
				Size = new Vector3(radius * 2, radius * 2, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a plane in the scene view.
		/// </summary>
		/// <param name="center">The center of the plane.</param>
		/// <param name="normal"> The normal of the plane.</param>
		/// <param name="size">The size of the plane.</param>
		/// <param name="color">The color of the plane.</param>
		/// <param name="duration">The duration the plane will be visible for.</param>
		public static void Plane(Vector3 center, Vector3 normal, Vector2 size, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Plane,
				Center = center,
				Direction = normal,
				Size = new Vector3(size.x, size.y, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a wireframe plane in the scene view.
		/// </summary>
		/// <param name="center">The center of the plane.</param>
		/// <param name="normal"> The normal of the plane.</param>
		/// <param name="size">The size of the plane.</param>
		/// <param name="color">The color of the plane.</param>
		/// <param name="duration">The duration the plane will be visible for.</param>
		public static void WirePlane(Vector3 center, Vector3 normal, Vector2 size, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.WirePlane,
				Center = center,
				Direction = normal,
				Size = new Vector3(size.x, size.y, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a cone in the scene view.
		/// </summary>
		/// <param name="apex">The position of the cone's apex.</param>
		/// <param name="direction">The direction the cone is facing.</param>
		/// <param name="angle">The angle of the cone in degrees.</param>
		/// <param name="height">The height of the cone.</param>
		/// <param name="color">The color of the cone.</param>
		/// <param name="duration">The duration the cone will be visible for.</param>
		public static void Cone(Vector3 apex, Vector3 direction, float angle, float height, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.Cone,
				Center = apex,
				Direction = direction,
				Size = new Vector3(angle, height, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}

		/// <summary>
		///     Draw a wireframe cone in the scene view.
		/// </summary>
		/// <param name="apex">The position of the cone's apex.</param>
		/// <param name="direction">The direction the cone is facing.</param>
		/// <param name="angle">The angle of the cone in degrees.</param>
		/// <param name="height">The height of the cone.</param>
		/// <param name="color">The color of the cone.</param>
		/// <param name="duration">The duration the cone will be visible for.</param>
		public static void WireCone(Vector3 apex, Vector3 direction, float angle, float height, Color color, float duration = 5f)
		{
			DebugManager.Instance.Add(new DebugManager.DebugInfo
			{
				Shape = DebugManager.DebugShape.WireCone,
				Center = apex,
				Direction = direction,
				Size = new Vector3(angle, height, 0),
				Color = color,
				Duration = duration,
				EndTime = Time.time + duration
			});
		}
	}
}