using Exortech.NetReflector;

namespace Elasticsearch.Net.Connection.Thrift.Transport
{
	[ReflectorType("SocketSettings")]
	public class TSocketSettings
	{
		protected bool Equals(TSocketSettings other)
		{
			return SendBufferSize == other.SendBufferSize 
				&& ReceiveBufferSize == other.ReceiveBufferSize 
				&& SendTimeout == other.SendTimeout 
				&& ReceiveTimeout == other.ReceiveTimeout 
				&& ConnectTimeout == other.ConnectTimeout;
		}

		public override int GetHashCode()
		{
				int hashCode = SendBufferSize;
				hashCode = (hashCode*397) ^ ReceiveBufferSize;
				hashCode = (hashCode*397) ^ SendTimeout;
				hashCode = (hashCode*397) ^ ReceiveTimeout;
				hashCode = (hashCode*397) ^ ConnectTimeout;
				return hashCode;
		}

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
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((TSocketSettings) obj);
		}
	}
}