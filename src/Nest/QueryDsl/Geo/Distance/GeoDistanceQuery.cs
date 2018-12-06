using System.Runtime.Serialization;
using System.Text;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	[JsonFormatter(typeof(GeoDistanceQueryFormatter))]
	public interface IGeoDistanceQuery : IFieldNameQuery
	{
		Distance Distance { get; set; }

		GeoDistanceType? DistanceType { get; set; }

		GeoLocation Location { get; set; }

		GeoValidationMethod? ValidationMethod { get; set; }
	}

	public class GeoDistanceQuery : FieldNameQueryBase, IGeoDistanceQuery
	{
		public Distance Distance { get; set; }
		public GeoDistanceType? DistanceType { get; set; }
		public GeoLocation Location { get; set; }
		public GeoValidationMethod? ValidationMethod { get; set; }
		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.GeoDistance = this;

		internal static bool IsConditionless(IGeoDistanceQuery q) =>
			q.Location == null || q.Distance == null || q.Field.IsConditionless();
	}

	public class GeoDistanceQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<GeoDistanceQueryDescriptor<T>, IGeoDistanceQuery, T>
			, IGeoDistanceQuery where T : class
	{
		protected override bool Conditionless => GeoDistanceQuery.IsConditionless(this);
		Distance IGeoDistanceQuery.Distance { get; set; }
		GeoDistanceType? IGeoDistanceQuery.DistanceType { get; set; }
		GeoLocation IGeoDistanceQuery.Location { get; set; }
		GeoValidationMethod? IGeoDistanceQuery.ValidationMethod { get; set; }

		public GeoDistanceQueryDescriptor<T> Location(GeoLocation location) => Assign(a => a.Location = location);

		public GeoDistanceQueryDescriptor<T> Location(double lat, double lon) => Assign(a => a.Location = new GeoLocation(lat, lon));

		public GeoDistanceQueryDescriptor<T> Distance(Distance distance) => Assign(a => a.Distance = distance);

		public GeoDistanceQueryDescriptor<T> Distance(double distance, DistanceUnit unit) => Assign(a => a.Distance = new Distance(distance, unit));

		public GeoDistanceQueryDescriptor<T> DistanceType(GeoDistanceType? type) => Assign(a => a.DistanceType = type);

		public GeoDistanceQueryDescriptor<T> ValidationMethod(GeoValidationMethod? validation) => Assign(a => a.ValidationMethod = validation);
	}

	internal class GeoDistanceQueryFormatter : IJsonFormatter<IGeoDistanceQuery>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "_name", 0 },
			{ "boost", 1 },
			{ "validation_method", 2 },
			{ "distance", 3 },
			{ "distance_type", 4 }
		};

		public IGeoDistanceQuery Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
				return null;

			var query = new GeoDistanceQuery();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var property = reader.ReadPropertyNameSegmentRaw();
				if (Fields.TryGetValue(property, out var value))
				{
					switch (value)
					{
						case 0:
							query.Name = reader.ReadString();
							break;
						case 1:
							query.Boost = reader.ReadDouble();
							break;
						case 2:
							query.ValidationMethod = formatterResolver.GetFormatter<GeoValidationMethod>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 3:
							query.Distance = formatterResolver.GetFormatter<Distance>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 4:
							query.DistanceType = formatterResolver.GetFormatter<GeoDistanceType>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}
				}
				else
				{
					query.Field = property.Utf8String();
					query.Location = formatterResolver.GetFormatter<GeoLocation>()
						.Deserialize(ref reader, formatterResolver);
				}
			}

			return query;
		}

		public void Serialize(ref JsonWriter writer, IGeoDistanceQuery value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var written = false;

			writer.WriteBeginObject();

			if (!value.Name.IsNullOrEmpty())
			{
				writer.WritePropertyName("_name");
				writer.WriteString(value.Name);
				written = true;
			}

			if (value.Boost != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("boost");
				writer.WriteDouble(value.Boost.Value);
				written = true;
			}

			if (value.ValidationMethod != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("validation_method");
				formatterResolver.GetFormatter<GeoValidationMethod>()
					.Serialize(ref writer, value.ValidationMethod.Value, formatterResolver);
				written = true;
			}

			if (value.Distance != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("distance");
				formatterResolver.GetFormatter<Distance>()
					.Serialize(ref writer, value.Distance, formatterResolver);
				written = true;
			}

			if (value.DistanceType != null)
			{
				if (written)
					writer.WriteValueSeparator();

				writer.WritePropertyName("distance_type");
				formatterResolver.GetFormatter<GeoDistanceType>()
					.Serialize(ref writer, value.DistanceType.Value, formatterResolver);
				written = true;
			}

			if (written)
				writer.WriteValueSeparator();

			var settings = formatterResolver.GetConnectionSettings();
			writer.WritePropertyName(settings.Inferrer.Field(value.Field));
			formatterResolver.GetFormatter<GeoLocation>()
				.Serialize(ref writer, value.Location, formatterResolver);

			writer.WriteEndObject();
		}
	}
}
