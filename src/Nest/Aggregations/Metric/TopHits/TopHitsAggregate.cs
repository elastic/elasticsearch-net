using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
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
			_hits = hits;
		}

		internal TopHitsAggregate(IEnumerable<JObject> hits, JsonSerializer serializer)
		{
			_hits = hits;
			_defaultSerializer = serializer;
		}

		public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer = null) 
			where T : class
		{
			var s = serializer ?? _defaultSerializer;

			return s != null 
				? _hits.Select(h => h.ToObject<Hit<T>>(s)) 
				: _hits.Select(h => h.ToObject<Hit<T>>());
		}

		public IEnumerable<T> Documents<T>(JsonSerializer serializer = null) where T : class =>
			this.Hits<T>(serializer).Select(h => h.Source);
	}
}
