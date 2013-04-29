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
		public InMemoryConnection(IConnectionSettings settings) : base(settings)
		{

		}

		protected override ConnectionStatus DoSynchronousRequest(HttpWebRequest request, string data = null)
		{
			var status = new ConnectionStatus("{ \"status\" : \"USING NEST IN MEMORY CONNECTION\" }")
			{
				Request = data,
				RequestUrl = request.RequestUri.ToString(),
				RequestMethod = request.Method
			};
			return status;
		}

		protected override Task<ConnectionStatus> DoAsyncRequest(HttpWebRequest request, string data = null)
		{
			return Task.Factory.StartNew<ConnectionStatus>(() =>
			{
				var status = new ConnectionStatus("{ \"status\" : \"USING NEST IN MEMORY CONNECTION\" }")
				{
					Request = data,
					RequestUrl = request.RequestUri.ToString(),
					RequestMethod = request.Method
				};
				return status;
			});
		}
	
	}
}
