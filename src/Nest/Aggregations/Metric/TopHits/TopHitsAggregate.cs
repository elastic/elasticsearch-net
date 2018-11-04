using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly JsonSerializer _defaultSerializer;
		private readonly IEnumerable<JObject> _hits;

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IEnumerable<JObject> hits) => _hits = hits ?? Enumerable.Empty<JObject>();

		internal TopHitsAggregate(IEnumerable<JObject> hits, JsonSerializer serializer)
		{
			_hits = hits ?? Enumerable.Empty<JObject>();
			_defaultSerializer = serializer;
		}

		public double? MaxScore { get; set; }

		public long Total { get; set; }

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
			ConvertHits<T>(serializer).ToList().AsReadOnly();

		public IReadOnlyCollection<T> Documents<T>(JsonSerializer serializer = null) where T : class =>
			ConvertHits<T>(serializer).Select(h => h.Source).ToList().AsReadOnly();
	}
}
