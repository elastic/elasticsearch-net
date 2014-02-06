using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class GetAliasesResponseConverter : JsonConverter
	{
		public override bool CanWrite { get { return false; } }

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
										JsonSerializer serializer)
		{
			var dict = new Dictionary<string, IndexAliases>();
			serializer.Populate(reader, dict);

			return new GetAliasesResponse()
			{
				Indices = dict,
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}
	}
}