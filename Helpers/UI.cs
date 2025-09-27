using System;
using System.Linq;
using UnityEngine.UIElements;
using WDK.UI;

namespace WDK.Helpers
{
	public static class UI
	{
		/// <summary>
		///     Ignores the given navigation directions for the given VisualElement.
		/// </summary>
		public static void RemoveNavigation(this VisualElement elem, params NavigationMoveEvent.Direction[] direction)
		{
			elem?.RegisterCallback<NavigationMoveEvent>(evt =>
			{
				if (!direction.Contains(evt.direction)) return;

				evt.StopPropagation();
				elem.focusController.IgnoreEvent(evt);
			});
		}

		/// <summary>
		///     Adds a navigation direction to the given VisualElement.
		/// </summary>
		public static void AddNavigation(this VisualElement elem, NavigationMoveEvent.Direction direction, VisualElement target)
		{
			if (elem == null || target == null)
				return;

			elem.RegisterCallback<NavigationMoveEvent>(evt =>
			{
				if (evt.direction != direction) return;

				target.Focus();
				evt.StopPropagation();
				elem.focusController.IgnoreEvent(evt);
			});
		}

		/// <summary>
		///     Adds a navigation direction to the given VisualElement using a target function.
		/// </summary>
		public static void AddNavigation(this VisualElement elem, NavigationMoveEvent.Direction direction, Func<VisualElement> targetFunc)
		{
			if (elem == null || targetFunc == null)
				return;

			elem.RegisterCallback<NavigationMoveEvent>(evt =>
			{
				if (evt.direction != direction) return;

				var target = targetFunc();
				if (target == null) return;

				target.Focus();
				evt.StopPropagation();
				elem.focusController.IgnoreEvent(evt);
			});
		}

		/// <summary>
		///     Adds a navigation direction to all children of the given VisualElement.
		/// </summary>
		public static void AddNavigationToAll<T>(this VisualElement elem, NavigationMoveEvent.Direction direction, T target)
			where T : VisualElement
		{
			if (elem == null || target == null)
				return;

			var children = elem.Query<T>().ToList();
			foreach (var child in children) child.AddNavigation(direction, target);
		}

		/// <summary>
		///     Creates a new UIBuilder for the given VisualElement.
		/// </summary>
		public static UIBuilder Builder(this VisualElement elem)
		{
			return new UIBuilder(elem);
		}

		/// <summary>
		///     Creates a new UIBuilder for the given VisualElement and adds a class to it.
		/// </summary>
		public static UIBuilder Builder(this VisualElement elem, string @class)
		{
			elem.AddToClassList(@class);
			var builder = new UIBuilder(elem);
			return builder;
		}
	}
}