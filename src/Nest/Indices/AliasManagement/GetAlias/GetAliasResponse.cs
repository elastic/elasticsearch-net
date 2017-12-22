using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(GetAliasResponseConverter))]
	public interface IGetAliasResponse : IResponse
	{
		IReadOnlyDictionary<string, IReadOnlyList<AliasDefinition>> Indices { get; }
		/// <summary>
		/// An additional error message if an error occurs.
		/// </summary>
		/// <remarks>Applies to Elasticsearch 5.5.0+</remarks>
		string Error { get; }
		int? StatusCode { get; }
	}

	public class GetAliasResponse : ResponseBase, IGetAliasResponse
	{
		public IReadOnlyDictionary<string, IReadOnlyList<AliasDefinition>> Indices { get; internal set; } = EmptyReadOnly<string, IReadOnlyList<AliasDefinition>>.Dictionary;

		public override bool IsValid => this.Indices.Count > 0;
		public string Error { get; internal set; }
		public int? StatusCode { get; internal set; }
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
			var j = DictionaryResponseJsonConverterHelpers.ReadServerErrorFirst(reader, out var error, out var statusCode);

			//Read the remaining properties as aliases
			var dict = serializer.Deserialize<Dictionary<string, Dictionary<string, Dictionary<string, AliasDefinition>>>>(j.CreateReader());
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

			return new GetAliasResponse { Indices = indices, Error = error?.Reason, StatusCode = statusCode};
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
