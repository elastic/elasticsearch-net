using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

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
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.2,
					include_in_all = false,
					ignore_malformed = true,
					format = "MM/dd/yyyy",
					null_value = DateTime.MinValue
				}
			}
		};

#pragma warning disable 618 // usage of IncludeInAll
		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Date(b => b
				.Name(p => p.LastActivity)
				.DocValues(false)
				.Similarity(SimilarityOption.Classic)
				.Store()
				.Index(false)
				.Boost(1.2)
				.IncludeInAll(false)
				.IgnoreMalformed()
				.Format("MM/dd/yyyy")
				.NullValue(DateTime.MinValue)
			);
#pragma warning restore 618

		protected override IProperties InitializerProperties => new Properties
		{
			{ "lastActivity", new DateProperty
				{
					DocValues = false,
					Similarity = SimilarityOption.Classic,
					Store = true,
					Index = false,
					Boost = 1.2,
#pragma warning disable 618
					IncludeInAll = false,
#pragma warning restore 618
					IgnoreMalformed = true,
					Format = "MM/dd/yyyy",
					NullValue = DateTime.MinValue
				}
			}
		};
	}
}
