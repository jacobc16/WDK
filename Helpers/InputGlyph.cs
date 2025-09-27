using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WDK.Controls;
using WDK.UI;

namespace WDK.Helpers
{
	public static class InputGlyph
	{
		public enum InputDeviceType
		{
			Xbox,
			Playstation,
			SteamDeck
		}

		private static readonly Dictionary<string, InputGlyphData> _data = new();

		internal static void GetGlyphs()
		{
			var devices = Resources.LoadAll<InputGlyphData>("InputGlyphs");

			foreach (var device in devices)
				_data.Add(device.name, device);
		}

		/// <summary>
		///     Get the glyph for a specific action based on the current input device.
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public static Texture2D GetGlyph(InputAction action)
		{
			var inputManager = InputManager.Instance;

			if (inputManager is null)
			{
				Debug.LogWarning("InputManager instance not found.");
				return null;
			}

			var deviceType = inputManager.CurrentDeviceType;

			return GetGlyph(deviceType, action);
		}

		/// <summary>
		///     Get the glyph for a specific action based on the specified input device.
		/// </summary>
		/// <param name="deviceType"></param>
		/// <param name="action"></param>
		/// <returns></returns>
		public static Texture2D GetGlyph(InputManager.DeviceType deviceType, InputAction action)
		{
			var inputManager = InputManager.Instance;

			switch (action.name)
			{
				case "Move":
				{
					// show move glyph
					break;
				}
			}

			var bindings = action.bindings;

			foreach (var inputBinding in bindings)
			{
				var path = inputBinding.effectivePath;

				switch (deviceType)
				{
					case InputManager.DeviceType.MouseAndKeyboard when path.StartsWith("<Keyboard>"):
					{
						var keyName = path.Split('/')[1];
						if (_data["Keyboard"].glyphs.TryGetValue(keyName, out var glyph))
							return glyph;
						break;
					}
					case InputManager.DeviceType.MouseAndKeyboard when path.StartsWith("<Mouse>") || path.StartsWith("<Pointer>"):
					{
						var buttonName = path.Split('/')[1];
						if (_data["Mouse"].glyphs.TryGetValue(buttonName, out var glyph))
							return glyph;
						break;
					}
					case InputManager.DeviceType.Controller when path.StartsWith("<Gamepad>"):
					{
						var buttonName = path.Split('/')[1];
						if (_data[inputManager.CurrentInputDeviceType.ToString()].glyphs.TryGetValue(buttonName, out var glyph))
							return glyph;
						break;
					}
				}
			}

			Debug.LogWarning($"Glyph for action '{action.name}' not found for device {deviceType}");
			return null;
		}
	}
}