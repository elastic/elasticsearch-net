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
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class WaitingInMemoryConnection : InMemoryConnection
	{
		private readonly TimeSpan _waitTime;

		public WaitingInMemoryConnection(TimeSpan waitTime, byte[] responseBody, int statusCode = 200, Exception exception = null)
			: base(responseBody, statusCode, exception) => _waitTime = waitTime;

		public override TResponse Request<TResponse>(RequestData requestData)
		{
			Thread.Sleep(_waitTime);
			return base.Request<TResponse>(requestData);
		}

		public override async Task<TResponse> RequestAsync<TResponse>(RequestData requestData, CancellationToken cancellationToken)
		{
			await Task.Delay(_waitTime, cancellationToken).ConfigureAwait(false);
			return await base.RequestAsync<TResponse>(requestData, cancellationToken).ConfigureAwait(false);
		}
	}
}
