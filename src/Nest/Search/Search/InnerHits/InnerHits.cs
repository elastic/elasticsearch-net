using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<InnerHits>))]
	public interface IInnerHits
	{
		/// <summary>
		/// Provides a second level of collapsing, NOTE: Elasticsearch only supports collapsing up to two levels.
		/// </summary>
		[JsonProperty("collapse")]
		IFieldCollapse Collapse { get; set; }

		[JsonProperty("docvalue_fields")]
		Fields DocValueFields { get; set; }

		[JsonProperty("explain")]
		bool? Explain { get; set; }

		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("highlight")]
		IHighlight Highlight { get; set; }

		[JsonProperty("ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[JsonProperty("name")]
		string Name { get; set; }

		[JsonProperty("script_fields")]
		IScriptFields ScriptFields { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty("version")]
		bool? Version { get; set; }
	}

	public class InnerHits : IInnerHits
	{
		/// <inheritdoc cref="IInnerHits.Collapse" />
		public IFieldCollapse Collapse { get; set; }

		public Fields DocValueFields { get; set; }

		public bool? Explain { get; set; }

		public int? From { get; set; }

		public IHighlight Highlight { get; set; }

		public bool? IgnoreUnmapped { get; set; }
		public string Name { get; set; }

		public IScriptFields ScriptFields { get; set; }

		public int? Size { get; set; }

		public IList<ISort> Sort { get; set; }

		public Union<bool, ISourceFilter> Source { get; set; }

		public bool? Version { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class InnerHitsDescriptor<T> : DescriptorBase<InnerHitsDescriptor<T>, IInnerHits>, IInnerHits where T : class
	{
		IFieldCollapse IInnerHits.Collapse { get; set; }
		Fields IInnerHits.DocValueFields { get; set; }
		bool? IInnerHits.Explain { get; set; }
		int? IInnerHits.From { get; set; }
		IHighlight IInnerHits.Highlight { get; set; }
		bool? IInnerHits.IgnoreUnmapped { get; set; }
		string IInnerHits.Name { get; set; }
		IScriptFields IInnerHits.ScriptFields { get; set; }
		int? IInnerHits.Size { get; set; }
		IList<ISort> IInnerHits.Sort { get; set; }
		Union<bool, ISourceFilter> IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }

		public InnerHitsDescriptor<T> From(int? from) => Assign(from, (a, v) => a.From = v);

		public InnerHitsDescriptor<T> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		public InnerHitsDescriptor<T> Name(string name) => Assign(name, (a, v) => a.Name = v);

		public InnerHitsDescriptor<T> Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		public InnerHitsDescriptor<T> Version(bool? version = true) => Assign(version, (a, v) => a.Version = v);

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) =>
			Assign(selector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter.
		/// </summary>
		public InnerHitsDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> selector) =>
			Assign(selector, (a, v) => a.Highlight = v?.Invoke(new HighlightDescriptor<T>()));

		public InnerHitsDescriptor<T> Source(bool enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		public InnerHitsDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<T>())));

		public InnerHitsDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(selector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		public InnerHitsDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.DocValueFields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		public InnerHitsDescriptor<T> DocValueFields(Fields fields) => Assign(fields, (a, v) => a.DocValueFields = v);

		public InnerHitsDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(ignoreUnmapped, (a, v) => a.IgnoreUnmapped = v);

		/// <inheritdoc cref="IInnerHits.Collapse" />
		public InnerHitsDescriptor<T> Collapse(Func<FieldCollapseDescriptor<T>, IFieldCollapse> collapseSelector) =>
			Assign(collapseSelector, (a, v) => a.Collapse = v?.Invoke(new FieldCollapseDescriptor<T>()));
	}
}
