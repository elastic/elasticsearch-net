using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class SignificantTermsAggregationDescriptor<T> : BucketAggregationBaseDescriptor<SignificantTermsAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public SignificantTermsAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}
		
		[JsonProperty("size")]
		internal int? _Size { get; set; }

		public SignificantTermsAggregationDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		
		[JsonProperty("shard_size")]
		internal int? _ShardSize { get; set; }

		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize)
		{
			this._ShardSize = shardSize;
			return this;
		}

		[JsonProperty("min_doc_count")]
		internal int? _MinimumDocumentCount { get; set; }

		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			this._MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		[JsonProperty("execution_hit")]
		internal TermsAggregationExecutionHint? _ExecutionHint { get; set; }

		public SignificantTermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint)
		{
			this._ExecutionHint = executionHint;
			return this;
		}

		[JsonProperty("include")]
		internal IDictionary<string, string> _Include { get; set; }

		public SignificantTermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null)
		{
			this._Include = new Dictionary<string, string> { {"pattern", includePattern}};
			if (!regexFlags.IsNullOrEmpty())
				this._Include.Add("pattern", regexFlags);
			return this;
		}
		
		[JsonProperty("exclude")]
		internal IDictionary<string, string> _Exclude { get; set; }

		public SignificantTermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null)
		{
			this._Exclude = new Dictionary<string, string> { {"pattern", excludePattern}};
			if (!regexFlags.IsNullOrEmpty())
				this._Exclude.Add("pattern", regexFlags);
			return this;
		}
	}
}