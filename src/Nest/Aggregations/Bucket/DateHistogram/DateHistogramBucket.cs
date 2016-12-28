using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class DateHistogramBucket : HistogramBucket
	{
        private static readonly long _epochTicks = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero).Ticks;
		
		// Get a DateTime form of the returned key
		public DateTime Date => new DateTime(_epochTicks + ((long)this.Key * TimeSpan.TicksPerMillisecond), DateTimeKind.Utc);
	}
}
