		using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nest.Domain.Connection;

namespace Nest
{
	public class NoTaskHttpConnection : Connection
	{
		public NoTaskHttpConnection(IConnectionSettings settings) : base(settings)
		{
		}

		protected virtual Task<ConnectionStatus> DoAsyncRequest(HttpWebRequest request, string data = null)
		{
			var operation = new AsyncRequestOperation( 
				request, 
				data, 
				_ConnectionSettings, 
				new ConnectionStatusTracer( this._ConnectionSettings.TraceEnabled ) );
			return operation.Task;
		}

	}
}
