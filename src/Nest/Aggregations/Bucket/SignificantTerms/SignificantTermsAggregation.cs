using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SignificantTermsAggregation>))]
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
		IMutualInformationHeuristic MutualInformation { get; set; }

		[JsonProperty("chi_square")]
		IChiSquareHeuristic ChiSquare { get; set; }

		[JsonProperty("gnd")]
		IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }

		[JsonProperty("percentage")]
		IPercentageScoreHeuristic PercentageScore { get; set; }

		[JsonProperty("script_heuristic")]
		IScriptedHeuristic Script { get; set; }

		[JsonProperty("background_filter")]
		QueryContainer BackgroundFilter { get; set; }

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
		public IMutualInformationHeuristic MutualInformation { get; set; }
		public IChiSquareHeuristic ChiSquare { get; set; }
		public IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }
		public IPercentageScoreHeuristic PercentageScore { get; set; }
		public IScriptedHeuristic Script { get; set; }
		public QueryContainer BackgroundFilter { get; set; }

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

		IMutualInformationHeuristic ISignificantTermsAggregation.MutualInformation { get; set; }

		IChiSquareHeuristic ISignificantTermsAggregation.ChiSquare { get; set; }

		IGoogleNormalizedDistanceHeuristic ISignificantTermsAggregation.GoogleNormalizedDistance { get; set; }

		IPercentageScoreHeuristic ISignificantTermsAggregation.PercentageScore { get; set; }

		IScriptedHeuristic ISignificantTermsAggregation.Script { get; set; }

		QueryContainer ISignificantTermsAggregation.BackgroundFilter { get; set; }

		public SignificantTermsAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		public SignificantTermsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public SignificantTermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint? hint) => Assign(a => a.ExecutionHint = hint);

		public SignificantTermsAggregationDescriptor<T> Include(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> include) => 
			Assign(a => a.Include = include?.Invoke(new FluentDictionary<string, string>()));

		public SignificantTermsAggregationDescriptor<T> Exclude(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> exclude) => 
			Assign(a => a.Exclude = exclude?.Invoke(new FluentDictionary<string, string>()));

		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		public SignificantTermsAggregationDescriptor<T> MutualInformation(Func<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic> mutualInformationSelector = null) =>
			Assign(a => a.MutualInformation = mutualInformationSelector.InvokeOrDefault(new MutualInformationHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> ChiSquare(Func<ChiSquareHeuristicDescriptor, IChiSquareHeuristic> chiSquareSelector) =>
			Assign(a => a.ChiSquare = chiSquareSelector.InvokeOrDefault(new ChiSquareHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> GoogleNormalizedDistance(Func<GoogleNormalizedDistanceHeuristicDescriptor, IGoogleNormalizedDistanceHeuristic> gndSelector) =>
			Assign(a => a.GoogleNormalizedDistance = gndSelector.InvokeOrDefault(new GoogleNormalizedDistanceHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> PercentageScore(Func<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic> percentageScoreSelector) =>
			Assign(a => a.PercentageScore = percentageScoreSelector.InvokeOrDefault(new PercentageScoreHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, IScriptedHeuristic> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptedHeuristicDescriptor()));

		public SignificantTermsAggregationDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.BackgroundFilter = selector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}