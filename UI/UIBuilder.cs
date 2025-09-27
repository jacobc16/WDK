using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace WDK.UI
{
	public class UIBuilder
	{
		/// <summary>
		///     Create a new UIBuilder instance with the provided VisualElement.
		/// </summary>
		/// <param name="elem">The VisualElement to use as the root element.</param>
		public UIBuilder(VisualElement elem)
		{
			Elem = elem;
		}

		public VisualElement Elem { get; }

		/// <summary>
		///     Create a new UIBuilder instance with an empty VisualElement.
		/// </summary>
		/// <returns>The created UIBuilder instance.</returns>
		public static UIBuilder Create()
		{
			return new UIBuilder(new VisualElement());
		}

		/// <summary>
		///     Create a new UIBuilder instance with a VisualElement that has the specified class name.
		/// </summary>
		/// <param name="className">The class name to add to the new VisualElement.</param>
		/// <returns>The created UIBuilder instance.</returns>
		public static UIBuilder Create(string className)
		{
			var elem = new VisualElement();
			elem.AddToClassList(className);
			return new UIBuilder(elem);
		}

		/// <summary>
		///     Add a new element to the current VisualElement.
		/// </summary>
		/// <param name="element">The element to add.</param>
		/// <param name="className">The optional class name to add to the new element.</param>
		/// <typeparam name="T">The type of VisualElement to add.</typeparam>
		/// <returns>>The added element.</returns>
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
		/// <param name="className">The optional class name to add to the new element.</param>
		/// <returns>The created and added element.</returns>
		public VisualElement Add(string className = null)
		{
			return Add(new VisualElement(), className);
		}

		/// <summary>
		///     Add a new element to the current VisualElement and initialize it with the provided action.
		/// </summary>
		/// <param name="initializer">The action to initialize the new element.</param>
		/// <param name="className">The optional class name to add to the new element.</param>
		/// <typeparam name="T">The type of VisualElement to create and add.</typeparam>
		/// <returns>The created and added element.</returns>
		public T Add<T>(Action<T> initializer = null, string className = null) where T : VisualElement, new()
		{
			var element = new T();
			initializer?.Invoke(element);
			return Add(element, className);
		}

		/// <summary>
		///     Add a new Button to the current VisualElement.
		/// </summary>
		/// <param name="text">The text to display on the button.</param>
		/// <param name="onClick">The action to perform when the button is clicked.</param>
		/// <param name="className">The optional class name to add to the button.</param>
		/// <returns>The created Button element.</returns>
		public Button AddButton(string text, Action onClick = null, string className = null)
		{
			var button = Add(new Button(() => { onClick?.Invoke(); }) { text = text }, className);

			return button;
		}

		/// <summary>
		///     Add a new Label to the current VisualElement.
		/// </summary>
		/// <param name="text">The text to display in the label.</param>
		/// <param name="className">The optional class name to add to the label.</param>
		/// <returns>The created Label element.</returns>
		public Label AddLabel(string text, string className = null)
		{
			var label = Add(new Label { text = text }, className);

			return label;
		}

		/// <summary>
		///     Adds a new Image to the current VisualElement.
		/// </summary>
		/// <param name="image">The texture to use for the image.</param>
		/// <param name="className">The optional class name to add to the image.</param>
		/// <returns>The created Image element, or null if the texture is null.</returns>
		public Image AddImage(Texture2D image, string className = null)
		{
			if (!image)
				return null;

			var img = new Image { image = image };
			Add(img, className);

			return img;
		}

		/// <summary>
		///     Adds a new Image to the current VisualElement.
		/// </summary>
		/// <param name="image">The sprite to use for the image.</param>
		/// <param name="className">The optional class name to add to the image.</param>
		/// <returns>The created Image element, or null if the sprite is null.</returns>
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
		/// <param name="styleAction">The action to apply to each element of type T.</param>
		/// <typeparam name="T">The type of VisualElement to style.</typeparam>
		/// <returns>The current UIBuilder instance for chaining.</returns>
		public UIBuilder StyleAll<T>(Action<T> styleAction) where T : VisualElement
		{
			foreach (var elem in Elem.Query<T>().ToList())
				styleAction?.Invoke(elem);

			return this;
		}

		/// <summary>
		///     Style all elements with the specified class name within the current VisualElement.
		/// </summary>
		/// <param name="className">The class name to search for.</param>
		/// <param name="styleAction">The action to apply to each element with the specified class name.</param>
		/// <returns>The current UIBuilder instance for chaining.</returns>
		public UIBuilder StyleAll(string className, Action<VisualElement> styleAction)
		{
			foreach (var elem in Elem.Query().Class(className).ToList())
				styleAction?.Invoke(elem);

			return this;
		}

		/// <summary>
		///     Adds another UIBuilder's VisualElement to the current VisualElement.
		/// </summary>
		/// <param name="uiBuilder">The UIBuilder whose VisualElement to add.</param>
		/// <returns>The current UIBuilder instance for chaining.</returns>
		public UIBuilder Add(UIBuilder uiBuilder)
		{
			if (uiBuilder?.Elem == null)
				return this;

			Elem.Add(uiBuilder.Elem);
			return this;
		}
	}
}