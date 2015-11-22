using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	public interface IGlobalInnerHit : IInnerHits
	{
		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		[JsonConverter(typeof (VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHit : InnerHits, IGlobalInnerHit
	{
		public QueryContainer Query { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHitDescriptor<T> : IGlobalInnerHit where T : class
	{
		private IGlobalInnerHit Self => this;

		private GlobalInnerHitDescriptor<T> _assign(Action<IGlobalInnerHit> assigner) => Fluent.Assign(this, assigner);

		QueryContainer IGlobalInnerHit.Query { get; set; }
		IDictionary<string, IInnerHitsContainer> IGlobalInnerHit.InnerHits { get; set; }
		string IInnerHits.Name { get; set; }
		int? IInnerHits.From { get; set; }
		int? IInnerHits.Size { get; set; }
		IList<KeyValuePair<Field, ISort>> IInnerHits.Sort { get; set; }
		IHighlight IInnerHits.Highlight { get; set; }
		bool? IInnerHits.Explain { get; set; }
		ISourceFilter IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }
		IList<Field> IInnerHits.FielddataFields { get; set; }
		IDictionary<string, IScriptQuery> IInnerHits.ScriptFields { get; set; }

		public GlobalInnerHitDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector) => 
			_assign(a => a.Query = querySelector?.Invoke(new QueryContainerDescriptor<T>()));
		
		public GlobalInnerHitDescriptor<T> InnerHits(Func<
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>, 
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>
			> innerHitsSelector)
		{
			if (innerHitsSelector == null)
			{
				Self.InnerHits = null;
				return this;
			}
			var containers = innerHitsSelector(new FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>())
				.Where(kv => kv.Value != null)
				.Select(kv => new {Key = kv.Key, Value = kv.Value(new InnerHitsContainerDescriptor<T>())})
				.Where(kv => kv.Value != null)
				.ToDictionary(kv => kv.Key, kv => kv.Value);
			if (containers == null || containers.Count == 0)
			{
				Self.InnerHits = null;
				return this;
			}
			Self.InnerHits = containers;
			return this;
		}

		public GlobalInnerHitDescriptor<T> From(int? from) => _assign(a => a.From = from);

		public GlobalInnerHitDescriptor<T> Size(int? size) => _assign(a => a.Size = size);

		public GlobalInnerHitDescriptor<T> Name(string name) => _assign(a => a.Name = name);

		public GlobalInnerHitDescriptor<T> FielddataFields(params string[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());
		
		public GlobalInnerHitDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		public GlobalInnerHitDescriptor<T> Explain(bool explain = true) => _assign(a => a.Explain = explain);

		public GlobalInnerHitDescriptor<T> Version(bool version = true) => _assign(a => a.Version = version);

		public GlobalInnerHitDescriptor<T> Sort(Func<SortDescriptor<T>, SortDescriptor<T>> sortSelector) =>
			_assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>()).InternalSortState.ToListOrNullIfEmpty());

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public GlobalInnerHitDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			_assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));
		
		public GlobalInnerHitDescriptor<T> Source(bool include = true)=> _assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);
		
		public GlobalInnerHitDescriptor<T> Source(Func<SourceFilterDescriptor<T>, SourceFilterDescriptor<T>> sourceSelector) =>
			_assign(a => a.Source = sourceSelector?.Invoke(new SourceFilterDescriptor<T>()));

		//TODO ScriptFileds needs an encapsulated descriptor
		public GlobalInnerHitDescriptor<T> ScriptFields(
				Func<FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>,
				 FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>> scriptFields)
		{
			if (scriptFields == null) return null;

			var scriptFieldDescriptors = scriptFields(new FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>());
			if (scriptFieldDescriptors == null || scriptFieldDescriptors.All(d => d.Value == null))
			{
				Self.ScriptFields = null;
				return this;
			}
			Self.ScriptFields = new FluentDictionary<string, IScriptQuery>();
			foreach (var d in scriptFieldDescriptors)
			{
				if (d.Value == null)
					continue;
				Self.ScriptFields.Add(d.Key, d.Value(new ScriptQueryDescriptor<T>()));
			}
			return this;
		}
	}
}