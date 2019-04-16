using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.Integration;

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
					similarity = "BM25",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.Integer)
				.DocValues(true)
				.Similarity(SimilarityOption.BM25)
				.Store()
				.Index(false)
				.Boost(1.5)
				.NullValue(0.0)
				.IgnoreMalformed()
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"numberOfCommits", new NumberProperty(NumberType.Integer)
				{
					DocValues = true,
					Similarity = SimilarityOption.BM25,
					Store = true,
					Index = false,
					Boost = 1.5,
					NullValue = 0.0,
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
					similarity = "BM25",
					store = true,
					index = false,
					boost = 1.5,
					null_value = 0.0,
					ignore_malformed = true,
					coerce = true
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Number(n => n
				.Name(p => p.NumberOfCommits)
				.Type(NumberType.ScaledFloat)
				.ScalingFactor(10)
				.DocValues(true)
				.Similarity(SimilarityOption.BM25)
				.Store()
				.Index(false)
				.Boost(1.5)
				.NullValue(0.0)
				.IgnoreMalformed()
				.Coerce()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"numberOfCommits", new NumberProperty(NumberType.ScaledFloat)
				{
					ScalingFactor = 10,
					DocValues = true,
					Similarity = SimilarityOption.BM25,
					Store = true,
					Index = false,
					Boost = 1.5,
					NullValue = 0.0,
					IgnoreMalformed = true,
					Coerce = true
				}
			}
		};
	}
}
