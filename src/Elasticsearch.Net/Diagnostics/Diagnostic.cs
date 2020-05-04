// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Diagnostics;

namespace Elasticsearch.Net.Diagnostics 
{
	/// <summary>
	/// Internal subclass of <see cref="Activity"/> that implements <see cref="IDisposable"/> to
	/// make it easier to use.
	/// </summary>
	internal class Diagnostic<TState> : Diagnostic<TState, TState>
	{
		public Diagnostic(string operationName, DiagnosticSource source, TState state)
			: base(operationName, source, state) =>
			EndState = state;
	}
	
	internal class Diagnostic<TState, TStateEnd> : Activity, IDisposable
	{
		public static Diagnostic<TState, TStateEnd> Default { get; } = new Diagnostic<TState, TStateEnd>();

		private readonly DiagnosticSource _source;
		private TStateEnd _endState;
		private readonly bool _default;

		private Diagnostic() : base("__NOOP__") => _default = true;

		public Diagnostic(string operationName, DiagnosticSource source, TState state) : base(operationName)
		{
			_source = source;
			_source.StartActivity(SetStartTime(DateTime.UtcNow), state);
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


		//_source can be null if Default instance
		public void Dispose() => _source?.StopActivity(SetEndTime(DateTime.UtcNow), EndState);
	}
}