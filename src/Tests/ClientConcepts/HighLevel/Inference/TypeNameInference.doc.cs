using FluentAssertions;
using Nest;
using System;
using System.Runtime.Serialization;
using Elastic.Xunit.XunitPlumbing;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit;
using static Tests.Framework.RoundTripper;
using static Nest.Infer;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	/**[[type-name-inference]]
	*=== Type name inference
	*/
	public class TypeNameInference : DocumentationTestBase
	{
		/**
		* Type names are resolved in NEST by default, by lowercasing the CLR type name
		*/
		[U] public void DefaultTypeNameIsLowercase()
		{
			var settings = new ConnectionSettings();
			var resolver = new TypeNameResolver(settings);
			var type = resolver.Resolve<Project>();
			type.Should().Be("project");
		}

		/**
		 * [[elasticsearchtype-attribute]]
		 * ==== Applying a type name with `ElasticsearchTypeAttribute`
		 *
		 * A type name can be applied for a CLR type, using the Name property on `ElasticsearchTypeAttribute`
		 */
		[ElasticsearchType(Name = "attributed_project")]
		public class AttributedProject { }

		[U] public void TypeNameFromElasticsearchTypeAttribute()
		{
			var settings = new ConnectionSettings();
			var resolver = new TypeNameResolver(settings);
			var type = resolver.Resolve<AttributedProject>();
			type.Should().Be("attributed_project");
		}

		/**
		 * [[datacontract-attribute]]
		 * ==== Applying a type name with `DataContractAttribute`
		 *
		 * Similarly to <<elasticsearchtype-attribute, `ElasticsearchTypeAttribute`>>, a type name can be applied for a
		 * CLR type, using the Name property on `System.Runtime.Serialization.DataContractAttribute`
		 */
		[DataContract(Name = "data_contract_project")]
		public class DataContractProject { }

		[U] public void TypeNameFromDataContractAttribute()
		{
			var settings = new ConnectionSettings();
			var resolver = new TypeNameResolver(settings);
			var type = resolver.Resolve<DataContractProject>();
			type.Should().Be("data_contract_project");
		}

		/**
		* [[type-name-inferrer]]
		* ==== Override type name inferrer
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
	}
}
