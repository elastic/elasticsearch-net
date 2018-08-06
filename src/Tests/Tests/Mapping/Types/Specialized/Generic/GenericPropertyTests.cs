using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Specialized.Generic
{
	public class GenericPropertyTests : SingleMappingPropertyTestsBase
	{
		private const string GenericType = "{dynamic_type}";
		public GenericPropertyTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object SingleMappingJson { get; } = new {index = false, type= GenericType};

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
	}
}
