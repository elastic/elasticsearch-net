using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class MatchQueryJsonConverter : FieldNameQueryJsonConverter<MatchQuery> 
	{
		protected override object DeserializeJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			reader.Read(); //property name
			var fieldName = reader.Value as string;
			reader.Read(); // {

			var jo = JObject.Load(reader);
			var typePropery = jo.Property("type");
			IMatchQuery query = null;
			var r = jo.CreateReader();
			switch (typePropery?.Value.Value<string>() ?? "")
			{
				case "phrase":
					query = FromJson.ReadAs<MatchPhraseQuery>(r, objectType, existingValue, serializer);
					break;
				case "phrase_prefix":
					query = FromJson.ReadAs<MatchPhrasePrefixQuery>(r, objectType, existingValue, serializer);
					break;
				default:
					query = FromJson.ReadAs<MatchQuery>(r, objectType, existingValue, serializer);
					break;
			}

			if (query == null) return null;
			query.Field = fieldName;

			return query;
		}
	}
}