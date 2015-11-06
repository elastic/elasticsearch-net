using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SignificantTermsAggregation>))]
	public interface ISignificantTermsAggregation : IBucketAggregation
	{
		[JsonProperty("field")]
		Field Field { get; set; }

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
		IScriptedHeuristic Script { get; set; }

		[JsonProperty("background_filter")]
		IQueryContainer BackgroundFilter { get; set; }

	}

	public class SignificantTermsAggregation : BucketAggregationBase, ISignificantTermsAggregation
	{
		public Field Field { get; set; }
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
		public IScriptedHeuristic Script { get; set; }
		public IQueryContainer BackgroundFilter { get; set; }

		internal SignificantTermsAggregation() { }
		
		public SignificantTermsAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SignificantTerms = this;
	}

	public class SignificantTermsAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SignificantTermsAggregationDescriptor<T>, ISignificantTermsAggregation, T>
			, ISignificantTermsAggregation
		where T : class
	{
		Field ISignificantTermsAggregation.Field { get; set; }

		int? ISignificantTermsAggregation.Size { get; set; }

		int? ISignificantTermsAggregation.ShardSize { get; set; }

		int? ISignificantTermsAggregation.MinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ISignificantTermsAggregation.ExecutionHint { get; set; }

		IDictionary<string, string> ISignificantTermsAggregation.Include { get; set; }

		IDictionary<string, string> ISignificantTermsAggregation.Exclude { get; set; }

		MutualInformationHeuristic ISignificantTermsAggregation.MutualInformation { get; set; }

		ChiSquareHeuristic ISignificantTermsAggregation.ChiSquare { get; set; }

		GoogleNormalizedDistanceHeuristic ISignificantTermsAggregation.GoogleNormalizedDistance { get; set; }

		PercentageScoreHeuristic ISignificantTermsAggregation.PercentageScore { get; set; }

		IScriptedHeuristic ISignificantTermsAggregation.Script { get; set; }

		IQueryContainer ISignificantTermsAggregation.BackgroundFilter { get; set; }

		public SignificantTermsAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public SignificantTermsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public SignificantTermsAggregationDescriptor<T> MutualInformation(bool? backgroundIsSuperSet = null, bool? includeNegatives = null) =>
			Assign(a => a.MutualInformation = new MutualInformationHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			});

		public SignificantTermsAggregationDescriptor<T> ChiSquare(bool? backgroundIsSuperSet = null, bool? includeNegatives = null) =>
			Assign(a => a.ChiSquare = new ChiSquareHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
				IncludeNegatives = includeNegatives
			});

		public SignificantTermsAggregationDescriptor<T> GoogleNormalizedDistance(bool? backgroundIsSuperSet = null) =>
			Assign(a => a.GoogleNormalizedDistance = new GoogleNormalizedDistanceHeuristic
			{
				BackgroundIsSuperSet = backgroundIsSuperSet,
			});

		public SignificantTermsAggregationDescriptor<T> PercentageScore() => Assign(a => a.PercentageScore = new PercentageScoreHeuristic());

		public SignificantTermsAggregationDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, IScriptedHeuristic> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptedHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.BackgroundFilter = selector(new QueryContainerDescriptor<T>()));
	}
}