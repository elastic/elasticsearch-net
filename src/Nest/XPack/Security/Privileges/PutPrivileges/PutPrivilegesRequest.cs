using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.security.put_privileges.json")]
	[JsonConverter(typeof(PutPrivilegesConverter))]
	public partial interface IPutPrivilegesRequest : IProxyRequest
	{
		IAppPrivileges Applications { get; set; }
	}

	public partial class PutPrivilegesRequest
	{
		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);

		public IAppPrivileges Applications { get; set; }
	}

	public partial class PutPrivilegesDescriptor
	{
		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);

		IAppPrivileges IPutPrivilegesRequest.Applications { get; set; }

		public PutPrivilegesDescriptor Applications(Func<AppPrivilegesDescriptor, IPromise<IAppPrivileges>> selector) =>
			Assign(a => a.Applications = selector?.Invoke(new AppPrivilegesDescriptor())?.Value);
	}

	internal class PutPrivilegesConverter : VerbatimDictionaryKeysJsonConverter<string, IPrivileges>
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (!(value is IPutPrivilegesRequest request)) return;
			base.WriteJson(writer, request.Applications, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var appPrivileges = serializer.Deserialize<AppPrivileges>(reader);
			return new PutPrivilegesRequest { Applications = appPrivileges };
		}
	}
}
