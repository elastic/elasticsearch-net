using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	internal class UpgradeStatusResponseJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new UpgradeStatusResponse();
			reader.Read();
			if (reader.TokenType == JsonToken.EndObject)
				return response;
			bool newResponse = false;
			if ((reader.Value as string) == "size_in_bytes")
			{
				newResponse = true;
				reader.Read();
				response.SizeInBytes = (long)reader.Value;
				reader.Read();
				reader.Read();
				response.SizeToUpgradeInBytes = reader.Value.ToString();
				reader.Read();
				reader.Read();
				response.SizeToUpgradeAncientInBytes = reader.Value.ToString();
				reader.Read();
				if (reader.TokenType != JsonToken.EndObject)
				{
					reader.Read();
					reader.Read();
				}
			}
			var upgrades = new Dictionary<string, UpgradeStatus>();
			while(true)
			{
				var status = new UpgradeStatus();
				var index = reader.Value as string;
				reader.Read();
				reader.Read();
				reader.Read();
				status.SizeInBytes = (long)reader.Value;
				reader.Read();
				reader.Read();
				status.SizeToUpgradeInBytes = reader.Value.ToString();
				if (newResponse)
				{
					reader.Read();
					reader.Read();
					status.SizeToUpgradeAncientInBytes = reader.Value.ToString();
				}
				upgrades.Add(index, status);
				reader.Read();
				reader.Read();
				if (reader.TokenType == JsonToken.EndObject)
					break;
			}
			reader.Read();
			response.Upgrades = upgrades;
			return response;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
