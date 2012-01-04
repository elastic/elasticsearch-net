using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Domain
{
	public class OptimizeParams
	{
		public int MaximumSegments { get; set; }
		public bool OnlyExpungeDeletes { get; set; }
		public bool Refresh { get; set; }
		public bool Flush { get; set; }
		public bool WaitForMerge { get; set; }
		public OptimizeParams()
		{
			this.MaximumSegments = 1;
			this.OnlyExpungeDeletes = false;
			this.Refresh = true;
			this.Flush = true;
			this.WaitForMerge = true;
		}
	}
}
