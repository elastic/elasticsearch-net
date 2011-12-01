using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.Domain
{
	public class StatsParams
	{
		public StatsInfo InfoOn { get; set; }
		public bool Refresh { get; set; }
		public List<string> Types { get; set; }
		public List<string> Groups { get; set; }
	}
}
