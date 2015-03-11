using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Elasticsearch.Net.Connection.Configuration;
using Elasticsearch.Net.ConnectionPool;
using PurifyNet;

namespace Elasticsearch.Net.Connection.RequestState
{
	internal static class TransportRequestState
	{
		private static long _requestId = 0;
		public static long RequestId {
			get
			{
				var r = Interlocked.Increment(ref _requestId);
				return r;
			}
		} 
	}

	public class TransportRequestState<T> : IDisposable, ITransportRequestState
	{
		private readonly bool _traceEnabled;
		private readonly bool _metricsEnabled;
		private readonly long _requestId;

		private Stopwatch _stopwatch;

		private ElasticsearchResponse<T> _result;
		private Uri _currentNode;

		public IConnectionConfigurationValues ClientSettings { get; private set; }

		public IRequestParameters RequestParameters { get; private set; }

		public IRequestConfiguration RequestConfiguration
		{
			get
			{
				return this.RequestParameters == null ? null : this.RequestParameters.RequestConfiguration;
			}
		}

		public bool UsingPooling
		{
			get
			{
				var pool = this.ClientSettings.ConnectionPool;
				return pool != null && pool.GetType() != typeof(SingleNodeConnectionPool);
			}
		}

		public string Method { get; private set; }

		public string Path { get; private set; }

		public byte[] PostData { get; private set; }

		public int? Seed { get; set; }

		public Func<IElasticsearchResponse, Stream, object> ResponseCreationOverride { get; set; }

		public bool SniffedOnConnectionFailure { get; set; }

		public int Retried { get { return Math.Max(0, this.SeenNodes.Count() - 1); } }
		public int Pings { get; set; }
		public int Sniffs { get; set; }

		public List<Uri> SeenNodes { get; private set; }
		public List<Exception> SeenExceptions { get; private set; }
		public List<RequestMetrics> RequestMetrics { get; set; }

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

		public DateTime StartedOn { get; private set; }
		public long SerializationTime { get; private set; }
		public long RequestEndTime { get; private set; }
		public long DeserializationTime { get; private set; }

		public TransportRequestState(
			IConnectionConfigurationValues settings,
			IRequestParameters requestParameters,
			string method,
			string path)
		{
			this._requestId = TransportRequestState.RequestId;
			this.StartedOn = DateTime.UtcNow;
			this.SeenNodes = new List<Uri>();
			this.SeenExceptions = new List<Exception>();
			this.ClientSettings = settings;
			this.RequestParameters = requestParameters;
			this._traceEnabled = settings.TraceEnabled;
			this._metricsEnabled = settings.MetricsEnabled;
			if (this._metricsEnabled || this._traceEnabled)
				this._stopwatch = Stopwatch.StartNew();

			if (this._traceEnabled)
			{
				Trace.TraceInformation("NEST start:{0} {1} {2}"
					, this._requestId
					, _result.RequestMethod
					, _result.RequestUrl
				);
			}

			this.Method = method;
			this.Path = path;
			if (this.RequestParameters != null)
			{
				if (this.RequestParameters.QueryString != null)
					this.Path += RequestParameters.QueryString.ToNameValueCollection(ClientSettings.Serializer).ToQueryString();
				this.ResponseCreationOverride = this.RequestParameters.DeserializationState;
			}
		}

		public void TickSerialization(byte[] postData)
		{
			this.PostData = postData;
			if (this._metricsEnabled)
				this.SerializationTime = this._stopwatch.ElapsedMilliseconds;
		}


		public IRequestTimings InitiateRequest(RequestType requestType)
		{
			if (!this.ClientSettings.MetricsEnabled)
				return NoopRequestTimings.Instance;

			if (this.RequestMetrics == null) this.RequestMetrics = new List<RequestMetrics>();
			return new RequestTimings(requestType, this.CurrentNode, this.Path, this.RequestMetrics);
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
			if (result == null)
			{
				if (this._stopwatch != null) this._stopwatch.Stop();
				return;
			}
			result.NumberOfRetries = this.Retried;
			if (this.ClientSettings.MetricsEnabled)
				result.Metrics = new CallMetrics
				{
					Path = this.Path,
					SerializationTime = this.SerializationTime,
					DeserializationTime = this.DeserializationTime,
					StartedOn = this.StartedOn,
					CompletedOn = DateTime.UtcNow,
					Requests = this.RequestMetrics
				};
			
			//TODO this forced set is done in several places, shouldn't this be enough?
			var objectNeedsResponseRef = result.Response as IResponseWithRequestInformation;
			if (objectNeedsResponseRef != null) objectNeedsResponseRef.RequestInformation = result;

			if (this.ClientSettings.ConnectionStatusHandler != null)
				this.ClientSettings.ConnectionStatusHandler(result);
			
			if (this._stopwatch != null) this._stopwatch.Stop();

			if (!_traceEnabled) return;

			this._result = result;

		}

		public void Dispose()
		{
			if (!_traceEnabled || this._result == null)
				return;

			if (_result.Success)
			{
				Trace.TraceInformation("NEST end:{0} {1} {2} ({3}):\r\n{4}"
					, this._requestId
					, _result.RequestMethod
					, _result.RequestUrl
					, _stopwatch.Elapsed
					, _result
				);
			}
			else
			{
				Trace.TraceError(
					"NEST end:{0} {1} {2} ({3}):\r\n{4}"
					, this._requestId
					, _result.RequestMethod
					, _result.RequestUrl
					, _stopwatch.Elapsed
					, _result.ToString()
				);
			}
		}

	}
}
