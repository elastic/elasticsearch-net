using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptedMetricsAggregation>))]
	public interface IScriptedMetricAggregation : IMetricAggregation
	{
		[JsonProperty("init_script")]
		string InitScript { get; set; }

		[JsonProperty("init_script_file")]
		string InitScriptFile { get; set; }
		
		[JsonProperty("init_script_id")]
		string InitScriptId { get; set; }
		
		[JsonProperty("map_script")]
		string MapScript { get; set; }
		
		[JsonProperty("map_script_file")]
		string MapScriptFile { get; set; }
		
		[JsonProperty("map_script_id")]
		string MapScriptId { get; set; }
		
		[JsonProperty("combine_script")]
		string CombineScript { get; set; }
		
		[JsonProperty("combine_script_file")]
		string CombineScriptFile { get; set; }
		
		[JsonProperty("combine_script_id")]
		string CombineScriptId { get; set; }
		
		[JsonProperty("reduce_script")]
		string ReduceScript { get; set; }
		
		[JsonProperty("reduce_script_file")]
		string ReduceScriptFile { get; set; }
		
		[JsonProperty("reduce_script_id")]
		string ReduceScriptId { get; set; }
		
		[JsonProperty("reduce_params")]
		IDictionary<string, object> ReduceParams { get; set; }
	}

	public class ScriptedMetricsAggregation : MetricAggregation, IScriptedMetricAggregation
	{
		public string InitScript { get; set; }
		public string InitScriptFile { get; set; }
		public string InitScriptId { get; set; }
		public string MapScript { get; set; }
		public string MapScriptFile { get; set; }
		public string MapScriptId { get; set; }
		public string CombineScript { get; set; }
		public string CombineScriptFile { get; set; }
		public string CombineScriptId { get; set; }
		public string ReduceScript { get; set; }
		public string ReduceScriptFile { get; set; }
		public string ReduceScriptId { get; set; }
		public IDictionary<string, object> ReduceParams { get; set; }

		public ScriptedMetricsAggregation(string name, FieldName field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ScriptedMetric = this;
	}
	public class ScriptedMetricAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<ScriptedMetricAggregationDescriptor<T>, IScriptedMetricAggregation, T>
		, IScriptedMetricAggregation
		where T : class
	{
		string IScriptedMetricAggregation.InitScript { get; set; }
		string IScriptedMetricAggregation.InitScriptFile { get; set; }
		string IScriptedMetricAggregation.InitScriptId { get; set; }
		string IScriptedMetricAggregation.MapScript { get; set; }
		string IScriptedMetricAggregation.MapScriptFile { get; set; }
		string IScriptedMetricAggregation.MapScriptId { get; set; }
		string IScriptedMetricAggregation.CombineScript { get; set; }
		string IScriptedMetricAggregation.CombineScriptFile { get; set; }
		string IScriptedMetricAggregation.CombineScriptId { get; set; }
		string IScriptedMetricAggregation.ReduceScript { get; set; }
		string IScriptedMetricAggregation.ReduceScriptFile { get; set; }
		string IScriptedMetricAggregation.ReduceScriptId { get; set; }
		IDictionary<string, object> IScriptedMetricAggregation.ReduceParams { get; set; }

		public ScriptedMetricAggregationDescriptor<T> InitScript(string script) => Assign(a => a.InitScript = script);

		public ScriptedMetricAggregationDescriptor<T> InitScriptFile(string file) => Assign(a => a.InitScriptFile = file);

		public ScriptedMetricAggregationDescriptor<T> InitScriptId(string id) => Assign(a => a.InitScriptId = id);

		public ScriptedMetricAggregationDescriptor<T> MapScript(string script) => Assign(a => a.MapScript = script);

		public ScriptedMetricAggregationDescriptor<T> MapScriptFile(string file) => Assign(a => a.MapScriptFile = file);

		public ScriptedMetricAggregationDescriptor<T> MapScriptId(string id) => Assign(a => a.MapScriptId = id);

		public ScriptedMetricAggregationDescriptor<T> CombineScript(string script) => Assign(a => a.CombineScript = script);

		public ScriptedMetricAggregationDescriptor<T> CombineScriptFile(string file) => Assign(a => a.CombineScriptFile = file);

		public ScriptedMetricAggregationDescriptor<T> CombineScriptId(string id) => Assign(a => a.CombineScriptId = id);

		public ScriptedMetricAggregationDescriptor<T> ReduceScript(string script) => Assign(a => a.ReduceScript = script);

		public ScriptedMetricAggregationDescriptor<T> ReduceScriptFile(string file) => Assign(a => a.ReduceScriptFile = file);

		public ScriptedMetricAggregationDescriptor<T> ReduceScriptId(string id) => Assign(a => a.InitScriptId = id);

		public ScriptedMetricAggregationDescriptor<T> ReduceParams(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
				Assign(a => a.ReduceParams = paramSelector(new FluentDictionary<string, object>()));
	}
}
