using UnityEngine;

namespace WDK.Helpers
{
	public static class GameObjectExtensions
	{
		/// <summary>
		///     Clones the given GameObject.
		/// </summary>
		/// <param name="original">The GameObject to clone.</param>
		/// <returns>The cloned GameObject.</returns>
		public static GameObject Clone(this GameObject original)
		{
			var clone = Object.Instantiate(original);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its parent.
		/// </summary>
		/// <param name="original">The GameObject to clone.</param>
		/// <param name="parent">The parent Transform to set on the cloned GameObject.</param>
		/// <param name="instantiateInWorldSpace">
		///     If true, the position and rotation of the original are maintained in world space;
		///     otherwise, they are relative to the parent.
		/// </param>
		/// <returns>The cloned GameObject.</returns>
		public static GameObject Clone(this GameObject original, Transform parent, bool instantiateInWorldSpace = false)
		{
			var clone = Object.Instantiate(original, parent, instantiateInWorldSpace);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position and rotation.
		/// </summary>
		/// <param name="original">The GameObject to clone.</param>
		/// <param name="position">The position to set on the cloned GameObject.</param>
		/// <param name="rotation">The rotation to set on the cloned GameObject.</param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation)
		{
			var clone = Object.Instantiate(original, position, rotation);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position, rotation and parent.
		/// </summary>
		/// <param name="original">The GameObject to clone.</param>
		/// <param name="position">The position to set on the cloned GameObject.</param>
		/// <param name="rotation">The rotation to set on the cloned GameObject.</param>
		/// <param name="parent">The parent Transform to set on the cloned GameObject.</param>
		/// <returns>The cloned GameObject.</returns>
		public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation, Transform parent)
		{
			var clone = Object.Instantiate(original, position, rotation, parent);

			return clone;
		}

		/// <summary>
		///     Clones the given GameObject and sets its position.
		/// </summary>
		/// <param name="original">The GameObject to clone.</param>
		/// <param name="position">The position to set on the cloned GameObject.</param>
		/// <returns></returns>
		public static GameObject Clone(this GameObject original, Vector3 position)
		{
			return original.Clone(position, Quaternion.identity);
		}
	}
}