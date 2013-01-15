using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class HealthParams
	{
		public HealthLevel? CheckLevel { get; set; }
		public HealthStatus? WaitForStatus { get; set; }
		public int? WaitForRelocatingShards { get; set; }
		public int? WaitForMinNodes { get; set; }
		public string Timeout { get; set; }
	}
}
