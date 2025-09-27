using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.XInput;
using WDK.Helpers;
using WDK.Utility;

namespace WDK.Controls
{
	[RequireComponent(typeof(PlayerInput))]
	public sealed class InputManager : Singleton<InputManager>
	{
		public enum DeviceType
		{
			MouseAndKeyboard,
			Controller
		}

		internal PlayerInput PlayerInput;

		internal Input input { get; private set; }

		public DeviceType CurrentDeviceType =>
			PlayerInput.currentControlScheme == "Keyboard&Mouse" ? DeviceType.MouseAndKeyboard : DeviceType.Controller;

		public InputGlyph.InputDeviceType CurrentInputDeviceType => GetInputDeviceType();

		protected override void Awake()
		{
			base.Awake();

			PlayerInput = GetComponent<PlayerInput>();

			input = new Input();

			InputGlyph.GetGlyphs();
		}

		private InputGlyph.InputDeviceType GetInputDeviceType()
		{
			return Gamepad.current switch
			{
				XInputController => InputGlyph.InputDeviceType.Xbox,
				DualShockGamepad => InputGlyph.InputDeviceType.Playstation,
				_ => InputGlyph.InputDeviceType.SteamDeck // TODO: Verify that this would work
			};
		}
	}
}