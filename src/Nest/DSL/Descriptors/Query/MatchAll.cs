using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class MatchAll : IQuery
	{
		[JsonProperty(PropertyName = "boost")]
		public double? Boost { get; internal set; }
		[JsonProperty(PropertyName = "norm_field")]
		public string NormField { get; internal set; }
		
		public MatchAll() {
			
		}
	}
}
