using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase, ILazyDeserialize
	{
		[JsonProperty("hits")]
		private TopHitsProxy _hits { get; set; }

		JsonSerializer ILazyDeserialize.Serializer { get; set; }

		public long Total { get { return _hits.Total; } }
		public double? MaxScore { get { return _hits.MaxScore; } }

		public IEnumerable<Hit<T>> Hits<T>(JsonSerializer serializer = null)
			where T : class
		{
			var s = serializer ?? ((ILazyDeserialize)this).Serializer;
			return s != null
				? _hits.Hits.Select(h => h.ToObject<Hit<T>>(s))
				: _hits.Hits.Select(h => h.ToObject<Hit<T>>());
		}

		public IEnumerable<T> Documents<T>(JsonSerializer serializer = null) where T : class =>
			this.Hits<T>(serializer).Select(h => h.Source);
	}

	[JsonObject(MemberSerialization.OptIn)]
	internal class TopHitsProxy
	{
		[JsonProperty("total")]
		public long Total { get; set; }

		[JsonProperty("max_score")]
		public double? MaxScore { get; set; }

		[JsonProperty("hits")]
		public IEnumerable<JObject> Hits { get; set; }
	}
}
