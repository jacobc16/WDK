using UnityEngine;

namespace WDK.Helpers
{
	public static class GameObjectExtensions
	{
		/// <summary>
		///     Clones the given GameObject.
		/// </summary>
		/// <param name="original"></param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original)
		{
			var clone = Object.Instantiate(original);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its parent.
		/// </summary>
		/// <param name="original"></param>
		/// <param name="parent"></param>
		/// <param name="instantiateInWorldSpace"></param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Transform parent, bool instantiateInWorldSpace = false)
		{
			var clone = Object.Instantiate(original, parent, instantiateInWorldSpace);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position and rotation.
		/// </summary>
		/// <param name="original"></param>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation)
		{
			var clone = Object.Instantiate(original, position, rotation);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position, rotation and parent.
		/// </summary>
		/// <param name="original"></param>
		/// <param name="position"></param>
		/// <param name="rotation"></param>
		/// <param name="parent"></param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation, Transform parent)
		{
			var clone = Object.Instantiate(original, position, rotation, parent);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position.
		/// </summary>
		/// <param name="original"></param>
		/// <param name="position"></param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Vector3 position)
		{
			return original.Clone(position, Quaternion.identity);
		}
	}
}