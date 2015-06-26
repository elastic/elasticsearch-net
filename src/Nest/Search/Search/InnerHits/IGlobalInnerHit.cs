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
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHit : InnerHits, IGlobalInnerHit
	{
		public IQueryContainer Query { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
	}

	public class GlobalInnerHitDescriptor<T> : IGlobalInnerHit where T : class
	{
		private IGlobalInnerHit Self { get { return this; } }

		IQueryContainer IGlobalInnerHit.Query { get; set; }
		IDictionary<string, IInnerHitsContainer> IGlobalInnerHit.InnerHits { get; set; }
		string IInnerHits.Name { get; set; }
		int? IInnerHits.From { get; set; }
		int? IInnerHits.Size { get; set; }
		IList<KeyValuePair<PropertyPathMarker, ISort>> IInnerHits.Sort { get; set; }
		IHighlightRequest IInnerHits.Highlight { get; set; }
		bool? IInnerHits.Explain { get; set; }
		ISourceFilter IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }
		IList<PropertyPathMarker> IInnerHits.FielddataFields { get; set; }
		IDictionary<string, IScriptQuery> IInnerHits.ScriptFields { get; set; }

		public GlobalInnerHitDescriptor<T> Query(Func<QueryDescriptor<T>, IQueryContainer> querySelector)
		{
			Self.Query = querySelector == null ? null : querySelector(new QueryDescriptor<T>());
			return this;
		}

		public GlobalInnerHitDescriptor<T> InnerHits(
			Func<
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

		public GlobalInnerHitDescriptor<T> From(int? from)
		{
			Self.From = from;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Size(int? size)
		{
			Self.Size = size;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public GlobalInnerHitDescriptor<T> FielddataFields(params string[] fielddataFields)
		{
			if (fielddataFields.HasAny())
				return this;
			Self.FielddataFields = fielddataFields.Select(f => (PropertyPathMarker)f).ToList();
			return this;
		}

		public GlobalInnerHitDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields)
		{
			if (!fielddataFields.HasAny())
				return this;
			Self.FielddataFields = fielddataFields.Select(e => (PropertyPathMarker)e).ToList();
			return this;
		}

		public GlobalInnerHitDescriptor<T> Explain(bool explain = true)
		{
			Self.Explain = explain;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Version(bool version = true)
		{
			Self.Version = version;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Sort(Func<SortDescriptor<T>, SortDescriptor<T>> sortSelector)
		{
			if (sortSelector == null) return this;

			var descriptor = sortSelector(new SortDescriptor<T>());

			Self.Sort = descriptor.InternalSortState.Count == 0 ? null : descriptor.InternalSortState;
			return this;
		}

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public GlobalInnerHitDescriptor<T> Highlight(Action<HighlightDescriptor<T>> highlightDescriptor)
		{
			highlightDescriptor.ThrowIfNull("highlightDescriptor");
			var d = new HighlightDescriptor<T>();
			highlightDescriptor(d);
			Self.Highlight = d;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Source(bool include = true)
		{
			if (!include)
			{
				Self.Source = new SourceFilter
				{
					Exclude = new PropertyPathMarker[] {"*"}
				};
			}
			else Self.Source = null;
			return this;
		}

		public GlobalInnerHitDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector)
		{
			if (sourceSelector == null) return this;
			Self.Source = sourceSelector(new SearchSourceDescriptor<T>());
			return this;
		}

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