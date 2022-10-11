// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.Aggregations;
using System;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.Experimental
{
	internal static class AggregateDictionaryExtensions
	{
		/// <summary>
		/// WARNING: EXPERIMENTAL API
		/// <para>This API provides simplified access to terms aggregations.</para>
		/// </summary>
		/// <remarks>Experimental APIs are subject to changes or removal and should be used with caution.</remarks>
		public static TermsAggregate<string> GetTerms(this AggregateDictionary aggregationDictionary, string key) => aggregationDictionary.GetTerms<string>(key);

		/// <summary>
		/// WARNING: EXPERIMENTAL API
		/// <para>This API provides simplified access to terms aggregations.</para>
		/// </summary>
		/// <remarks>Experimental APIs are subject to changes or removal and should be used with caution.</remarks>
		public static TermsAggregate<TKey> GetTerms<TKey>(this AggregateDictionary aggregationDictionary, string key)
		{
			if (!aggregationDictionary.TryGetValue(key, out var agg))
			{
				return null;
			}

			switch (agg)
			{
				case StringTermsAggregate stringTerms:
					var buckets = stringTerms.Buckets.Select(b => new TermsBucket<TKey>(b.BackingDictionary) { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.Key.ToString() }).ToReadOnlyCollection(); // ToString is a temp hack here
					return new TermsAggregate<TKey>
					{
						Buckets = buckets,
						Meta = stringTerms.Meta,
						DocCountErrorUpperBound = stringTerms.DocCountErrorUpperBound,
						SumOtherDocCount = stringTerms.SumOtherDocCount
					};

				case DoubleTermsAggregate doubleTerms:
					var doubleTermsBuckets = doubleTerms.Buckets.Select(b => new TermsBucket<TKey>(b.BackingDictionary) { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
					return new TermsAggregate<TKey>
					{
						Buckets = doubleTermsBuckets,
						Meta = doubleTerms.Meta,
						DocCountErrorUpperBound = doubleTerms.DocCountErrorUpperBound,
						SumOtherDocCount = doubleTerms.SumOtherDocCount
					};

				case LongTermsAggregate longTerms:
					var longTermsBuckets = longTerms.Buckets.Select(b => new TermsBucket<TKey>(b.BackingDictionary) { DocCount = b.DocCount, DocCountError = b.DocCountError, Key = GetKeyFromBucketKey<TKey>(b.Key), KeyAsString = b.KeyAsString }).ToReadOnlyCollection();
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

		private static TKey GetKeyFromBucketKey<TKey>(object key)
		{
			if (typeof(TKey).IsEnum)
			{
				return (TKey)Enum.Parse(typeof(TKey), key.ToString(), true);
			}

			if (key is FieldValue fieldValue)
			{
				return (TKey)Convert.ChangeType(fieldValue.Value, typeof(TKey));
			}

			return (TKey)Convert.ChangeType(key, typeof(TKey));
		}
	}
}
