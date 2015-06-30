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

		[JsonProperty("percentage")]
		PercentageScoreHeuristic PercentageScore { get; set; }

		[JsonProperty("script_heuristic")]
		ScriptedHeuristic Script { get; set; }

		[JsonProperty("background_filter")]
		IQueryContainer BackgroundFilter { get; set; }

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
		public PercentageScoreHeuristic PercentageScore { get; set; }
		public ScriptedHeuristic Script { get; set; }
		public IQueryContainer BackgroundFilter { get; set; }
	}

	public class SignificantTermsAggregatorDescriptor<T>
		: BucketAggregatorBaseDescriptor<SignificantTermsAggregatorDescriptor<T>, ISignificantTermsAggregator, T>
			, ISignificantTermsAggregator
		where T : class
	{
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

		PercentageScoreHeuristic ISignificantTermsAggregator.PercentageScore { get; set; }

		ScriptedHeuristic ISignificantTermsAggregator.Script { get; set; }

		IQueryContainer ISignificantTermsAggregator.BackgroundFilter { get; set; }

		public SignificantTermsAggregatorDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public SignificantTermsAggregatorDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public SignificantTermsAggregatorDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public SignificantTermsAggregatorDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public SignificantTermsAggregatorDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public SignificantTermsAggregatorDescriptor<T> MutualInformation(bool? backgroundIsSuperSet = null, bool? includeNegatives = null) =>
			Assign(a => a.MutualInformation = new MutualInformationHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			});

		public SignificantTermsAggregatorDescriptor<T> ChiSquare(bool? backgroundIsSuperSet = null, bool? includeNegatives = null) =>
			Assign(a => a.ChiSquare = new ChiSquareHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			});

		public SignificantTermsAggregatorDescriptor<T> GoogleNormalizedDistance(bool? backgroundIsSuperSet = null) =>
			Assign(a => a.GoogleNormalizedDistance = new GoogleNormalizedDistanceHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
			});

		public SignificantTermsAggregatorDescriptor<T> PercentageScore() => Assign(a => a.PercentageScore = new PercentageScoreHeuristic());

		public SignificantTermsAggregatorDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, ScriptedHeuristicDescriptor> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptedHeuristicDescriptor())?.ScriptedHeuristic);

		public SignificantTermsAggregatorDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.BackgroundFilter = selector(new QueryContainerDescriptor<T>()));
	}
}