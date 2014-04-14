using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class RandomScoreFunction
	{
		[JsonProperty(PropertyName = "seed")]
		internal int? _Seed { get; set; }

		public RandomScoreFunction(int seed)
		{
			_Seed = seed;
		}

		public RandomScoreFunction()
		{
		}
	}
}