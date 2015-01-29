using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class TopHitsMetric : IMetricAggregation
	{
		private IEnumerable<JObject> _hits;

		internal JsonSerializer _defaultSerializer;

		public TopHitsMetric()
		{
		}
		
		internal TopHitsMetric(IEnumerable<JObject> hits)
		{
			_hits = hits;
		}

		internal TopHitsMetric(IEnumerable<JObject> hits, JsonSerializer serializer)
		{
			_hits = hits;
			_defaultSerializer = serializer;
		}

		public long Total { get; set; }
		public double? MaxScore { get; set; }

		public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer = null) where T : class
		{
			var s = serializer ?? _defaultSerializer;

			if (s != null)
				return _hits.Select(h => h.ToObject<Hit<T>>(s));

			return _hits.Select(h => h.ToObject<Hit<T>>());
		}

		public IEnumerable<T> Documents<T>(JsonSerializer serializer = null) where T : class
		{
			return this.Hits<T>(serializer).Select(h => h.Source);
		}
	}
}
