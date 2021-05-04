// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using FluentAssertions;
using Nest;
using System;
using System.Runtime.Serialization;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.DocumentationTests;
using Xunit;
using static Tests.Core.Serialization.SerializationTestHelper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[types-and-relations-inference]]
	*=== Relation names inference
	*
	*/
	public class TypesAndRelationsInference : DocumentationTestBase
	{
		/**
		* [[relation-names]]
		* [float]
		* === Relation names
		*
		* Prior to Elasticsearch 6.x you could have multiple types per index. They acted as a discrimatory column but were often
		* confused with tables. The fact that the mapping API's treated them as seperate entities did not help.
		*
		* The general guideline has always been to use a single type per index. Starting from 6.x this is also enforced.
		* Some features still need to store multiple types in a single index such as Parent/Child join relations.
		*
		* Both `Parent` and `Child` will need to have resolve to the same typename to be indexed into the same index.
		*
		* Therefore in 6.x we need a different type that translates a CLR type to a join relation. This can be configured seperately
		* using `.RelationName()`
		*/
		[U] public void RelationNameConfiguration()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<CommitActivity>(m => m
					.IndexName("projects-and-commits")
					.RelationName("commits")
				)
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects-and-commits")
					.RelationName("projects")
				);

			var resolver = new RelationNameResolver(settings);
			var relation = resolver.Resolve<Project>();
			relation.Should().Be("projects");

			relation = resolver.Resolve<CommitActivity>();
			relation.Should().Be("commits");
		}

		/**
		* `RelationName` uses the `DefaultTypeNameInferrer` to translate CLR types to a string representation.
		*
		* Explicit `TypeName` configuration does not affect how the default relation for the CLR type
		* is represented though
		*/
		[U] public void TypeNameExplicitConfigurationDoesNotAffectRelationName()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects-and-commits")
				);

			var resolver = new RelationNameResolver(settings);
			var relation = resolver.Resolve<Project>();
			relation.Should().Be("project");
		}

		//hide
		[U] public void RoundTripSerializationPreservesCluster()
		{
			Expect("project").WhenSerializing(Relation<Project>());
		}

		//hide
		[U] public void EqualsValidation()
		{
			var relation = (RelationName)"projects";
			var relationOther = (RelationName)"p";

			relation.Should().NotBe(relationOther);
			relation.Should().Be("projects");
			relation.Should().Be((RelationName)"projects");

			Relation<Project>().Should().Be(Relation<Project>());
			Relation<Project>().Should().NotBe(Relation<Developer>());
		}

		//hide
		[U] public void GetHashCodeValidation()
		{
			var relation = (RelationName)"projects";

			relation.GetHashCode().Should().NotBe(0);

			Relation<Project>().GetHashCode().Should().Be(Relation<Project>().GetHashCode()).And.NotBe(0);
			Relation<Project>().GetHashCode().Should().NotBe(Relation<Developer>().GetHashCode()).And.NotBe(0);

		}
	}
}
