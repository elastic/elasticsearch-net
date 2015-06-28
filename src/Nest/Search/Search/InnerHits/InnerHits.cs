using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	public interface IInnerHits
	{
		[JsonProperty("name")]
		string Name { get; set; }

		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "sort")]
		[JsonConverter(typeof(SortCollectionConverter))]
		IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "explain")]
		bool? Explain { get; set; }

		[JsonProperty(PropertyName = "_source")]
		[JsonConverter(typeof(ReadAsTypeConverter<SourceFilter>))]
		ISourceFilter Source { get; set; }

		[JsonProperty(PropertyName = "version")]
		bool? Version { get; set; }

		[JsonProperty(PropertyName = "fielddata_fields")]
		IList<PropertyPathMarker> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IScriptQuery> ScriptFields { get; set; }
	}


	public class InnerHits : IInnerHits
	{
		public string Name { get; set; }

		public int? From { get; set; }

		public int? Size { get; set; }

		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }

		public IHighlightRequest Highlight { get; set; }

		public bool? Explain { get; set; }

		public ISourceFilter Source { get; set; }

		public bool? Version { get; set; }

		public IList<PropertyPathMarker> FielddataFields { get; set; }

		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class InnerHitsDescriptor<T> : IInnerHits where T : class
 	{
		private IInnerHits Self => this;
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

		public InnerHitsDescriptor<T> From(int? from)
		{
			Self.From = from;
			return this;
		}

		public InnerHitsDescriptor<T> Size(int? size)
		{
			Self.Size = size;
			return this;
		}

		public InnerHitsDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public InnerHitsDescriptor<T> FielddataFields(params string[] fielddataFields)
		{
			if (!fielddataFields.HasAny())
				return this;
			Self.FielddataFields = fielddataFields.Select(f => (PropertyPathMarker)f).ToList();
			return this;
		}

		public InnerHitsDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields)
		{
			if (!fielddataFields.HasAny())
				return this;
			Self.FielddataFields = fielddataFields.Select(e => (PropertyPathMarker)e).ToList();
			return this;
		}

		public InnerHitsDescriptor<T> Explain(bool explain = true)
		{
			Self.Explain = explain;
			return this;
		}

		public InnerHitsDescriptor<T> Version(bool version = true)
		{
			Self.Version = version;
			return this;
		}

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, SortDescriptor<T>> sortSelector)
		{
			if (sortSelector == null) return this;

			var descriptor = sortSelector(new SortDescriptor<T>());

			Self.Sort = descriptor.InternalSortState.Count == 0 ? null : descriptor.InternalSortState;
			return this;
		}

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public InnerHitsDescriptor<T> Highlight(Action<HighlightDescriptor<T>> highlightDescriptor)
		{
			highlightDescriptor.ThrowIfNull("highlightDescriptor");
			var d = new HighlightDescriptor<T>();
			highlightDescriptor(d);
			Self.Highlight = d;
			return this;
		}

		public InnerHitsDescriptor<T> Source(bool include = true)
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

		public InnerHitsDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector)
		{
			if (sourceSelector == null) return this;
			Self.Source = sourceSelector(new SearchSourceDescriptor<T>());
			return this;
		}

		public InnerHitsDescriptor<T> ScriptFields(
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
