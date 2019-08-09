using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Nest
{
	/// <summary>
	/// Boosts the relevance score of documents closer to a provided origin date or point. For example, you can use this query to give
	/// more weight to documents closer to a certain date or location.
	/// You can use the distance_feature query to find the nearest neighbors to a location. You can also use the query in a bool
	/// search’s should filter to add boosted relevance scores to the bool query’s scores.
	/// </summary>
	[JsonFormatter(typeof(DistanceFeatureQueryFormatter))]
	[InterfaceDataContract]
	public interface IDistanceFeatureQuery : IFieldNameQuery
	{
		/// <summary>
		/// Date or point of origin used to calculate distances.
		// If the field value is a date or date_nanos field, the origin value must be a date. Date Math, such as now-1h, is supported.
		// If the field value is a geo_point field, the origin value must be a geopoint.
		/// </summary>
		[DataMember(Name = "origin")]
		Union<DateMath, GeoLocation> Origin { get; set; }

		/// <summary>
		/// Distance from the origin at which relevance scores receive half of the boost value.
		// If the field value is a date or date_nanos field, the pivot value must be a time unit, such as 1h or 10d.
		// If the field value is a geo_point field, the pivot value must be a distance unit, such as 1km or 12m.
		/// </summary>
		[DataMember(Name = "pivot")]
		Union<Time, Distance> Pivot { get; set; }
	}

	public class DistanceFeatureQuery : FieldNameQueryBase, IDistanceFeatureQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		internal static bool IsConditionless(IDistanceFeatureQuery q) => q.Field.IsConditionless() || q.Origin == null && q.Pivot == null;

		internal override void InternalWrapInContainer(IQueryContainer container) => container.DistanceFeature = this;

		public Union<DateMath, GeoLocation> Origin { get; set; }

		public Union<Time, Distance> Pivot { get; set; }
	}

	public class DistanceFeatureQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<DistanceFeatureQueryDescriptor<T>, IDistanceFeatureQuery, T>
			, IDistanceFeatureQuery where T : class
	{
		Union<DateMath, GeoLocation> IDistanceFeatureQuery.Origin { get; set; }

		Union<Time, Distance> IDistanceFeatureQuery.Pivot { get; set; }

		protected override bool Conditionless => DistanceFeatureQuery.IsConditionless(this);

		/// <inheritdoc cref="IDistanceFeatureQuery.Origin" />
		public DistanceFeatureQueryDescriptor<T> Origin(DateMath origin) =>
			Assign(origin, (a, v) => a.Origin = v);

		/// <inheritdoc cref="IDistanceFeatureQuery.Origin" />
		public DistanceFeatureQueryDescriptor<T> Origin(GeoLocation origin) =>
			Assign(origin, (a, v) => a.Origin = v);

		/// <inheritdoc cref="IDistanceFeatureQuery.Pivot" />
		public DistanceFeatureQueryDescriptor<T> Pivot(Time pivot) =>
			Assign(pivot, (a, v) => a.Pivot = v);

		/// <inheritdoc cref="IDistanceFeatureQuery.Pivot" />
		public DistanceFeatureQueryDescriptor<T> Pivot(Distance pivot) =>
			Assign(pivot, (a, v) => a.Pivot = v);
	}

	internal class DistanceFeatureQueryFormatter : IJsonFormatter<IDistanceFeatureQuery>
	{
		public void Serialize(ref JsonWriter writer, IDistanceFeatureQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();

			writer.WritePropertyName("field");
			var fieldFormatter = formatterResolver.GetFormatter<Field>();
			fieldFormatter.Serialize(ref writer, value.Field, formatterResolver);
			writer.WriteValueSeparator();

			writer.WritePropertyName("origin");
			formatterResolver.GetFormatter<Union<DateMath, GeoLocation>>().Serialize(ref writer, value.Origin, formatterResolver);

			writer.WriteValueSeparator();

			writer.WritePropertyName("pivot");
			formatterResolver.GetFormatter<Union<Time, Distance>>().Serialize(ref writer, value.Pivot, formatterResolver);

			writer.WriteValueSeparator();

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
			}

			writer.WriteEndObject();
		}

		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "field", 0 },
			{ "origin", 1 },
			{ "pivot", 2 },
			{ "boost", 3 }
		};

		public IDistanceFeatureQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
				return null;

			var query = new DistanceFeatureQuery();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				if (Fields.TryGetValue(reader.ReadPropertyNameSegmentRaw(), out var value))
				{
					switch (value)
					{
						case 0:
							query.Field = formatterResolver.GetFormatter<Field>().Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							query.Origin = formatterResolver.GetFormatter<Union<DateMath, GeoLocation>>().Deserialize(ref reader, formatterResolver);
							break;
						case 2:
							query.Pivot = formatterResolver.GetFormatter<Union<Time, Distance>>().Deserialize(ref reader, formatterResolver);
							break;
						case 3:
							query.Boost = reader.ReadDouble();
							break;
					}
				}
				else
					reader.ReadNextBlock();
			}

			return query;
		}
	}
}
