using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Elasticsearch.Net.Connection.RequestState
{
	public interface IRequestTimings : IDisposable
	{
		void Finish(bool success, int? httpStatusCode);
	}

	internal class NoopRequestTimings : IRequestTimings
	{
		public static NoopRequestTimings Instance = new NoopRequestTimings();

		public void Finish(bool success, int? httpStatusCode)
		{
		}

		public void Dispose()
		{
		}
	}

	internal class RequestTimings : IRequestTimings
	{
		private readonly Uri _node;
		private readonly string _path;
		private readonly List<RequestMetrics> _requestMetrics;
		private readonly Stopwatch _stopwatch;
		private readonly DateTime _startedOn;
		private readonly RequestType _type;
		private bool _success;
		private int? _httpStatusCode;

		public RequestTimings(RequestType type, Uri node, string path, List<RequestMetrics> requestMetrics)
		{
			_startedOn = DateTime.UtcNow;
			_node = node;
			_path = path;
			_requestMetrics = requestMetrics;
			_type = type;
			_stopwatch = Stopwatch.StartNew();
		}

		public void Finish(bool success, int? httpStatusCode)
		{
			this._stopwatch.Stop();
			this._success = success;
			this._httpStatusCode = httpStatusCode;
		}

		public void Dispose()
		{
			this._stopwatch.Stop();
			this._requestMetrics.Add(new RequestMetrics
			{
				StartedOn = this._startedOn,
				Node = this._node,
				EllapsedMilliseconds = this._stopwatch.ElapsedMilliseconds,
				Path = this._path,
				RequestType = this._type,
				Success = this._success,
				HttpStatusCode = this._httpStatusCode
			});
		}
	}

}