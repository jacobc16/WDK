using UnityEditor;
using WDK.Helpers;

#if UNITY_EDITOR
namespace WDK.DebugMenu.Editor
{
	public class DebugUIContextMenu
	{
		[MenuItem("GameObject/WDK/Debug UI", false, 10)]
		public static void CreateInputManager()
		{
			var debugUI = AssetDatabase.LoadAssetAtPath<DebugUI>("Assets/WDK/DebugMenu/Prefabs/Debug UI.prefab").gameObject.Clone();
			debugUI.name = "Debug UI";
		}
	}
}
#endif