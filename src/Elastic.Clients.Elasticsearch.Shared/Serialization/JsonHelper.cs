// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

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
