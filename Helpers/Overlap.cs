using System;
using UnityEngine;

namespace WDK.Helpers
{
	public static class Overlap
	{
		public enum OverlapType
		{
			Box,
			Sphere,
			Capsule
		}

		/// <summary>
		///     Checks for overlapping colliders within a sphere area.
		/// </summary>
		/// <param name="point">The center point of the sphere.</param>
		/// <param name="radius">The radius of the sphere.</param>
		/// <returns>An OverlapObject to configure and run the overlap check.</returns>
		public static OverlapObject Sphere(Vector3 point, float radius)
		{
			return new OverlapObject(point, radius, OverlapType.Sphere);
		}
	}

	public class OverlapObject
	{
		private readonly Vector3 _point;
		private readonly float _radius;
		private readonly Overlap.OverlapType _type;
		private LayerMask _layerMask = ~0;

		public OverlapObject(Vector3 point, float radius, Overlap.OverlapType type)
		{
			_point = point;
			_radius = radius;
			_type = type;
		}

		/// <summary>
		///     Sets the layer mask for the overlap check.
		/// </summary>
		/// <param name="layerMask">The layer mask to use for the overlap check.</param>
		/// <returns>The current OverlapObject for method chaining.</returns>
		public OverlapObject WithLayerMask(LayerMask layerMask)
		{
			_layerMask |= layerMask;
			return this;
		}

		/// <summary>
		///     Runs the overlap check and returns the result.
		/// </summary>
		/// <returns>An OverlapResult containing the results of the overlap check.</returns>
		public OverlapResult Run()
		{
			switch (_type)
			{
				case Overlap.OverlapType.Sphere:
				{
					var colliders = Physics.OverlapSphere(_point, _radius, _layerMask);
					return new OverlapResult
					{
						IsOverlapping = colliders.Length > 0,
						Colliders = colliders
					};
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public readonly struct OverlapResult
	{
		public bool IsOverlapping { get; init; }
		public Collider[] Colliders { get; init; }
	}
}