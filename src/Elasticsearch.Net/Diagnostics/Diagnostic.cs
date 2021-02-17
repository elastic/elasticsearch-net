// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;

namespace Elasticsearch.Net.Diagnostics
{
	/// <summary>
	/// Diagnostic that creates, starts and stops <see cref="Activity"/> that implements <see cref="IDisposable"/> to
	/// make it easier to use.
	/// </summary>
	internal class Diagnostic<TState> : Diagnostic<TState, TState>
	{
		public Diagnostic(string operationName, DiagnosticSource source, TState state)
			: base(operationName, source, state) =>
			EndState = state;
	}

	internal class Diagnostic<TState, TStateEnd> : IDisposable
	{
		public static Diagnostic<TState, TStateEnd> Default { get; } = new();

		private readonly DiagnosticSource _source;
		private readonly bool _default;
		private readonly Activity _activity;
		private TStateEnd _endState;

		private Diagnostic() => _default = true;

		public Diagnostic(string operationName, DiagnosticSource source, TState state)
		{
			_source = source;
			_activity = new Activity(operationName);
			_source.StartActivity(_activity, state);
		}

		public TStateEnd EndState
		{
			get => _endState;
			internal set
			{
				//do not store state on default instance
				if (_default) return;
				_endState =  value;
			}
		}

		public void Dispose()
		{
			_source?.StopActivity(_activity, EndState);
			_activity?.Dispose();
		}
	}
}
