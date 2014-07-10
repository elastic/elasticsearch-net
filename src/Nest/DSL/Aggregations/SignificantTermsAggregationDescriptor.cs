using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SignificantTermsAggregator>))]
	public interface ISignificantTermsAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker _Field { get; set; }

		[JsonProperty("size")]
		int? _Size { get; set; }

		[JsonProperty("shard_size")]
		int? _ShardSize { get; set; }

		[JsonProperty("min_doc_count")]
		int? _MinimumDocumentCount { get; set; }

		[JsonProperty("execution_hit")]
		TermsAggregationExecutionHint? _ExecutionHint { get; set; }

		[JsonProperty("include")]
		IDictionary<string, string> _Include { get; set; }

		[JsonProperty("exclude")]
		IDictionary<string, string> _Exclude { get; set; }
	}

	public class SignificantTermsAggregator : BucketAggregator, ISignificantTermsAggregator
	{
		public PropertyPathMarker _Field { get; set; }
		public int? _Size { get; set; }
		public int? _ShardSize { get; set; }
		public int? _MinimumDocumentCount { get; set; }
		public TermsAggregationExecutionHint? _ExecutionHint { get; set; }
		public IDictionary<string, string> _Include { get; set; }
		public IDictionary<string, string> _Exclude { get; set; }
	}

	public class SignificantTermsAggregationDescriptor<T> : BucketAggregationBaseDescriptor<SignificantTermsAggregationDescriptor<T>, T>, ISignificantTermsAggregator where T : class
	{
		private ISignificantTermsAggregator Self { get { return this; } }

		PropertyPathMarker ISignificantTermsAggregator._Field { get; set; }
		
		int? ISignificantTermsAggregator._Size { get; set; }

		int? ISignificantTermsAggregator._ShardSize { get; set; }

		int? ISignificantTermsAggregator._MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ISignificantTermsAggregator._ExecutionHint { get; set; }

		IDictionary<string, string> ISignificantTermsAggregator._Include { get; set; }

		IDictionary<string, string> ISignificantTermsAggregator._Exclude { get; set; }

		public SignificantTermsAggregationDescriptor<T> Field(string field)
		{
			Self._Field = field;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self._Field = field;
			return this;
		}
		

		public SignificantTermsAggregationDescriptor<T> Size(int size)
		{
			Self._Size = size;
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize)
		{
			Self._ShardSize = shardSize;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			Self._MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint)
		{
			Self._ExecutionHint = executionHint;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null)
		{
			Self._Include = new Dictionary<string, string> { {"pattern", includePattern}};
			if (!regexFlags.IsNullOrEmpty())
				Self._Include.Add("pattern", regexFlags);
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null)
		{
			Self._Exclude = new Dictionary<string, string> { {"pattern", excludePattern}};
			if (!regexFlags.IsNullOrEmpty())
				Self._Exclude.Add("pattern", regexFlags);
			return this;
		}
	}
}