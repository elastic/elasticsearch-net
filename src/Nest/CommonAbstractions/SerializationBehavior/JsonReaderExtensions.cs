using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal static class JsonReaderExtensions
	{
		// https://github.com/JamesNK/Newtonsoft.Json/issues/862
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static JToken ReadTokenWithDateParseHandlingNone(this JsonReader reader)
		{
			var dateParseHandling = reader.DateParseHandling;
			reader.DateParseHandling = DateParseHandling.None;
			var token = JToken.ReadFrom(reader);
			reader.DateParseHandling = dateParseHandling;
			return token;
		}

		public static T ReadToEnd<T>(this JsonReader reader, int depth, T r = null)
			where T : class
		{
			if (reader.Depth <= depth) return r;
			do
			{
				reader.Read();
			} while (reader.Depth >= depth && reader.TokenType != JsonToken.EndObject);
			return r;
		}

		public static T ExhaustTo<T>(this JsonReader reader, int depth, T r = null, JsonToken endType = JsonToken.EndObject)
			where T : class
		{
			if (reader.Depth < depth) return r;
			while (reader.Depth >= depth)
			{
				if (reader.Depth == depth && reader.TokenType == endType)
				{
					break;
				}
				var readAnything = reader.Read();
				if (!readAnything || reader.Depth < depth) break;
			}
			return r;
		}

		public static object ExhaustTo(this JsonReader reader, int depth, JsonToken endType = JsonToken.EndObject) => reader.ExhaustTo<object>(depth, null, endType);
	}
}
