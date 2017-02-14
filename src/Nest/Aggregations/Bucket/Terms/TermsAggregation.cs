using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<TermsAggregation<object>>))]
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
	}

	public interface ITermsAggregation<TFieldType> : ITermsAggregation
	{
		[JsonProperty("missing")]
		TFieldType Missing { get; set; }
	}

	public class TermsAggregation<TFieldType> : BucketAggregationBase, ITermsAggregation<TFieldType>
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
		public TFieldType Missing { get; set; }

		internal TermsAggregation() { }

		public TermsAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.Terms = this;
	}

	public class TermsAggregation : TermsAggregation<string>
	{
		public TermsAggregation(string name) : base(name) { }
	}

	public class TermsAggregationDescriptor<T, TFieldType>
		: BucketAggregationDescriptorBase<TermsAggregationDescriptor<T, TFieldType>, ITermsAggregation<TFieldType>, T>
			, ITermsAggregation<TFieldType>
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

		TFieldType ITermsAggregation<TFieldType>.Missing { get; set; }

		public TermsAggregationDescriptor<T, TFieldType> Field(Field field) => Assign(a => a.Field = field);

		public TermsAggregationDescriptor<T, TFieldType> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public TermsAggregationDescriptor<T, TFieldType> Script(string script) => Assign(a => a.Script = (InlineScript)script);

		public TermsAggregationDescriptor<T, TFieldType> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptDescriptor()));

		public TermsAggregationDescriptor<T, TFieldType> Size(int size) => Assign(a => a.Size = size);

		public TermsAggregationDescriptor<T, TFieldType> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public TermsAggregationDescriptor<T, TFieldType> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public TermsAggregationDescriptor<T, TFieldType> ExecutionHint(TermsAggregationExecutionHint executionHint) =>
			Assign(a => a.ExecutionHint = executionHint);

		public TermsAggregationDescriptor<T, TFieldType> Order(TermsOrder order) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(order);
		});

		public TermsAggregationDescriptor<T, TFieldType> OrderAscending(string key) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(new TermsOrder { Key = key, Order = SortOrder.Ascending });
		});

		public TermsAggregationDescriptor<T, TFieldType> OrderDescending(string key) => Assign(a =>
		{
			a.Order = a.Order ?? new List<TermsOrder>();
			a.Order.Add(new TermsOrder { Key = key, Order = SortOrder.Descending });
		});

		public TermsAggregationDescriptor<T, TFieldType> Include(long partion, long numberOfPartitions) =>
			Assign(a => a.Include = new TermsIncludeExclude() {Partition = partion, NumberOfPartitions = numberOfPartitions});

		public TermsAggregationDescriptor<T, TFieldType> Include(string includePattern, string regexFlags = null) =>
			Assign(a => a.Include = new TermsIncludeExclude() { Pattern = includePattern, Flags = regexFlags });

		public TermsAggregationDescriptor<T, TFieldType> Include(IEnumerable<string> values) =>
			Assign(a => a.Include = new TermsIncludeExclude { Values = values });

		public TermsAggregationDescriptor<T, TFieldType> Exclude(string excludePattern, string regexFlags = null) =>
			Assign(a => a.Exclude = new TermsIncludeExclude() { Pattern = excludePattern, Flags = regexFlags });

		public TermsAggregationDescriptor<T, TFieldType> Exclude(IEnumerable<string> values) =>
			Assign(a => a.Exclude = new TermsIncludeExclude { Values = values });

		public TermsAggregationDescriptor<T, TFieldType> CollectMode(TermsAggregationCollectMode collectMode) =>
			Assign(a => a.CollectMode = collectMode);

		public TermsAggregationDescriptor<T, TFieldType> Missing(TFieldType missing) => Assign(a => a.Missing = missing);
	}
}
