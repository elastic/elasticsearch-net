using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SignificantTermsAggregation>))]
	public interface ISignificantTermsAggregation : IBucketAggregation
	{
		/// <summary>
		/// The field on which to run the aggregation
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// Defines how many term buckets should be returned out of the overall
		/// terms list
		/// </summary>
		[JsonProperty("size")]
		int? Size { get; set; }

		/// <summary>
		/// Controls the number of candidate terms produced by each shard from which
		/// the <see cref="Size"/> of terms is selected.
		/// </summary>
		[JsonProperty("shard_size")]
		int? ShardSize { get; set; }

		/// <summary>
		/// Return only terms that match equal to or more than a configurable
		/// number of hits
		/// </summary>
		[JsonProperty("min_doc_count")]
		int? MinimumDocumentCount { get; set; }

		/// <summary>
		/// Determines the mechanism by which aggregations are executed
		/// </summary>
		[JsonProperty("execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[Obsolete("Deprecated and will be fixed in 6.0. Use IncludeTerms")]
		IDictionary<string, string> Include { get; set; }

		/// <summary>
		/// Include term values for which buckets will be created.
		/// </summary>
		[JsonProperty("include")]
		SignificantTermsIncludeExclude IncludeTerms { get; set; }

		[Obsolete("Deprecated and will be fixed in 6.0. Use ExcludeTerms")]
		IDictionary<string, string> Exclude { get; set; }

		/// <summary>
		/// Exclude term values for which buckets will be created.
		/// </summary>
		[JsonProperty("exclude")]
		SignificantTermsIncludeExclude ExcludeTerms { get; set; }

		/// <summary>
		/// Use mutual information to calculate significance score
		/// </summary>
		[JsonProperty("mutual_information")]
		IMutualInformationHeuristic MutualInformation { get; set; }

		/// <summary>
		/// Use chi square to calculate significance score
		/// </summary>
		[JsonProperty("chi_square")]
		IChiSquareHeuristic ChiSquare { get; set; }

		/// <summary>
		/// Use Google normalized distance to calculate significance score
		/// </summary>
		[JsonProperty("gnd")]
		IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }

		/// <summary>
		/// Use percentage to calculate significance score.
		/// <para>A simple calculation of the number of documents in the foreground
		/// sample with a term divided by the number of documents in the background
		/// with the term. By default this produces a score greater than zero
		///  and less than one.</para>
		/// </summary>
		[JsonProperty("percentage")]
		IPercentageScoreHeuristic PercentageScore { get; set; }

		/// <summary>
		/// Use a script to calculate a custom significance score.
		/// </summary>
		[JsonProperty("script_heuristic")]
		IScriptedHeuristic Script { get; set; }

		/// <summary>
		/// The default source of statistical information for background term
		/// frequencies is the entire index. This scope can be narrowed
		/// through the use of a background filter to focus in on significant
		/// terms within a narrower context
		/// </summary>
		[JsonProperty("background_filter")]
		QueryContainer BackgroundFilter { get; set; }

	}

	public class SignificantTermsAggregation : BucketAggregationBase, ISignificantTermsAggregation
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public int? Size { get; set; }
		/// <inheritdoc />
		public int? ShardSize { get; set; }
		/// <inheritdoc />
		public int? MinimumDocumentCount { get; set; }
		/// <inheritdoc />
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }

		[Obsolete("Deprecated and will be fixed in 6.0. Use IncludeTerms")]
		public IDictionary<string, string> Include { get; set; }

		/// <inheritdoc />
		public SignificantTermsIncludeExclude IncludeTerms { get; set; }

		[Obsolete("Deprecated and will be fixed in 6.0. Use ExcludeTerms")]
		public IDictionary<string, string> Exclude { get; set; }

		/// <inheritdoc />
		public SignificantTermsIncludeExclude ExcludeTerms { get; set; }
		/// <inheritdoc />
		public IMutualInformationHeuristic MutualInformation { get; set; }
		/// <inheritdoc />
		public IChiSquareHeuristic ChiSquare { get; set; }
		/// <inheritdoc />
		public IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }
		/// <inheritdoc />
		public IPercentageScoreHeuristic PercentageScore { get; set; }
		/// <inheritdoc />
		public IScriptedHeuristic Script { get; set; }
		/// <inheritdoc />
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

		[Obsolete("Deprecated and will be fixed in 6.0. Use IncludeTerms")]
		IDictionary<string, string> ISignificantTermsAggregation.Include { get; set; }

		SignificantTermsIncludeExclude ISignificantTermsAggregation.IncludeTerms { get; set; }

		[Obsolete("Deprecated and will be fixed in 6.0. Use ExcludeTerms")]
		IDictionary<string, string> ISignificantTermsAggregation.Exclude { get; set; }

		SignificantTermsIncludeExclude ISignificantTermsAggregation.ExcludeTerms { get; set; }

		IMutualInformationHeuristic ISignificantTermsAggregation.MutualInformation { get; set; }

		IChiSquareHeuristic ISignificantTermsAggregation.ChiSquare { get; set; }

		IGoogleNormalizedDistanceHeuristic ISignificantTermsAggregation.GoogleNormalizedDistance { get; set; }

		IPercentageScoreHeuristic ISignificantTermsAggregation.PercentageScore { get; set; }

		IScriptedHeuristic ISignificantTermsAggregation.Script { get; set; }

		QueryContainer ISignificantTermsAggregation.BackgroundFilter { get; set; }

		public SignificantTermsAggregationDescriptor<T> Field(string field) => Assign(a => a.Field = field);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint? hint) => Assign(a => a.ExecutionHint = hint);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Include(string includePattern) =>
			Assign(a => a.IncludeTerms = new SignificantTermsIncludeExclude(includePattern));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(a => a.IncludeTerms = new SignificantTermsIncludeExclude(values));

		[Obsolete("Use overload that accepts string or IEnumerable<string>")]
		public SignificantTermsAggregationDescriptor<T> Include(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> include) =>
			Assign(a => a.Include = include?.Invoke(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Exclude(string excludePattern) =>
			Assign(a => a.ExcludeTerms = new SignificantTermsIncludeExclude(excludePattern));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(a => a.ExcludeTerms = new SignificantTermsIncludeExclude(values));

		[Obsolete("Use overload that accepts string or IEnumerable<string>")]
		public SignificantTermsAggregationDescriptor<T> Exclude(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> exclude) =>
			Assign(a => a.Exclude = exclude?.Invoke(new FluentDictionary<string, string>()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> ShardSize(int shardSize) => Assign(a => a.ShardSize = shardSize);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> MinimumDocumentCount(int minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> MutualInformation(Func<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic> mutualInformationSelector = null) =>
			Assign(a => a.MutualInformation = mutualInformationSelector.InvokeOrDefault(new MutualInformationHeuristicDescriptor()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> ChiSquare(Func<ChiSquareHeuristicDescriptor, IChiSquareHeuristic> chiSquareSelector) =>
			Assign(a => a.ChiSquare = chiSquareSelector.InvokeOrDefault(new ChiSquareHeuristicDescriptor()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> GoogleNormalizedDistance(Func<GoogleNormalizedDistanceHeuristicDescriptor, IGoogleNormalizedDistanceHeuristic> gndSelector) =>
			Assign(a => a.GoogleNormalizedDistance = gndSelector.InvokeOrDefault(new GoogleNormalizedDistanceHeuristicDescriptor()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> PercentageScore(Func<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic> percentageScoreSelector) =>
			Assign(a => a.PercentageScore = percentageScoreSelector.InvokeOrDefault(new PercentageScoreHeuristicDescriptor()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, IScriptedHeuristic> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptedHeuristicDescriptor()));

		/// <inheritdoc />
		public SignificantTermsAggregationDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.BackgroundFilter = selector?.Invoke(new QueryContainerDescriptor<T>()));
	}
}
