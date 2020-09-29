// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Linq;
using Nest.Utf8Json;

namespace Nest
{
	public class TopHitsAggregate : MetricAggregateBase
	{
		private readonly IJsonFormatterResolver _formatterResolver;
		private readonly IList<LazyDocument> _hits;

		public TopHitsAggregate() { }

		internal TopHitsAggregate(IList<LazyDocument> hits, IJsonFormatterResolver formatterResolver)
		{
			_hits = hits;
			_formatterResolver = formatterResolver;
		}

		public double? MaxScore { get; set; }

		public TotalHits Total { get; set; }

		private IEnumerable<IHit<TDocument>> ConvertHits<TDocument>()
			where TDocument : class
		{
			var formatter = _formatterResolver.GetFormatter<IHit<TDocument>>();
			return _hits.Select(h =>
			{
				var reader = new JsonReader(h.Bytes);
				return formatter.Deserialize(ref reader, _formatterResolver);
			});
		}

		public IReadOnlyCollection<IHit<TDocument>> Hits<TDocument>()
			where TDocument : class =>
			ConvertHits<TDocument>().ToList().AsReadOnly();

		public IReadOnlyCollection<TDocument> Documents<TDocument>() where TDocument : class =>
			ConvertHits<TDocument>().Select(h => h.Source).ToList().AsReadOnly();
	}
}
