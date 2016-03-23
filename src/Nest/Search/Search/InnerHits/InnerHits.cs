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
		ISourceFilter Source { get; set; }

		[JsonProperty(PropertyName = "version")]
		bool? Version { get; set; }

		[JsonProperty(PropertyName = "fielddata_fields")]
		IList<Field> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		IScriptFields ScriptFields { get; set; }
	}

	public class InnerHits : IInnerHits
	{
		public string Name { get; set; }

		public int? From { get; set; }

		public int? Size { get; set; }

		public IList<ISort> Sort { get; set; }

		public IHighlight Highlight { get; set; }

		public bool? Explain { get; set; }

		public ISourceFilter Source { get; set; }

		public bool? Version { get; set; }

		public IList<Field> FielddataFields { get; set; }

		public IScriptFields ScriptFields { get; set; }
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
		ISourceFilter IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }
		IList<Field> IInnerHits.FielddataFields { get; set; }
		IScriptFields IInnerHits.ScriptFields { get; set; }

		public InnerHitsDescriptor<T> From(int? from) => Assign(a => a.From = from);

		public InnerHitsDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public InnerHitsDescriptor<T> Name(string name) => Assign(a => a.Name = name);

		public InnerHitsDescriptor<T> FielddataFields(params Field[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.ToListOrNullIfEmpty());

		public InnerHitsDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field)f).ToListOrNullIfEmpty());

		public InnerHitsDescriptor<T> Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		public InnerHitsDescriptor<T> Version(bool? version = true) => Assign(a => a.Version = version);

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) => Assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter.
		/// </summary>
		public InnerHitsDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		//TODO map source of union bool/SourceFileter
		public InnerHitsDescriptor<T> Source(bool include = true) => Assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);

		public InnerHitsDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> sourceSelector) =>
			Assign(a => a.Source = sourceSelector?.Invoke(new SourceFilterDescriptor<T>()));

		public InnerHitsDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);
	}
}
