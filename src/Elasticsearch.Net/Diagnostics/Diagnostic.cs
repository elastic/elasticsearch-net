/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
