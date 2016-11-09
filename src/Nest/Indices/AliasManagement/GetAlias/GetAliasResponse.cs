using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(GetAliasResponseConverter))]
	public interface IGetAliasResponse : IResponse
	{
		IReadOnlyDictionary<string, IReadOnlyList<AliasDefinition>> Indices { get; }
	}

	public class GetAliasResponse : ResponseBase, IGetAliasResponse
	{
		public IReadOnlyDictionary<string, IReadOnlyList<AliasDefinition>> Indices { get; internal set; } = EmptyReadOnly<string, IReadOnlyList<AliasDefinition>>.Dictionary;
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
			var indices = new Dictionary<string, IReadOnlyList<AliasDefinition>>();

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

			return new GetAliasResponse { Indices = indices };
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
