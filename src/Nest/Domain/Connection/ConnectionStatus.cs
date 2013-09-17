using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using Nest.Resolvers;
using Newtonsoft.Json;


namespace Nest
{
	public class ConnectionStatus
	{
		private readonly IConnectionSettings _settings;
		private string mockJsonResponse;
		public bool Success { get; private set; }
		public ConnectionError Error { get; private set; }
		public string RequestMethod { get; internal set; }
		public string RequestUrl { get; internal set; }
		public string Result { get; internal set; }
		public string Request { get; internal set; }


		//TODO probably nicer if we make this factory ConnectionStatus.Error() and ConnectionStatus.Valid()
		//and make these constructors private.

		private ConnectionStatus(IConnectionSettings settings)
		{
			this.TypeNameResolver = new TypeNameResolver();
			this.IdResolver = new IdResolver();
			this.IndexNameResolver = new IndexNameResolver(settings);
		}

		protected IndexNameResolver IndexNameResolver { get; private set; }
		protected IdResolver IdResolver { get; private set; }
		protected TypeNameResolver TypeNameResolver { get; private set; }

		public ConnectionStatus(IConnectionSettings settings, Exception e) : this(settings)
		{
			this._settings = settings;
			this.Success = false;
			this.Error = new ConnectionError(e);
			this.Result = this.Error.Response;
		}
		public ConnectionStatus(IConnectionSettings settings, string result) : this(settings)
		{
			this._settings = settings;
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
