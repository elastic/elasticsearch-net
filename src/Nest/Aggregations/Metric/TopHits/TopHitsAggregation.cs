using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(AggregationJsonConverter<TopHitsAggregation>))]
	public interface ITopHitsAggregation : IMetricAggregation
	{
		[JsonProperty("docvalue_fields")]
		Fields DocValueFields { get; set; }

		[JsonProperty("explain")]
		bool? Explain { get; set; }

		[Obsolete("Removed in NEST 6.x")]
		[JsonProperty("fielddata_fields")]
		Fields FielddataFields { get; set; }

		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("highlight")]
		IHighlight Highlight { get; set; }

		[JsonProperty("script_fields")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<ScriptFields>))]
		IScriptFields ScriptFields { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty("stored_fields")]
		Fields StoredFields { get; set; }

		[JsonProperty("track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty("version")]
		bool? Version { get; set; }
	}

	public class TopHitsAggregation : MetricAggregationBase, ITopHitsAggregation
	{
		internal TopHitsAggregation() { }

		public TopHitsAggregation(string name) : base(name, null) { }

		public Fields DocValueFields { get; set; }
		public bool? Explain { get; set; }

		[Obsolete("Removed in NEST 6.x")]
		public Fields FielddataFields { get; set; }

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

		[Obsolete("Removed in NEST 6.x")]
		Fields ITopHitsAggregation.FielddataFields { get; set; }

		int? ITopHitsAggregation.From { get; set; }

		IHighlight ITopHitsAggregation.Highlight { get; set; }

		IScriptFields ITopHitsAggregation.ScriptFields { get; set; }

		int? ITopHitsAggregation.Size { get; set; }

		IList<ISort> ITopHitsAggregation.Sort { get; set; }

		Union<bool, ISourceFilter> ITopHitsAggregation.Source { get; set; }

		Fields ITopHitsAggregation.StoredFields { get; set; }

		bool? ITopHitsAggregation.TrackScores { get; set; }

		bool? ITopHitsAggregation.Version { get; set; }

		public TopHitsAggregationDescriptor<T> From(int from) => Assign(a => a.From = from);

		public TopHitsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		[Obsolete("Use Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>>) that accepts multiple sort options")]
		public TopHitsAggregationDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector) => Assign(a =>
		{
			a.Sort = a.Sort ?? new List<ISort>();
			var sort = sortSelector?.Invoke(new SortFieldDescriptor<T>());
			if (sort != null) a.Sort.Add(sort);
		});

		public TopHitsAggregationDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) =>
			Assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Source(bool enabled = true) =>
			Assign(a => a.Source = enabled);

		public TopHitsAggregationDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<T>())));

		public TopHitsAggregationDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		public TopHitsAggregationDescriptor<T> Explain(bool explain = true) => Assign(a => a.Explain = explain);

		public TopHitsAggregationDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> scriptFieldsSelector) =>
			Assign(a => a.ScriptFields = scriptFieldsSelector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		[Obsolete("Removed in NEST 6.x")]
		public TopHitsAggregationDescriptor<T> FielddataFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.FielddataFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> Version(bool version = true) => Assign(a => a.Version = version);

		public TopHitsAggregationDescriptor<T> TrackScores(bool trackScores = true) => Assign(a => a.TrackScores = trackScores);

		public TopHitsAggregationDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.DocValueFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public TopHitsAggregationDescriptor<T> DocValueFields(Fields fields) => Assign(a => a.DocValueFields = fields);
	}
}
