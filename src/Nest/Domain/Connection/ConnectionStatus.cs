using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;



namespace Nest
{
	public class ConnectionStatus
	{
		public bool Success { get; private set; }
		public ConnectionError Error { get; private set; }
		public string RequestMethod { get; internal set; }
		public string RequestUrl { get; internal set; }
		public string Result { get; internal set; }
		public string Request { get; internal set; }

		public ConnectionStatus(Exception e)
		{
			this.Success = false;
			this.Error = new ConnectionError(e);
			this.Result = this.Error.Response;
		}

		public ConnectionStatus(ConnectionError error)
		{
			this.Success = false;
			if (error != null)
			{
				this.Error = error;
				this.Result = this.Error.Response;
			}
		}
		public ConnectionStatus(string result)
		{
			this.Success = true;
			this.Result = result;
		}
		public override string ToString()
		{
			var r = this;
			var e = r.Error;
			var printFormat = "StatusCode: {1}, {0}\tMethod: {2}, {0}\tUrl: {3}, {0}\tRequest: {4}, {0}\tResponse: {5}";
			var print = printFormat.F(
			  Environment.NewLine,
			  e != null ? e.HttpStatusCode : HttpStatusCode.OK,
			  r.RequestMethod,
			  r.RequestUrl,
			  r.Request,
			  r.Result
			);
			if (!this.Success)
			{
				var errorFormat = "{0}\tExceptionMessage: {1}{0}\t StackTrace: {2}";
				print += errorFormat.F(Environment.NewLine, e.ExceptionMessage, e.OriginalException.StackTrace);
			}
			return print;
		}

	}
}
