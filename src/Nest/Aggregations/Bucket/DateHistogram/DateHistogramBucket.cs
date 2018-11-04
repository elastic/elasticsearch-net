using System;

namespace Nest
{
	public class DateHistogramBucket : KeyedBucket<double>
	{
		private static readonly long _epochTicks = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero).Ticks;

		// Get a DateTime form of the returned key
		public DateTime Date => new DateTime(_epochTicks + (long)Key * TimeSpan.TicksPerMillisecond, DateTimeKind.Utc);
	}
}
