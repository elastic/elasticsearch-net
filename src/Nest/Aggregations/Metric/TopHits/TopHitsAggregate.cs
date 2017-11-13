using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly IList<LazyDocument> _hits;

		public long Total { get; set; }

		public double? MaxScore { get; set; }

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IList<LazyDocument> hits)
		{
			_hits = hits;
		}

		private IEnumerable<Hit<TDocument>> ConvertHits<TDocument>()
			where TDocument : class => _hits.Select(h => h.As<Hit<TDocument>>());

		public IReadOnlyCollection<Hit<TDocument>> Hits<TDocument>()
			where TDocument : class =>
			this.ConvertHits<TDocument>().ToList().AsReadOnly();

		public IReadOnlyCollection<TDocument> Documents<TDocument>() where TDocument : class =>
			this.ConvertHits<TDocument>().Select(h => h.Source).ToList().AsReadOnly();
	}
}
