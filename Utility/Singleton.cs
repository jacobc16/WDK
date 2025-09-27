using UnityEngine;

namespace WDK.Utility
{
	/// <summary>
	///     This is a simple singleton pattern implementation for MonoBehaviour classes.
	/// </summary>
	/// <typeparam name="T">The type of the singleton class.</typeparam>
	public class Singleton<T> : MonoBehaviour
	{
		/// <summary>
		///     The instance of the singleton.
		/// </summary>
		public static T Instance { get; private set; }

		protected virtual void Awake()
		{
			Instance = (T)(object)this;
		}

		protected virtual void OnDestroy()
		{
			Instance = default;
		}
	}
}