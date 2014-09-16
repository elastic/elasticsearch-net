using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
	}

	public class TopHitsAggregator : MetricAggregator, ITopHitsAggregator
	{
		public int? From { get; set; }
		public int? Size { get; set; }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
		public ISourceFilter Source { get; set; }
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
	}
}
