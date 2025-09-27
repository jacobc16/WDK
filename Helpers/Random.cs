using System;

namespace WDK.Helpers
{
	public static class Random
	{
		/// <summary>
		///     The current seed.
		/// </summary>
		public static string Seed { get; private set; }

		/// <summary>
		///     Generates a new random seed based on the current time.
		/// </summary>
		/// <returns></returns>
		public static string GenerateSeed()
		{
			var seed = DateTime.Now.Ticks.ToString();
			SetSeed(seed);

			return seed;
		}

		/// <summary>
		///     Sets the random seed for Unity's Random class.
		/// </summary>
		/// <param name="seed"></param>
		public static void SetSeed(string seed)
		{
			Seed = seed;

			var newSeed = int.TryParse(seed, out var num) ? num : seed.GetHashCode();

			UnityEngine.Random.InitState(newSeed);
		}

		/// <inheritdoc cref="SetSeed(string)" />
		public static void SetSeed(int seed)
		{
			Seed = seed.ToString();
			UnityEngine.Random.InitState(seed);
		}
	}
}