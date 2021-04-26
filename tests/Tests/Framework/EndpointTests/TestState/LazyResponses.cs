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
using System.Threading.Tasks;
using Nest;

namespace Tests.Framework.EndpointTests.TestState
{
	public class LazyResponses : AsyncLazy<Dictionary<ClientMethod, IResponse>>
	{
		public LazyResponses(Func<Dictionary<ClientMethod, IResponse>> factory) : this("__ignored__", factory) {}

		public LazyResponses(Func<Task<Dictionary<ClientMethod, IResponse>>> factory) : this("__ignored__", factory) {}

		public LazyResponses(string name, Func<Dictionary<ClientMethod, IResponse>> factory) : base(factory) => Name = name;

		public LazyResponses(string name, Func<Task<Dictionary<ClientMethod, IResponse>>> factory) : base(factory) => Name = name;

		public static LazyResponses Empty { get; } = new LazyResponses("__empty__", () => new Dictionary<ClientMethod, IResponse>());

		public string Name { get; }
	}
}
