using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<TopLevelInnerHit>))]
	public interface ITopLevelInnerHit : IInnerHits
	{
		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("inner_hits")]
		ITopLevelInnerHits InnerHits { get; set; }

		[JsonProperty("type")]
		TypeName Type { get; set; }

		[JsonProperty("path")]
		Field Path { get; set; }
	}

	public class TopLevelInnerHit : InnerHits, ITopLevelInnerHit
	{
		public QueryContainer Query { get; set; }
		public ITopLevelInnerHits InnerHits { get; set; }
		public TypeName Type { get; set; }
		public Field Path { get; set; }
	}

	public class TopLevelInnerHit<T> : DescriptorBase<TopLevelInnerHit<T>, ITopLevelInnerHit>, ITopLevelInnerHit
		where T : class
	{
		QueryContainer ITopLevelInnerHit.Query { get; set; }
		ITopLevelInnerHits ITopLevelInnerHit.InnerHits { get; set; }
		TypeName ITopLevelInnerHit.Type { get; set; }
		Field ITopLevelInnerHit.Path { get; set; }
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

		public TopLevelInnerHit<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) =>
			Assign(a => a.Query = querySelector?.InvokeQuery(new QueryContainerDescriptor<T>()));

		public TopLevelInnerHit<T> InnerHits(Func<TopLevelInnerHitsDescriptor<T>, IPromise<ITopLevelInnerHits>> selector) =>
			Assign(a => a.InnerHits = selector?.Invoke(new TopLevelInnerHitsDescriptor<T>())?.Value);

		public TopLevelInnerHit<T> Type(TypeName type) => Assign(a => a.Type = type);

		public TopLevelInnerHit<T> Type<TOther>() where TOther : class => Assign(a => a.Type = typeof(TOther));

		public TopLevelInnerHit<T> Path(Field path) => Assign(a => a.Path = path);

		public TopLevelInnerHit<T> Path(Expression<Func<T, object>> path) => Assign(a => a.Path = path);

		public TopLevelInnerHit<T> From(int? from) => Assign(a => a.From = from);

		public TopLevelInnerHit<T> Size(int? size) => Assign(a => a.Size = size);

		public TopLevelInnerHit<T> Name(string name) => Assign(a => a.Name = name);

		public TopLevelInnerHit<T> FielddataFields(params Field[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.ToListOrNullIfEmpty());

		public TopLevelInnerHit<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		public TopLevelInnerHit<T> Explain(bool explain = true) => Assign(a => a.Explain = explain);

		public TopLevelInnerHit<T> Version(bool version = true) => Assign(a => a.Version = version);

		public TopLevelInnerHit<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) => Assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter.
		/// </summary>
		public TopLevelInnerHit<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		public TopLevelInnerHit<T> Source(bool include = true)=> Assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);

		public TopLevelInnerHit<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> sourceSelector) =>
			Assign(a => a.Source = sourceSelector?.Invoke(new SourceFilterDescriptor<T>()));

		public TopLevelInnerHit<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);
	}
}
