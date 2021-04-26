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
using Nest;
using Tests.Domain;
using Tests.IndexModules;

namespace Tests.Mapping.LocalMetadata
{
	public class LocalMetdataUsageTests : UsageTestBase<ITypeMapping, TypeMappingDescriptor<Project>, TypeMapping>
	{
		// Ensure local metadata is never serialized
		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "float"
				}
			}
		};

		protected override Func<TypeMappingDescriptor<Project>, ITypeMapping> Fluent => f => f
			.Properties(ps => ps
				.Number(t => t
					.Name(p => p.NumberOfCommits)
					.LocalMetadata(m => m
						.Add("foo", "bar")
					)
				)
			);


		protected override TypeMapping Initializer => new TypeMapping
		{
			Properties = new Properties
			{
				{ "numberOfCommits", new NumberProperty { LocalMetadata = new Dictionary<string, object> { { "foo", "bar" } } } }
			}
		};
	}
}
