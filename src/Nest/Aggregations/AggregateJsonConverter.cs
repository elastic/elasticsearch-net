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
		private readonly string _aggregateTypeKey = "aggregate_type";

		public override bool CanConvert(Type objectType) => objectType == typeof(IAggregate);

		public override bool CanWrite => false;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);
			var aggregate = ReadAggregate(o);
			return aggregate;
		}

		private IAggregate ReadAggregate(JObject o)
		{
			var metaProperty = o.Property("meta");
			if (metaProperty == null) return null;

			var meta = metaProperty.Value.ToObject<Dictionary<string, object>>();
			if (!meta.ContainsKey(_aggregateTypeKey)) return null;

			var typeName = meta[_aggregateTypeKey] as string;

			// Remove the injected deserialization key/value so that we don't dirty the users expected results
			meta.Remove(_aggregateTypeKey);

			var type = Type.GetType(typeName, true);
			var aggregate = o.ToObject(type) as IAggregate;
			aggregate.Meta = meta.HasAny() ? meta : null;

			var bucketAggregate = aggregate as BucketAggregateBase;
			if (bucketAggregate != null)
			{
				var properties = o.Properties().Where(p => p.Name != "meta");
				bucketAggregate.Aggregations = ReadSubAggregates(properties);
			}

			MaybeReadBuckets<SignificantTermsBucket>(aggregate, o);
			MaybeReadBuckets<RangeBucket>(aggregate, o);
			MaybeReadBuckets<KeyedBucket>(aggregate, o);
			MaybeReadBuckets<HistogramBucket>(aggregate, o);

			return aggregate;
		}

		/// <summary>
		/// **If** the aggregate is a MultiBucketAggregate and has buckets then we'll read them, otherwise do nothing
		/// </summary>
		private void MaybeReadBuckets<TBucket>(IAggregate aggregate, JObject o)
			where TBucket : BucketBase
		{
			var multiBucketAggregate = aggregate as MultiBucketAggregate<TBucket>;

			if (multiBucketAggregate == null || multiBucketAggregate.Buckets == null || !multiBucketAggregate.Buckets.HasAny())
				return;

			foreach (var bucket in multiBucketAggregate.Buckets)
			{
				var bucketsProperty = o.Properties().Where(p => p.Name == "buckets").FirstOrDefault();
				if (bucketsProperty != null)
				{
					var bucketsArray = bucketsProperty.Value.ToObject<JArray>().Select(b => b.ToObject<JObject>());
					foreach (var bucketItem in bucketsArray)
					{
						if (bucketItem.Properties().Any(p => p.Name == "key" && bucket.Matches(p.Value)))
						{
							bucket.Aggregations = ReadSubAggregates(bucketItem.Properties());
						}
					}
				}
			}
		}

		private IDictionary<string, IAggregate> ReadSubAggregates(IEnumerable<JProperty> properties)
		{
			var aggregates = new Dictionary<string, IAggregate>();
			foreach (var property in properties.Where(p => p.Value.Type == JTokenType.Object))
			{
				var o = property.Value.ToObject<JObject>();
				var meta = o.Properties().Where(p => p.Name == "meta").FirstOrDefault();
				if (meta != null)
				{
					var aggregate = ReadAggregate(o);
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