using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace WDK.UI
{
	public class UIBuilder
	{
		public UIBuilder(VisualElement elem)
		{
			Elem = elem;
		}

		public VisualElement Elem { get; }

		/// <summary>
		///     Create a new UIBuilder instance with an empty VisualElement.
		/// </summary>
		/// <returns></returns>
		public static UIBuilder Create()
		{
			return new UIBuilder(new VisualElement());
		}

		/// <summary>
		///     Create a new UIBuilder instance with a VisualElement that has the specified class name.
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public static UIBuilder Create(string className)
		{
			var elem = new VisualElement();
			elem.AddToClassList(className);
			return new UIBuilder(elem);
		}

		/// <summary>
		///     Add a new element to the current VisualElement.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="className"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Add<T>(T element, string className = null) where T : VisualElement
		{
			if (Elem == null || element == null)
				return null;

			if (!string.IsNullOrWhiteSpace(className))
				element.AddToClassList(className);

			Elem.Add(element);

			return element;
		}

		/// <summary>
		///     Add a new empty VisualElement to the current VisualElement.
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public VisualElement Add(string className = null)
		{
			return Add(new VisualElement(), className);
		}

		/// <summary>
		///     Add a new element to the current VisualElement and initialize it with the provided action.
		/// </summary>
		/// <param name="initializer"></param>
		/// <param name="className"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T Add<T>(Action<T> initializer = null, string className = null) where T : VisualElement, new()
		{
			var element = new T();
			initializer?.Invoke(element);
			return Add(element, className);
		}

		/// <summary>
		///     Add a new Button to the current VisualElement.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="onClick"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public Button AddButton(string text, Action onClick = null, string className = null)
		{
			var button = Add(new Button(() => { onClick?.Invoke(); }) { text = text }, className);

			return button;
		}

		/// <summary>
		///     Add a new Label to the current VisualElement.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public Label AddLabel(string text, string className = null)
		{
			var label = Add(new Label { text = text }, className);

			return label;
		}

		/// <summary>
		///     Adds a new Image to the current VisualElement.
		/// </summary>
		/// <param name="image"></param>
		/// <param name="className"></param>
		/// <returns></returns>
		public Image AddImage(Texture2D image, string className = null)
		{
			if (!image)
				return null;

			var img = new Image { image = image };
			Add(img, className);

			return img;
		}

		/// <inheritdoc cref="AddImage(Texture2D)" />
		public Image AddImage(Sprite image, string className = null)
		{
			if (!image)
				return null;

			var img = new Image { image = image.texture };
			Add(img, className);

			return img;
		}

		/// <summary>
		///     Style all elements of type T within the current VisualElement.
		/// </summary>
		/// <param name="styleAction"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public UIBuilder StyleAll<T>(Action<T> styleAction) where T : VisualElement
		{
			foreach (var elem in Elem.Query<T>().ToList())
				styleAction?.Invoke(elem);

			return this;
		}

		/// <summary>
		///     Style all elements with the specified class name within the current VisualElement.
		/// </summary>
		/// <param name="className"></param>
		/// <param name="styleAction"></param>
		/// <returns></returns>
		public UIBuilder StyleAll(string className, Action<VisualElement> styleAction)
		{
			foreach (var elem in Elem.Query().Class(className).ToList())
				styleAction?.Invoke(elem);

			return this;
		}

		/// <summary>
		///     Adds another UIBuilder's VisualElement to the current VisualElement.
		/// </summary>
		/// <param name="uiBuilder"></param>
		/// <returns></returns>
		public UIBuilder AddUI(UIBuilder uiBuilder)
		{
			if (uiBuilder?.Elem == null)
				return this;

			Elem.Add(uiBuilder.Elem);
			return this;
		}
	}
}