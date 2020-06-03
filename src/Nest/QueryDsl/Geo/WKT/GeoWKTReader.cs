// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Nest
{
	/// <summary>
	/// Reads Well-Known Text (WKT) into <see cref="IGeoShape" /> types
	/// </summary>
	public class GeoWKTReader
	{
		/// <summary>
		/// Reads Well-Known Text (WKT) into a new instance of <see cref="IGeoShape" />
		/// </summary>
		public static IGeoShape Read(string wellKnownText)
		{
			using (var tokenizer = new WellKnownTextTokenizer(new StringReader(wellKnownText)))
				return Read(tokenizer, null);
		}

		private static IGeoShape Read(WellKnownTextTokenizer tokenizer, string shapeType)
		{
			var token = tokenizer.NextToken();

			if (token != TokenType.Word)
				throw new GeoWKTException(
					$"Expected word but found {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);

			var type = tokenizer.TokenValue.ToUpperInvariant();

			if (shapeType != null && shapeType != GeoShapeType.GeometryCollection && type != shapeType)
				throw new GeoWKTException($"Expected geometry type {shapeType} but found {type}");

			switch (type)
			{
				case GeoShapeType.Point:
					var point = ParsePoint(tokenizer);
					point.Format = GeoFormat.WellKnownText;
					return point;
				case GeoShapeType.MultiPoint:
					var multiPoint = ParseMultiPoint(tokenizer);
					multiPoint.Format = GeoFormat.WellKnownText;
					return multiPoint;
				case GeoShapeType.LineString:
					var lineString = ParseLineString(tokenizer);
					lineString.Format = GeoFormat.WellKnownText;
					return lineString;
				case GeoShapeType.MultiLineString:
					var multiLineString = ParseMultiLineString(tokenizer);
					multiLineString.Format = GeoFormat.WellKnownText;
					return multiLineString;
				case GeoShapeType.Polygon:
					var polygon = ParsePolygon(tokenizer);
					polygon.Format = GeoFormat.WellKnownText;
					return polygon;
				case GeoShapeType.MultiPolygon:
					var multiPolygon = ParseMultiPolygon(tokenizer);
					multiPolygon.Format = GeoFormat.WellKnownText;
					return multiPolygon;
				case GeoShapeType.BoundingBox:
					var envelope = ParseBoundingBox(tokenizer);
					envelope.Format = GeoFormat.WellKnownText;
					return envelope;
				case GeoShapeType.GeometryCollection:
					var geometryCollection = ParseGeometryCollection(tokenizer);
					geometryCollection.Format = GeoFormat.WellKnownText;
					return geometryCollection;
				default:
					throw new GeoWKTException($"Unknown geometry type: {type}");
			}
		}

		private static PointGeoShape ParsePoint(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var point = new PointGeoShape(ParseCoordinate(tokenizer));
			NextCloser(tokenizer);

			return point;
		}

		private static MultiPointGeoShape ParseMultiPoint(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var coordinates = ParseCoordinates(tokenizer);
			return new MultiPointGeoShape(coordinates);
		}

		private static LineStringGeoShape ParseLineString(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var coordinates = ParseCoordinates(tokenizer);
			return new LineStringGeoShape(coordinates);
		}

		private static MultiLineStringGeoShape ParseMultiLineString(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var coordinates = ParseCoordinateLists(tokenizer);
			return new MultiLineStringGeoShape(coordinates);
		}

		private static PolygonGeoShape ParsePolygon(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var coordinates = ParseCoordinateLists(tokenizer);
			return new PolygonGeoShape(coordinates);
		}

		private static MultiPolygonGeoShape ParseMultiPolygon(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var coordinates = new List<IEnumerable<IEnumerable<GeoCoordinate>>>
			{
				ParseCoordinateLists(tokenizer)
			};

			while (NextCloserOrComma(tokenizer) == TokenType.Comma)
				coordinates.Add(ParseCoordinateLists(tokenizer));

			return new MultiPolygonGeoShape(coordinates);
		}

		private static EnvelopeGeoShape ParseBoundingBox(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var minLon = NextNumber(tokenizer);
			NextComma(tokenizer);
			var maxLon = NextNumber(tokenizer);
			NextComma(tokenizer);
			var maxLat = NextNumber(tokenizer);
			NextComma(tokenizer);
			var minLat = NextNumber(tokenizer);
			NextCloser(tokenizer);
			return new EnvelopeGeoShape(new[] { new GeoCoordinate(maxLat, minLon), new GeoCoordinate(minLat, maxLon) });
		}

		private static GeometryCollection ParseGeometryCollection(WellKnownTextTokenizer tokenizer)
		{
			if (NextEmptyOrOpen(tokenizer) == TokenType.Word)
				return null;

			var geometries = new List<IGeoShape>
			{
				Read(tokenizer, GeoShapeType.GeometryCollection)
			};

			while (NextCloserOrComma(tokenizer) == TokenType.Comma)
				geometries.Add(Read(tokenizer, null));

			return new GeometryCollection { Geometries = geometries };
		}

		private static List<IEnumerable<GeoCoordinate>> ParseCoordinateLists(WellKnownTextTokenizer tokenizer)
		{
			var coordinates = new List<IEnumerable<GeoCoordinate>>();

			NextEmptyOrOpen(tokenizer);
			coordinates.Add(ParseCoordinates(tokenizer));

			while (NextCloserOrComma(tokenizer) == TokenType.Comma)
			{
				NextEmptyOrOpen(tokenizer);
				coordinates.Add(ParseCoordinates(tokenizer));
			}

			return coordinates;
		}

		private static List<GeoCoordinate> ParseCoordinates(WellKnownTextTokenizer tokenizer)
		{
			var coordinates = new List<GeoCoordinate>();

			if (IsNumberNext(tokenizer) || tokenizer.NextToken() == TokenType.LParen)
				coordinates.Add(ParseCoordinate(tokenizer));

			while (NextCloserOrComma(tokenizer) == TokenType.Comma)
			{
				var isOpenParen = false;

				if (IsNumberNext(tokenizer) || (isOpenParen = tokenizer.NextToken() == TokenType.LParen))
					coordinates.Add(ParseCoordinate(tokenizer));

				if (isOpenParen)
					NextCloser(tokenizer);
			}

			return coordinates;
		}

		private static GeoCoordinate ParseCoordinate(WellKnownTextTokenizer tokenizer)
		{
			var lon = NextNumber(tokenizer);
			var lat = NextNumber(tokenizer);
			double? z = null;

			if (IsNumberNext(tokenizer))
				z = NextNumber(tokenizer);

			return z == null
				? new GeoCoordinate(lat, lon)
				: new GeoCoordinate(lat, lon, z.Value);
		}

		internal static void NextCloser(WellKnownTextTokenizer tokenizer)
		{
			if (tokenizer.NextToken() != TokenType.RParen)
				throw new GeoWKTException(
					$"Expected {(char)WellKnownTextTokenizer.RParen} " +
					$"but found: {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);
		}

		private static void NextComma(WellKnownTextTokenizer tokenizer)
		{
			if (tokenizer.NextToken() != TokenType.Comma)
				throw new GeoWKTException(
					$"Expected {(char)WellKnownTextTokenizer.Comma} but found: {tokenizer.TokenString()}",
					tokenizer.LineNumber,
					tokenizer.Position);
		}

		internal static TokenType NextEmptyOrOpen(WellKnownTextTokenizer tokenizer)
		{
			var token = tokenizer.NextToken();
			if (token == TokenType.LParen ||
				token == TokenType.Word && tokenizer.TokenValue.Equals(WellKnownTextTokenizer.Empty, StringComparison.OrdinalIgnoreCase))
				return token;

			throw new GeoWKTException(
				$"Expected {WellKnownTextTokenizer.Empty} or {(char)WellKnownTextTokenizer.LParen} " +
				$"but found: {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);
		}

		private static TokenType NextCloserOrComma(WellKnownTextTokenizer tokenizer)
		{
			var token = tokenizer.NextToken();
			if (token == TokenType.Comma || token == TokenType.RParen)
				return token;

			throw new GeoWKTException(
				$"Expected {(char)WellKnownTextTokenizer.Comma} or {(char)WellKnownTextTokenizer.RParen} " +
				$"but found: {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);
		}

		internal static double NextNumber(WellKnownTextTokenizer tokenizer)
		{
			if (tokenizer.NextToken() == TokenType.Word)
			{
				if (string.Equals(tokenizer.TokenValue, WellKnownTextTokenizer.NaN, StringComparison.OrdinalIgnoreCase))
					return double.NaN;

				if (double.TryParse(
					tokenizer.TokenValue,
					NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign | NumberStyles.AllowExponent,
					CultureInfo.InvariantCulture, out var d))
					return d;
			}

			throw new GeoWKTException(
				$"Expected number but found: {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);
		}

		internal static bool IsNumberNext(WellKnownTextTokenizer tokenizer)
		{
			var token = tokenizer.PeekToken();
			return token == TokenType.Word;
		}
	}

	/// <summary>
	/// Character types when parsing Well-Known Text
	/// </summary>
	internal enum CharacterType : byte
	{
		Whitespace,
		Alpha,
		Comment
	}

	/// <summary>
	/// Well-Known Text token types
	/// </summary>
	internal enum TokenType : byte
	{
		None,
		Word,
		LParen,
		RParen,
		Comma
	}

	/// <summary>
	/// Tokenizes a sequence of characters into Well-Known Text
	/// (WKT) <see cref="TokenType" />
	/// </summary>
	internal class WellKnownTextTokenizer : IDisposable
	{
		public const int CarriageReturn = '\r';
		private const int CharacterTypesLength = 256;
		public const int Comma = ',';
		public const int Comment = '#';
		public const int Dot = '.';
		public const string Empty = "EMPTY";

		public const int Linefeed = '\n';
		public const int LParen = '(';
		public const int Minus = '-';
		public const string NaN = "NAN";
		private const int NeedChar = int.MaxValue;
		public const int Plus = '+';
		public const int RParen = ')';

		private static readonly CharacterType[] CharacterTypes = new CharacterType[CharacterTypesLength];
		private readonly List<char> _buffer = new List<char>();

		private readonly TextReader _reader;
		private int _peekChar = NeedChar;
		private bool _pushed;

		static WellKnownTextTokenizer()
		{
			// build a map of ASCII chars and their types
			// Any unmapped ASCII will be considered whitespace
			// and anything > 0 outside of ASCII will be considered alpha.
			Chars('a', 'z', CharacterType.Alpha);
			Chars('A', 'Z', CharacterType.Alpha);
			Chars(128 + 32, 255, CharacterType.Alpha);
			Chars('0', '9', CharacterType.Alpha);
			Chars(LParen, RParen, CharacterType.Alpha);
			Chars(Plus, Plus, CharacterType.Alpha);
			Chars(Comma, Comma, CharacterType.Alpha);
			Chars(Minus, Dot, CharacterType.Alpha);
			Chars(Comment, Comment, CharacterType.Comment);
		}

		public WellKnownTextTokenizer(TextReader reader) =>
			_reader = reader ?? throw new ArgumentNullException(nameof(reader));

		/// <summary>
		/// Gets the current line number
		/// </summary>
		public int LineNumber { get; private set; } = 1;

		/// <summary>
		/// Gets the current position
		/// </summary>
		public int Position { get; private set; }

		/// <summary>
		/// Gets the current token type
		/// </summary>
		public TokenType TokenType { get; private set; } = TokenType.None;

		/// <summary>
		/// Gets the current token value
		/// </summary>
		public string TokenValue { get; private set; }

		/// <summary>
		/// Disposes of the reader from which characters are read
		/// </summary>
		public void Dispose() => _reader?.Dispose();

		private static void Chars(int low, int high, CharacterType type)
		{
			if (low < 0)
				low = 0;

			if (high >= CharacterTypesLength)
				high = CharacterTypesLength - 1;

			while (low <= high)
				CharacterTypes[low++] = type;
		}

		/// <summary>
		/// A user friendly string for the current token
		/// </summary>
		public string TokenString()
		{
			switch (TokenType)
			{
				case TokenType.Word:
					return TokenValue;
				case TokenType.None:
					return "END-OF-STREAM";
				case TokenType.LParen:
					return "(";
				case TokenType.RParen:
					return ")";
				case TokenType.Comma:
					return ",";
				default:
					return $"\'{(char)_peekChar}\'";
			}
		}

		private int Read()
		{
			Position++;
			return _reader.Read();
		}

		/// <summary>
		/// Peeks at the next token without changing the state
		/// of the reader
		/// </summary>
		public TokenType PeekToken()
		{
			var position = Position;
			var token = NextToken();
			Position = position;
			_pushed = true;
			return token;
		}

		/// <summary>
		/// Gets the next token, advancing the position
		/// </summary>
		public TokenType NextToken()
		{
			if (_pushed)
			{
				_pushed = false;

				// Add the length of peeked token
				Position += !string.IsNullOrEmpty(TokenValue)
					? 1 + TokenValue.Length
					: 1;

				return TokenType;
			}

			TokenValue = null;

			var c = _peekChar;
			if (c < 0)
				c = NeedChar;

			if (c == NeedChar)
			{
				c = Read();
				if (c < 0)
					return TokenType = TokenType.None;
			}

			// reset the peek character for next token
			_peekChar = NeedChar;

			var characterType = c < CharacterTypesLength
				? CharacterTypes[c]
				: CharacterType.Alpha;

			// consume all whitespace
			while (characterType == CharacterType.Whitespace)
			{
				if (c == CarriageReturn)
				{
					LineNumber++;
					Position = 0;
					c = Read();
					if (c == Linefeed)
						c = Read();
				}
				else
				{
					if (c == Linefeed)
					{
						LineNumber++;
						Position = 0;
					}

					c = Read();
				}

				if (c < 0)
					return TokenType = TokenType.None;

				characterType = c < CharacterTypesLength
					? CharacterTypes[c]
					: CharacterType.Alpha;
			}

			switch (c)
			{
				case LParen:
					return TokenType = TokenType.LParen;
				case RParen:
					return TokenType = TokenType.RParen;
				case Comma:
					return TokenType = TokenType.Comma;
			}

			if (characterType == CharacterType.Alpha)
			{
				var i = 0;

				do
				{
					_buffer.Insert(i++, (char)c);
					c = Read();

					if (c < 0)
						characterType = CharacterType.Whitespace;
					else if (c < CharacterTypesLength)
					{
						if (c == LParen || c == RParen || c == Comma)
							break;

						characterType = CharacterTypes[c];
					}
					else
						characterType = CharacterType.Alpha;
				} while (characterType == CharacterType.Alpha);

				_peekChar = c;
				TokenValue = new string(_buffer.ToArray(), 0, i);

				return TokenType = TokenType.Word;
			}

			if (characterType == CharacterType.Comment)
			{
				// consume all characters on comment line
				while ((c = Read()) != Linefeed && c != CarriageReturn && c >= 0) { }

				_peekChar = c;
				return NextToken();
			}

			return TokenType = TokenType.None;
		}
	}
}
