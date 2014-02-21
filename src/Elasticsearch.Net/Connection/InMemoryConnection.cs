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
		private ElasticsearchResponse _fixedResult;

		public InMemoryConnection(IConnectionSettings2 settings)
			: base(settings)
		{

		}
		public InMemoryConnection(IConnectionSettings2 settings, ElasticsearchResponse fixedResult)
			: base(settings)
		{
			this._fixedResult = fixedResult;
		}

		protected override ElasticsearchResponse DoSynchronousRequest(HttpWebRequest request, byte[] data = null)
		{
			return this.ReturnConnectionStatus(request, data);
		}

		private ElasticsearchResponse ReturnConnectionStatus(HttpWebRequest request, byte[] data)
		{
			var cs = this._fixedResult ?? new ElasticsearchResponse(this._ConnectionSettings, "{ \"USING NEST IN MEMORY CONNECTION\"  : null }")
			{
				Request = data.Utf8String(),
				RequestUrl = request.RequestUri.ToString(),
				RequestMethod = request.Method
			};
			_ConnectionSettings.ConnectionStatusHandler(cs);
			return cs;
		}

		protected override Task<ElasticsearchResponse> DoAsyncRequest(HttpWebRequest request, byte[] data = null)
		{
			return Task.Factory.StartNew<ElasticsearchResponse>(() =>
			{
				var cs = this.ReturnConnectionStatus(request, data);
				_ConnectionSettings.ConnectionStatusHandler(cs);
				return cs;
			});
		}

	}
}
