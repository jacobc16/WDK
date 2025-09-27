using UnityEngine;
using UnityEngine.InputSystem;

namespace WDK.Controls
{
	internal delegate void OnBindingRebinded(string actionName);

	public partial class Input
	{
		internal static bool IsSprintToggle;
		internal static bool InvertControllerMovement;

		/// <summary>
		///     Indicates if the player is currently using a controller.
		/// </summary>
		public static bool IsUsingController => InputManager.Instance.CurrentDeviceType == InputManager.DeviceType.Controller;

		/// <summary>
		///     Returns the current mouse position in screen coordinates.
		/// </summary>
		public static Vector2 MousePosition => Mouse.current.position.ReadValue();

		internal static event OnBindingRebinded BindingRebinded;
	}
}