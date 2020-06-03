// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Text;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Nest;

namespace Nest
{
	internal enum ShapeFormat
	{
		Object,
		Array,
		WellKnownText,
		String,
	}

	/// <summary>
	/// Represents a point in the cartesian plane.
	/// </summary>
	[JsonFormatter(typeof(CartesianPointFormatter))]
	public class CartesianPoint : IEquatable<CartesianPoint>
	{
		internal ShapeFormat Format = ShapeFormat.Object;

		public float X { get; set; }
		public float Y { get; set; }

		public CartesianPoint()
		{
		}

		public CartesianPoint(float x, float y)
		{
			X = x;
			Y = y;
		}

		public bool Equals(CartesianPoint other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			return X.Equals(other.X) && Y.Equals(other.Y);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;

			return Equals((CartesianPoint)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode();
			}
		}

		public static CartesianPoint FromCoordinates(string coordinates)
		{
			var values = coordinates.Split(',');
			if (values.Length > 3 || values.Length < 2)
				throw new InvalidOperationException(
					$"failed to parse {coordinates}, expected 2 or 3 coordinates but found: {values.Length}");

			var s = values[0].Trim();
			if (!float.TryParse(s, out var x))
				throw new InvalidOperationException($"failed to parse float for x from {s}");

			s = values[1].Trim();
			if (!float.TryParse(s, out var y))
				throw new InvalidOperationException($"failed to parse float for y from {s}");

			if (values.Length > 2)
			{
				s = values[2].Trim();
				if (!float.TryParse(s, out var _))
					throw new InvalidOperationException($"failed to parse float for z from {s}");
			}

			return new CartesianPoint(x, y) { Format = ShapeFormat.String };
		}

		public static CartesianPoint FromWellKnownText(string wkt)
		{
			using var tokenizer = new WellKnownTextTokenizer(new StringReader(wkt));
			var token = tokenizer.NextToken();

			if (token != TokenType.Word)
				throw new GeoWKTException(
					$"Expected word but found {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);

			var type = tokenizer.TokenValue.ToUpperInvariant();
			if (type != GeoShapeType.Point)
				throw new GeoWKTException(
					$"Expected {GeoShapeType.Point} but found {type}", tokenizer.LineNumber, tokenizer.Position);

			if (GeoWKTReader.NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var x = Convert.ToSingle(GeoWKTReader.NextNumber(tokenizer));
			var y = Convert.ToSingle(GeoWKTReader.NextNumber(tokenizer));

			// ignore any z value for now
			if (GeoWKTReader.IsNumberNext(tokenizer))
				GeoWKTReader.NextNumber(tokenizer);

			var point = new CartesianPoint(x, y) { Format = ShapeFormat.WellKnownText };
			GeoWKTReader.NextCloser(tokenizer);

			return point;
		}

		public static implicit operator CartesianPoint(string value)
		{
			try
			{
				return value.IndexOf(",", StringComparison.InvariantCultureIgnoreCase) > -1
					? FromCoordinates(value)
					: FromWellKnownText(value);
			}
			catch
			{
				// implicit conversions should never fail
				return null;
			}
		}

		public static bool operator ==(CartesianPoint left, CartesianPoint right) => Equals(left, right);

		public static bool operator !=(CartesianPoint left, CartesianPoint right) => !Equals(left, right);
	}

	internal class CartesianPointFormatter : IJsonFormatter<CartesianPoint>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary { { "x", 0 }, { "y", 1 }, { "z", 2 } };

		public void Serialize(ref JsonWriter writer, CartesianPoint value, IJsonFormatterResolver formatterResolver)
		{
			if (value is null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Format)
			{
				case ShapeFormat.Object:
					writer.WriteBeginObject();
					writer.WritePropertyName("x");
					writer.WriteSingle(value.X);
					writer.WriteValueSeparator();
					writer.WritePropertyName("y");
					writer.WriteSingle(value.Y);
					writer.WriteEndObject();
					break;
				case ShapeFormat.Array:
					writer.WriteBeginArray();
					writer.WriteSingle(value.X);
					writer.WriteValueSeparator();
					writer.WriteSingle(value.Y);
					writer.WriteEndArray();
					break;
				case ShapeFormat.WellKnownText:
					writer.WriteQuotation();
					writer.WriteRaw(Encoding.UTF8.GetBytes(GeoShapeType.Point));
					writer.WriteByte((byte)' ');
					writer.WriteByte((byte)'(');
					writer.WriteSingle(value.X);
					writer.WriteByte((byte)' ');
					writer.WriteSingle(value.Y);
					writer.WriteByte((byte)')');
					writer.WriteQuotation();
					break;
				case ShapeFormat.String:
					writer.WriteQuotation();
					writer.WriteSingle(value.X);
					writer.WriteValueSeparator();
					writer.WriteSingle(value.Y);
					writer.WriteQuotation();
					break;
			}
		}

		public CartesianPoint Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.BeginObject:
				{
					var count = 0;
					var point = new CartesianPoint { Format = ShapeFormat.Object };
					while (reader.ReadIsInObject(ref count))
					{
						var property = reader.ReadPropertyNameSegmentRaw();
						if (Fields.TryGetValue(property, out var value))
						{
							switch (value)
							{
								case 0:
									point.X = reader.ReadSingle();
									break;
								case 1:
									point.Y = reader.ReadSingle();
									break;
								case 2:
									reader.ReadSingle();
									break;
							}
						}
						else
							throw new JsonParsingException($"Unknown property {property.Utf8String()} when parsing {nameof(CartesianPoint)}");
					}

					return point;
				}
				case JsonToken.BeginArray:
				{
					var count = 0;
					var point = new CartesianPoint { Format = ShapeFormat.Array };
					while (reader.ReadIsInArray(ref count))
					{
						switch (count)
						{
							case 1:
								point.X = reader.ReadSingle();
								break;
							case 2:
								point.Y = reader.ReadSingle();
								break;
							case 3:
								reader.ReadSingle();
								break;
							default:
								throw new JsonParsingException($"Expected 2 or 3 coordinates but found {count}");
						}
					}

					return point;
				}
				case JsonToken.String:
				{
					var value = reader.ReadString();
					return value.IndexOf(",", StringComparison.InvariantCultureIgnoreCase) > -1
						? CartesianPoint.FromCoordinates(value)
						: CartesianPoint.FromWellKnownText(value);
				}
				default:
					throw new JsonParsingException($"Unexpected token type {token} when parsing {nameof(CartesianPoint)}");
			}
		}
	}
}
