using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TermsAggregation>))]
	public interface ITermsAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

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

	public class TermsAggregation : BucketAggregationBase, ITermsAggregation
	{
		public Field Field { get; set; }
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

		internal TermsAggregation() { }

		public TermsAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Terms = this;
	}

	public class TermsAggregationDescriptor<T> 
		: BucketAggregationDescriptorBase<TermsAggregationDescriptor<T>, ITermsAggregation, T>
			, ITermsAggregation 
		where T : class
	{
		Field ITermsAggregation.Field { get; set; }
		
		string ITermsAggregation.Script { get; set; }
		
		int? ITermsAggregation.Size { get; set; }

		int? ITermsAggregation.ShardSize { get; set; }

		int? ITermsAggregation.MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ITermsAggregation.ExecutionHint { get; set; }

		IDictionary<string, string> ITermsAggregation.Order { get; set; }

		TermsIncludeExclude ITermsAggregation.Include { get; set; }

		TermsIncludeExclude ITermsAggregation.Exclude { get; set; }

		IDictionary<string, object> ITermsAggregation.Params { get; set; }

		TermsAggregationCollectMode? ITermsAggregation.CollectMode { get; set; }

		public TermsAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public TermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);


		public TermsAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = script);

		public TermsAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector) =>
			Assign(a=>a.Params = paramSelector?.Invoke(new FluentDictionary<string, object>()));

		public TermsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public TermsAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public TermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint) =>
			Assign(a => a.ExecutionHint = executionHint);

		public TermsAggregationDescriptor<T> OrderAscending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "asc" } });

		public TermsAggregationDescriptor<T> OrderDescending(string key) =>
			Assign(a => a.Order = new Dictionary<string, string> { { key, "desc" } });

		public TermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null) =>
			Assign(a => a.Include = new TermsIncludeExclude() { Pattern = includePattern, Flags = regexFlags });

		public TermsAggregationDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(a => a.Include = new TermsIncludeExclude { Values = values });

		public TermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null) =>
			Assign(a => a.Exclude = new TermsIncludeExclude() { Pattern = excludePattern, Flags = regexFlags });

		public TermsAggregationDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(a => a.Exclude = new TermsIncludeExclude { Values = values });

		public TermsAggregationDescriptor<T> CollectMode(TermsAggregationCollectMode collectMode) =>
			Assign(a => a.CollectMode = collectMode);

	}
}