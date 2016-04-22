using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<GlobalInnerHit>))]
	public interface IGlobalInnerHit : IInnerHits
	{
		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		INamedInnerHits InnerHits { get; set; }
	}

	public class GlobalInnerHit : InnerHits, IGlobalInnerHit
	{
		public QueryContainer Query { get; set; }
		public INamedInnerHits InnerHits { get; set; }
	}

	public class GlobalInnerHitDescriptor<T> : DescriptorBase<GlobalInnerHitDescriptor<T>, IGlobalInnerHit>, IGlobalInnerHit
		where T : class
	{
		QueryContainer IGlobalInnerHit.Query { get; set; }
		INamedInnerHits IGlobalInnerHit.InnerHits { get; set; }
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

		public GlobalInnerHitDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) => 
			Assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
		
		public GlobalInnerHitDescriptor<T> InnerHits(Func<NamedInnerHitsDescriptor<T>, IPromise<INamedInnerHits>> selector) => 
			Assign(a => a.InnerHits = selector?.Invoke(new NamedInnerHitsDescriptor<T>())?.Value);

		public GlobalInnerHitDescriptor<T> From(int? from) => Assign(a => a.From = from);

		public GlobalInnerHitDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		public GlobalInnerHitDescriptor<T> Name(string name) => Assign(a => a.Name = name);

		public GlobalInnerHitDescriptor<T> FielddataFields(params string[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());
		
		public GlobalInnerHitDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		public GlobalInnerHitDescriptor<T> Explain(bool explain = true) => Assign(a => a.Explain = explain);

		public GlobalInnerHitDescriptor<T> Version(bool version = true) => Assign(a => a.Version = version);

		public GlobalInnerHitDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> sortSelector) => Assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public GlobalInnerHitDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));
		
		public GlobalInnerHitDescriptor<T> Source(bool include = true)=> Assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);
		
		public GlobalInnerHitDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> sourceSelector) =>
			Assign(a => a.Source = sourceSelector?.Invoke(new SourceFilterDescriptor<T>()));

		public GlobalInnerHitDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) => 
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);
	}
}