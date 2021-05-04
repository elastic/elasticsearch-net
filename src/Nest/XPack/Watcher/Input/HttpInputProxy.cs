// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
