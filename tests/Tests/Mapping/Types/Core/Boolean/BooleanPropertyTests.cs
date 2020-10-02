// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.Boolean
{
	public class BooleanPropertyTests : PropertyTestsBase
	{
		public BooleanPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "boolean",
					doc_values = false,
					store = true,
					index = false,
					null_value = false,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Boolean(b => b
				.Name(p => p.Name)
				.DocValues(false)
				.Store()
				.Index(false)
				.NullValue(false)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new BooleanProperty
				{
					DocValues = false,
					Store = true,
					Index = false,
					NullValue = false
				}
			}
		};
	}
}
