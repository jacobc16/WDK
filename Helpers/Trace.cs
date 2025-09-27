using System;
using System.Collections.Generic;
using UnityEngine;
using Input = WDK.Controls.Input;

namespace WDK.Helpers
{
	public static class Trace
	{
		public enum TraceType
		{
			Ray,
			Box,
			Sphere,
			Capsule
		}

		private static Camera _mainCamera;

		/// <summary>
		///     Access the main camera in the scene. Can be set manually if needed.
		/// </summary>
		public static Camera MainCamera
		{
			get
			{
				_mainCamera ??= Camera.main;
				return _mainCamera;
			}
			internal set => _mainCamera = value;
		}

		/// <summary>
		///     Starts a trace from the camera to the mouse position
		/// </summary>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Screen =>
			Ray(MainCamera?.ScreenPointToRay(Input.MousePosition) ?? new Ray(Vector3.zero, Vector3.forward));

		/// <summary>
		///     Creates a trace from the camera's position forward.
		/// </summary>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Ray()
		{
			if (MainCamera is not null)
				return Ray(MainCamera.transform.position, MainCamera.transform.forward);

			Debug.LogWarning("[Trace] Main Camera is not assigned. Returning empty trace.");
			return new TraceObject();
		}

		/// <summary>
		///     Creates a trace from a ray.
		/// </summary>
		/// <param name="ray">The ray to trace from.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Ray(Ray ray, float distance = float.MaxValue)
		{
			var traceObject = new TraceObject(ray, TraceType.Ray, distance);
			return traceObject;
		}

		/// <summary>
		///     Creates a trace from a ray defined by an origin and direction.
		/// </summary>
		/// <param name="origin">The origin point of the ray.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Ray(Vector3 origin, Vector3 direction, float distance = float.MaxValue)
		{
			direction.Normalize();

			var ray = new Ray(origin, direction);
			return Ray(ray, distance);
		}

		/// <summary>
		///     Creates a sphere trace from a ray.
		/// </summary>
		/// <param name="ray">The ray to trace from.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Sphere(Ray ray, float radius, float distance = float.MaxValue)
		{
			var traceObject = new TraceObject(ray, TraceType.Sphere, distance);
			traceObject.Size(radius);
			return traceObject;
		}

		/// <summary>
		///     Castes a sphere trace from a ray defined by an origin and direction.
		/// </summary>
		/// <param name="origin">The origin point of the ray.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Sphere(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue)
		{
			direction.Normalize();

			var ray = new Ray(origin, direction);
			return Sphere(ray, radius, distance);
		}

		/// <summary>
		///     Castes a box trace from a ray.
		/// </summary>
		/// <param name="ray">The ray to trace from.</param>
		/// <param name="size">The size of the box (length of one side).</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Box(Ray ray, float size, float distance = float.MaxValue)
		{
			var traceObject = new TraceObject(ray, TraceType.Box, distance);
			traceObject.Size(size);
			return traceObject;
		}

		/// <summary>
		///     Castes a box trace from a ray defined by an origin and direction.
		/// </summary>
		/// <param name="origin">The origin point of the ray.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="size">The size of the box (length of one side).</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Box(Vector3 origin, Vector3 direction, float size, float distance = float.MaxValue)
		{
			direction.Normalize();

			var ray = new Ray(origin, direction);
			return Box(ray, size, distance);
		}

		/// <summary>
		///     Castes a capsule trace from a ray.
		/// </summary>
		/// <param name="ray">The ray to trace from.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Capsule(Ray ray, float radius, float distance = float.MaxValue)
		{
			var traceObject = new TraceObject(ray, TraceType.Capsule, distance);
			traceObject.Size(radius);
			return traceObject;
		}

		/// <summary>
		///     Castes a capsule trace from a ray defined by an origin and direction.
		/// </summary>
		/// <param name="origin">The origin point of the ray.</param>
		/// <param name="direction">The direction of the ray.</param>
		/// <param name="radius">The radius of the capsule.</param>
		/// <param name="distance">The maximum distance of the trace. Default is infinity.</param>
		/// <returns>A TraceObject to configure and run the trace.</returns>
		public static TraceObject Capsule(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue)
		{
			direction.Normalize();

			var ray = new Ray(origin, direction);
			return Capsule(ray, radius, distance);
		}
	}

	public class TraceObject
	{
		private readonly float _distance;
		private readonly bool _hasNoInput;
		private readonly Ray _ray;
		private readonly Trace.TraceType _type;
		private LayerMask _mask = ~0;
		private float _size;

		public TraceObject()
		{
			_hasNoInput = true;
		}

		public TraceObject(Ray ray, Trace.TraceType type, float distance = float.MaxValue)
		{
			_ray = ray;
			_type = type;
			if (distance <= 0)
				distance = Mathf.Infinity;
			_distance = distance;
		}

		public HashSet<GameObject> IgnoredObjects { get; } = new();

		/// <summary>
		///     Ignores a specific GameObject in the trace.
		/// </summary>
		/// <param name="gameObject">The GameObject to ignore.</param>
		/// <returns>The current TraceObject for method chaining.</returns>
		public TraceObject IgnoreGameObject(GameObject gameObject)
		{
			IgnoredObjects.Add(gameObject);
			return this;
		}

		/// <summary>
		///     Only traces objects that are on the specified layer mask.
		/// </summary>
		/// <param name="mask">The layer mask to use for the trace.</param>
		/// <returns>The current TraceObject for method chaining.</returns>
		public TraceObject WithLayerMask(LayerMask mask)
		{
			_mask |= mask;
			return this;
		}

