using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptedMetricsAggregator>))]
	public interface IScriptedMetricAggregator : IMetricAggregator
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

	public class ScriptedMetricsAggregator : MetricAggregator, IScriptedMetricAggregator
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
	}

	public class ScriptedMetricsAgg : MetricAgg, IScriptedMetricAggregator
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

		public ScriptedMetricsAgg(string name, Field field) : base(name, field) { }

		internal override void WrapInContainer(AggregationContainer c) => c.ScriptedMetric = this;
	}
	public class ScriptedMetricAggregatorDescriptor<T>
		: MetricAggregationBaseDescriptor<ScriptedMetricAggregatorDescriptor<T>, IScriptedMetricAggregator, T>
		, IScriptedMetricAggregator
		where T : class
	{
		string IScriptedMetricAggregator.InitScript { get; set; }
		string IScriptedMetricAggregator.InitScriptFile { get; set; }
		string IScriptedMetricAggregator.InitScriptId { get; set; }
		string IScriptedMetricAggregator.MapScript { get; set; }
		string IScriptedMetricAggregator.MapScriptFile { get; set; }
		string IScriptedMetricAggregator.MapScriptId { get; set; }
		string IScriptedMetricAggregator.CombineScript { get; set; }
		string IScriptedMetricAggregator.CombineScriptFile { get; set; }
		string IScriptedMetricAggregator.CombineScriptId { get; set; }
		string IScriptedMetricAggregator.ReduceScript { get; set; }
		string IScriptedMetricAggregator.ReduceScriptFile { get; set; }
		string IScriptedMetricAggregator.ReduceScriptId { get; set; }
		IDictionary<string, object> IScriptedMetricAggregator.ReduceParams { get; set; }

		public ScriptedMetricAggregatorDescriptor<T> InitScript(string script) => Assign(a => a.InitScript = script);

		public ScriptedMetricAggregatorDescriptor<T> InitScriptFile(string file) => Assign(a => a.InitScriptFile = file);

		public ScriptedMetricAggregatorDescriptor<T> InitScriptId(string id) => Assign(a => a.InitScriptId = id);

		public ScriptedMetricAggregatorDescriptor<T> MapScript(string script) => Assign(a => a.MapScript = script);

		public ScriptedMetricAggregatorDescriptor<T> MapScriptFile(string file) => Assign(a => a.MapScriptFile = file);

		public ScriptedMetricAggregatorDescriptor<T> MapScriptId(string id) => Assign(a => a.MapScriptId = id);

		public ScriptedMetricAggregatorDescriptor<T> CombineScript(string script) => Assign(a => a.CombineScript = script);

		public ScriptedMetricAggregatorDescriptor<T> CombineScriptFile(string file) => Assign(a => a.CombineScriptFile = file);

		public ScriptedMetricAggregatorDescriptor<T> CombineScriptId(string id) => Assign(a => a.CombineScriptId = id);

		public ScriptedMetricAggregatorDescriptor<T> ReduceScript(string script) => Assign(a => a.ReduceScript = script);

		public ScriptedMetricAggregatorDescriptor<T> ReduceScriptFile(string file) => Assign(a => a.ReduceScriptFile = file);

		public ScriptedMetricAggregatorDescriptor<T> ReduceScriptId(string id) => Assign(a => a.InitScriptId = id);

		public ScriptedMetricAggregatorDescriptor<T> ReduceParams(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
				Assign(a => a.ReduceParams = paramSelector(new FluentDictionary<string, object>()));
	}
}
