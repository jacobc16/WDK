namespace WDK.Saving
{
	/// <summary>
	///     This interface is for objects that can be saved and loaded.
	///     Entity class has an GUID to uniquely identify each instance.
	/// </summary>
	/// <typeparam name="T">The type of data to be saved and loaded.</typeparam>
	public interface ISavable<T>
	{
		/// <summary>
		///     This method is called when saving the object to a savable state.
		///     You will need to call this method yourself when saving.
		/// </summary>
		/// <returns></returns>
		public SavableData<T> OnSave();

		/// <summary>
		///     This methoedd is called when loading the object from a saved state.
		///     You will need to call this method yourself after instantiating the object.
		/// </summary>
		/// <param name="data">The the saved data to load from.</param>
		public void OnLoad(SavableData<T> data);
	}
}