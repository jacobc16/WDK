using UnityEditor;
using UnityEngine.UIElements;
using WDK.UI;

#if UNITY_EDITOR

namespace WDK.Editor
{
	[CustomEditor(typeof(InputGlyphData))]
	public class InputGlyphDataEditor : UnityEditor.Editor
	{
		private ListView listView;
		private TemplateContainer templateContainer;

		public override VisualElement CreateInspectorGUI()
		{
			templateContainer = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WDK/InputGlyphs/UI/Editor/InputGlyphEditor.uxml")
				.CloneTree();

			listView = templateContainer.Q<ListView>();

			return templateContainer;
		}
	}
}

#endif