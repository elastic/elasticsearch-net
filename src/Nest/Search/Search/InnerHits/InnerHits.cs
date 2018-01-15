using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<InnerHits>))]
	public interface IInnerHits
	{
		[JsonProperty("name")]
		string Name { get; set; }

		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlight Highlight { get; set; }

		[JsonProperty(PropertyName = "explain")]
		bool? Explain { get; set; }

		[JsonProperty(PropertyName = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty(PropertyName = "version")]
		bool? Version { get; set; }

		[JsonProperty(PropertyName = "fielddata_fields")]
		IList<Field> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		IScriptFields ScriptFields { get; set; }

		[JsonProperty("docvalue_fields")]
		Fields DocValueFields { get; set; }
	}

	public class InnerHits : IInnerHits
	{
		public string Name { get; set; }

		public int? From { get; set; }

		public int? Size { get; set; }

		public IList<ISort> Sort { get; set; }

		public IHighlight Highlight { get; set; }

		public bool? Explain { get; set; }

		public Union<bool, ISourceFilter> Source { get; set; }

		public bool? Version { get; set; }

		public IList<Field> FielddataFields { get; set; }

		public IScriptFields ScriptFields { get; set; }

		public Fields DocValueFields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class InnerHitsDescriptor<T> : DescriptorBase<InnerHitsDescriptor<T>, IInnerHits>, IInnerHits where T : class
	{
		string IInnerHits.Name { get; set; }
		int? IInnerHits.From { get; set; }
		int? IInnerHits.Size { get; set; }
		IList<ISort> IInnerHits.Sort { get; set; }
		IHighlight IInnerHits.Highlight { get; set; }
		bool? IInnerHits.Explain { get; set; }
		Union<bool, ISourceFilter> IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }
		IList<Field> IInnerHits.FielddataFields { get; set; }
		IScriptFields IInnerHits.ScriptFields { get; set; }
		Fields IInnerHits.DocValueFields { get; set; }

		public InnerHitsDescriptor<T> From(int? from) => Assign(a => a.From = from);

		public InnerHitsDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public InnerHitsDescriptor<T> Name(string name) => Assign(a => a.Name = name);

		public InnerHitsDescriptor<T> FielddataFields(params Field[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.ToListOrNullIfEmpty());

		public InnerHitsDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field)f).ToListOrNullIfEmpty());

		public InnerHitsDescriptor<T> Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		public InnerHitsDescriptor<T> Version(bool? version = true) => Assign(a => a.Version = version);

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) => Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

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
	}
}
