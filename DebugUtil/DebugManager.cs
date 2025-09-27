using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using WDK.Utility;

namespace WDK.DebugUtil
{
	public sealed class DebugManager : Singleton<DebugManager>
	{
		public enum DebugShape
		{
			Line,
			Box,
			Sphere,
			Circle,
			Plane,
			Cone,
			WireBox,
			WireSphere,
			WireCone,
			WireCircle,
			WirePlane
		}

		private readonly List<DebugInfo> _debugInfos = new();
		private Material _debugMaterial;

		public Material DebugMaterial
		{
			get { return _debugMaterial ??= GetMaterial(); }
		}

		private void Start()
		{
			RenderPipelineManager.endCameraRendering += RenderPipelineManagerOnendCameraRendering;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			_debugInfos.Clear();
			RenderPipelineManager.endCameraRendering -= RenderPipelineManagerOnendCameraRendering;
		}

		private static Material GetMaterial()
		{
			return Resources.Load<Material>("Materials/DebugMaterial");
		}

		private void RenderPipelineManagerOnendCameraRendering(ScriptableRenderContext arg1, Camera arg2)
		{
			foreach (var info in _debugInfos.ToList())
			{
				if (info.IsExpired)
				{
					_debugInfos.Remove(info);
					continue;
				}

				DebugMaterial.SetPass(0);

				GL.PushMatrix();

				switch (info.Shape)
				{
					case DebugShape.Line:
					{
						DrawLine(info);
						break;
					}
					case DebugShape.Box:
					{
						DrawBox(info);
						break;
					}
					case DebugShape.WireBox:
					{
						DrawWireBox(info);
						break;
					}
					case DebugShape.Sphere:
					{
						DrawSphere(info);

						break;
					}
					case DebugShape.WireSphere:
					{
						DrawWireSphere(info);
						break;
					}
					case DebugShape.Circle:
					{
						DrawCircle(info);
						break;
					}
					case DebugShape.Plane:
					{
						DrawPlane(info);
						break;
					}
					case DebugShape.Cone:
					{
						DrawCone(info);
						break;
					}
					case DebugShape.WireCone:
					{
						DrawWireCone(info);
						break;
					}
					case DebugShape.WireCircle:
					{
						DrawWireCircle(info);
						break;
					}
					case DebugShape.WirePlane:
					{
						DrawWirePlane(info);
						break;
					}
					default:
						throw new ArgumentOutOfRangeException();
				}

				GL.End();
				GL.PopMatrix();
			}
		}

		private void DrawLine(DebugInfo info)
		{
			GL.Begin(GL.LINES);
			GL.Color(info.Color);
			GL.Vertex(info.Start);
			GL.Vertex(info.End);
		}

		private void DrawBox(DebugInfo info)
		{
			var halfSize = info.Size * 0.5f;
			var c = info.Center;

			var p0 = c + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
			var p1 = c + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
			var p2 = c + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
			var p3 = c + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);

			var p4 = c + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
			var p5 = c + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
			var p6 = c + new Vector3(halfSize.x, halfSize.y, halfSize.z);
			var p7 = c + new Vector3(-halfSize.x, halfSize.y, halfSize.z);

			GL.Begin(GL.QUADS);
			GL.Color(info.Color);

			GL.Vertex(p0);
			GL.Vertex(p1);
			GL.Vertex(p2);
			GL.Vertex(p3);
			GL.Vertex(p3);
			GL.Vertex(p2);
			GL.Vertex(p1);
			GL.Vertex(p0);

			GL.Vertex(p4);
			GL.Vertex(p5);
			GL.Vertex(p6);
			GL.Vertex(p7);
			GL.Vertex(p7);
			GL.Vertex(p6);
			GL.Vertex(p5);
			GL.Vertex(p4);

			GL.Vertex(p3);
			GL.Vertex(p2);
			GL.Vertex(p6);
			GL.Vertex(p7);
			GL.Vertex(p7);
			GL.Vertex(p6);
			GL.Vertex(p2);
			GL.Vertex(p3);

