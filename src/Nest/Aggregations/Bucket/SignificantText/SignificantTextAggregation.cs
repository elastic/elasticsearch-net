// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An aggregation that returns interesting or unusual occurrences of free-text terms in a set.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(SignificantTextAggregation))]
	public interface ISignificantTextAggregation : IBucketAggregation
	{
		/// <summary>
		/// The default source of statistical information for background term
		/// frequencies is the entire index. This scope can be narrowed
		/// through the use of a background filter to focus in on significant
		/// terms within a narrower context
		/// </summary>
		[DataMember(Name ="background_filter")]
		QueryContainer BackgroundFilter { get; set; }

		/// <summary>
		/// Use chi square to calculate significance score
		/// </summary>
		[DataMember(Name ="chi_square")]
		IChiSquareHeuristic ChiSquare { get; set; }

		/// <summary>
		/// Exclude term values for which buckets will be created.
		/// </summary>
		[DataMember(Name ="exclude")]
		IncludeExclude Exclude { get; set; }

		/// <summary>
		/// Determines the mechanism by which aggregations are executed
		/// </summary>
		[DataMember(Name ="execution_hint")]
		TermsAggregationExecutionHint? ExecutionHint { get; set; }

		/// <summary>
		/// The field on which to run the aggregation
		/// </summary>
		[DataMember(Name ="field")]
		Field Field { get; set; }

		/// <summary>
		/// Whether to filter out near-duplicate text
		/// </summary>
		[DataMember(Name ="filter_duplicate_text")]
		bool? FilterDuplicateText { get; set; }

		/// <summary>
		/// Use Google normalized distance to calculate significance score
		/// </summary>
		[DataMember(Name ="gnd")]
		IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }

		/// <summary>
		/// Include term values for which buckets will be created.
		/// </summary>
		[DataMember(Name ="include")]
		IncludeExclude Include { get; set; }

		/// <summary>
		/// Return only terms that match equal to or more than a configurable
		/// number of hits
		/// </summary>
		[DataMember(Name ="min_doc_count")]
		long? MinimumDocumentCount { get; set; }

		/// <summary>
		/// Use mutual information to calculate significance score
		/// </summary>
		[DataMember(Name ="mutual_information")]
		IMutualInformationHeuristic MutualInformation { get; set; }

		/// <summary>
		/// Use percentage to calculate significance score.
		/// <para>
		/// A simple calculation of the number of documents in the foreground
		/// sample with a term divided by the number of documents in the background
		/// with the term. By default this produces a score greater than zero
		///  and less than one.
		/// </para>
		/// </summary>
		[DataMember(Name ="percentage")]
		IPercentageScoreHeuristic PercentageScore { get; set; }

		/// <summary>
		/// Use a script to calculate a custom significance score.
		/// </summary>
		[DataMember(Name ="script_heuristic")]
		IScriptedHeuristic Script { get; set; }

		/// <summary>
		/// Regulates the certainty a shard has if the term should actually be added to the candidate
		/// list or not with respect to the <see cref="MinimumDocumentCount" />.
		/// Terms will only be considered if their local shard frequency within
		/// the set is higher than the <see cref="ShardMinimumDocumentCount" />.
		/// </summary>
		[DataMember(Name ="shard_min_doc_count")]
		long? ShardMinimumDocumentCount { get; set; }

		/// <summary>
		/// Controls the number of candidate terms produced by each shard from which
		/// the <see cref="Size" /> of terms is selected.
		/// </summary>
		[DataMember(Name ="shard_size")]
		int? ShardSize { get; set; }

		/// <summary>
		/// Defines how many term buckets should be returned out of the overall
		/// terms list
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }

		/// <summary>
		/// Ordinarily the indexed field name and the original JSON field being
		/// retrieved share the same name. However with more complex field
		/// mappings using features like copy_to the source JSON field(s)
		/// and the indexed field being aggregated can differ.
		/// In these cases it is possible to list the JSON _source fields
		/// from which text will be analyzed using <see cref="SourceFields" />
		/// </summary>
		[DataMember(Name ="source_fields")]
		Fields SourceFields { get; set; }
	}

	/// <inheritdoc cref="ISignificantTextAggregation" />
	public class SignificantTextAggregation : BucketAggregationBase, ISignificantTextAggregation
	{
		internal SignificantTextAggregation() { }

		public SignificantTextAggregation(string name) : base(name) { }

		/// <inheritdoc />
		public QueryContainer BackgroundFilter { get; set; }

		/// <inheritdoc />
		public IChiSquareHeuristic ChiSquare { get; set; }

		/// <inheritdoc />
		public IncludeExclude Exclude { get; set; }

		/// <inheritdoc />
		public TermsAggregationExecutionHint? ExecutionHint { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public bool? FilterDuplicateText { get; set; }

		/// <inheritdoc />
		public IGoogleNormalizedDistanceHeuristic GoogleNormalizedDistance { get; set; }

		/// <inheritdoc />
		public IncludeExclude Include { get; set; }

		/// <inheritdoc />
		public long? MinimumDocumentCount { get; set; }

		/// <inheritdoc />
		public IMutualInformationHeuristic MutualInformation { get; set; }

		/// <inheritdoc />
		public IPercentageScoreHeuristic PercentageScore { get; set; }

		/// <inheritdoc />
		public IScriptedHeuristic Script { get; set; }

		/// <inheritdoc />
		public long? ShardMinimumDocumentCount { get; set; }

		/// <inheritdoc />
		public int? ShardSize { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }

		/// <inheritdoc />
		public Fields SourceFields { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.SignificantText = this;
	}

	/// <inheritdoc cref="ISignificantTextAggregation" />
	public class SignificantTextAggregationDescriptor<T>
		: BucketAggregationDescriptorBase<SignificantTextAggregationDescriptor<T>, ISignificantTextAggregation, T>
			, ISignificantTextAggregation
		where T : class
	{
		QueryContainer ISignificantTextAggregation.BackgroundFilter { get; set; }

		IChiSquareHeuristic ISignificantTextAggregation.ChiSquare { get; set; }

		IncludeExclude ISignificantTextAggregation.Exclude { get; set; }

		TermsAggregationExecutionHint? ISignificantTextAggregation.ExecutionHint { get; set; }
		Field ISignificantTextAggregation.Field { get; set; }

		bool? ISignificantTextAggregation.FilterDuplicateText { get; set; }

		IGoogleNormalizedDistanceHeuristic ISignificantTextAggregation.GoogleNormalizedDistance { get; set; }

		IncludeExclude ISignificantTextAggregation.Include { get; set; }

		long? ISignificantTextAggregation.MinimumDocumentCount { get; set; }

		IMutualInformationHeuristic ISignificantTextAggregation.MutualInformation { get; set; }

		IPercentageScoreHeuristic ISignificantTextAggregation.PercentageScore { get; set; }

		IScriptedHeuristic ISignificantTextAggregation.Script { get; set; }

		long? ISignificantTextAggregation.ShardMinimumDocumentCount { get; set; }

		int? ISignificantTextAggregation.ShardSize { get; set; }

		int? ISignificantTextAggregation.Size { get; set; }

		Fields ISignificantTextAggregation.SourceFields { get; set; }

		/// <inheritdoc cref="ISignificantTextAggregation.Field" />
		public SignificantTextAggregationDescriptor<T> Field(Field field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Field" />
		public SignificantTextAggregationDescriptor<T> Field<TValue>(Expression<Func<T, TValue>> field) => Assign(field, (a, v) => a.Field = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Size" />
		public SignificantTextAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="ISignificantTextAggregation.ExecutionHint" />
		public SignificantTextAggregationDescriptor<T> ExecutionHint(TermsAggregationExecutionHint? hint) => Assign(hint, (a, v) => a.ExecutionHint = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Include" />
		public SignificantTextAggregationDescriptor<T> Include(string includePattern) =>
			Assign(new IncludeExclude(includePattern), (a, v) => a.Include = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Include" />
		public SignificantTextAggregationDescriptor<T> Include(IEnumerable<string> values) =>
			Assign(new IncludeExclude(values), (a, v) => a.Include = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Exclude" />
		public SignificantTextAggregationDescriptor<T> Exclude(string excludePattern) =>
			Assign(new IncludeExclude(excludePattern), (a, v) => a.Exclude = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Exclude" />
		public SignificantTextAggregationDescriptor<T> Exclude(IEnumerable<string> values) =>
			Assign(new IncludeExclude(values), (a, v) => a.Exclude = v);

		/// <inheritdoc cref="ISignificantTextAggregation.ShardSize" />
		public SignificantTextAggregationDescriptor<T> ShardSize(int? shardSize) => Assign(shardSize, (a, v) => a.ShardSize = v);

		/// <inheritdoc cref="ISignificantTextAggregation.MinimumDocumentCount" />
		public SignificantTextAggregationDescriptor<T> MinimumDocumentCount(long? minimumDocumentCount) =>
			Assign(minimumDocumentCount, (a, v) => a.MinimumDocumentCount = v);

		/// <inheritdoc cref="ISignificantTextAggregation.ShardMinimumDocumentCount" />
		public SignificantTextAggregationDescriptor<T> ShardMinimumDocumentCount(long? shardMinimumDocumentCount) =>
			Assign(shardMinimumDocumentCount, (a, v) => a.ShardMinimumDocumentCount = v);

		/// <inheritdoc cref="ISignificantTextAggregation.MutualInformation" />
		public SignificantTextAggregationDescriptor<T> MutualInformation(
			Func<MutualInformationHeuristicDescriptor, IMutualInformationHeuristic> mutualInformationSelector = null
		) =>
			Assign(mutualInformationSelector.InvokeOrDefault(new MutualInformationHeuristicDescriptor()), (a, v) => a.MutualInformation = v);

		/// <inheritdoc cref="ISignificantTextAggregation.ChiSquare" />
		public SignificantTextAggregationDescriptor<T> ChiSquare(Func<ChiSquareHeuristicDescriptor, IChiSquareHeuristic> chiSquareSelector) =>
			Assign(chiSquareSelector.InvokeOrDefault(new ChiSquareHeuristicDescriptor()), (a, v) => a.ChiSquare = v);

		/// <inheritdoc cref="ISignificantTextAggregation.GoogleNormalizedDistance" />
		public SignificantTextAggregationDescriptor<T> GoogleNormalizedDistance(
			Func<GoogleNormalizedDistanceHeuristicDescriptor, IGoogleNormalizedDistanceHeuristic> gndSelector
		) =>
			Assign(gndSelector.InvokeOrDefault(new GoogleNormalizedDistanceHeuristicDescriptor()), (a, v) => a.GoogleNormalizedDistance = v);

		/// <inheritdoc cref="ISignificantTextAggregation.PercentageScore" />
		public SignificantTextAggregationDescriptor<T> PercentageScore(
			Func<PercentageScoreHeuristicDescriptor, IPercentageScoreHeuristic> percentageScoreSelector
		) =>
			Assign(percentageScoreSelector.InvokeOrDefault(new PercentageScoreHeuristicDescriptor()), (a, v) => a.PercentageScore = v);

		/// <inheritdoc cref="ISignificantTextAggregation.Script" />
		public SignificantTextAggregationDescriptor<T> Script(Func<ScriptedHeuristicDescriptor, IScriptedHeuristic> scriptSelector) =>
			Assign(scriptSelector, (a, v) => a.Script = v?.Invoke(new ScriptedHeuristicDescriptor()));

		/// <inheritdoc cref="ISignificantTextAggregation.BackgroundFilter" />
		public SignificantTextAggregationDescriptor<T> BackgroundFilter(Func<QueryContainerDescriptor<T>, QueryContainer> selector) =>
			Assign(selector, (a, v) => a.BackgroundFilter = v?.Invoke(new QueryContainerDescriptor<T>()));

		/// <inheritdoc cref="ISignificantTextAggregation.FilterDuplicateText" />
		public SignificantTextAggregationDescriptor<T> FilterDuplicateText(bool? filterDuplicateText = true) =>
			Assign(filterDuplicateText, (a, v) => a.FilterDuplicateText = v);

		/// <inheritdoc cref="ISignificantTextAggregation.SourceFields" />
		public SignificantTextAggregationDescriptor<T> SourceFields(Func<FieldsDescriptor<T>, IPromise<Fields>> sourceFields) =>
			Assign(sourceFields, (a, v) => a.SourceFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ISignificantTextAggregation.SourceFields" />
		public SignificantTextAggregationDescriptor<T> SourceFields(Fields sourceFields) => Assign(sourceFields, (a, v) => a.SourceFields = v);
	}
}
