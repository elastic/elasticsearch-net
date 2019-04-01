using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ScriptedMetricAggregation>))]
	public interface IScriptedMetricAggregation : IMetricAggregation
	{
		[JsonProperty("combine_script")]
		IScript CombineScript { get; set; }

		[JsonProperty("init_script")]
		IScript InitScript { get; set; }

		[JsonProperty("map_script")]
		IScript MapScript { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("reduce_script")]
		IScript ReduceScript { get; set; }
	}

	public class ScriptedMetricAggregation : MetricAggregationBase, IScriptedMetricAggregation
	{
		internal ScriptedMetricAggregation() { }

		public ScriptedMetricAggregation(string name) : base(name, null) { }

		public IScript CombineScript { get; set; }
		public IScript InitScript { get; set; }
		public IScript MapScript { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public IScript ReduceScript { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.ScriptedMetric = this;
	}

	public class ScriptedMetricAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ScriptedMetricAggregationDescriptor<T>, IScriptedMetricAggregation, T>
			, IScriptedMetricAggregation
		where T : class
	{
		IScript IScriptedMetricAggregation.CombineScript { get; set; }
		IScript IScriptedMetricAggregation.InitScript { get; set; }
		IScript IScriptedMetricAggregation.MapScript { get; set; }
		IDictionary<string, object> IScriptedMetricAggregation.Params { get; set; }
		IScript IScriptedMetricAggregation.ReduceScript { get; set; }

		public ScriptedMetricAggregationDescriptor<T> InitScript(string script) => Assign((InlineScript)script, (a, v) => a.InitScript = v);

		public ScriptedMetricAggregationDescriptor<T> InitScript(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.InitScript = v?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> MapScript(string script) => Assign((InlineScript)script, (a, v) => a.MapScript = v);

		public ScriptedMetricAggregationDescriptor<T> MapScript(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.MapScript = v?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> CombineScript(string script) => Assign((InlineScript)script, (a, v) => a.CombineScript = v);

		public ScriptedMetricAggregationDescriptor<T> CombineScript(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.CombineScript = v?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> ReduceScript(string script) => Assign((InlineScript)script, (a, v) => a.ReduceScript = v);

		public ScriptedMetricAggregationDescriptor<T> ReduceScript(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.ReduceScript = v?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T>
			Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(paramSelector, (a, v) => a.Params = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
