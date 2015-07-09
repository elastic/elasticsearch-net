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
		IList<KeyValuePair<FieldName, ISort>> Sort { get; set; }

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
		IList<FieldName> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		[JsonConverter(typeof (DictionaryKeysAreNotFieldNamesJsonConverter))]
		IDictionary<string, IScriptQuery> ScriptFields { get; set; }
	}

	public class InnerHits : IInnerHits
	{
		public string Name { get; set; }

		public int? From { get; set; }

		public int? Size { get; set; }

		public IList<KeyValuePair<FieldName, ISort>> Sort { get; set; }

		public IHighlightRequest Highlight { get; set; }

		public bool? Explain { get; set; }

		public ISourceFilter Source { get; set; }

		public bool? Version { get; set; }

		public IList<FieldName> FielddataFields { get; set; }

		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class InnerHitsDescriptor<T> : IInnerHits where T : class
 	{
		private IInnerHits Self => this;

		private InnerHitsDescriptor<T> _assign(Action<IInnerHits> assigner) => Fluent.Assign(this, assigner);

		string IInnerHits.Name { get; set; }
		int? IInnerHits.From { get; set; }
		int? IInnerHits.Size { get; set; }
		IList<KeyValuePair<FieldName, ISort>> IInnerHits.Sort { get; set; }
		IHighlightRequest IInnerHits.Highlight { get; set; }
		bool? IInnerHits.Explain { get; set; }
		ISourceFilter IInnerHits.Source { get; set; }
		bool? IInnerHits.Version { get; set; }
		IList<FieldName> IInnerHits.FielddataFields { get; set; }
		IDictionary<string, IScriptQuery> IInnerHits.ScriptFields { get; set; }

		public InnerHitsDescriptor<T> From(int? from) => _assign(a => a.From = from);

		public InnerHitsDescriptor<T> Size(int? size) => _assign(a => a.Size = size);

		public InnerHitsDescriptor<T> Name(string name) => _assign(a => a.Name = name);

		public InnerHitsDescriptor<T> FielddataFields(params string[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (FieldName) f).ToListOrNullIfEmpty());
		
		public InnerHitsDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (FieldName) f).ToListOrNullIfEmpty());

		public InnerHitsDescriptor<T> Explain(bool explain = true) => _assign(a => a.Explain = explain);

		public InnerHitsDescriptor<T> Version(bool version = true) => _assign(a => a.Version = version);

		public InnerHitsDescriptor<T> Sort(Func<SortDescriptor<T>, SortDescriptor<T>> sortSelector) =>
			_assign(a => a.Sort = sortSelector?.Invoke(new SortDescriptor<T>()).InternalSortState.ToListOrNullIfEmpty());
		
		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public InnerHitsDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlightRequest> highlightSelector) =>
			_assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));
		
		public InnerHitsDescriptor<T> Source(bool include = true) => _assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);

		public InnerHitsDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector) =>
			_assign(a => a.Source = sourceSelector?.Invoke(new SearchSourceDescriptor<T>()));
		
		//TODO ScriptFileds needs an encapsulated descriptor
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
