// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Generic
{
	public class GenericPropertyTests : SingleMappingPropertyTestsBase
	{
		private const string GenericType = "{dynamic_type}";

		public GenericPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<SingleMappingSelector<object>, IProperty> FluentSingleMapping => m => m
			.Generic(g => g
				.Type(GenericType)
				.Index(false)
			);

		protected override IProperty InitializerSingleMapping { get; } = new GenericProperty
		{
			Type = GenericType,
			Index = false
		};

		protected override object SingleMappingJson { get; } = new { index = false, type = GenericType };
	}
}
