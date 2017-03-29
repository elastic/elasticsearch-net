using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.Number
{
	public class NumberPropertyTests : PropertyTestsBase
	{
		public NumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "integer",
					doc_values = true,
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					include_in_all = false,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

#pragma warning disable 618 // Usage of IncludeInAll
		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.Integer)
				.DocValues(true)
				.Similarity(SimilarityOption.Classic)
				.Store()
				.Index(false)
				.Boost(1.5)
				.NullValue(0.0)
				.IncludeInAll(false)
				.IgnoreMalformed()
				.Coerce()
			);
#pragma warning restore 618

		protected override IProperties InitializerProperties => new Properties
		{
			{ "numberOfCommits", new NumberProperty(NumberType.Integer)
				{
					DocValues = true,
					Similarity = SimilarityOption.Classic,
					Store = true,
					Index = false,
					Boost = 1.5,
					NullValue = 0.0,
#pragma warning disable 618
					IncludeInAll = false,
#pragma warning restore 618
					IgnoreMalformed = true,
					Coerce = true
				}
			}
		};
	}

	public class ScaledFloatNumberPropertyTests : PropertyTestsBase
	{
		public ScaledFloatNumberPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "scaled_float",
					scaling_factor = 10.0,
					doc_values = true,
					similarity = "classic",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					include_in_all = false,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

#pragma warning disable 618 // Usage of IncludeInAll
		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.ScaledFloat)
				.ScalingFactor(10)
				.DocValues(true)
				.Similarity(SimilarityOption.Classic)
				.Store()
				.Index(false)
				.Boost(1.5)
				.NullValue(0.0)
				.IncludeInAll(false)
				.IgnoreMalformed()
				.Coerce()
			);
#pragma warning restore 618


		protected override IProperties InitializerProperties => new Properties
		{
			{ "numberOfCommits", new NumberProperty(NumberType.ScaledFloat)
				{
					ScalingFactor = 10,
					DocValues = true,
					Similarity = SimilarityOption.Classic,
					Store = true,
					Index = false,
					Boost = 1.5,
					NullValue = 0.0,
#pragma warning disable 618
					IncludeInAll = false,
#pragma warning restore 618
					IgnoreMalformed = true,
					Coerce = true
				}
			}
		};
	}
}
