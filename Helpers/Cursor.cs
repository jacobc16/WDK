using UnityEngine;

namespace WDK.Helpers
{
	public static class Cursor
	{
		public enum CursorType
		{
			Arrow,
			Pointer,
			HandOpen,
			HandClosed,
			Sell
		}

		public static bool IsCursorLocked => UnityEngine.Cursor.lockState == CursorLockMode.Locked;
		public static bool IsCursorVisible => UnityEngine.Cursor.visible;

		public static void SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode = CursorMode.Auto)
		{
			if (texture is null)
			{
				Debug.LogWarning("Attempted to set a null cursor texture.");
				return;
			}

			UnityEngine.Cursor.SetCursor(texture, hotspot, cursorMode);
		}

		public static void ResetCursor()
		{
			UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

		public static void LockCursor(bool lockCursor)
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