using Unity.Properties;
using UnityEngine.UIElements;

namespace WDK.Helpers
{
	public static class VisualElementsExtensions
	{
		/// <summary>
		///     Binds a VisualElement to a property path with the specified binding mode.
		/// </summary>
		/// <param name="elem">The VisualElement to bind.</param>
		/// <param name="id">The property ID to bind (e.g., "text", "value").</param>
		/// <param name="property">The property path to bind to.</param>
		/// <param name="mode">The binding mode (default is BindingMode.ToTarget).</param>
		public static void Bind(this VisualElement elem, string id, string property, BindingMode mode = BindingMode.ToTarget)
		{
			elem.SetBinding(id, new DataBinding
			{
				dataSourcePath = new PropertyPath(property),
				bindingMode = mode
			});
		}

		/// <summary>
		///     Binds the "text" property of a VisualElement to a specified property path with the given binding mode.
		/// </summary>
		/// <param name="elem">The VisualElement to bind.</param>
		/// <param name="property">The property path to bind to.</param>
		/// <param name="mode">The binding mode (default is BindingMode.ToTarget).</param>
		public static void BindText(this VisualElement elem, string property, BindingMode mode = BindingMode.ToTarget)
		{
			elem.Bind("text", property, mode);
		}

		/// <summary>
		///     Binds the "value" property of a VisualElement to a specified property path with the given binding mode.
		/// </summary>
		/// <param name="elem">The VisualElement to bind.</param>
		/// <param name="property">The property path to bind to.</param>
		/// <param name="mode">The binding mode (default is BindingMode.ToTarget).</param>
		public static void BindValue(this VisualElement elem, string property, BindingMode mode = BindingMode.ToTarget)
		{
			elem.Bind("value", property, mode);
		}

		/// <summary>
		/// </summary>
		/// <param name="elem">The VisualElement to bind.</param>
		/// <param name="style">The style property to bind (e.g., "color", "fontSize").</param>
		/// <param name="property">The property path to bind to.</param>
		/// <param name="mode">The binding mode (default is BindingMode.ToTarget).</param>
		public static void BindStyle(this VisualElement elem, string style, string property, BindingMode mode = BindingMode.ToTarget)
		{
			elem.Bind($"style.{style}", property, mode);
		}
	}
}