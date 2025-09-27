using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WDK.UI
{
	[CreateAssetMenu(fileName = "InputGlyphData", menuName = "WDK/InputGlyphData", order = 1)]
	public sealed class InputGlyphData : ScriptableObject
	{
		public List<GlyphEntry> glyphEntries = new();
		private Dictionary<string, Texture2D> _glyphs;

		public Dictionary<string, Texture2D> glyphs =>
			_glyphs ??= glyphEntries.ToDictionary(entry => entry.name, entry => entry.texture);

		[Serializable]
		public struct GlyphEntry
		{
			public string name;
			public Texture2D texture;

			public GlyphEntry(string name, Texture2D texture)
			{
				this.name = name;
				this.texture = texture;
			}
		}
	}
}