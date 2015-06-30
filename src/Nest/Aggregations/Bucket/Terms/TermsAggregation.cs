using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<TermsAggregator>))]
	public interface ITermsAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("script")]
		string Script { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[JsonProperty("order")]
		IDictionary<string, string> Order { get; set; }

		[JsonProperty("include")]
		TermsIncludeExclude Include { get; set; }

		[JsonProperty("exclude")]
		TermsIncludeExclude Exclude { get; set; }

		[JsonProperty("params")]
		IDictionary<string, object> Params { get; set; }

		[JsonProperty("collect_mode")]
		TermsAggregationCollectMode? CollectMode { get; set; }
	}

	public class TermsAggregator : BucketAggregator, ITermsAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public string Script { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }
		public IDictionary<string, string> Order { get; set; }
		public TermsIncludeExclude Include { get; set; }
		public TermsIncludeExclude Exclude { get; set; }
		public IDictionary<string, object> Params { get; set; }
		public TermsAggregationCollectMode? CollectMode { get; set; }
	}


	public class TermsAggregatorDescriptor<T> 
		: BucketAggregatorBaseDescriptor<TermsAggregatorDescriptor<T>, ITermsAggregator, T>
			, ITermsAggregator 
		where T : class
	{
		PropertyPathMarker ITermsAggregator.Field { get; set; }
		
		string ITermsAggregator.Script { get; set; }
		
		int? ITermsAggregator.Size { get; set; }

		int? ITermsAggregator.ShardSize { get; set; }

		int? ITermsAggregator.MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ITermsAggregator.ExecutionHint { get; set; }

		IDictionary<string, string> ITermsAggregator.Order { get; set; }

		TermsIncludeExclude ITermsAggregator.Include { get; set; }

		TermsIncludeExclude ITermsAggregator.Exclude { get; set; }

		IDictionary<string, object> ITermsAggregator.Params { get; set; }

		TermsAggregationCollectMode? ITermsAggregator.CollectMode { get; set; }

		public TermsAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public TermsAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);


		public TermsAggregatorDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public TermsAggregatorDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a=>a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public TermsAggregatorDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public TermsAggregatorDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermsAggregatorDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public TermsAggregatorDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint) =>
			Assign(a => a.ExecutionHint = executionHint);

		public TermsAggregatorDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "asc" } });

		public TermsAggregatorDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "desc" } });

		public TermsAggregatorDescriptor<T> Include(string includePattern, string regexFlags = null) =>
			Assign(a => a.Include = new TermsIncludeExclude() { Pattern = includePattern, Flags = regexFlags });

		public TermsAggregatorDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(a => a.Include = new TermsIncludeExclude { Values = values });

		public TermsAggregatorDescriptor<T> Exclude(string excludePattern, string regexFlags = null) =>
			Assign(a => a.Exclude = new TermsIncludeExclude() { Pattern = excludePattern, Flags = regexFlags });

		public TermsAggregatorDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(a => a.Exclude = new TermsIncludeExclude { Values = values });

		public TermsAggregatorDescriptor<T> CollectMode(TermsAggregationCollectMode collectMode) =>
			Assign(a => a.CollectMode = collectMode);

	}
}