using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<ScriptedMetricAggregation>))]
	public interface IScriptedMetricAggregation : IMetricAggregation
	{
		[JsonProperty("init_script")]
		IScript InitScript { get; set; }
		
		[JsonProperty("map_script")]
		IScript MapScript { get; set; }

		[JsonProperty("combine_script")]
		IScript CombineScript { get; set; }

		[JsonProperty("reduce_script")]
		IScript ReduceScript { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }
	}

	public class ScriptedMetricAggregation : MetricAggregationBase, IScriptedMetricAggregation
	{
		public IScript InitScript { get; set; }
		public IScript MapScript { get; set; }
		public IScript CombineScript { get; set; }
		public IScript ReduceScript { get; set; }
		public IDictionary<string, object> Params { get; set; }

		internal ScriptedMetricAggregation() { }

		public ScriptedMetricAggregation(string name) : base(name, null) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ScriptedMetric = this;
	}
	public class ScriptedMetricAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ScriptedMetricAggregationDescriptor<T>, IScriptedMetricAggregation, T>
		, IScriptedMetricAggregation
		where T : class
	{
		IScript IScriptedMetricAggregation.InitScript { get; set; }
		IScript IScriptedMetricAggregation.MapScript { get; set; }
		IScript IScriptedMetricAggregation.CombineScript { get; set; }
		IScript IScriptedMetricAggregation.ReduceScript { get; set; }
		IDictionary<string, object> IScriptedMetricAggregation.Params { get; set; }

		public ScriptedMetricAggregationDescriptor<T> InitScript(string script) => Assign(a => a.InitScript = (InlineScript)script);
		public ScriptedMetricAggregationDescriptor<T> InitScript(Func<ScriptDescriptor, IScript> scriptSelector) => 
			Assign(a => a.InitScript = scriptSelector?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> MapScript(string script) => Assign(a => a.MapScript = (InlineScript)script);
		public ScriptedMetricAggregationDescriptor<T> MapScript(Func<ScriptDescriptor, IScript> scriptSelector) => 
			Assign(a => a.MapScript = scriptSelector?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> CombineScript(string script) => Assign(a => a.CombineScript = (InlineScript)script);
		public ScriptedMetricAggregationDescriptor<T> CombineScript(Func<ScriptDescriptor, IScript> scriptSelector) => 
			Assign(a => a.CombineScript = scriptSelector?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> ReduceScript(string script) => Assign(a => a.ReduceScript = (InlineScript)script);
		public ScriptedMetricAggregationDescriptor<T> ReduceScript(Func<ScriptDescriptor, IScript> scriptSelector) => 
			Assign(a => a.ReduceScript = scriptSelector?.Invoke(new ScriptDescriptor()));

		public ScriptedMetricAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
				Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));
	}
}
