using System;
using UnityEngine;

namespace WDK.Utility
{
	public class TimeUntil
	{
		public TimeUntil()
		{
			StartTime = DateTime.Now.Ticks / (double)TimeSpan.TicksPerSecond;
		}

		/// <summary>
		///     The time since this instance was created or last set.
		/// </summary>
		public double StartTime { get; }

		/// <summary>
		///     The number of seconds we are counting to.
		/// </summary>
		public float Duration { get; init; }

		/// <summary>
		///     Returns true if the specified duration has passed since the creation of this TimeUntil instance or since the last
		///     reset.
		/// </summary>
		public bool Passed => DateTime.Now.Ticks / (double)TimeSpan.TicksPerSecond - StartTime >= Duration;

		/// <summary>
		///     The fraction of time passed, from 0 to 1.
		/// </summary>
		public float Fraction => Mathf.Clamp01((float)StartTime / Duration);

		public static implicit operator bool(TimeUntil timeUntil)
		{
			return timeUntil?.Passed ?? false;
		}

		public static implicit operator TimeUntil(float value)
		{
			return new TimeUntil { Duration = value };
		}
	}
}