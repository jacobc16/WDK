using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using WDK.Helpers;
using WDK.Utility;

namespace WDK.DebugMenu
{
	public sealed class DebugUI : Singleton<DebugUI>
	{
		public enum TraceMode
		{
			Screen,
			Camera
		}

		public VisualTreeAsset debugItemTemplate;
		public UIDocument uiDocument;
		public TraceMode traceMode = TraceMode.Camera;
		private VisualElement _debugMenu;
		private bool _isDebugMenuOpen;
		private ScrollView _scrollView;
		internal List<DebugItem> DebugItems { get; set; } = new();

		public bool ShouldHide { get; set; }

		private void Start()
		{
			if (!uiDocument) return;

			var root = uiDocument.rootVisualElement;

			_debugMenu = root.Q("Debug");
			_scrollView = _debugMenu.Q<ScrollView>();

			DefaultDebugUI();

			Enable();
		}

		private void Update()
		{
			if (!_isDebugMenuOpen || ShouldHide) return;

			var ray = traceMode switch
			{
				TraceMode.Screen => Trace.Screen.Run(),
				TraceMode.Camera => Trace.Ray().Run(),
				_ => throw new ArgumentOutOfRangeException()
			};

			if (!ray.Hit)
			{
				SetTitle(string.Empty);
				_scrollView.Clear();
				return;
			}

			var go = ray.GameObject;

			Display(go);
		}

		private void DefaultDebugUI()
		{
			AddDisplayItem("Transform", (Transform t, VisualElement content) =>
			{
				var builder = content.Builder();
				builder.AddLabel($"Position: {t.position}");
				builder.AddLabel($"Rotation: {t.rotation.eulerAngles}");
				builder.AddLabel($"Scale: {t.localScale}");
			}, 100);
		}

		/// <summary>
		///     Adds a custom display item to the debug UI.
		/// </summary>
		/// <param name="name">The name of the group.</param>
		/// <param name="displayAction"></param>
		/// <param name="priority">How high up in the debug list to show.</param>
		/// <typeparam name="T">The component you want to display debug info on.</typeparam>
		public static void AddDisplayItem<T>(string name, Action<T, VisualElement> displayAction, int priority = 0)
		{
			var item = new CustomDebugItem<T>
			{
				Name = name,
				DisplayAction = displayAction,
				Priority = priority
			};

			Instance.DebugItems.Add(item);
			Instance.DebugItems.Sort((a, b) => b.Priority.CompareTo(a.Priority));
		}

		/// <summary>
		///     Adds a custom display item to the debug UI that operates on GameObjects.
		/// </summary>
		/// <param name="displayAction"></param>
		/// <param name="priority"></param>
		public void AddDisplayItem(Action<GameObject, VisualElement> displayAction, int priority = 0)
		{
			AddDisplayItem("Custom Display", displayAction, priority);
		}

		private void Display(GameObject obj)
		{
			_scrollView.Clear();
			SetTitle(obj.name);

			foreach (var item in DebugItems)
			{
				var itemType = item.GetType();
				if (!itemType.IsGenericType || itemType.GetGenericTypeDefinition() != typeof(CustomDebugItem<>)) continue;

				var componentType = itemType.GetGenericArguments()[0];
				if (!obj.TryGetComponent(componentType, out var component)) continue;

				var itemUI = debugItemTemplate.Instantiate();

				itemUI.Q<Label>("CategoryName").text = item.Name;
				var content = itemUI.Q<VisualElement>("Content");

				item.Display(component, content);

				_scrollView.Add(itemUI);
			}
		}

		public void SetTitle(string title)
		{
			if (!uiDocument) return;

			var titleElement = _debugMenu.Q<Label>("ObjectName");

			if (titleElement == null) return;

			titleElement.text = title;
		}

		public void Enable()
		{
			uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
			_isDebugMenuOpen = true;
		}

		public void Disable()
		{
			uiDocument.rootVisualElement.style.display = DisplayStyle.None;
			_isDebugMenuOpen = false;
		}
	}

	public class DebugItem
	{
		public string Name { get; set; }
		public int Priority { get; set; }

		public virtual void Display(Component component, VisualElement content)
		{
		}

		public virtual void Display(GameObject gameObject, VisualElement content)
		{
		}
	}

	public class CustomDebugItem<T> : DebugItem
	{
		public Action<T, VisualElement> DisplayAction { get; set; }

		public override void Display(Component component, VisualElement content)
		{
			if (component is T tComponent)
				DisplayAction?.Invoke(tComponent, content);
		}
	}
}