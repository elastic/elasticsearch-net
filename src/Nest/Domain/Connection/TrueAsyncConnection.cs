		using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Nest.Domain.Connection;

namespace Nest
{
	public class TrueAsyncConnection : IConnection
	{
		const int BUFFER_SIZE = 1024;

		private IConnectionSettings _ConnectionSettings { get; set; }
		private readonly bool _enableTrace;

		public TrueAsyncConnection(IConnectionSettings settings)
		{
			if (settings == null)
				throw new ArgumentNullException("settings");

			this._ConnectionSettings = settings;
			this._enableTrace = settings.TraceEnabled;
		}

		public ConnectionStatus GetSync(string path)
		{
			return this.HeaderOnlyRequest(path, "GET");
		}
		public ConnectionStatus HeadSync(string path)
		{
			return this.HeaderOnlyRequest(path, "HEAD");
		}

		public ConnectionStatus PostSync(string path, string data)
		{
			return this.BodyRequest(path, data, "POST");
		}
		public ConnectionStatus PutSync(string path, string data)
		{
			return this.BodyRequest(path, data, "PUT");
		}
		public ConnectionStatus DeleteSync(string path)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection);
		}
		public ConnectionStatus DeleteSync(string path, string data)
		{
			var connection = this.CreateConnection(path, "DELETE");
			return this.DoSynchronousRequest(connection, data);
		}

		public Task<ConnectionStatus> Get(string path)
		{
			var r = this.CreateConnection(path, "GET");
			return this.DoAsyncRequest(r);
		}
		public Task<ConnectionStatus> Head(string path)
		{
			var r = this.CreateConnection(path, "HEAD");
			return this.DoAsyncRequest(r);
		}
		public Task<ConnectionStatus> Post(string path, string data)
		{
			var r = this.CreateConnection(path, "POST");
			return this.DoAsyncRequest(r, data);

		}
		public Task<ConnectionStatus> Put(string path, string data)
		{
			var r = this.CreateConnection(path, "PUT");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ConnectionStatus> Delete(string path, string data)
		{
			var r = this.CreateConnection(path, "DELETE");
			return this.DoAsyncRequest(r, data);
		}
		public Task<ConnectionStatus> Delete(string path)
		{
			var r = this.CreateConnection(path, "DELETE");
			return this.DoAsyncRequest(r);
		}

		private static void ThreadTimeoutCallback(object state, bool timedOut)
		{
			if (timedOut)
			{
				HttpWebRequest request = state as HttpWebRequest;
				if (request != null)
				{
					request.Abort();
				}
			}
		}

		private ConnectionStatus HeaderOnlyRequest(string path, string method)
		{
			var connection = this.CreateConnection(path, method);
			return this.DoSynchronousRequest(connection);
		}

		private ConnectionStatus BodyRequest(string path, string data, string method)
		{
			var connection = this.CreateConnection(path, method);
			return this.DoSynchronousRequest(connection, data);
		}

		protected virtual HttpWebRequest CreateConnection(string path, string method)
		{

			var myReq = this.CreateWebRequest(path, method);
			this.SetBasicAuthorizationIfNeeded(myReq);
			this.SetProxyIfNeeded(myReq);
			return myReq;
		}

		private void SetProxyIfNeeded(HttpWebRequest myReq)
		{
			//myReq.Proxy = null;
			if (!string.IsNullOrEmpty(this._ConnectionSettings.ProxyAddress))
			{
				var proxy = new WebProxy();
				var uri = new Uri(this._ConnectionSettings.ProxyAddress);
				var credentials = new NetworkCredential(this._ConnectionSettings.ProxyUsername, this._ConnectionSettings.ProxyPassword);
				proxy.Address = uri;
				proxy.Credentials = credentials;
				myReq.Proxy = proxy;
			}
		}

		private void SetBasicAuthorizationIfNeeded(HttpWebRequest myReq)
		{
			var myUri = this._ConnectionSettings.Uri;
			if (myUri != null && !string.IsNullOrEmpty(myUri.UserInfo))
			{
				myReq.Headers["Authorization"] =
					"Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(myUri.UserInfo));
			}
		}

		protected virtual HttpWebRequest CreateWebRequest(string path, string method)
		{
			var url = this._CreateUriString(path);
			
			HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
			myReq.Accept = "application/json";
			myReq.ContentType = "application/json";

			var timeout = this._ConnectionSettings.Timeout;
			myReq.Timeout = timeout; // 1 minute timeout.
			myReq.ReadWriteTimeout = timeout; // 1 minute timeout.
			myReq.Method = method;
			return myReq;
		}

		protected virtual ConnectionStatus DoSynchronousRequest(HttpWebRequest request, string data = null)
		{
			using (var tracer = new ConnectionStatusTracer(this._ConnectionSettings.TraceEnabled))
			{
				ConnectionStatus cs = null;
				if (data != null)
				{
					using (var r = request.GetRequestStream())
					{
						byte[] buffer = Encoding.UTF8.GetBytes(data);
						r.Write(buffer, 0, buffer.Length);
					}
				}
				try
				{
					using (var response = (HttpWebResponse)request.GetResponse())
					using (var responseStream = response.GetResponseStream())
					using (var streamReader = new StreamReader(responseStream))
					{
						string result = streamReader.ReadToEnd();
						cs = new ConnectionStatus(result)
						{
							Request = data,
							RequestUrl = request.RequestUri.ToString(),
							RequestMethod = request.Method
						};
						tracer.SetResult(cs);
						return cs;
					}
				}
				catch (WebException webException)
				{
					cs = new ConnectionStatus(webException)
					{
						Request = data,
						RequestUrl = request.RequestUri.ToString(),
						RequestMethod = request.Method
					};
					tracer.SetResult(cs);

                    _ConnectionSettings.ConnectionStatusHandler(cs);

					return cs;
				}
			}	
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

		private Uri _CreateUriString(string path)
		{
            var s = this._ConnectionSettings;
			
				
			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(s.Uri, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			LeaveDotsAndSlashesEscaped(s.Uri);
			var uri = new Uri(s.Uri, path);
			LeaveDotsAndSlashesEscaped(uri);
			return uri;
			var url = s.Uri.AbsoluteUri + path;
			//WebRequest.Create will replace %2F with / 
			//this is a 'security feature'
			//see http://mikehadlow.blogspot.nl/2011/08/how-to-stop-systemuri-un-escaping.html
			//and http://msdn.microsoft.com/en-us/library/ee656542%28v=vs.100%29.aspx
			//NEST will by default double escape these so that if nest is the only way you talk to elasticsearch
			//it won't barf.
			//If you manually set the config settings to NOT forefully unescape dots and slashes be sure to call 
			//.SetDontDoubleEscapePathDotsAndSlashes() on the connection settings.
			//return );

			//return this._ConnectionSettings.DontDoubleEscapePathDotsAndSlashes ? url : url.Replace("%2F", "%252F");
		}

		// System.UriSyntaxFlags is internal, so let's duplicate the flag privately
		private const int UnEscapeDotsAndSlashes = 0x2000000;

		public static void LeaveDotsAndSlashesEscaped(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}

			FieldInfo fieldInfo = uri.GetType().GetField("m_Syntax", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Syntax' field not found");
			}
			object uriParser = fieldInfo.GetValue(uri);

			fieldInfo = typeof(UriParser).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
			if (fieldInfo == null)
			{
				throw new MissingFieldException("'m_Flags' field not found");
			}
			object uriSyntaxFlags = fieldInfo.GetValue(uriParser);

			// Clear the flag that we don't want
			uriSyntaxFlags = (int)uriSyntaxFlags & ~UnEscapeDotsAndSlashes;

			fieldInfo.SetValue(uriParser, uriSyntaxFlags);
		}
	
	}
}
