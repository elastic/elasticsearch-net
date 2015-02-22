using System;
using System.Collections.Generic;
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


	public class TermsAggregationDescriptor<T> : BucketAggregationBaseDescriptor<TermsAggregationDescriptor<T>, T>, ITermsAggregator where T : class
	{
		private ITermsAggregator Self { get { return this; } }

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
		
		public TermsAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public TermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}


		public TermsAggregationDescriptor<T> Script(string script)
		{
			Self.Script = script;
			return this;
		}

		public TermsAggregationDescriptor<T> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramSelector)
		{
			Self.Params = paramSelector(new FluentDictionary<string, object>());
			return this;
		}

		public TermsAggregationDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}
		
		public TermsAggregationDescriptor<T> ShardSize(int shardSize)
		{
			Self.ShardSize = shardSize;
			return this;
		}

		public TermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			Self.MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		public TermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint)
		{
			Self.ExecutionHint = executionHint;
			return this;
		}

		public TermsAggregationDescriptor<T> OrderAscending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "asc"}};
			return this;
		}
	
		public TermsAggregationDescriptor<T> OrderDescending(string key)
		{
			Self.Order = new Dictionary<string, string> { {key, "desc"}};
			return this;
		}

		public TermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null)
		{
			Self.Include = new TermsIncludeExclude() { Pattern = includePattern, Flags = regexFlags };
			return this;
		}
		
		public TermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null)
		{
			Self.Exclude = new TermsIncludeExclude() { Pattern = excludePattern, Flags = regexFlags };
			return this;
		}
		public TermsAggregationDescriptor<T> CollectMode(TermsAggregationCollectMode collectMode)
		{
			Self.CollectMode = collectMode;
			return this;
		}
	}
}