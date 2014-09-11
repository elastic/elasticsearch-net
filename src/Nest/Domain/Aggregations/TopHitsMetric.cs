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

		public TopHitsMetric()
		{
		}
		
		internal TopHitsMetric(IEnumerable<JObject> hits)
		{
			_hits = hits;
		}

		public long Total { get; set; }
		public double? MaxScore { get; set; }

		public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer = null) where T : class
		{
			if (serializer != null)
				return _hits.Select(h => h.ToObject<Hit<T>>(serializer));
			return _hits.Select(h => h.ToObject<Hit<T>>());
		}

		public IEnumerable<T> Documents<T>(JsonSerializer serializer = null) where T : class
		{
			return this.Hits<T>(serializer).Select(h => h.Source);
		}


	}

}
