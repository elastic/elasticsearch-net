using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{

	//todo interface!
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchAll : IQuery
	{
		[JsonProperty(PropertyName = "boost")]
		public double? Boost { get; internal set; }
		[JsonProperty(PropertyName = "norm_field")]
		public string NormField { get; internal set; }

		bool IQuery.IsConditionless { get { return false; } }

		public MatchAll() {
			
		}
	}
}
