using System;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Specialized.Generic
{
	public class GenericPropertyTests : SingleMappingPropertyTestsBase
	{
		private const string GenericType = "{dynamic_type}";
		public GenericPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object SingleMappingJson { get; } = new {index = "no", type= GenericType};

		protected override Func<SingleMappingDescriptor<object>, IProperty> FluentSingleMapping => m => m
			.Generic(g => g
				.Type(GenericType)
				.Index(FieldIndexOption.No)
			);

		protected override IProperty InitializerSingleMapping { get; } = new GenericProperty
		{
			Type = GenericType,
			Index = FieldIndexOption.No
		};
	}
}
