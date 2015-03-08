using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class WeightFunction<T> : FunctionScoreFunction<T> where T : class
	{
		[JsonProperty(PropertyName = "weight")]
		internal double _Weight { get; set; }

		public WeightFunction(double weight)
		{
			_Weight = weight;
		}
	}
}
