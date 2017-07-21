using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldRuleBaseJsonConverter: VerbatimDictionaryKeysJsonConverter<string, object>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return ReadFieldRule(reader, objectType, existingValue, serializer);
		}

		public static FieldRuleBase ReadFieldRule(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var depth = reader.Depth;
			if (reader.TokenType != JsonToken.StartObject) return reader.ReadToEnd<FieldRuleBase>(depth);
			reader.Read();
			if (reader.TokenType != JsonToken.PropertyName) return reader.ReadToEnd<FieldRuleBase>(depth);
			var propertyName = (string) reader.Value;
			switch (propertyName)
			{
				case "username":
					var username = reader.ReadAsString();
					return reader.ExhaustTo(depth, new UsernameRule(username));
				case "dn":
					var dn = reader.ReadAsString();
					return reader.ExhaustTo(depth, new DistinguishedNameRule(dn));
				case "realm.name":
					var realm = reader.ReadAsString();
					return reader.ExhaustTo(depth, new RealmRule(realm));
				case "groups":
					reader.Read(); // [
					var groups = serializer.Deserialize<List<string>>(reader);
					return reader.ExhaustTo(depth, new GroupsRule(groups));
				default:
					if (!propertyName.StartsWith("metadata."))
						return reader.ReadToEnd<FieldRuleBase>(depth, null);
					reader.Read(); //
					var key = propertyName.Replace("metadata.", "");
					var metadata = serializer.Deserialize<object>(reader);
					return reader.ExhaustTo(depth, new MetadataRule(key, metadata));
			}

		}
	}
}
