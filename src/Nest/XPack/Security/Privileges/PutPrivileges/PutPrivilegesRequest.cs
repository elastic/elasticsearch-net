/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);
	}

	public partial class PutPrivilegesDescriptor
	{
		IAppPrivileges IPutPrivilegesRequest.Applications { get; set; }

		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
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
