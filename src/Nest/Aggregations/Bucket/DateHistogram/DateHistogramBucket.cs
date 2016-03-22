using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public class DateHistogramBucket : HistogramBucket
	{
		// Get a DateTime form of the returned key
		public DateTime Date => DateTime.SpecifyKind(new DateTime(1970, 1, 1).AddMilliseconds(0 + this.Key), DateTimeKind.Utc);
	}
}
