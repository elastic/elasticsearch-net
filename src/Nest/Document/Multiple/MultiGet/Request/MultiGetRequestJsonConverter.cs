using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class MultiGetRequestJsonConverter : JsonConverter
	{
		public override bool CanRead => false;
		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IMultiGetRequest)value;
			writer.WriteStartObject();
			if (!(request?.Documents.HasAny()).GetValueOrDefault(false))
			{
				writer.WriteEndObject();
				return;
			}

			List<IMultiGetOperation> docs;
			var requestHasIndex = request.Index != null;
			var requestHasType = request.Type != null;

			if (requestHasIndex || requestHasType)
			{
				var settings = serializer.GetConnectionSettings();
				var resolvedIndex = requestHasIndex
					? request.Index.GetString(settings)
					: null;
				var resolvedType = requestHasType
					? ((IUrlParameter)request.Type).GetString(settings)
					: null;

				docs = request.Documents.Select(d =>
					{
						if (requestHasIndex && d.Index != null)
						{
							var docIndex = d.Index.GetString(settings);
							if (string.Equals(resolvedIndex, docIndex))
								d.Index = null;
						}

						if (requestHasType && d.Type != null)
						{
							var docType = ((IUrlParameter)d.Type).GetString(settings);
							if (string.Equals(resolvedType, docType))
								d.Type = null;
						}

						return d;
					})
					.ToList();
			}
			else
				docs = request.Documents.ToList();

			var flatten = docs.All(p => p.CanBeFlattened);

			writer.WritePropertyName(flatten ? "ids" : "docs");
			writer.WriteStartArray();
			foreach (var id in docs)
			{
				if (flatten)
					serializer.Serialize(writer, id.Id);
				else
					serializer.Serialize(writer, id);
			}
			writer.WriteEndArray();
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();
	}
}
