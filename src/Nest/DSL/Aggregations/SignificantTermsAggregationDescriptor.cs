using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SignificantTermsAggregator>))]
	public interface ISignificantTermsAggregator : IBucketAggregator
	{
		[JsonProperty("field")]
		PropertyPathMarker Field { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		[JsonProperty("execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[JsonProperty("include")]
		IDictionary<string, string> Include { get; set; }

		[JsonProperty("exclude")]
		IDictionary<string, string> Exclude { get; set; }

		[JsonProperty("mutual_information")]
		MutualInformationHeuristic MutualInformation { get; set; }

		[JsonProperty("chi_square")]
		ChiSquareHeuristic ChiSquare { get; set; }

		[JsonProperty("gnd")]
		GoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }

		[JsonProperty("background_filter")]
		IFilterContainer BackgroundFilter { get; set; }

	}

	public class SignificantTermsAggregator : BucketAggregator, ISignificantTermsAggregator
	{
		public PropertyPathMarker Field { get; set; }
		public int? Size { get; set; }
		public int? ShardSize { get; set; }
		public int? MinimumDocumentCount { get; set; }
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }
		public IDictionary<string, string> Include { get; set; }
		public IDictionary<string, string> Exclude { get; set; }
		public MutualInformationHeuristic MutualInformation { get; set; }
		public ChiSquareHeuristic ChiSquare { get; set; }
		public GoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }
		public IFilterContainer BackgroundFilter { get; set; }
	}

	public class SignificantTermsAggregationDescriptor<T> : BucketAggregationBaseDescriptor<SignificantTermsAggregationDescriptor<T>, T>, ISignificantTermsAggregator where T : class
	{
		private ISignificantTermsAggregator Self { get { return this; } }

		PropertyPathMarker ISignificantTermsAggregator.Field { get; set; }
		
		int? ISignificantTermsAggregator.Size { get; set; }

		int? ISignificantTermsAggregator.ShardSize { get; set; }

		int? ISignificantTermsAggregator.MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ISignificantTermsAggregator.ExecutionHint { get; set; }

		IDictionary<string, string> ISignificantTermsAggregator.Include { get; set; }

		IDictionary<string, string> ISignificantTermsAggregator.Exclude { get; set; }

		MutualInformationHeuristic ISignificantTermsAggregator.MutualInformation { get; set; }

		ChiSquareHeuristic ISignificantTermsAggregator.ChiSquare { get; set; }

		GoogleNormalizedDistanceHeuristic ISignificantTermsAggregator.GoogleNormalizedDistance { get; set; }

		IFilterContainer ISignificantTermsAggregator.BackgroundFilter { get; set; }

		public SignificantTermsAggregationDescriptor<T> Field(string field)
		{
			Self.Field = field;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field)
		{
			Self.Field = field;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize)
		{
			Self.ShardSize = shardSize;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount)
		{
			Self.MinimumDocumentCount = minimumDocumentCount;
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint executionHint)
		{
			Self.ExecutionHint = executionHint;
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> Include(string includePattern, string regexFlags = null)
		{
			Self.Include = new Dictionary<string, string> { {"pattern", includePattern}};
			if (!regexFlags.IsNullOrEmpty())
				Self.Include.Add("pattern", regexFlags);
			return this;
		}
		
		public SignificantTermsAggregationDescriptor<T> Exclude(string excludePattern, string regexFlags = null)
		{
			Self.Exclude = new Dictionary<string, string> { {"pattern", excludePattern}};
			if (!regexFlags.IsNullOrEmpty())
				Self.Exclude.Add("pattern", regexFlags);
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> MutualInformation(bool? backgroundIsSuperSet = null, bool? includeNegatives = null)
		{
			Self.MutualInformation = new MutualInformationHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			};
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> ChiSquare(bool? backgroundIsSuperSet = null, bool? includeNegatives = null)
		{
			Self.ChiSquare = new ChiSquareHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			};
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> GoogleNormalizedDistance(bool? backgroundIsSuperSet = null)
		{
			Self.GoogleNormalizedDistance = new GoogleNormalizedDistanceHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
			};
			return this;
		}

		public SignificantTermsAggregationDescriptor<T> BackgroundFilter(Func<FilterDescriptor<T>, FilterContainer> selector)
		{
			this.Self.BackgroundFilter = selector(new FilterDescriptor<T>());
			return this;
		}
	}
}