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
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Ip
{
	public class IpPropertyTests : PropertyTestsBase
	{
		public IpPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "ip",
					index = false,
					null_value = "127.0.0.1",
					doc_values = true,
					store = true,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Ip(s => s
				.Name(p => p.Name)
				.Index(false)
				.NullValue("127.0.0.1")
				.DocValues()
				.Store()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new IpProperty
				{
					Index = false,
					NullValue = "127.0.0.1",
					DocValues = true,
					Store = true,
				}
			}
		};
	}
}
