using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IGetAliasResponse : IResponse
	{
		IReadOnlyDictionary<IndexName, IndexAliases> Indices { get; }
	}

	public class IndexAliases
	{
		[JsonProperty("aliases")]
		public IReadOnlyDictionary<string, AliasDefinition> Aliases { get; internal set; } = EmptyReadOnly<string, AliasDefinition>.Dictionary;
	}

	[JsonConverter(typeof(GetAliasResponseConverter))]
	public class GetAliasResponse : DictionaryResponseBase<IndexName, IndexAliases>, IGetAliasResponse
	{
		[JsonIgnore]
		public IReadOnlyDictionary<IndexName, IndexAliases> Indices => Self.BackingDictionary;

		public override bool IsValid => this.Indices.Count > 0;
	}

	internal class GetAliasResponseConverter : ResolvableDictionaryResponseJsonConverter<GetAliasResponse, IndexName, IndexAliases>
	{
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var j = JObject.Load(reader);
			var errorProperty =j.Property("error");
			string error = null;
			if (errorProperty?.Value?.Type == JTokenType.String)
			{
				error = errorProperty.Value.Value<string>();
				errorProperty.Remove();
			}
			var statusProperty =j.Property("status");
			int? statusCode = null;
			if (statusProperty?.Value?.Type == JTokenType.Integer)
			{
				statusCode = statusProperty.Value.Value<int>();
				statusProperty.Remove();
			}

			var response = (GetAliasResponse)base.ReadJson(j.CreateReader(), objectType, existingValue, serializer);
			response.Error = error.IsNullOrEmpty() ? null : new Error { Reason = error };
			response.StatusCode = statusCode;
			return response;
		}
	}

}
