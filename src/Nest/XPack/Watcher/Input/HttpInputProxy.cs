using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(HttpInputProxy))]
	public interface IHttpInputProxy
	{
		string Host { get; set; }
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
