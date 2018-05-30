using FluentAssertions;
using Nest;
using System;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[types-and-relations-inference]]
	*=== Types and relations inference
	*
	*/
	public class TypesAndRelationsInference : DocumentationTestBase
	{
		/**
		* ==== TypeName default
		* Type names are resolved by lowercasing the type name
		*/

		[U] public void DefaultTypeNameIsLowercase()
		{
			var settings = new ConnectionSettings();
			var resolver = new TypeNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("project");
		}
		/**
		* [[default-type-name]]
		* ==== Default Type Name
		* With Elasticsearch 6.x you can only have a single type per index and in the long run types will be phased out.
		* The need to tag types is no longer necessary so in many cases it makes sense to use a single fixed type.
		*/
		[U] public void DefaultTypeName()
		{
			var settings = new ConnectionSettings().DefaultTypeName("doc");
			var resolver = new TypeNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("doc");
		}

		/**
		* [[type-name-inferrer]]
		* ==== Override tye name inferer
		* You can provide a delegate to override the default type name inferrer for types
		*/
		[U] public void OverridingDefaultTypeNameInferrer()
		{
			var settings = new ConnectionSettings()
				.DefaultTypeNameInferrer(t=>t.Name.ToLower() + "-suffix");
			var resolver = new TypeNameResolver(settings);
			var type = resolver.Resolve<Project>();
			type.Should().Be("project-suffix");
		}

		/**
		* [[relation-names]]
		* ==== Relation names
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
					.TypeName("doc")
					.RelationName("commits")
				)
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects-and-commits")
					.TypeName("doc")
					.RelationName("projects")
				);

			var resolver = new RelationNameResolver(settings);
			var relation = resolver.Resolve<Project>();
			relation.Should().Be("projects");

			relation = resolver.Resolve<CommitActivity>();
			relation.Should().Be("commits");
		}

		/***
		*
		* `RelationName` use the `DefaultTypeNameInferrer` to translate CLR Type's to a string representation
		*
		* Explicit `TypeName` configuration does not affect how the default relation for the CLR type is represented though
		*/
		[U] public void TypeNameExplicitConfigurationDoesNotAffectRelationName()
		{
			var settings = new ConnectionSettings()
				.DefaultMappingFor<Project>(m => m
					.IndexName("projects-and-commits")
					.TypeName("doc")
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


			Nest.Types types1 = "foo,bar";
			Nest.Types types2 = "bar,foo";
			types1.Should().Be(types2);
			(types1 == types2).Should().BeTrue();
		}

		//hide
		[U] public void GetHashCodeValidation()
		{
			var relation = (RelationName)"projects";
			var typeName = (TypeName)"projects";

			relation.GetHashCode().Should().NotBe(typeName.GetHashCode()).And.NotBe(0);

			Relation<Project>().GetHashCode().Should().Be(Relation<Project>().GetHashCode()).And.NotBe(0);
			Relation<Project>().GetHashCode().Should().NotBe(Relation<Developer>().GetHashCode()).And.NotBe(0);

		}
	}
}
