using System.Runtime.Serialization;
using Utf8Json;

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

		public HttpInputProxyDescriptor Host(string host) => Assign(a => a.Host = host);

		public HttpInputProxyDescriptor Port(int? port) => Assign(a => a.Port = port);
	}
}
