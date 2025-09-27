using UnityEngine;
using UnityEngine.UIElements;

namespace WDK.UI
{
	public delegate void OnDraggableMoved(Vector2 position);

	/// <summary>
	///     This class allows a VisualElement to be dragged around within its parent container.
	/// </summary>
	public class DraggableManipulator : PointerManipulator
	{
		private Vector2 maxBounds;
		private Vector2? minBounds;

		public DraggableManipulator(VisualElement target)
		{
			this.target = target;
		}

		private bool enabled { get; set; }

		/// <summary>
		///     This event is triggered whenever the draggable element is moved.
		/// </summary>
		public event OnDraggableMoved OnDraggableMoved;

		public static DraggableManipulator Create(VisualElement target)
		{
			return new DraggableManipulator(target);
		}

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
			target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
			target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
			target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
			target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
			target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
			target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
		}

		private void PointerUpHandler(PointerUpEvent evt)
		{
			if (enabled && target.HasPointerCapture(evt.pointerId))
				target.ReleasePointer(evt.pointerId);
		}

		private static bool IsOrHasAncestorOfType<T>(VisualElement ve) where T : VisualElement
		{
			return ve is T || ve?.GetFirstAncestorOfType<T>() != null;
		}

		private void PointerDownHandler(PointerDownEvent evt)
		{
			var ve = evt.target as VisualElement;
			if (IsOrHasAncestorOfType<Button>(ve) || IsOrHasAncestorOfType<TextField>(ve))
				return;

			target.CapturePointer(evt.pointerId);
			enabled = true;

			if (minBounds is not null) return;

			var worldBounds = target.worldBound;
			minBounds = new Vector2(-worldBounds.x, -worldBounds.y);
			maxBounds = new Vector2(target.resolvedStyle.width + worldBounds.x,
				target.resolvedStyle.height + worldBounds.y);
		}

		private void PointerMoveHandler(PointerMoveEvent evt)
		{
			if (!enabled || !target.HasPointerCapture(evt.pointerId)) return;

			var currentPos = target.style.translate.value;

			var bounds = minBounds ?? Vector2.zero;

			var newPosition = Vector2.zero;
			newPosition.x = Mathf.Clamp(currentPos.x.value + evt.deltaPosition.x, bounds.x,
				target.panel.visualTree.worldBound.width - maxBounds.x);
			newPosition.y = Mathf.Clamp(currentPos.y.value + evt.deltaPosition.y, bounds.y,
				target.panel.visualTree.worldBound.height - maxBounds.y);

			target.style.translate = newPosition;

			if (currentPos != newPosition)
				OnDraggableMoved?.Invoke(newPosition);
		}

		private void PointerCaptureOutHandler(PointerCaptureOutEvent evt)
		{
			if (enabled) enabled = false;
		}
	}
}