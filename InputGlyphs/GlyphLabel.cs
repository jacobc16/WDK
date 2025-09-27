using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;
using WDK.Controls;
using WDK.Helpers;
using Input = WDK.Controls.Input;

namespace WDK.UI
{
	[UxmlElement]
	public sealed partial class GlyphLabel : VisualElement
	{
		private readonly List<VisualElement> _elements = new();
		private InputManager.DeviceType _currentDeviceType;
		private string _currentGlyphTexture;
		private bool _didUpdate;

		private string _lastGlyphTexture;

		private string _text;

		public bool GetGlyphDynamically = true;
		public InputManager.DeviceType GlyphDeviceType = InputManager.DeviceType.MouseAndKeyboard;

		public GlyphLabel()
		{
			pickingMode = PickingMode.Ignore;
			schedule.Execute(RenderUI);
			schedule.Execute(() =>
			{
				if (!Application.isPlaying) return;

				if (_currentDeviceType != InputManager.Instance.CurrentDeviceType)
					RenderUI();
			}).Every(100);

			Input.BindingRebinded += BindingChanged;

			RegisterCallback<DetachFromPanelEvent>(evt => { Input.BindingRebinded -= BindingChanged; });
		}

		/// <summary>
		///     The text template for the GlyphLabel. Use {ActionName} for glyphs, [Keyboard] for keyboard-specific text.
		///     Examples:
		///     "Press {Jump} to jump."
		///     Press [{Escape} or] {Pause} to pause. (Shows "Press Esc or Start to pause." on keyboard, "Press Esc to pause." on
		///     controller)
		/// </summary>
		[UxmlAttribute]
		public string text
		{
			get => _text;
			set
			{
				if (_text == value) return;

				_text = value;
				RenderUI();
			}
		}

		private void BindingChanged(string actionName)
		{
			if (string.IsNullOrEmpty(_text)) return;
			if (!_text.Contains($"{{{actionName}}}")) return;

			RenderUI();
		}

		private void RenderUI()
		{
			Clear();
			_elements.Clear();

			_currentDeviceType = InputManager.Instance?.CurrentDeviceType ?? InputManager.DeviceType.MouseAndKeyboard;

			foreach (var segment in ParseSegments(text))
			{
				if (!ShouldRenderSegment(segment.device)) continue;

				foreach (var subSegment in ParseGlyphsAndText(segment.value))
				{
					var trimmedValue = subSegment.value.Trim();
					if (string.IsNullOrEmpty(trimmedValue)) continue;

					if (subSegment.isGlyph)
					{
						var glyph = GetGlyph(trimmedValue);
						if (!glyph) continue;

						var img = new Image
						{
							image = glyph,
							pickingMode = PickingMode.Ignore
						};
						img.AddToClassList("glyph");
						_elements.Add(img);
						Add(img);
					}
					else
					{
						var label = new Label(trimmedValue)
						{
							pickingMode = PickingMode.Ignore
						};
						label.AddToClassList("text");
						_elements.Add(label);
						Add(label);
					}
				}
			}
		}

		private bool ShouldRenderSegment(string device)
		{
			if (string.IsNullOrEmpty(device)) return true;
			return ( device == "kb" && _currentDeviceType == InputManager.DeviceType.MouseAndKeyboard )
			       || ( device == "controller" && _currentDeviceType != InputManager.DeviceType.MouseAndKeyboard );
		}

		private IEnumerable<Segment> ParseSegments(string template)
		{
			var regex = new Regex(@"\(\[([^\]]+)\]\)|\[([^\]]+)\]");
			var lastIndex = 0;
			foreach (Match match in regex.Matches(template))
			{
				if (match.Index > lastIndex)
					yield return new Segment
					{
						value = template.Substring(lastIndex, match.Index - lastIndex).Trim(),
						device = null
					};

				if (match.Groups[1].Success)
					yield return new Segment
					{
						value = match.Groups[1].Value.Trim(),
						device = "controller"
					};
				else if (match.Groups[2].Success)
					yield return new Segment
					{
						value = match.Groups[2].Value.Trim(),
						device = "kb"
					};
				lastIndex = match.Index + match.Length;
			}

			if (lastIndex < template.Length)
				yield return new Segment
				{
					value = template.Substring(lastIndex).Trim(),
					device = null
				};
		}

		private IEnumerable<Segment> ParseGlyphsAndText(string input)
		{
			var regex = new Regex(@"\{([^}]+)\}");
			var lastIndex = 0;
			foreach (Match match in regex.Matches(input))
			{
				if (match.Index > lastIndex)
					yield return new Segment
					{
						isGlyph = false,
						value = input.Substring(lastIndex, match.Index - lastIndex).Trim()
					};
				yield return new Segment
				{
					isGlyph = true,
					value = match.Groups[1].Value.Trim()
				};
				lastIndex = match.Index + match.Length;
			}

			if (lastIndex < input.Length)
				yield return new Segment
				{
					isGlyph = false,
					value = input.Substring(lastIndex).Trim()
				};
		}

		private Texture2D GetGlyph(string glyphName)
		{
			var inputs = Input.Player;

			var inputAction = inputs?.Get().FindAction(glyphName);

			if (inputAction == null)
				return null;

			var glyph = GetGlyphDynamically ? InputGlyph.GetGlyph(inputAction) : InputGlyph.GetGlyph(GlyphDeviceType, inputAction);

			return glyph;
		}

		private struct Segment
		{
			public bool isGlyph;
			public string value;
			public string device;
		}
	}
}