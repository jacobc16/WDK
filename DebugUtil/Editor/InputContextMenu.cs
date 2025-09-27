using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace WDK.DebugUtil.Editor
{
	public class DebugContextMenu
	{
		[MenuItem("GameObject/WDK/Debug Manager", false, 10)]
		public static void CreateDebugManager()
		{
			if (Object.FindFirstObjectByType<DebugManager>() is not null)
			{
				Debug.LogWarning("A Debug Manager already exists in the scene.");
				return;
			}

			var go = new GameObject("Debug Manager");
			go.AddComponent<DebugManager>();
		}
	}
}
#endif