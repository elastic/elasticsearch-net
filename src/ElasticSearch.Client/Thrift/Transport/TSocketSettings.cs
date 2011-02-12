using Exortech.NetReflector;

namespace Thrift.Transport
{
	[ReflectorType("SocketSettings")]
	public class TSocketSettings
	{
		public static readonly TSocketSettings DefaultSettings = new TSocketSettings
		{
			SendBufferSize = 8192,
			ReceiveBufferSize = 8192,
			SendTimeout = 10000,
			ReceiveTimeout = 10000,
			ConnectTimeout = 3000
		};

		[ReflectorProperty("SendBufferSize", InstanceType = typeof(int), Required = false)]
		public int SendBufferSize { get; set; }

		[ReflectorProperty("ReceiveBufferSize", InstanceType = typeof(int), Required = false)]
		public int ReceiveBufferSize { get; set; }

		[ReflectorProperty("SendTimeout", InstanceType = typeof(int), Required = false)]
		public int SendTimeout { get; set; }

		[ReflectorProperty("ReceiveTimeout", InstanceType = typeof(int), Required = false)]
		public int ReceiveTimeout { get; set; }

		[ReflectorProperty("ConnectTimeout", InstanceType = typeof(int), Required = false)]
		public int ConnectTimeout { get; set; }

		public override bool Equals(object obj)
		{
			var other = obj as TSocketSettings;
			return (other != null
					&& other.SendBufferSize == SendBufferSize
					&& other.ReceiveBufferSize == ReceiveBufferSize
					&& other.SendTimeout == SendTimeout
					&& other.ReceiveTimeout == ReceiveTimeout
					&& other.ConnectTimeout == ConnectTimeout);
		}
	}
}