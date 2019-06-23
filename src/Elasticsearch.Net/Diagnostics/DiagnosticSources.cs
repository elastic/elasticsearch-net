using System;
using System.Diagnostics;

namespace Elasticsearch.Net.Diagnostics
{
	
	public static class DiagnosticSources
	{
		public static string RequestPipeline { get; } = typeof(RequestPipeline).FullName;
		public static HttpConnectionDiagnosticKeys HttpConnection { get; } = new HttpConnectionDiagnosticKeys(); 

		private class EmptyDisposable : IDisposable
		{
			public void Dispose() { }
		}

		private static EmptyDisposable SingletonDisposable { get; } = new EmptyDisposable();

		internal static IDisposable Diagnose(this DiagnosticSource source, string operationName, IDateTimeProvider dateTimeProvider = null)
		{
			if (!source.IsEnabled(operationName)) return SingletonDisposable;
			return new Diagnose(operationName, source, dateTimeProvider = null);
		}
	}

	public interface IDiagnosticsKeys
	{
		string SourceName { get; }
	}
	
	public class HttpConnectionDiagnosticKeys : IDiagnosticsKeys
	{
		public string SourceName { get; } = typeof(HttpConnection).FullName;
		public string Send { get; } = nameof(Send);
		public string Receive { get; } = nameof(Receive);
	}


	internal class Diagnose : Activity, IDisposable
	{
		private readonly string _operationName;
		private readonly DiagnosticSource _source;
		private readonly IDateTimeProvider _dateTimeProvider;

		public Diagnose(string operationName, DiagnosticSource source, IDateTimeProvider dateTimeProvider) : base(operationName)
		{
			(_operationName, _source, _dateTimeProvider) = (operationName, source, dateTimeProvider ?? DateTimeProvider.Default);
			_source.StartActivity(SetStartTime(_dateTimeProvider.Now()), _operationName);
		}


		public void Dispose() => _source.StopActivity(SetEndTime(_dateTimeProvider.Now()), _operationName);
	}
}
