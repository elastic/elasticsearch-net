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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Client;
using Tests.Core.Serialization;

namespace Tests.IndexModules
{
	public abstract class UsageTestBase<TInterface, TDescriptor, TInitializer> : ExpectJsonTestBase
		where TDescriptor : TInterface, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected UsageTestBase() : base(TestClient.DefaultInMemoryClient) =>
			// ReSharper disable once VirtualMemberCallInConstructor
			FluentInstance = Fluent(new TDescriptor());

		protected abstract Func<TDescriptor, TInterface> Fluent { get; }
		protected abstract TInitializer Initializer { get; }

		protected virtual bool TestObjectInitializer => true;
		private TInterface FluentInstance { get; }

		[U] protected void SerializesInitializer()
		{
			if (TestObjectInitializer) RoundTripsOrSerializes<TInterface>(Initializer);
		}

		[U] protected void SerializesFluent() => RoundTripsOrSerializes(FluentInstance);
	}

	public abstract class PromiseUsageTestBase<TInterface, TDescriptor, TInitializer> : ExpectJsonTestBase
		where TDescriptor : IPromise<TInterface>, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected PromiseUsageTestBase() : base(TestClient.DefaultInMemoryClient) =>
			// ReSharper disable once VirtualMemberCallInConstructor
			FluentInstance = Fluent(new TDescriptor())?.Value;

		protected abstract Func<TDescriptor, IPromise<TInterface>> Fluent { get; }
		protected abstract TInitializer Initializer { get; }
		private TInterface FluentInstance { get; }

		[U] protected void SerializesInitializer() => Tester.RoundTrips<TInterface>(Initializer, ExpectJson);

		[U] protected void SerializesFluent() => Tester.RoundTrips(FluentInstance, ExpectJson);
	}
}
