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

namespace Tests.Mapping.Types.Core.Date
{
	public class DatePropertyTests : PropertyTestsBase
	{
		public DatePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				lastActivity = new
				{
					type = "date",
					doc_values = false,
					store = true,
					index = false,
					ignore_malformed = true,
					format = "yyyy-MM-dd'T'HH:mm[:ss][.S]",
					null_value = DateTime.MinValue
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Date(b => b
				.Name(p => p.LastActivity)
				.DocValues(false)
				.Store()
				.Index(false)
				.IgnoreMalformed()
				.Format("yyyy-MM-dd'T'HH:mm[:ss][.S]")
				.NullValue(DateTime.MinValue)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"lastActivity", new DateProperty
				{
					DocValues = false,
					Store = true,
					Index = false,
					IgnoreMalformed = true,
					Format = "yyyy-MM-dd'T'HH:mm[:ss][.S]",
					NullValue = DateTime.MinValue
				}
			}
		};
	}
}
