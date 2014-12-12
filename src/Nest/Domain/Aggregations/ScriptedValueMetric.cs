
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class ScriptedValueMetric : IMetricAggregation
	{
		public object Value { get; set; }

		public T ValueAs<T>()
		{
			var jToken = this.Value as JToken;
			
			if (jToken != null)
				return jToken.ToObject<T>();

			return (T)this.Value;
		}
	}
}
