using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net.Connection.Configuration;

namespace Elasticsearch.Net.Connection
{
	public class TransportRequestState<T> : IDisposable
	{
		private readonly bool _traceEnabled;
		private Stopwatch _stopwatch;

		private ElasticsearchResponse<T> _result;
		
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


		public long SerializationTime { get; private set; }
		public long RequestEndTime { get; private set; }
		public long DeserializationTime { get; private set; }


		public TransportRequestState(
			IConnectionConfigurationValues settings,
			IRequestParameters requestParameters, 
			string method, 
			string path)
		{
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
			this.SerializationTime = this._stopwatch.ElapsedMilliseconds;
		}


		public void SetResult(ElasticsearchResponse<T> result)
		{
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
