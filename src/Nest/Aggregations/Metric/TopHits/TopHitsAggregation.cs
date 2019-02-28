using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

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

		public TopHitsAggregationDescriptor<T> From(int? from) => Assign(a => a.From = from);

		public TopHitsAggregationDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public TopHitsAggregationDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) =>
			Assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Source(bool? enabled = true) =>
			Assign(a => a.Source = enabled);

		public TopHitsAggregationDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<T>())));

		public TopHitsAggregationDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		public TopHitsAggregationDescriptor<T> Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		public TopHitsAggregationDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> scriptFieldsSelector) =>
			Assign(a => a.ScriptFields = scriptFieldsSelector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		public TopHitsAggregationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Version(bool? version = true) => Assign(a => a.Version = version);

		public TopHitsAggregationDescriptor<T> TrackScores(bool? trackScores = true) => Assign(a => a.TrackScores = trackScores);

		public TopHitsAggregationDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.DocValueFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> DocValueFields(Fields fields) => Assign(a => a.DocValueFields = fields);
	}
}
