using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<TermsAggregation>))]
	public interface ITermsAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

		[JsonProperty("script")]
		IScript Script { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[JsonProperty("order")]
		IList<TermsOrder> Order { get; set; }

		[JsonProperty("include")]
		TermsIncludeExclude Include { get; set; }

		[JsonProperty("exclude")]
		TermsIncludeExclude Exclude { get; set; }

		[JsonProperty("collect_mode")]
		TermsAggregationCollectMode? CollectMode { get; set; }

		[JsonProperty("missing")]
		string Missing { get; set; }
	}

	public class TermsAggregation : BucketAggregationBase, ITermsAggregation
	{
		public Field Field { get; set; }
		public IScript Script { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }
		public IList<TermsOrder> Order { get; set; }
		public TermsIncludeExclude Include { get; set; }
		public TermsIncludeExclude Exclude { get; set; }
		public TermsAggregationCollectMode? CollectMode { get; set; }
		public string Missing { get; set; }

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

		IScript ITermsAggregation.Script { get; set; }

		int? ITermsAggregation.Size { get; set; }

		int? ITermsAggregation.ShardSize { get; set; }

		int? ITermsAggregation.MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ITermsAggregation.ExecutionHint { get; set; }

		IList<TermsOrder> ITermsAggregation.Order { get; set; }

		TermsIncludeExclude ITermsAggregation.Include { get; set; }

		TermsIncludeExclude ITermsAggregation.Exclude { get; set; }

		TermsAggregationCollectMode? ITermsAggregation.CollectMode { get; set; }

		string ITermsAggregation.Missing { get; set; }

		public TermsAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		public TermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public TermsAggregationDescriptor<T> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public TermsAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public TermsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public TermsAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public TermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint) =>
			Assign(a => a.ExecutionHint = executionHint);

		public TermsAggregationDescriptor<T> Order(TermsOrder order) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(order);
		});

		public TermsAggregationDescriptor<T> OrderAscending(string key) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(new TermsOrder { Key = key, Order = SortOrder.Ascending });
		});

		public TermsAggregationDescriptor<T> OrderDescending(string key) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(new TermsOrder { Key = key, Order = SortOrder.Descending });
		});

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

		public TermsAggregationDescriptor<T> Missing(string missing) => Assign(a => a.Missing = missing);
	}
}
