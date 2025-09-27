using System;

namespace WDK.Utility
{
	/// <summary>
	///     A class that tracks the time since it was created or last set.
	/// </summary>
	public class TimeSince
	{
		private readonly double _startTime;

		public TimeSince()
		{
			_startTime = DateTime.Now.Ticks / (double)TimeSpan.TicksPerSecond;
		}

		/// <summary>
		///     The time in seconds since this instance was created or last set.
		/// </summary>
		public float Value
		{
			get => (float)( DateTime.Now.Ticks / (double)TimeSpan.TicksPerSecond - _startTime );
			private init => _startTime = DateTime.Now.Ticks / (double)TimeSpan.TicksPerSecond - value;
		}

		public static implicit operator float(TimeSince timeSince)
		{
			return timeSince.Value;
		}

		public static implicit operator double(TimeSince timeSince)
		{
			return timeSince.Value;
		}

		public static implicit operator TimeSince(float value)
		{
			return new TimeSince { Value = value };
		}
	}
}