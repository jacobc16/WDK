using UnityEditor;
using UnityEngine;

namespace WDK.Entities
{
	/// <summary>
	///     The base class for all entities in the game.
	///     Ideally this should be used alongside ISavable to ensure each entity has a unique identifier for saving and
	///     loading.
	///     You will need to call the OnSave and OnLoad methods yourself when saving/loading and set the Id property when
	///     instantiating from a save.
	/// </summary>
	public class Entity : MonoBehaviour
	{
		/// <summary>
		///     The unique identifier for this entity.
		/// </summary>
		public GUID Id { get; set; }

		protected virtual void Awake()
		{
			if (Id.Empty())
				Id = GUID.Generate();
		}
	}
}