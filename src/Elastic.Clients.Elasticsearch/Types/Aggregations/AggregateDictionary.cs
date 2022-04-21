// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.Aggregations
{
	public partial class AggregateDictionary
	{
		public EmptyTermsAggregate? EmptyTerms(string key) => TryGet<EmptyTermsAggregate?>(key);

		public bool IsEmptyTerms(string key) => !BackingDictionary.TryGetValue(key, out var agg) || agg is EmptyTermsAggregate;

		public bool TryGetStringTerms(string key, out StringTermsAggregate? aggregate)
		{
			aggregate = null;

			if (BackingDictionary.TryGetValue(key, out var agg) && agg is StringTermsAggregate stringTermsAgg)
			{
				aggregate = stringTermsAgg;
				return true;
			}

			return false;
		}

		public AvgAggregate? Average(string key) => TryGet<AvgAggregate?>(key);

		public TermsAggregate<string> Terms(string key) => Terms<string>(key);

		public TermsAggregate<TKey> Terms<TKey>(string key)
		{
			if (!BackingDictionary.TryGetValue(key, out var agg))
			{
				return null;
			}

			switch (agg)
			{
				case EmptyTermsAggregate empty:
					return new TermsAggregate<TKey>
					{
						Buckets = Array.Empty<TermsBucket<TKey>>().ToReadOnlyCollection(),
						Meta = empty.Meta,
						DocCountErrorUpperBound = empty.DocCountErrorUpperBound,
						SumOtherDocCount = empty.SumOtherDocCount
					};
				case StringTermsAggregate stringTerms:
					var buckets = stringTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.Key }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = buckets,
						Meta = stringTerms.Meta,
						DocCountErrorUpperBound = stringTerms.DocCountErrorUpperBound,
						SumOtherDocCount = stringTerms.SumOtherDocCount
					};
				case DoubleTermsAggregate doubleTerms:
					var doubleTermsBuckets = doubleTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = doubleTermsBuckets,
						Meta = doubleTerms.Meta,
						DocCountErrorUpperBound = doubleTerms.DocCountErrorUpperBound,
						SumOtherDocCount = doubleTerms.SumOtherDocCount
					};
				case LongTermsAggregate longTerms:
					var longTermsBuckets = longTerms.Buckets.Select(b => new TermsBucket<TKey> { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = longTermsBuckets,
						Meta = longTerms.Meta,
						DocCountErrorUpperBound = longTerms.DocCountErrorUpperBound,
						SumOtherDocCount = longTerms.SumOtherDocCount
					};

					// TODO - Multi-terms
			}

			return null;
		}

		private static TKey GetKeyFromBucketKey<TKey>(object key) =>
			typeof(TKey).IsEnum
				? (TKey)Enum.Parse(typeof(TKey), key.ToString(), true)
				: (TKey)Convert.ChangeType(key, typeof(TKey));
	}
}
