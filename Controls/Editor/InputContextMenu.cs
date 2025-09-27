using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace WDK.Controls.Editor
{
	public class InputContextMenu
	{
		[MenuItem("GameObject/WDK/Input Manager", false, 10)]
		public static void CreateInputManager()
		{
			if (Object.FindFirstObjectByType<InputManager>() is not null)
			{
				Debug.LogWarning("An Input Manager already exists in the scene.");
				return;
			}

			var go = new GameObject("Input Manager");
			go.AddComponent<InputManager>();
		}
	}
}
#endif