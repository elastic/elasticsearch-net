using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<TopHitsAggregator>))]
	public interface ITopHitsAggregator : IMetricAggregator
	{
		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("sort")]
		[JsonConverter(typeof(SortCollectionConverter))]
		IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }

		[JsonProperty("_source")]
		ISourceFilter Source { get; set; }

		[JsonProperty("highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty("explain")]
		bool? Explain { get; set; }

		[JsonProperty("script_fields")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IScriptQuery> ScriptFields { get; set; }

		[JsonProperty("fielddata_fields")]
		IEnumerable<PropertyPathMarker> FieldDataFields { get; set; }

		[JsonProperty("version")]
		bool? Version { get; set; }
	}

	public class TopHitsAggregator : MetricAggregator, ITopHitsAggregator
	{
		public int? From { get; set; }
		public int? Size { get; set; }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
		public ISourceFilter Source { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public bool? Explain { get; set; }
		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
		public IEnumerable<PropertyPathMarker> FieldDataFields { get; set; }
		public bool? Version { get; set; }
	}

	public class TopHitsAggregationDescriptor<T>
		: MetricAggregationBaseDescriptor<TopHitsAggregationDescriptor<T>, ITopHitsAggregator, T>
			, ITopHitsAggregator
		where T : class
	{

		int? ITopHitsAggregator.From { get; set; }

		int? ITopHitsAggregator.Size { get; set; }

		IList<KeyValuePair<PropertyPathMarker, ISort>> ITopHitsAggregator.Sort { get; set; }

		ISourceFilter ITopHitsAggregator.Source { get; set; }

		IHighlightRequest ITopHitsAggregator.Highlight { get; set; }

		bool? ITopHitsAggregator.Explain { get; set; }

		IDictionary<string, IScriptQuery> ITopHitsAggregator.ScriptFields { get; set; }

		IEnumerable<PropertyPathMarker> ITopHitsAggregator.FieldDataFields { get; set; }

		bool? ITopHitsAggregator.Version { get; set; }

		public TopHitsAggregationDescriptor<T> From(int from) => Assign(a => a.From = from);

		public TopHitsAggregationDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		public TopHitsAggregationDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			sortSelector.ThrowIfNull("sortSelector");

			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			this.Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(descriptor.Field, descriptor));

			return this;
		}

		public TopHitsAggregationDescriptor<T> Source(bool include = true) =>
			Assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);

		public TopHitsAggregationDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector) =>
			Assign(a => a.Source = sourceSelector?.Invoke(new SearchSourceDescriptor<T>()));

		public TopHitsAggregationDescriptor<T> Highlight(Func<HighlightDescriptor<T>, HighlightDescriptor<T>> highlightDescriptor) =>
			Assign(a => a.Highlight = highlightDescriptor?.Invoke(new HighlightDescriptor<T>()));

		public TopHitsAggregationDescriptor<T> Explain(bool explain = true) => Assign(a => a.Explain = explain);
		
		//TODO scriptfields needs a better encapsulation (seperate descriptor)
		public TopHitsAggregationDescriptor<T> ScriptFields(
			Func<FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>,
			 FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>> scriptFields)
		{
			scriptFields.ThrowIfNull("scriptFields");
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

		public TopHitsAggregationDescriptor<T> FieldDataFields(params PropertyPathMarker[] fields) =>
			Assign(a => a.FieldDataFields = fields);

		public TopHitsAggregationDescriptor<T> FieldDataFields(params Expression<Func<T, object>>[] objectPaths) =>
			Assign(a => a.FieldDataFields = objectPaths?.Select(e => (PropertyPathMarker) e).ToListOrNullIfEmpty());

		public TopHitsAggregationDescriptor<T> Version(bool version = true) => Assign(a => a.Version = version);

	}
}
