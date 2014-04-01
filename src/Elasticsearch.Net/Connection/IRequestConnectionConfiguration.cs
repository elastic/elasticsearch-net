using System.ComponentModel;

namespace Elasticsearch.Net.Connection
{
	public interface IRequestConnectionConfiguration
	{
		int? TimeoutRequest { get; }
		int? TimeoutConnect { get; }
	}
	

	public class RequestConnectionConfiguration : RequestConnectionConfiguration<RequestConnectionConfiguration>, IRequestConnectionConfiguration
	{

	}
	
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RequestConnectionConfiguration<T> : IRequestConnectionConfiguration
		where T : RequestConnectionConfiguration<T>, IRequestConnectionConfiguration
	{
		private int? _timeoutRequest;
		int? IRequestConnectionConfiguration.TimeoutRequest
		{
			get { return _timeoutRequest; }
		}

		private int? _timeoutConnection;
		int? IRequestConnectionConfiguration.TimeoutConnect
		{
			get { return _timeoutConnection; }
		}

		public T RequestTimeout(int request)
		{
			this._timeoutRequest = request;
			return (T)this;
		}
		public T ConnectTimeout(int request)
		{
			this._timeoutConnection = request;
			return (T)this;
		}

	}
}