using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class AggregateJsonConverter : JsonConverter
	{
				public override bool CanConvert(Type objectType) => objectType == typeof(IAggregate);

		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var root = JObject.Load(reader);
			var aggregate = ReadAggregate(root, serializer);
			return aggregate;
		}

		private IAggregate ReadAggregate(JObject jObject, JsonSerializer serializer)
		{
			var metaProperty = jObject.Property("meta");
			if (metaProperty == null)
				throw new Exception("Cannot deserialize aggregation result. The response is missing metadata.");

			var meta = metaProperty.Value.ToObject<Dictionary<string, object>>();
			if (!meta.ContainsKey(AggregationMetadata.Key))
				throw new Exception($"Cannot deserialize aggregation result. Metadata is missing key: {AggregationMetadata.Key}.");

			var type = (string)meta[AggregationMetadata.Key];
			// Remove the injected metadata so that we don't dirty the users results
			meta.Remove(AggregationMetadata.Key);

			var aggregate = jObject.ToObject(AggregationMetadata.Map[type]) as IAggregate;
			aggregate.Meta = meta.HasAny() ? meta : null;

			var topHits = aggregate as TopHitsAggregate;
			if (topHits != null)
				topHits.Serializer = serializer;

			var bucketAggregate = aggregate as BucketAggregateBase;
			if (bucketAggregate != null)
			{
				var properties = jObject.Properties().Where(p => p.Name != "meta");
				bucketAggregate.Aggregations = ReadSubAggregates(properties, serializer);
			}

			MaybeReadBuckets<SignificantTermsBucket>(aggregate, jObject, serializer);
			MaybeReadBuckets<RangeBucket>(aggregate, jObject, serializer);
			MaybeReadBuckets<KeyedBucket>(aggregate, jObject, serializer);
			MaybeReadBuckets<HistogramBucket>(aggregate, jObject, serializer);
			MaybeReadBuckets<DateHistogramBucket>(aggregate, jObject, serializer);
			MaybeReadBuckets<FiltersBucket>(aggregate, jObject, serializer);

			return aggregate;
		}

		/// <summary>
		/// **If** the aggregate is a MultiBucketAggregate and has buckets then we'll read them, otherwise do nothing
		/// </summary>
		private void MaybeReadBuckets<TBucket>(IAggregate aggregate, JObject jObject, JsonSerializer serializer)
			where TBucket : BucketBase
		{
			var multiBucketAggregate = aggregate as MultiBucketAggregate<TBucket>;

			if (multiBucketAggregate == null || multiBucketAggregate.Buckets == null || !multiBucketAggregate.Buckets.HasAny())
				return;

			foreach (var bucket in multiBucketAggregate.Buckets)
			{
				var bucketsProperty = jObject.Properties().FirstOrDefault(p => p.Name == "buckets");
				if (bucketsProperty != null)
				{
					var bucketsArray = bucketsProperty.Value.ToObject<JArray>().Select(b => b.ToObject<JObject>());
					foreach (var bucketItem in bucketsArray)
					{
						if (bucketItem.Properties().Any(p => p.Name == "key" && bucket.Matches(p.Value)))
						{
							bucket.Aggregations = ReadSubAggregates(bucketItem.Properties(), serializer);
						}
					}
				}
			}
		}

		private IDictionary<string, IAggregate> ReadSubAggregates(IEnumerable<JProperty> properties, JsonSerializer serializer)
		{
			var aggregates = new Dictionary<string, IAggregate>();
			foreach (var property in properties.Where(p => p.Value.Type == JTokenType.Object))
			{
				var jObject = property.Value.ToObject<JObject>();
				var meta = jObject.Properties().FirstOrDefault(p => p.Name == "meta");
				if (meta != null)
				{
					var aggregate = ReadAggregate(jObject, serializer);
					aggregates.Add(property.Name, aggregate);
				}
			}
			return aggregates.HasAny() ? aggregates : null;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}
}
