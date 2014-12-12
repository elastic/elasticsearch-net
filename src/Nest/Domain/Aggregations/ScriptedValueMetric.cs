
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public class ScriptedValueMetric : IMetricAggregation
	{
		internal object _Value { get; set; }

		public T Value<T>()
		{
			var jToken = this._Value as JToken;
			
			if (jToken != null)
				return jToken.ToObject<T>();

			return (T)this._Value;
		}
	}
}
