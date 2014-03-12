using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	public class TermsAggregationDescriptor<T> : BucketAggregationBaseDescriptor<TermsAggregationDescriptor<T>, T>
		where T : class
	{
		[JsonProperty("field")]
		internal PropertyPathMarker _Field { get; set; }
		
		public TermsAggregationDescriptor<T> Field(string field)
		{
			this._Field = field;
			return this;
		}

		public TermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			this._Field = field;
			return this;
		}

		[JsonProperty("script")]
		internal string _Script { get; set; }

		public TermsAggregationDescriptor<T> Script(string script)
		{
			this._Script = script;
			return this;
		}

		[JsonProperty("params")]
		internal FluentDictionary<string, object> _Params { get; set; }

		public TermsAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			this._Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}
		[JsonProperty("size")]
		internal int? _Size { get; set; }

		public TermsAggregationDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		
		[JsonProperty("shard_size")]
		internal int? _ShardSize { get; set; }

		public TermsAggregationDescriptor<T> ShardSize(int shardSize)
		{
			this._ShardSize = shardSize;
			return this;
		}

		[JsonProperty("min_doc_count")]
		internal int? _MinimumDocumentCount { get; set; }

		public TermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			this._MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		[JsonProperty("execution_hit")]
		internal TermsAggregationExecutionHint? _ExecutionHint { get; set; }

		public TermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint)
		{
			this._ExecutionHint = executionHint;
			return this;
		}

		[JsonProperty("order")]
		internal IDictionary<string, string> _Order { get; set; }

		public TermsAggregationDescriptor<T> OrderAscending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public TermsAggregationDescriptor<T> OrderDescending(string key)
		{
			this._Order = new Dictionary<string, string> { {key, "desc"}};
			return this;
		}

		[JsonProperty("include")]
		internal IDictionary<string, string> _Include { get; set; }

		public TermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null)
		{
			this._Include = new Dictionary<string, string> { {"pattern", includePattern}};
			if (!regexFlags.IsNullOrEmpty())
				this._Include.Add("pattern", regexFlags);
			return this;
		}
		
		[JsonProperty("exclude")]
		internal IDictionary<string, string> _Exclude { get; set; }

		public TermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null)
		{
			this._Exclude = new Dictionary<string, string> { {"pattern", excludePattern}};
			if (!regexFlags.IsNullOrEmpty())
				this._Exclude.Add("pattern", regexFlags);
			return this;
		}
	}
}