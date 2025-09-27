using UnityEngine;

namespace WDK.Helpers
{
	public static class Cursor
	{
		/// <summary>
		///     Returns true if the cursor is currently locked (in the center of the screen and invisible).
		/// </summary>
		public static bool IsCursorLocked => UnityEngine.Cursor.lockState == CursorLockMode.Locked;

		/// <summary>
		///     Returns true if the cursor is currently visible.
		/// </summary>
		public static bool IsCursorVisible => UnityEngine.Cursor.visible;

		/// <summary>
		///     Sets the cursor to the specified texture.
		/// </summary>
		/// <param name="texture">The texture to set the cursor to.</param>
		/// <param name="hotspot">The hotspot of the cursor.</param>
		/// <param name="cursorMode">The cursor mode. Default is Auto.</param>
		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode = CursorMode.Auto)
		{
			if (texture is null)
			{
				Debug.LogWarning("Attempted to set a null cursor texture.");
				return;
			}

			UnityEngine.Cursor.SetCursor(texture, hotspot, cursorMode);
		}

		/// <summary>
		///     Resets the cursor to the default system cursor.
		/// </summary>
		public static void ResetCursor()
		{
			UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}


		/// <summary>
		///     Toggles the cursor lock state and visibility.
		/// </summary>
		/// <param name="lockCursor">
		///     If true, locks the cursor to the center of the screen and makes it invisible. If false,
		///     unlocks the cursor and makes it visible.
		/// </param>
		public static void ToggleCursor(bool lockCursor)
		{
			if (lockCursor)
			{
				UnityEngine.Cursor.lockState = CursorLockMode.Locked;
				UnityEngine.Cursor.visible = false;
			}
			else
			{
				UnityEngine.Cursor.lockState = CursorLockMode.None;
				UnityEngine.Cursor.visible = true;
			}
		}
	}
}