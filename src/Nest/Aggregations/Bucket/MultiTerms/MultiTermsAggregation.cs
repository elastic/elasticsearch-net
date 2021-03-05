// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(MultiTermsAggregation))]
	public interface IMultiTermsAggregation : IBucketAggregation
	{
		[DataMember(Name ="collect_mode")]
		TermsAggregationCollectMode? CollectMode { get; set; }
		
		[DataMember(Name ="min_doc_count")]
		int? MinimumDocumentCount { get; set; }
		
		[DataMember(Name ="order")]
		IList<TermsOrder> Order { get; set; }

		[DataMember(Name = "script")]
		IScript Script { get; set; }

		[DataMember(Name = "shard_min_doc_count")]
		int? ShardMinimumDocumentCount { get; set; }

		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }
		
		[DataMember(Name ="show_term_doc_count_error")]
		bool? ShowTermDocCountError { get; set; }

		[DataMember(Name ="size")]
		int? Size { get; set; }

		[DataMember(Name = "terms")]
		IEnumerable<ITerm> Terms { get; set; }
	}
	
	public class MultiTermsAggregation : BucketAggregationBase, IMultiTermsAggregation
	{
		internal MultiTermsAggregation() { }

		public MultiTermsAggregation(string name) : base(name) { }

		public TermsAggregationCollectMode? CollectMode { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public IList<TermsOrder> Order { get; set; }
		public IScript Script { get; set; }
		public int? ShardMinimumDocumentCount { get; set; }
		public int? ShardSize { get; set; }
		public bool? ShowTermDocCountError { get; set; }
		public int? Size { get; set; }
		public IEnumerable<ITerm> Terms { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.MultiTerms = this;
	}

	public class MultiTermsAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<MultiTermsAggregationDescriptor<T>, IMultiTermsAggregation, T>, IMultiTermsAggregation
		where T : class
	{
		TermsAggregationCollectMode? IMultiTermsAggregation.CollectMode { get; set; }
		int? IMultiTermsAggregation.MinimumDocumentCount { get; set; }
		IList<TermsOrder> IMultiTermsAggregation.Order { get; set; }
		IScript IMultiTermsAggregation.Script { get; set; }
		int? IMultiTermsAggregation.ShardMinimumDocumentCount { get; set; }
		int? IMultiTermsAggregation.ShardSize { get; set; }
		bool? IMultiTermsAggregation.ShowTermDocCountError { get; set; }
		int? IMultiTermsAggregation.Size { get; set; }
		IEnumerable<ITerm> IMultiTermsAggregation.Terms { get; set; }

		public MultiTermsAggregationDescriptor<T> CollectMode(TermsAggregationCollectMode? collectMode) =>
			Assign(collectMode, (a, v) => a.CollectMode = v);

		public MultiTermsAggregationDescriptor<T> MinimumDocumentCount(int? minimumDocumentCount) =>
			Assign(minimumDocumentCount, (a, v) => a.MinimumDocumentCount = v);

		public MultiTermsAggregationDescriptor<T> Order(Func<TermsOrderDescriptor<T>, IPromise<IList<TermsOrder>>> selector) =>
			Assign(selector, (a, v) => a.Order = v?.Invoke(new TermsOrderDescriptor<T>())?.Value);
		
		public MultiTermsAggregationDescriptor<T> Script(string script) => Assign((InlineScript)script, (a, v) => a.Script = v);

		public MultiTermsAggregationDescriptor<T> Script(Func<ScriptDescriptor, IScript> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptDescriptor()));

		public MultiTermsAggregationDescriptor<T> ShardMinimumDocumentCount(int? shardMinimumDocumentCount) =>
			Assign(shardMinimumDocumentCount, (a, v) => a.ShardMinimumDocumentCount = v);

		public MultiTermsAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		public MultiTermsAggregationDescriptor<T> ShowTermDocCountError(bool? showTermDocCountError = true) =>
			Assign(showTermDocCountError, (a, v) => a.ShowTermDocCountError = v);

		public MultiTermsAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public MultiTermsAggregationDescriptor<T> Terms(params ITerm[] ranges) =>
			Assign(ranges.ToListOrNullIfEmpty(), (a, v) => a.Terms = v);
		
		public MultiTermsAggregationDescriptor<T> Terms(params Func<TermDescriptor<T>, ITerm>[] ranges) =>
			Assign(ranges?.Select(r => r(new TermDescriptor<T>())).ToListOrNullIfEmpty(), (a, v) => a.Terms = v);

		public MultiTermsAggregationDescriptor<T> Terms(IEnumerable<Func<TermDescriptor<T>, ITerm>> ranges) =>
			Assign(ranges?.Select(r => r(new TermDescriptor<T>())).ToListOrNullIfEmpty(), (a, v) => a.Terms = v);
	}
}
