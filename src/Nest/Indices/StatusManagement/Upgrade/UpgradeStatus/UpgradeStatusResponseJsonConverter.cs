using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Nest
{
	// TODO: See if this is actually needed
//	internal class UpgradeStatusResponseFormatter : IJsonFormatter<UpgradeStatusResponse>
//	{
//		public void Serialize(ref JsonWriter writer, UpgradeStatusResponse value, IJsonFormatterResolver formatterResolver)
//		{
//			throw new NotSupportedException();
//		}
//
//		public UpgradeStatusResponse Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
//		{
//			var response = new UpgradeStatusResponse();
//			reader.ReadNext();
//			if (reader.GetCurrentJsonToken() == JsonToken.EndObject)
//				return response;
//
//			var newResponse = false;
//
//			var count = 0;
//			while (reader.ReadIsInObject(ref count))
//			{
//				var propertyName = reader.ReadPropertyName();
//				if (propertyName == "size_in_bytes")
//				{
//					newResponse = true;
//					response.SizeInBytes = reader.ReadInt64();
//
//					reader.ReadNext();
//					reader.ReadNext();
//
//					response.SizeToUpgradeInBytes = reader.ReadString();
//
//					reader.ReadNext();
//					reader.ReadNext();
//					response.SizeToUpgradeAncientInBytes = reader.ReadString();
//
//					while (reader.GetCurrentJsonToken() != JsonToken.EndObject)
//						reader.ReadNext();
//				}
//
//
//			}
//
//
//			if (reader.Read() as string == "size_in_bytes")
//			{
//				newResponse = true;
//				reader.Read();
//				response.SizeInBytes = (long)reader.Value;
//				reader.Read();
//				reader.Read();
//				response.SizeToUpgradeInBytes = reader.Value.ToString();
//				reader.Read();
//				reader.Read();
//				response.SizeToUpgradeAncientInBytes = reader.Value.ToString();
//				reader.Read();
//				if (reader.TokenType != JsonToken.EndObject)
//				{
//					reader.Read();
//					reader.Read();
//				}
//			}
//			var upgrades = new Dictionary<string, UpgradeStatus>();
//			while (true)
//			{
//				var status = new UpgradeStatus();
//				var index = reader.Value as string;
//				reader.Read();
//				reader.Read();
//				reader.Read();
//				status.SizeInBytes = (long)reader.Value;
//				reader.Read();
//				reader.Read();
//				status.SizeToUpgradeInBytes = reader.Value.ToString();
//				if (newResponse)
//				{
//					reader.Read();
//					reader.Read();
//					status.SizeToUpgradeAncientInBytes = reader.Value.ToString();
//				}
//				upgrades.Add(index, status);
//				reader.Read();
//				reader.Read();
//				if (reader.TokenType == JsonToken.EndObject)
//					break;
//			}
//			reader.Read();
//			response.Upgrades = upgrades;
//			return response;
//		}
//	}
}
