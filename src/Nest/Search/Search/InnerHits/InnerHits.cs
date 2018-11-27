using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(InnerHits))]
	public interface IInnerHits
	{
		/// <summary>
		/// Provides a second level of collapsing, NOTE: Elasticsearch only supports collapsing up to two levels.
		/// </summary>
		[DataMember(Name ="collapse")]
		IFieldCollapse Collapse { get; set; }

		[DataMember(Name ="docvalue_fields")]
		Fields DocValueFields { get; set; }

		[DataMember(Name ="explain")]
		bool? Explain { get; set; }

		[DataMember(Name ="from")]
		int? From { get; set; }

		[DataMember(Name ="highlight")]
		IHighlight Highlight { get; set; }

		[DataMember(Name ="ignore_unmapped")]
		bool? IgnoreUnmapped { get; set; }

		[DataMember(Name ="name")]
		string Name { get; set; }

		[DataMember(Name ="script_fields")]
		IScriptFields ScriptFields { get; set; }

		[DataMember(Name ="size")]
		int? Size { get; set; }

		[DataMember(Name ="sort")]
		IList<ISort> Sort { get; set; }

		[DataMember(Name ="_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name ="version")]
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

	[DataContract]
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

		public InnerHitsDescriptor<T> From(int? from) => Assign(a => a.From = from);

		public InnerHitsDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public InnerHitsDescriptor<T> Name(string name) => Assign(a => a.Name = name);

		public InnerHitsDescriptor<T> Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		public InnerHitsDescriptor<T> Version(bool? version = true) => Assign(a => a.Version = version);

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter.
		/// </summary>
		public InnerHitsDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> selector) =>
			Assign(a => a.Highlight = selector?.Invoke(new HighlightDescriptor<T>()));

		public InnerHitsDescriptor<T> Source(bool enabled = true) => Assign(a => a.Source = enabled);

		public InnerHitsDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<T>())));

		public InnerHitsDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		public InnerHitsDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.DocValueFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public InnerHitsDescriptor<T> DocValueFields(Fields fields) => Assign(a => a.DocValueFields = fields);

		public InnerHitsDescriptor<T> IgnoreUnmapped(bool? ignoreUnmapped = true) => Assign(a => a.IgnoreUnmapped = ignoreUnmapped);

		/// <inheritdoc cref="IInnerHits.Collapse" />
		public InnerHitsDescriptor<T> Collapse(Func<FieldCollapseDescriptor<T>, IFieldCollapse> collapseSelector) =>
			Assign(a => a.Collapse = collapseSelector?.Invoke(new FieldCollapseDescriptor<T>()));
	}
}
