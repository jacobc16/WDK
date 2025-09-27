using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
namespace WDK.UI.Editor
{
	public static class UIEditorMenu
	{
		[MenuItem("WDK/UI/Convert Panel Asset")]
		public static void ConvertPanelAsset()
		{
			var path = "Assets/UI Toolkit/PanelSettings.asset";
			var panel = AssetDatabase.LoadAssetAtPath<PanelSettings>(path);
			if (panel is null) return;

			panel.scaleMode = PanelScaleMode.ScaleWithScreenSize;
			panel.referenceResolution = new Vector2Int(1920, 1080);
			panel.match = 0.5f;

			EditorUtility.SetDirty(panel);
			AssetDatabase.SaveAssets();
			Debug.Log($"Converted Panel Asset at {path}");
		}
	}
}
#endif