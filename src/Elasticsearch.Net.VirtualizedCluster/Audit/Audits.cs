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
using System.Collections.Generic;

namespace Elasticsearch.Net.VirtualizedCluster.Audit
{
	public class CallTraceState
	{
		public CallTraceState(AuditEvent e) => Event = e;

		public Action<string, Elasticsearch.Net.Audit> AssertWithBecause { get; set; }

		public AuditEvent Event { get; private set; }

		public int? Port { get; set; }

		public Action<Elasticsearch.Net.Audit> SimpleAssert { get; set; }
	}

	public class ClientCall : List<CallTraceState>
	{
		public ClientCall() { }

		public ClientCall(Func<RequestConfigurationDescriptor, IRequestConfiguration> requestOverrides) => RequestOverrides = requestOverrides;

		public Action<IConnectionPool> AssertPoolAfterCall { get; private set; }
		public Action<IElasticsearchResponse> AssertResponse { get; private set; }
		public Func<RequestConfigurationDescriptor, IRequestConfiguration> RequestOverrides { get; }

		public void Add(AuditEvent key, Action<Elasticsearch.Net.Audit> value) => Add(new CallTraceState(key) { SimpleAssert = value });

		public void Add(AuditEvent key, int port) => Add(new CallTraceState(key) { Port = port });

		public void Add(AuditEvent key) => Add(new CallTraceState(key));

		public void Add(Action<IConnectionPool> pool) => AssertPoolAfterCall = pool;

		public void Add(AuditEvent key, int port, Action<IElasticsearchResponse> assertResponse)
		{
			Add(new CallTraceState(key) { Port = port });
			AssertResponse = assertResponse;
		}
	}
}
