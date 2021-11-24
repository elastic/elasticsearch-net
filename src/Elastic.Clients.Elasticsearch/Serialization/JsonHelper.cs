using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class JsonHelper
{
	public static bool TryReadUntilStringPropertyValue(ref Utf8JsonReader reader, byte[] propertyNameBytes)
	{
		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.PropertyName && reader.ValueTextEquals(propertyNameBytes))
			{
				reader.Read();
				return true;
			}
		}

		return false;
	}
}
