// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.IO;
using Elastic.Transport;
using Nest.Utf8Json;

namespace Nest
{
	[MapsApi("security.put_privileges")]
	[JsonFormatter(typeof(PutPrivilegesFormatter))]
	public partial interface IPutPrivilegesRequest : IProxyRequest
	{
		IAppPrivileges Applications { get; set; }
	}

	public partial class PutPrivilegesRequest
	{
		public IAppPrivileges Applications { get; set; }

		void IProxyRequest.WriteJson(ITransportSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);
	}

	public partial class PutPrivilegesDescriptor
	{
		IAppPrivileges IPutPrivilegesRequest.Applications { get; set; }

		void IProxyRequest.WriteJson(ITransportSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);

		public PutPrivilegesDescriptor Applications(Func<AppPrivilegesDescriptor, IPromise<IAppPrivileges>> selector) =>
			Assign(selector, (a, v) => a.Applications = v?.Invoke(new AppPrivilegesDescriptor())?.Value);
	}

	internal class PutPrivilegesFormatter : IJsonFormatter<IPutPrivilegesRequest>
	{
		public IPutPrivilegesRequest Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var appPrivileges = formatterResolver.GetFormatter<AppPrivileges>().Deserialize(ref reader, formatterResolver);
			return new PutPrivilegesRequest { Applications = appPrivileges };
		}

		public void Serialize(ref JsonWriter writer, IPutPrivilegesRequest value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var formatter = formatterResolver.GetFormatter<IDictionary<string, IPrivileges>>();
			formatter.Serialize(ref writer, value.Applications, formatterResolver);
		}
	}
}
