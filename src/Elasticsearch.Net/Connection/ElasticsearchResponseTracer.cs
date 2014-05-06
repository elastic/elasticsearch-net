using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net.Connection
{
	public class ElasticsearchResponseTracer<T> : IDisposable
	{
		private readonly bool _enabled;
		private Stopwatch _stopwatch;

		private ElasticsearchResponse<T> _result;

		public ElasticsearchResponseTracer(bool enabled)
		{
			this._enabled = enabled;
			if (enabled)
			{
				this._stopwatch = Stopwatch.StartNew();
			}
		}

		public void SetResult(ElasticsearchResponse<T> status)
		{
			if (!_enabled)
				return;
			this._result = status;
			this._stopwatch.Stop();
		}

		public void Dispose()
		{
			if (!_enabled || this._result == null)
				return;

			if (_result.Success)
			{
				Trace.TraceInformation(
					"NEST {0} {1} ({2}):\r\n{3}"
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
