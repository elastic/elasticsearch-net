using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.Completion
{
	public class CompletionPropertyTests : PropertyTestsBase
	{
		public CompletionPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				suggest = new
				{
					type = "completion",
					search_analyzer = "standard",
					analyzer = "standard",
					preserve_separators = true,
					preserve_position_increments = true,
					max_input_length = 20
				}
            }
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.Completion(s => s
					.Name(p => p.Suggest)
					.SearchAnalyzer("standard")
					.Analyzer("standard")
					.PreserveSeparators()
					.PreservePositionIncrements()
					.MaxInputLength(20)
				);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "suggest", new CompletionProperty
				{
					SearchAnalyzer = "standard",
					Analyzer = "standard",
					PreserveSeparators = true,
					PreservePositionIncrements = true,
					MaxInputLength = 20
				}
			}
		};
	}
}
