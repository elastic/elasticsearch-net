using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<ScriptedMetricsAggregator>))]
	public interface IScriptedMetricAggregator
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

	public class ScriptedMetricAggregationDescriptor<T>
		: MetricAggregationBaseDescriptor<ScriptedMetricAggregationDescriptor<T>, T>, IScriptedMetricAggregator
		where T : class
	{
		IScriptedMetricAggregator Self { get { return this; } }

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

		public ScriptedMetricAggregationDescriptor<T> InitScript(string script)
		{
			this.Self.InitScript = script;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> InitScriptFile(string file)
		{
			this.Self.InitScriptFile = file;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> InitScriptId(string id)
		{
			this.Self.InitScriptId = id;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> MapScript(string script)
		{
			this.Self.MapScript = script;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> MapScriptFile(string file)
		{
			this.Self.MapScriptFile = file;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> MapScriptId(string id)
		{
			this.Self.MapScriptId = id;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> CombineScript(string script)
		{
			this.Self.CombineScript = script;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> CombineScriptFile(string file)
		{
			this.Self.CombineScriptFile = file;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> CombineScriptId(string id)
		{
			this.Self.CombineScriptId = id;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> ReduceScript(string script)
		{
			this.Self.ReduceScript = script;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> ReduceScriptFile(string file)
		{
			this.Self.ReduceScriptFile = file;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> ReduceScriptId(string id)
		{
			this.Self.ReduceScriptId = id;
			return this;
		}

		public ScriptedMetricAggregationDescriptor<T> ReduceParams(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this.Self.ReduceParams = paramSelector(new FluentDictionary<string, object>());
			return this;
		}
	}
}
