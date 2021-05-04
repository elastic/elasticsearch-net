// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(TopHitsAggregation))]
	public interface ITopHitsAggregation : IMetricAggregation
	{
		[DataMember(Name ="docvalue_fields")]
		Fields DocValueFields { get; set; }

		[DataMember(Name ="explain")]
		bool? Explain { get; set; }

		[DataMember(Name ="from")]
		int? From { get; set; }

		[DataMember(Name ="highlight")]
		IHighlight Highlight { get; set; }

		[DataMember(Name ="script_fields")]
		[ReadAs(typeof(ScriptFields))]
		IScriptFields ScriptFields { get; set; }

		[DataMember(Name ="size")]
		int? Size { get; set; }

		[DataMember(Name ="sort")]
		IList<ISort> Sort { get; set; }

		[DataMember(Name ="_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name ="stored_fields")]
		Fields StoredFields { get; set; }

		[DataMember(Name ="track_scores")]
		bool? TrackScores { get; set; }

		[DataMember(Name ="version")]
		bool? Version { get; set; }
	}

	public class TopHitsAggregation : MetricAggregationBase, ITopHitsAggregation
	{
		internal TopHitsAggregation() { }

		public TopHitsAggregation(string name) : base(name, null) { }

		public Fields DocValueFields { get; set; }
		public bool? Explain { get; set; }
		public int? From { get; set; }
		public IHighlight Highlight { get; set; }
		public IScriptFields ScriptFields { get; set; }
		public int? Size { get; set; }
		public IList<ISort> Sort { get; set; }
		public Union<bool, ISourceFilter> Source { get; set; }
		public Fields StoredFields { get; set; }
		public bool? TrackScores { get; set; }
		public bool? Version { get; set; }

		internal override void WrapInContainer(AggregationContainer c) => c.TopHits = this;
	}

	public class TopHitsAggregationDescriptor<T>
		: MetricAggregationDescriptorBase<TopHitsAggregationDescriptor<T>, ITopHitsAggregation, T>
			, ITopHitsAggregation
		where T : class
	{
		Fields ITopHitsAggregation.DocValueFields { get; set; }

		bool? ITopHitsAggregation.Explain { get; set; }
		int? ITopHitsAggregation.From { get; set; }

		IHighlight ITopHitsAggregation.Highlight { get; set; }

		IScriptFields ITopHitsAggregation.ScriptFields { get; set; }

		int? ITopHitsAggregation.Size { get; set; }

		IList<ISort> ITopHitsAggregation.Sort { get; set; }

		Union<bool, ISourceFilter> ITopHitsAggregation.Source { get; set; }

		Fields ITopHitsAggregation.StoredFields { get; set; }

		bool? ITopHitsAggregation.TrackScores { get; set; }

		bool? ITopHitsAggregation.Version { get; set; }

		public TopHitsAggregationDescriptor<T> From(int? from) => Assign(from, (a, v) => a.From = v);

		public TopHitsAggregationDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public TopHitsAggregationDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) =>
			Assign(sortSelector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Source(bool? enabled = true) =>
			Assign(enabled, (a, v) => a.Source = v);

		public TopHitsAggregationDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<T>())));

		public TopHitsAggregationDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(highlightSelector, (a, v) => a.Highlight = v?.Invoke(new HighlightDescriptor<T>()));

		public TopHitsAggregationDescriptor<T> Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		public TopHitsAggregationDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> scriptFieldsSelector) =>
			Assign(scriptFieldsSelector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		public TopHitsAggregationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Version(bool? version = true) => Assign(version, (a, v) => a.Version = v);

		public TopHitsAggregationDescriptor<T> TrackScores(bool? trackScores = true) => Assign(trackScores, (a, v) => a.TrackScores = v);

		public TopHitsAggregationDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.DocValueFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> DocValueFields(Fields fields) => Assign(fields, (a, v) => a.DocValueFields = v);
	}
}
