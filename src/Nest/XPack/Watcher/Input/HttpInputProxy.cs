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

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(HttpInputProxy))]
	public interface IHttpInputProxy
	{
		[DataMember(Name = "host")]
		string Host { get; set; }

		[DataMember(Name = "port")]
		int? Port { get; set; }
	}

	public class HttpInputProxy : IHttpInputProxy
	{
		public string Host { get; set; }

		public int? Port { get; set; }
	}

	public class HttpInputProxyDescriptor
		: DescriptorBase<HttpInputProxyDescriptor, IHttpInputProxy>, IHttpInputProxy
	{
		string IHttpInputProxy.Host { get; set; }
		int? IHttpInputProxy.Port { get; set; }

		public HttpInputProxyDescriptor Host(string host) => Assign(host, (a, v) => a.Host = v);

		public HttpInputProxyDescriptor Port(int? port) => Assign(port, (a, v) => a.Port = v);
	}
}