			GL.Vertex(p0);
			GL.Vertex(p1);
			GL.Vertex(p5);
			GL.Vertex(p4);
			GL.Vertex(p4);
			GL.Vertex(p5);
			GL.Vertex(p1);
			GL.Vertex(p0);

			GL.Vertex(p0);
			GL.Vertex(p3);
			GL.Vertex(p7);
			GL.Vertex(p4);
			GL.Vertex(p4);
			GL.Vertex(p7);
			GL.Vertex(p3);
			GL.Vertex(p0);

			GL.Vertex(p1);
			GL.Vertex(p2);
			GL.Vertex(p6);
			GL.Vertex(p5);
			GL.Vertex(p5);
			GL.Vertex(p6);
			GL.Vertex(p2);
			GL.Vertex(p1);
		}

		private void DrawWireBox(DebugInfo info)
		{
			var halfSize = info.Size * 0.5f;
			var c = info.Center;

			var p0 = c + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
			var p1 = c + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
			var p2 = c + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
			var p3 = c + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);

			var p4 = c + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
			var p5 = c + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
			var p6 = c + new Vector3(halfSize.x, halfSize.y, halfSize.z);
			var p7 = c + new Vector3(-halfSize.x, halfSize.y, halfSize.z);

			GL.Begin(GL.LINES);
			GL.Color(info.Color);

			GL.Vertex(p0);
			GL.Vertex(p1);
			GL.Vertex(p1);
			GL.Vertex(p2);
			GL.Vertex(p2);
			GL.Vertex(p3);
			GL.Vertex(p3);
			GL.Vertex(p0);

			GL.Vertex(p4);
			GL.Vertex(p5);
			GL.Vertex(p5);
			GL.Vertex(p6);
			GL.Vertex(p6);
			GL.Vertex(p7);
			GL.Vertex(p7);
			GL.Vertex(p4);

