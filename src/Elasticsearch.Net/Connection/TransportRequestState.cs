using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection.Configuration;
using PurifyNet;

namespace Elasticsearch.Net.Connection
{
	public class TransportRequestState<T> : IDisposable
	{
		private readonly bool _traceEnabled;
		private Stopwatch _stopwatch;

		private ElasticsearchResponse<T> _result;
		private Uri _currentNode;

		public IConnectionConfigurationValues ClientSettings { get; private set; }
		
		public IRequestParameters RequestParameters { get; private set;}

		public IRequestConfiguration RequestConfiguration
		{
			get
			{
				return this.RequestParameters == null ? null : this.RequestParameters.RequestConfiguration;
			}
		}

		public string Method { get; private set; }
		
		public string Path { get; private set; }

		public byte[] PostData { get; private set; }
		
		public int? Seed { get; set; }

		public Func<IElasticsearchResponse, Stream, object> DeserializationState { get; private set; }

		internal bool SniffedOnConnectionFailure { get; set; }

		public int Retried { get { return Math.Max(0, this.SeenNodes.Count() - 1); } }
		public int Pings { get; set; }
		public int Sniffs { get; set; }

		public List<Uri> SeenNodes { get; private set; }
		public List<Uri> PingNodes { get; private set; }
		public List<Uri> SniffNodes { get; private set; }

		public Uri CurrentNode
		{
			get 
			{ 
				return this.RequestConfiguration != null && this.RequestConfiguration.ForceNode != null 
					? this.RequestConfiguration.ForceNode 
					: _currentNode; 
			}
			set
			{
				_currentNode = value;
				this.SeenNodes.Add(value);
			}
		}

		public long SerializationTime { get; private set; }
		public long RequestEndTime { get; private set; }
		public long DeserializationTime { get; private set; }


		public TransportRequestState(
			IConnectionConfigurationValues settings,
			IRequestParameters requestParameters, 
			string method, 
			string path)
		{
			this.SeenNodes = new List<Uri>();
			this.ClientSettings = settings;
			this.RequestParameters = requestParameters;
			this._traceEnabled = settings.TraceEnabled;
			if (this._traceEnabled)
				this._stopwatch = Stopwatch.StartNew();this.Method = method;

			this.Method = method;
			this.Path = path;
			if (this.RequestParameters != null)
			{
				if (this.RequestParameters.QueryString != null) 
					this.Path += RequestParameters.QueryString.ToNameValueCollection(ClientSettings.Serializer).ToQueryString();
				this.DeserializationState = this.RequestParameters.DeserializationState;
			}
		}
		
		public void TickSerialization(byte[] postData)
		{
			this.PostData = postData;
			if (this._traceEnabled)
				this.SerializationTime = this._stopwatch.ElapsedMilliseconds;
		}

		public void RegisterSniff()
		{
			if (this.SniffNodes == null) this.SniffNodes = new List<Uri>();
			this.SniffNodes.Add(this.CurrentNode);
		}

		public void RegisterPing()
		{
			if (this.PingNodes == null) this.PingNodes = new List<Uri>();
			this.PingNodes.Add(this.CurrentNode);
		}
	
		public Uri CreatePathOnCurrentNode(string path = null)
		{
			var s = this.ClientSettings;
			path = path ?? this.Path;
			if (s.QueryStringParameters != null)
			{
				var tempUri = new Uri(this.CurrentNode, path);
				var qs = s.QueryStringParameters.ToQueryString(tempUri.Query.IsNullOrEmpty() ? "?" : "&");
				path += qs;
			}
			var uri = path.IsNullOrEmpty() ? this.CurrentNode : new Uri(this.CurrentNode, path);
			return uri.Purify();
		}

		public void SetResult(ElasticsearchResponse<T> result)
		{
			result.NumberOfRetries = this.Retried;


			var objectNeedsResponseRef = result.Response as IResponseWithRequestInformation;
			if (objectNeedsResponseRef != null) objectNeedsResponseRef.RequestInformation = result;
			
			if (this.ClientSettings.ConnectionStatusHandler != null)
				this.ClientSettings.ConnectionStatusHandler(result);
		
			if (!_traceEnabled) return;

			this._result = result;
			this._stopwatch.Stop();

		}

		public void Dispose()
		{
			if (!_traceEnabled || this._result == null)
				return;

			if (_result.Success)
			{
				Trace.TraceInformation("NEST {0} {1} ({2}):\r\n{3}"
					, _result.RequestMethod
					, _result.RequestUrl
					, _stopwatch.Elapsed
					, _result
				);
			}
			else
			{
				Trace.TraceError(
					"NEST {0} {1} ({2}):\r\n{3}"
					, _result.RequestMethod
					, _result.RequestUrl
					, _stopwatch.Elapsed.ToString()
					, _result.ToString()
				);
			}
		}

	}
}
