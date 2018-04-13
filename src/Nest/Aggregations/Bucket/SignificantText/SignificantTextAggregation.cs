using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// An aggregation that returns interesting or unusual occurrences of free-text terms in a set.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<SignificantTextAggregation>))]
	public interface ISignificantTextAggregation : IBucketAggregation
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
		long? MinimumDocumentCount { get; set; }

		/// <summary>
		/// Regulates the certainty a shard has if the term should actually be added to the candidate
		/// list or not with respect to the <see cref="MinimumDocumentCount"/>.
		/// Terms will only be considered if their local shard frequency within
		/// the set is higher than the <see cref="ShardMinimumDocumentCount"/>.
		/// </summary>
		[JsonProperty("shard_min_doc_count")]
		long? ShardMinimumDocumentCount { get; set; }

		/// <summary>
		/// Determines the mechanism by which aggregations are executed
		/// </summary>
		[JsonProperty("execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		/// <summary>
		/// Include term values for which buckets will be created.
		/// </summary>
		[JsonProperty("include")]
		IncludeExclude Include { get; set; }

		/// <summary>
		/// Exclude term values for which buckets will be created.
		/// </summary>
		[JsonProperty("exclude")]
		IncludeExclude Exclude { get; set; }

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

		/// <summary>
		/// Whether to filter out near-duplicate text
		/// </summary>
		[JsonProperty("filter_duplicate_text")]
		bool? FilterDuplicateText { get; set; }

		/// <summary>
		/// Ordinarily the indexed field name and the original JSON field being
		/// retrieved share the same name. However with more complex field
		/// mappings using features like copy_to the source JSON field(s)
		/// and the indexed field being aggregated can differ.
		/// In these cases it is possible to list the JSON _source fields
		/// from which text will be analyzed using <see cref="SourceFields"/>
		/// </summary>
		[JsonProperty("source_fields")]
		Fields SourceFields { get; set; }
	}

	/// <inheritdoc cref="ISignificantTextAggregation"/>
	public class SignificantTextAggregation : BucketAggregationBase, ISignificantTextAggregation
	{
		/// <inheritdoc />
		public Field Field { get; set; }
		/// <inheritdoc />
		public int? Size { get; set; }
		/// <inheritdoc />
		public int? ShardSize { get; set; }
		/// <inheritdoc />
		public long? MinimumDocumentCount { get; set; }
		/// <inheritdoc />
		public long? ShardMinimumDocumentCount { get; set; }
		/// <inheritdoc />
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }
		/// <inheritdoc />
		public IncludeExclude Include { get; set; }
		/// <inheritdoc />
		public IncludeExclude Exclude { get; set; }
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
		/// <inheritdoc />
		public bool? FilterDuplicateText { get; set; }
		/// <inheritdoc />
		public Fields SourceFields { get; set; }

		internal SignificantTextAggregation() { }

		public SignificantTextAggregation(string name) : base(name) { }

		internal override void WrapInContainer(AggregationContainer c) => c.SignificantText = this;
	}

	/// <inheritdoc cref="ISignificantTextAggregation"/>
	public class SignificantTextAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SignificantTextAggregationDescriptor<T>, ISignificantTextAggregation, T>
			, ISignificantTextAggregation
		where T : class
	{
		Field ISignificantTextAggregation.Field { get; set; }

		int? ISignificantTextAggregation.Size { get; set; }

		int? ISignificantTextAggregation.ShardSize { get; set; }

		long? ISignificantTextAggregation.MinimumDocumentCount { get; set; }

		long? ISignificantTextAggregation.ShardMinimumDocumentCount { get; set; }

		TermsAggregationExecutionHint? ISignificantTextAggregation.ExecutionHint { get; set; }

		IncludeExclude ISignificantTextAggregation.Include { get; set; }

		IncludeExclude ISignificantTextAggregation.Exclude { get; set; }

		IMutualInformationHeuristic ISignificantTextAggregation.MutualInformation { get; set; }

		IChiSquareHeuristic ISignificantTextAggregation.ChiSquare { get; set; }

		IGoogleNormalizedDistanceHeuristic ISignificantTextAggregation.GoogleNormalizedDistance { get; set; }

		IPercentageScoreHeuristic ISignificantTextAggregation.PercentageScore { get; set; }

		IScriptedHeuristic ISignificantTextAggregation.Script { get; set; }

		QueryContainer ISignificantTextAggregation.BackgroundFilter { get; set; }

		bool? ISignificantTextAggregation.FilterDuplicateText { get; set; }

		Fields ISignificantTextAggregation.SourceFields { get; set; }

		/// <inheritdoc cref="ISignificantTextAggregation.Field"/>
		public SignificantTextAggregationDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="ISignificantTextAggregation.Field"/>
		public SignificantTextAggregationDescriptor<T> Field(Expression<Func<T, object>> field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="ISignificantTextAggregation.Size"/>
		public SignificantTextAggregationDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// <inheritdoc cref="ISignificantTextAggregation.ExecutionHint"/>
		public SignificantTextAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint? hint) => Assign(a => a.ExecutionHint = hint);

		/// <inheritdoc cref="ISignificantTextAggregation.Include"/>
		public SignificantTextAggregationDescriptor<T> Include(string includePattern) =>
			Assign(a => a.Include = new IncludeExclude(includePattern));

		/// <inheritdoc cref="ISignificantTextAggregation.Include"/>
		public SignificantTextAggregationDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(a => a.Include = new IncludeExclude(values));

		/// <inheritdoc cref="ISignificantTextAggregation.Exclude"/>
		public SignificantTextAggregationDescriptor<T> Exclude(string excludePattern) =>
			Assign(a => a.Exclude = new IncludeExclude(excludePattern));

		/// <inheritdoc cref="ISignificantTextAggregation.Exclude"/>
		public SignificantTextAggregationDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(a => a.Exclude = new IncludeExclude(values));

		/// <inheritdoc cref="ISignificantTextAggregation.ShardSize"/>
		public SignificantTextAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(a => a.ShardSize = shardSize);

		/// <inheritdoc cref="ISignificantTextAggregation.MinimumDocumentCount"/>
		public SignificantTextAggregationDescriptor<T> MinimumDocumentCount(long? minimumDocumentCount) =>
			Assign(a => a.MinimumDocumentCount = minimumDocumentCount);

		/// <inheritdoc cref="ISignificantTextAggregation.ShardMinimumDocumentCount"/>
		public SignificantTextAggregationDescriptor<T> ShardMinimumDocumentCount(long? shardMinimumDocumentCount) =>
			Assign(a => a.ShardMinimumDocumentCount = shardMinimumDocumentCount);

		/// <inheritdoc cref="ISignificantTextAggregation.MutualInformation"/>
		public SignificantTextAggregationDescriptor<T> MutualInformation(Func<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic> mutualInformationSelector = null) =>
			Assign(a => a.MutualInformation = mutualInformationSelector.InvokeOrDefault(new MutualInformationHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.ChiSquare"/>
		public SignificantTextAggregationDescriptor<T> ChiSquare(Func<ChiSquareHeuristicDescriptor, IChiSquareHeuristic> chiSquareSelector) =>
			Assign(a => a.ChiSquare = chiSquareSelector.InvokeOrDefault(new ChiSquareHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.GoogleNormalizedDistance"/>
		public SignificantTextAggregationDescriptor<T> GoogleNormalizedDistance(Func<GoogleNormalizedDistanceHeuristicDescriptor, IGoogleNormalizedDistanceHeuristic> gndSelector) =>
			Assign(a => a.GoogleNormalizedDistance = gndSelector.InvokeOrDefault(new GoogleNormalizedDistanceHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.PercentageScore"/>
		public SignificantTextAggregationDescriptor<T> PercentageScore(Func<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic> percentageScoreSelector) =>
			Assign(a => a.PercentageScore = percentageScoreSelector.InvokeOrDefault(new PercentageScoreHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.Script"/>
		public SignificantTextAggregationDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, IScriptedHeuristic> scriptSelector) =>
			Assign(a => a.Script = scriptSelector?.Invoke(new ScriptedHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.BackgroundFilter"/>
		public SignificantTextAggregationDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(a => a.BackgroundFilter = selector?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="ISignificantTextAggregation.FilterDuplicateText"/>
		public SignificantTextAggregationDescriptor<T> FilterDuplicateText(bool? filterDuplicateText = true) => Assign(a => a.FilterDuplicateText = filterDuplicateText);

		/// <inheritdoc cref="ISignificantTextAggregation.SourceFields"/>
		public SignificantTextAggregationDescriptor<T> SourceFields(Func<FieldsDescriptor<T>, IPromise<Fields>> sourceFields) =>
			Assign(a => a.SourceFields = sourceFields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ISignificantTextAggregation.SourceFields"/>
		public SignificantTextAggregationDescriptor<T> SourceFields(Fields sourceFields) => Assign(a => a.SourceFields = sourceFields);

	}
}
