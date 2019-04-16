using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly IList<LazyDocument> _hits;
		private readonly IJsonFormatterResolver _formatterResolver;

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IList<LazyDocument> hits, IJsonFormatterResolver formatterResolver)
		{
			_hits = hits;
			_formatterResolver = formatterResolver;
		}

		public double? MaxScore { get; set; }

		public HitsTotal Total { get; set; }

		private IEnumerable<Hit<TDocument>> ConvertHits<TDocument>()
			where TDocument : class
		{
			var formatter = _formatterResolver.GetFormatter<Hit<TDocument>>();		
			return _hits.Select(h =>
			{
				var reader = new JsonReader(h.Bytes);
				return formatter.Deserialize(ref reader, _formatterResolver);
			});
		}

		public IReadOnlyCollection<Hit<TDocument>> Hits<TDocument>()
			where TDocument : class =>
			ConvertHits<TDocument>().ToList().AsReadOnly();

		public IReadOnlyCollection<TDocument> Documents<TDocument>() where TDocument : class =>
			ConvertHits<TDocument>().Select(h => h.Source).ToList().AsReadOnly();
	}
}
