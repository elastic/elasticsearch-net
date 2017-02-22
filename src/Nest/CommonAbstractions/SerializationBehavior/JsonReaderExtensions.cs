using Newtonsoft.Json;

namespace Nest_5_2_0
{
	internal static class JsonReaderExtensions
	{
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
	}
}
