namespace WDK.Saving
{
	/// <summary>
	///     This class wraps data to be saved and loaded.
	/// </summary>
	/// <typeparam name="T">The type of data to be saved and loaded.</typeparam>
	public class SavableData<T>
	{
		/// <summary>
		///     The actual data to be saved and loaded.
		/// </summary>
		public T Data { get; set; }

		/// <summary>
		///     Creates a new instance of SavableData with default value of T.
		/// </summary>
		/// <returns>A new instance of SavableData with default value of T.</returns>
		public static SavableData<T> Create()
		{
			return new SavableData<T>
			{
				Data = default
			};
		}

		/// <summary>
		///     Creates a new instance of SavableData with the specified data.
		/// </summary>
		/// <param name="data">The data to be saved and loaded.</param>
		/// <returns>A new instance of SavableData with the specified data.</returns>
		public static SavableData<T> Create(T data)
		{
			return new SavableData<T>
			{
				Data = data
			};
		}

		public static implicit operator T(SavableData<T> savableData)
		{
			return savableData.Data;
		}

		public void Deconstruct(out T data)
		{
			data = Data;
		}
	}
}