			GL.Vertex(p0);
			GL.Vertex(p4);
			GL.Vertex(p1);
			GL.Vertex(p5);
			GL.Vertex(p2);
			GL.Vertex(p6);
			GL.Vertex(p3);
			GL.Vertex(p7);
		}

		private void DrawSphere(DebugInfo info)
		{
			if (info.Size.x <= 0f) return;

			const int stacks = 16;
			const int slices = 24;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;

			GL.Begin(GL.TRIANGLES);
			GL.Color(info.Color);

			for (var stack = 0; stack < stacks; stack++)
			{
				var theta1 = Mathf.PI * stack / stacks;
				var theta2 = Mathf.PI * ( stack + 1 ) / stacks;

				for (var slice = 0; slice < slices; slice++)
				{
					var phi1 = 2f * Mathf.PI * slice / slices;
					var phi2 = 2f * Mathf.PI * ( slice + 1 ) / slices;

					var p1 = center + new Vector3(
						radius * Mathf.Sin(theta1) * Mathf.Cos(phi1),
						radius * Mathf.Cos(theta1),
						radius * Mathf.Sin(theta1) * Mathf.Sin(phi1)
					);
					var p2 = center + new Vector3(
						radius * Mathf.Sin(theta2) * Mathf.Cos(phi1),
						radius * Mathf.Cos(theta2),
						radius * Mathf.Sin(theta2) * Mathf.Sin(phi1)
					);
					var p3 = center + new Vector3(
						radius * Mathf.Sin(theta2) * Mathf.Cos(phi2),
						radius * Mathf.Cos(theta2),
						radius * Mathf.Sin(theta2) * Mathf.Sin(phi2)
					);
					var p4 = center + new Vector3(
						radius * Mathf.Sin(theta1) * Mathf.Cos(phi2),
						radius * Mathf.Cos(theta1),
						radius * Mathf.Sin(theta1) * Mathf.Sin(phi2)
					);

					GL.Vertex(p1);
					GL.Vertex(p2);
					GL.Vertex(p3);

					GL.Vertex(p1);
					GL.Vertex(p3);
					GL.Vertex(p4);
				}
			}
		}

		private void DrawWireSphere(DebugInfo info)
		{
			if (info.Size.x <= 0f)
				return;

			const int segments = 24;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;
			GL.Begin(GL.LINES);
			GL.Color(info.Color);

			for (var i = 0; i < segments; i++)
			{
				var theta1 = Mathf.PI * i / segments;
				var theta2 = Mathf.PI * ( i + 1 ) / segments;
				for (var j = 0; j < segments; j++)
				{
					var phi1 = 2f * Mathf.PI * j / segments;
					var phi2 = 2f * Mathf.PI * ( j + 1 ) / segments;

					var p1 = center + new Vector3(
						radius * Mathf.Sin(theta1) * Mathf.Cos(phi1),
						radius * Mathf.Cos(theta1),
						radius * Mathf.Sin(theta1) * Mathf.Sin(phi1)
					);
					var p2 = center + new Vector3(
						radius * Mathf.Sin(theta1) * Mathf.Cos(phi2),
						radius * Mathf.Cos(theta1),
						radius * Mathf.Sin(theta1) * Mathf.Sin(phi2)
					);
					GL.Vertex(p1);
					GL.Vertex(p2);
				}
			}

			for (var i = 0; i < segments; i++)
			{
				var phi = 2f * Mathf.PI * i / segments;
				for (var j = 0; j < segments; j++)
				{
					var theta1 = Mathf.PI * j / segments;
					var theta2 = Mathf.PI * ( j + 1 ) / segments;

					var p1 = center + new Vector3(
						radius * Mathf.Sin(theta1) * Mathf.Cos(phi),
						radius * Mathf.Cos(theta1),
						radius * Mathf.Sin(theta1) * Mathf.Sin(phi)
					);
					var p2 = center + new Vector3(
						radius * Mathf.Sin(theta2) * Mathf.Cos(phi),
						radius * Mathf.Cos(theta2),
						radius * Mathf.Sin(theta2) * Mathf.Sin(phi)
					);
					GL.Vertex(p1);
					GL.Vertex(p2);
				}
			}
		}

		private void DrawCircle(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Direction == Vector3.zero) return;

			const int segments = 32;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;
			var direction = info.Direction.normalized;
			var rotation = Quaternion.FromToRotation(Vector3.up, direction);

			GL.Begin(GL.TRIANGLES);
			GL.Color(info.Color);

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP2 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP3 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p1 = center;
				var p2 = center + rotation * localP2;
				var p3 = center + rotation * localP3;

				GL.Vertex(p1);
				GL.Vertex(p2);
				GL.Vertex(p3);

				GL.Vertex(p1);
				GL.Vertex(p3);
				GL.Vertex(p2);
			}
		}

		private void DrawWireCircle(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Direction == Vector3.zero) return;

			const int segments = 32;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;
			var direction = info.Direction.normalized;
			var rotation = Quaternion.FromToRotation(Vector3.up, direction);

			GL.Begin(GL.LINES);
			GL.Color(info.Color);

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP2 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP3 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p2 = center + rotation * localP2;
				var p3 = center + rotation * localP3;

				GL.Vertex(p2);
				GL.Vertex(p3);
			}
		}

		private void DrawPlane(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Size.y <= 0f || info.Direction == Vector3.zero) return;

			var center = info.Center;
			var size = info.Size;
			var direction = info.Direction.normalized;

			var right = Vector3.Cross(direction, Vector3.up);
			if (right == Vector3.zero) right = Vector3.Cross(direction, Vector3.forward);
			right = right.normalized;
			var up = Vector3.Cross(right, direction).normalized;

			var halfWidth = size.x * 0.5f;
			var halfHeight = size.y * 0.5f;

			var p0 = center - right * halfWidth - up * halfHeight;
			var p1 = center + right * halfWidth - up * halfHeight;
			var p2 = center + right * halfWidth + up * halfHeight;
			var p3 = center - right * halfWidth + up * halfHeight;

			GL.Begin(GL.QUADS);
			GL.Color(info.Color);

			GL.Vertex(p0);
			GL.Vertex(p3);
			GL.Vertex(p2);
			GL.Vertex(p1);

			GL.Vertex(p1);
			GL.Vertex(p2);
			GL.Vertex(p3);
			GL.Vertex(p0);
		}

		private void DrawWirePlane(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Size.y <= 0f || info.Direction == Vector3.zero) return;

			var center = info.Center;
			var size = info.Size;
			var direction = info.Direction.normalized;

			var right = Vector3.Cross(direction, Vector3.up);
			if (right == Vector3.zero) right = Vector3.Cross(direction, Vector3.forward);
			right = right.normalized;
			var up = Vector3.Cross(right, direction).normalized;

			var halfWidth = size.x * 0.5f;
			var halfHeight = size.y * 0.5f;

			var p0 = center - right * halfWidth - up * halfHeight;
			var p1 = center + right * halfWidth - up * halfHeight;
			var p2 = center + right * halfWidth + up * halfHeight;
			var p3 = center - right * halfWidth + up * halfHeight;

			GL.Begin(GL.LINES);
			GL.Color(info.Color);

			GL.Vertex(p0);
			GL.Vertex(p1);

			GL.Vertex(p1);
			GL.Vertex(p2);

			GL.Vertex(p2);
			GL.Vertex(p3);

			GL.Vertex(p3);
			GL.Vertex(p0);
		}

		private void DrawCone(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Size.y <= 0f || info.Direction == Vector3.zero) return;

			const int segments = 32;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;
			var height = info.Size.y;
			var direction = info.Direction.normalized;
			var apex = center + direction * height;

			var rotation = Quaternion.FromToRotation(Vector3.up, direction);

			GL.Begin(GL.TRIANGLES);
			GL.Color(info.Color);

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP1 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP2 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p1 = center + rotation * localP1;
				var p2 = center + rotation * localP2;

				GL.Vertex(apex);
				GL.Vertex(p1);
				GL.Vertex(p2);
			}

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP2 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP3 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p2 = center + rotation * localP2;
				var p3 = center + rotation * localP3;

				GL.Vertex(center);
				GL.Vertex(p2);
				GL.Vertex(p3);

				GL.Vertex(center);
				GL.Vertex(p3);
				GL.Vertex(p2);
			}
		}

		private void DrawWireCone(DebugInfo info)
		{
			if (info.Size.x <= 0f || info.Size.y <= 0f || info.Direction == Vector3.zero) return;

			const int segments = 32;
			var center = info.Center;
			var radius = info.Size.x * 0.5f;
			var height = info.Size.y;
			var direction = info.Direction.normalized;
			var apex = center + direction * height;

			var rotation = Quaternion.FromToRotation(Vector3.up, direction);

			GL.Begin(GL.LINES);
			GL.Color(info.Color);

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP1 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP2 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p1 = center + rotation * localP1;
				var p2 = center + rotation * localP2;

				GL.Vertex(apex);
				GL.Vertex(p1);
			}

			for (var i = 0; i < segments; i++)
			{
				var angle1 = 2f * Mathf.PI * i / segments;
				var angle2 = 2f * Mathf.PI * ( i + 1 ) / segments;

				var localP2 = new Vector3(Mathf.Cos(angle1) * radius, 0f, Mathf.Sin(angle1) * radius);
				var localP3 = new Vector3(Mathf.Cos(angle2) * radius, 0f, Mathf.Sin(angle2) * radius);

				var p2 = center + rotation * localP2;
				var p3 = center + rotation * localP3;

				GL.Vertex(p2);
				GL.Vertex(p3);
			}
		}

		public void Add(DebugInfo info)
		{
			_debugInfos.Add(info);
		}

		public class DebugInfo
		{
			public DebugShape Shape { get; set; }
			public Vector3 Start { get; set; }
			public Vector3 End { get; set; }
			public Color Color { get; set; }
			public float Duration { get; set; }
			public float EndTime { get; set; }
			public Vector3 Center { get; set; }
			public Vector3 Size { get; set; }
			public Vector3 Direction { get; set; }

			public bool IsExpired => Duration > 0 && Time.time >= EndTime;
		}
	}
}