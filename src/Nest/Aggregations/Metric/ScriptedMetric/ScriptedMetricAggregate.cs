
using System;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class ScriptedMetricAggregate : MetricAggregateBase
	{
		private readonly object _value;
		internal ScriptedMetricAggregate(object value) => _value = value;
		public ScriptedMetricAggregate() { }

		/// <summary>
		/// Get the result of the scripted metric aggregation as T
		/// </summary>
		/// <typeparam name="T">The type that best represents the result of your scripted metric aggrgation</typeparam>
		public T Value<T>()
		{
			return this._value is LazyDocument lazyDocument
				? lazyDocument.As<T>()
				: (T)Convert.ChangeType(this._value, typeof(T));
		}
	}
}
