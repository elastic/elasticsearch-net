using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly IList<LazyDocument> _hits;

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IList<LazyDocument> hits) => _hits = hits;

		public double? MaxScore { get; set; }

		public long Total { get; set; }

		private IEnumerable<Hit<TDocument>> ConvertHits<TDocument>()
			where TDocument : class => _hits.Select(h => h.AsUsingRequestResponseSerializer<Hit<TDocument>>());

		public IReadOnlyCollection<Hit<TDocument>> Hits<TDocument>()
			where TDocument : class =>
			ConvertHits<TDocument>().ToList().AsReadOnly();

		public IReadOnlyCollection<TDocument> Documents<TDocument>() where TDocument : class =>
			ConvertHits<TDocument>().Select(h => h.Source).ToList().AsReadOnly();
	}
}
