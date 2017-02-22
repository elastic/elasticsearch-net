using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest_5_2_0
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly IEnumerable<JObject> _hits;

		private readonly JsonSerializer _defaultSerializer;

		public long Total { get; set; }

		public double? MaxScore { get; set; }

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IEnumerable<JObject> hits)
		{
			_hits = hits ?? Enumerable.Empty<JObject>();
		}

		internal TopHitsAggregate(IEnumerable<JObject> hits, JsonSerializer serializer)
		{
			_hits = hits ?? Enumerable.Empty<JObject>();
			_defaultSerializer = serializer;
		}

		private IEnumerable<Hit<T>> ConvertHits<T>(JsonSerializer serializer = null)
			where T : class
		{
			var s = serializer ?? _defaultSerializer;

			return s != null
				? _hits.Select(h => h.ToObject<Hit<T>>(s))
				: _hits.Select(h => h.ToObject<Hit<T>>());
		}

		public IReadOnlyCollection<Hit<T>> Hits<T>(JsonSerializer serializer = null)
			where T : class =>
			this.ConvertHits<T>(serializer).ToList().AsReadOnly();

		public IReadOnlyCollection<T> Documents<T>(JsonSerializer serializer = null) where T : class =>
			this.ConvertHits<T>(serializer).Select(h => h.Source).ToList().AsReadOnly();
	}
}
