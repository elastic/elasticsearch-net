using System;
using System.Text;
using System.Net;
using System.Threading;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// 
	/// </summary>
	public class InMemoryConnection : Connection
	{
		private ConnectionStatus _fixedResult;

		public InMemoryConnection(IConnectionSettings settings)
			: base(settings)
		{

		}
		public InMemoryConnection(IConnectionSettings settings, ConnectionStatus fixedResult)
			: base(settings)
		{
			this._fixedResult = fixedResult;
			
		}


		protected override ConnectionStatus DoSynchronousRequest(HttpWebRequest request, string data = null)
		{
			return this.ReturnConnectionStatus(request, data);
		}

		private ConnectionStatus ReturnConnectionStatus(HttpWebRequest request, string data)
		{
			return this._fixedResult ?? new ConnectionStatus(this._ConnectionSettings, "{ \"status\" : \"USING NEST IN MEMORY CONNECTION\" }")
			{
				Request = data,
				RequestUrl = request.RequestUri.ToString(),
				RequestMethod = request.Method
			};
		}

		protected override Task<ConnectionStatus> DoAsyncRequest(HttpWebRequest request, string data = null)
		{
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				return this.ReturnConnectionStatus(request, data);
			});
		}

	}
}
