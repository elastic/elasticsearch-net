using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(GetAliasResponseConverter))]
	public interface IGetAliasesResponse : IResponse
	{
		IDictionary<string, IList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasesResponse : ResponseBase, IGetAliasesResponse
	{
		public IDictionary<string, IList<AliasDefinition>> Indices { get; internal set; }
	}

	internal class GetAliasResponseConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var dict = serializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, AliasDefinition>>>>(reader);
			var indices = new Dictionary<string, IList<AliasDefinition>>();

			foreach (var kv in dict)
			{
				var indexDict = kv.Key;
				var aliases = new List<AliasDefinition>();
				if (kv.Value != null && kv.Value.ContainsKey("aliases"))
				{
					var aliasDict = kv.Value["aliases"];
					if (aliasDict != null)
						aliases = aliasDict.Select(kva =>
						{
							var alias = kva.Value;
							alias.Name = kva.Key;
							return alias;
						}).ToList();
				}

				indices.Add(indexDict, aliases);
			}

			return new GetAliasesResponse { Indices = indices };
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