		/// <summary>
		///     Sets the size of the trace (for sphere, box, and capsule types).
		/// </summary>
		/// <param name="size">The size to set.</param>
		/// <returns>The current TraceObject for method chaining.</returns>
		public TraceObject Size(float size)
		{
			_size = size;
			return this;
		}

		private bool IsIgnored(GameObject gameObject)
		{
			return IgnoredObjects.Contains(gameObject);
		}

		/// <summary>
		///     Runs the trace and returns a single result.
		/// </summary>
		/// <returns>A TraceResult containing the result of the trace.</returns>
		public TraceResult Run()
		{
			if (_hasNoInput)
			{
				Debug.LogWarning("[Trace] No input provided for trace. Returning no hit.");
				return new TraceResult { Hit = false };
			}

			RaycastHit hitInfo;
			bool hit;

			switch (_type)
			{
				case Trace.TraceType.Ray:
				{
					hit = Physics.Raycast(_ray, out hitInfo, _distance, _mask);
					break;
				}
				case Trace.TraceType.Sphere:
				{
					hit = Physics.SphereCast(_ray, _size, out hitInfo, _distance, _mask);
					break;
				}
				case Trace.TraceType.Box:
				{
					var extents = Vector3.one * ( _size * 0.5f );
					hit = Physics.BoxCast(_ray.origin, extents, _ray.direction, out hitInfo,
						Quaternion.identity, _distance, _mask);
					break;
				}
				case Trace.TraceType.Capsule:
				{
					var point1 = _ray.origin + Vector3.up * ( _size * 0.5f );
					var point2 = _ray.origin - Vector3.up * ( _size * 0.5f );
					hit = Physics.CapsuleCast(point1, point2, _size, _ray.direction, out hitInfo, _distance, _mask);
					break;
				}
				default:
				{
					return new TraceResult { Hit = false };
				}
			}

			if (!hit) return new TraceResult { Hit = false };

			var go = hitInfo.collider.gameObject;
			if (IsIgnored(go)) return new TraceResult { Hit = false };

			return new TraceResult
			{
				Hit = true,
				GameObject = go,
				Distance = hitInfo.distance,
				HitPosition = hitInfo.point,
				Normal = hitInfo.normal
			};
		}

		/// <summary>
		///     Runs the trace and returns all results.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TraceResult> RunAll()
		{
			var hits = _type switch
			{
				Trace.TraceType.Ray => _mask != 0
					? Physics.RaycastAll(_ray, _distance, _mask)
					: Physics.RaycastAll(_ray, _distance),
				Trace.TraceType.Sphere => _mask != 0
					? Physics.SphereCastAll(_ray, _size, _distance, _mask)
					: Physics.SphereCastAll(_ray, _size, _distance),
				Trace.TraceType.Box => _mask != 0
					? Physics.BoxCastAll(_ray.origin, Vector3.one * ( _size * 0.5f ), _ray.direction, Quaternion.identity, _distance, _mask)
					: Physics.BoxCastAll(_ray.origin, Vector3.one * ( _size * 0.5f ), _ray.direction, Quaternion.identity, _distance),
				Trace.TraceType.Capsule => _mask != 0
					? Physics.CapsuleCastAll(
						_ray.origin + Vector3.up * ( _size * 0.5f ),
						_ray.origin - Vector3.up * ( _size * 0.5f ),
						_size, _ray.direction, _distance, _mask)
					: Physics.CapsuleCastAll(
						_ray.origin + Vector3.up * ( _size * 0.5f ),
						_ray.origin - Vector3.up * ( _size * 0.5f ),
						_size, _ray.direction, _distance),
				_ => Array.Empty<RaycastHit>()
			};

			foreach (var hit in hits)
			{
				var go = hit.collider.gameObject;
				if (IsIgnored(go)) continue;

				yield return new TraceResult
				{
					Hit = true,
					GameObject = go,
					Distance = hit.distance,
					HitPosition = hit.point,
					Normal = hit.normal
				};
			}
		}
	}

	public readonly struct TraceResult
	{
		/// <summary>
		///     Indicates whether the ray hit an object.
		/// </summary>
		public bool Hit { get; init; }

		/// <summary>
		///     The GameObject that was hit by the ray.
		/// </summary>
		public GameObject GameObject { get; init; }

		/// <summary>
		///     The distance from the ray's origin to the point of impact.
		/// </summary>
		public float Distance { get; init; }

		/// <summary>
		///     The position in world space where the ray hit the object.
		/// </summary>
		public Vector3 HitPosition { get; init; }

		/// <summary>
		///     The normal vector at the point of impact, pointing away from the surface.
		/// </summary>
		public Vector3 Normal { get; init; }

		/// <summary>
		///     Gets the first component of the hit GameObject.
		/// </summary>
		/// <typeparam name="T">The type of component to get.</typeparam>
		/// <returns>The component if found; otherwise, null.</returns>
		public T GetComponent<T>() where T : Component
		{
			return GameObject.GetComponent<T>();
		}

		/// <summary>
		///     Gets the first component of the hit GameObject in its parent hierarchy.
		/// </summary>
		/// <typeparam name="T">The type of component to get.</typeparam>
		/// <returns>The component if found; otherwise, null.</returns>
		public T GetComponentInParent<T>() where T : Component
		{
			return GameObject.GetComponentInParent<T>();
		}

		/// <summary>
		///     Gets the first component of the hit GameObject in its children.
		/// </summary>
		/// <typeparam name="T">The type of component to get.</typeparam>
		/// <returns>The component if found; otherwise, null.</returns>
		public T GetComponentInChildren<T>(bool includeInactive = false) where T : Component
		{
			return GameObject.GetComponentInChildren<T>(includeInactive);
		}
	}
}