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
		: MetricAggregationBaseDescriptor<TopHitsAggregationDescriptor<T>, T>, ITopHitsAggregator
		where T : class
	{
		ITopHitsAggregator Self { get { return this; } }

		int? ITopHitsAggregator.From { get; set; }

		int? ITopHitsAggregator.Size { get; set; }

		IList<KeyValuePair<PropertyPathMarker, ISort>> ITopHitsAggregator.Sort { get; set; }

		ISourceFilter ITopHitsAggregator.Source { get; set; }

		IHighlightRequest ITopHitsAggregator.Highlight { get; set; }

		bool? ITopHitsAggregator.Explain { get; set; }

		IDictionary<string, IScriptQuery> ITopHitsAggregator.ScriptFields { get; set; }

		IEnumerable<PropertyPathMarker> ITopHitsAggregator.FieldDataFields { get; set; }

		bool? ITopHitsAggregator.Version { get; set; }

		public TopHitsAggregationDescriptor<T> From(int from)
		{
			this.Self.From = from;
			return this;
		}

		public TopHitsAggregationDescriptor<T> Size(int size)
		{
			this.Self.Size = size;
			return this;
		}

		public TopHitsAggregationDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			sortSelector.ThrowIfNull("sortSelector");

			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();
			
			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			this.Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(descriptor.Field, descriptor));
			
			return this;
		}

		public TopHitsAggregationDescriptor<T> Source(bool include = true)
		{
			if (!include)
				this.Self.Source = new SourceFilter { Exclude = new PropertyPathMarker[] { "*" } };
			else
				this.Self.Source = null;

			return this;
		}

		public TopHitsAggregationDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector)
		{
			this.Self.Source = sourceSelector(new SearchSourceDescriptor<T>());
			return this;
		}

		public TopHitsAggregationDescriptor<T> Highlight(Func<HighlightDescriptor<T>, HighlightDescriptor<T>> highlightDescriptor)
		{
			highlightDescriptor.ThrowIfNull("highlightDescriptor");
			this.Self.Highlight = highlightDescriptor(new HighlightDescriptor<T>());
			return this;
		}

		public TopHitsAggregationDescriptor<T> Explain(bool explain = true)
		{
			this.Self.Explain = explain;
			return this;
		}

		public TopHitsAggregationDescriptor<T> ScriptFields(
			Func<FluentDictionary<string, Func<ScriptQueryDescriptor, ScriptQueryDescriptor>>,
		 FluentDictionary<string, Func<ScriptQueryDescriptor, ScriptQueryDescriptor>>> scriptFields)
		{
			scriptFields.ThrowIfNull("scriptFields");
			var scriptFieldDescriptors = scriptFields(new FluentDictionary<string, Func<ScriptQueryDescriptor, ScriptQueryDescriptor>>());
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
				Self.ScriptFields.Add(d.Key, d.Value(new ScriptQueryDescriptor()));
			}
			return this;
		}

		public TopHitsAggregationDescriptor<T> FieldDataFields(params PropertyPathMarker[] fields)
		{
			this.Self.FieldDataFields = fields;
			return this;
		}

		public TopHitsAggregationDescriptor<T> FieldDataFields(params Expression<Func<T, object>>[] objectPaths)
		{
			this.Self.FieldDataFields = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}

		public TopHitsAggregationDescriptor<T> Version (bool version = true)
		{
			this.Self.Version = version;
			return this;
		}
	}
}
