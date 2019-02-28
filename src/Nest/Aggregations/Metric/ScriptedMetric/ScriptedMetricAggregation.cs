using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ScriptedMetricAggregation))]
	public interface IScriptedMetricAggregation : IMetricAggregation
	{
		[DataMember(Name ="combine_script")]
		IScript CombineScript { get; set; }

		[DataMember(Name ="init_script")]
		IScript InitScript { get; set; }

		[DataMember(Name ="map_script")]
		IScript MapScript { get; set; }

		[DataMember(Name ="params")]
		IDictionary<string, object> Params { get; set; }

		[DataMember(Name ="reduce_script")]
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

		public ScriptedMetricAggregationDescriptor<T>
			Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a => a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));
	}
}
