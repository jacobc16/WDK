using UnityEngine;

namespace WDK.Utility
{
	public class Singleton<T> : MonoBehaviour
	{
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