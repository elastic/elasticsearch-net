// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;

namespace Elasticsearch.Net.Diagnostics
{
	public static class DiagnosticSources
	{
		internal class EmptyDisposable : IDisposable
		{
			public void Dispose() { }
		}

		internal static EmptyDisposable SingletonDisposable { get; } = new EmptyDisposable();

		internal static IDisposable Diagnose<TState>(this DiagnosticSource source, string operationName, TState state)
		{
			if (!source.IsEnabled(operationName)) return SingletonDisposable;

			return new Diagnostic<TState>(operationName, source, state);
		}

		internal static Diagnostic<TState, TStateStop> Diagnose<TState, TStateStop>(this DiagnosticSource source, string operationName, TState state)
		{
			if (!source.IsEnabled(operationName)) return Diagnostic<TState, TStateStop>.Default;

			return new Diagnostic<TState, TStateStop>(operationName, source, state);
		}
		
		internal static Diagnostic<TState, TEndState> Diagnose<TState, TEndState>(this DiagnosticSource source, string operationName, TState state, TEndState endState)
		{
			if (!source.IsEnabled(operationName)) return Diagnostic<TState, TEndState>.Default;

			return new Diagnostic<TState, TEndState>(operationName, source, state)
			{
				EndState = endState
			};
			
		}

		/// <summary>
		/// When subscribing to <see cref="AuditDiagnosticKeys.SourceName"/> you will be notified of all decisions in the request pipeline
		/// </summary>
		public static AuditDiagnosticKeys AuditTrailEvents { get; } = new AuditDiagnosticKeys();

		/// <summary>
		/// When subscribing to <see cref="RequestPipelineDiagnosticKeys.SourceName"/> you will be notified every time a sniff/ping or an API call to Elasticsearch happens
		/// </summary>
		public static RequestPipelineDiagnosticKeys RequestPipeline { get; } = new RequestPipelineDiagnosticKeys();

		/// <summary>
		/// When subscribing to <see cref="HttpConnectionDiagnosticKeys.SourceName"/> you will be notified every time a a connection starts and stops a request and starts and stops a a response
		/// </summary>
		public static HttpConnectionDiagnosticKeys HttpConnection { get; } = new HttpConnectionDiagnosticKeys();

		/// <summary>
		/// When subscribing to <see cref="SerializerDiagnosticKeys.SourceName"/> you will be notified every time a particular serializer writes or reads
		/// </summary>
		public static SerializerDiagnosticKeys Serializer { get; } = new SerializerDiagnosticKeys();


		private interface IDiagnosticsKeys
		{
			string SourceName { get; }
		}

		public class HttpConnectionDiagnosticKeys : IDiagnosticsKeys
		{
			public string SourceName { get; } = typeof(HttpConnection).FullName;
			public string SendAndReceiveHeaders { get; } = nameof(SendAndReceiveHeaders);
			public string ReceiveBody { get; } = nameof(ReceiveBody);
		}

		public class SerializerDiagnosticKeys : IDiagnosticsKeys
		{
			public string SourceName { get; } = typeof(IElasticsearchSerializer).FullName;
			public string Serialize { get; } = nameof(Serialize);
			public string Deserialize { get; } = nameof(Deserialize);
		}

		public class RequestPipelineDiagnosticKeys : IDiagnosticsKeys
		{
			public string SourceName { get; } = typeof(RequestPipeline).FullName;
			public string CallElasticsearch { get; } = nameof(CallElasticsearch);
			public string Ping { get; } = nameof(Ping);
			public string Sniff { get; } = nameof(Sniff);
		}

		public class AuditDiagnosticKeys : IDiagnosticsKeys
		{
			public string SourceName { get; } = typeof(Audit).FullName;
		}
	}
}
