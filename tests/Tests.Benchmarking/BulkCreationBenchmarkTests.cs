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

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Nest;
using Tests.Benchmarking.Framework;
using Tests.Domain;

namespace Tests.Benchmarking
{
	[BenchmarkConfig(10)]
	public class BulkCreationBenchmarkTests
	{
		private static readonly IList<Project> Projects = Project.Generator.Clone().Generate(10000);

		[GlobalSetup]
		public void Setup() { }

		[Benchmark(Description = "Descriptors.V8")]
		public Nest7.BulkDescriptor CreateUsingDecriptorsV8() => new Nest7.BulkDescriptor().IndexMany(Projects);

		[Benchmark(Description = "Descriptors")]
		public BulkDescriptor CreateUsingDecriptors() => new BulkDescriptor().IndexMany(Projects);

		[Benchmark(Description = "BulkCollection.AddRange")]
		public BulkRequest SynchronizedCollection()
		{
			var ops = new BulkOperationsCollection<IBulkOperation>();
			ops.AddRange(Projects.Select(p=> new BulkCreateOperation<Project>(p)));
			return new BulkRequest
			{
				Operations = ops
			};
		}

		[Benchmark(Description = "BulkCollection.const")]
		public BulkRequest SynchronizedCollectionConstructor()
		{
			var ops = new BulkOperationsCollection<IBulkOperation>(
				Projects.Select(p=> new BulkCreateOperation<Project>(p))
			);
			return new BulkRequest
			{
				Operations = ops
			};
		}

		[Benchmark(Description = "new List")]
		public BulkRequest NewList() =>
			new BulkRequest
			{
				Operations = new List<IBulkOperation>(
					Projects.Select(p=> new BulkCreateOperation<Project>(p))
				)
			};

	}
}
