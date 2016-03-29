
using System;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ScriptedMetricAggregate : MetricAggregateBase
	{
		internal object _Value { get; set; }

		/// <summary>
		/// Get the result of the scripted metric aggregation as T
		/// </summary>
		/// <typeparam name="T">The type that best represents the result of your scripted metric aggrgation</typeparam>
		public T Value<T>()
		{
			var jToken = this._Value as JToken;
			return jToken != null
				? jToken.ToObject<T>()
				: (T)Convert.ChangeType(this._Value, typeof(T));
		}
	}
}
