// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net;
using Nest.Utf8Json;
namespace Nest
{
	/// <summary>
	/// Description of the total number of hits of a query. The total hit count
	/// can't generally be computed accurately without visiting all matches, which
	/// is costly for queries that match lots of documents. Given that it is often
	/// enough to have a lower bounds of the number of hits, such as
	/// "there are more than 1000 hits", Elasticsearch has options to stop counting as soon
	/// as a threshold has been reached in order to improve query times.
	/// </summary>
	[JsonFormatter(typeof(TotalHitsFormatter))]
	public class TotalHits
	{
		/// <summary>Whether <see cref="Value"/> is the exact hit count, in which case <see cref="Relation"/> is
		/// <see cref="TotalHitsRelation.EqualTo"/>, or a lower bound of the total hit count, in which case
		/// <see cref="Relation"/> is <see cref="TotalHitsRelation.GreaterThanOrEqualTo"/>
		/// </summary>
		[DataMember(Name = "relation")]
		public TotalHitsRelation? Relation { get; internal set; }

		/// <summary>The value of the total hit count. Must be interpreted in the context of <see cref="Relation"/></summary>
		[DataMember(Name = "value")]
		public long Value { get; internal set; }
	}

	/// <summary>How the <see cref="TotalHits.Value"/> should be interpreted</summary>
	[StringEnum]
	public enum TotalHitsRelation
	{
		/// <summary>The total hit count is equal to <see cref="TotalHits.Value"/></summary>
		[EnumMember(Value = "eq")]
		EqualTo,

		/// <summary>The total hit count is greater than or equal to <see cref="TotalHits.Value"/></summary>
		[EnumMember(Value = "gte")]
		GreaterThanOrEqualTo,
	}

	internal class TotalHitsFormatter : IJsonFormatter<TotalHits>
	{
		private static readonly byte[] ValueField = JsonWriter.GetEncodedPropertyNameWithoutQuotation("value");
		private static readonly byte[] RelationField = JsonWriter.GetEncodedPropertyNameWithoutQuotation("relation");
		private static readonly EnumFormatter<TotalHitsRelation> RelationFormatter = new EnumFormatter<TotalHitsRelation>(true);

		public TotalHits Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.BeginObject:
					var count = 0;
					long value = -1;
					TotalHitsRelation? relation = null;
					while (reader.ReadIsInObject(ref count))
					{
						var propertyName = reader.ReadPropertyNameSegmentRaw();
						if (propertyName.EqualsBytes(ValueField))
							value = reader.ReadInt64();
						else if (propertyName.EqualsBytes(RelationField))
							relation = RelationFormatter.Deserialize(ref reader, formatterResolver);
						else
							reader.ReadNextBlock();
					}

					return new TotalHits { Value = value, Relation = relation };
				case JsonToken.Number:
					return new TotalHits { Value = reader.ReadInt64() };
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, TotalHits value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value.Relation.HasValue)
			{
				writer.WriteBeginObject();
				writer.WritePropertyName("value");
				writer.WriteInt64(value.Value);
				writer.WriteValueSeparator();
				writer.WritePropertyName("relation");
				RelationFormatter.Serialize(ref writer, value.Relation.Value, formatterResolver);
				writer.WriteEndObject();
			}
			else
				writer.WriteInt64(value.Value);
		}
	}
}
