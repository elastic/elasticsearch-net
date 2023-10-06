// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

internal class PartitionHelper<TDocument> : IEnumerable<IList<TDocument>>
{
	private readonly IEnumerable<TDocument> _items;
	private readonly int _partitionSize;
	private bool _hasMoreItems;

	internal PartitionHelper(IEnumerable<TDocument> i, int ps)
	{
		_items = i;
		_partitionSize = ps;
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	public IEnumerator<IList<TDocument>> GetEnumerator()
	{
		using (var enumerator = _items.GetEnumerator())
		{
			_hasMoreItems = enumerator.MoveNext();
			while (_hasMoreItems)
				yield return GetNextBatch(enumerator).ToList();
		}
	}

	private IEnumerable<TDocument> GetNextBatch(IEnumerator<TDocument> enumerator)
	{
		for (var i = 0; i < _partitionSize; ++i)
		{
			yield return enumerator.Current;

			_hasMoreItems = enumerator.MoveNext();
			if (!_hasMoreItems)
				yield break;
		}
	}
}
