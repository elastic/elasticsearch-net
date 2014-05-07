using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;
using FluentAssertions;

namespace Nest.Tests.Unit.Internals.Inferno
{
	[TestFixture]
	public class MapTypeNamesTests
	{
		public class Car {}
		public class Person {}
		public class Organization {}
		public class Developer {}
		public class NoopObject {}

		public class MyGeneric<T> where T : class {}

		[ElasticType(Name = "custotypo")]
		public class MyCustomAtrributeName { }

		[Test]
		public void ResolveToSepcifiedTypeNames()
		{
			var clientSettings = new ConnectionSettings(UnitTestDefaults.Uri, "mydefaultindex")
				.MapDefaultTypeNames(p => p
					.Add(typeof(Car), "automobile")
					.Add(typeof(Person), "human")
					.Add(typeof(Organization), "organisation")
					.Add(typeof(Developer), "codemonkey")
					.Add(typeof(MyGeneric<Developer>), "codemonkey-wrapped-in-bacon")
					.Add(typeof(MyGeneric<Organization>), "org-wrapped-in-bacon")
				);
			var inferrer = new ElasticInferrer(clientSettings);

			TypeNameMarker marker = typeof (Car);
			inferrer.TypeName(marker).Should().Be("automobile");

			marker = typeof (Person);
			inferrer.TypeName(marker).Should().Be("human");

			marker = typeof(Organization);
			inferrer.TypeName(marker).Should().Be("organisation");

			marker = typeof(Developer);
			inferrer.TypeName(marker).Should().Be("codemonkey");

			marker = typeof(MyGeneric<Developer>);
			inferrer.TypeName(marker).Should().Be("codemonkey-wrapped-in-bacon");

			marker = typeof(MyGeneric<Organization>);
			inferrer.TypeName(marker).Should().Be("org-wrapped-in-bacon");

			//Should fall back to the default lowercase since
			//it doesn't have an explicit default
			marker = typeof(NoopObject);
			inferrer.TypeName(marker).Should().Be("noopobject");

		}

		[Test]
		public void DefaultTypeNamesTakePrecedenceOverCustomTypeNameInferrer()
		{
			var clientSettings = new ConnectionSettings(UnitTestDefaults.Uri, "mydefaultindex")
				.MapDefaultTypeNames(p => p
					.Add(typeof(Developer), "codemonkey")
				)
				.SetDefaultTypeNameInferrer(t=>t.Name.ToUpperInvariant())
				;

			var inferrer = new ElasticInferrer(clientSettings);
			TypeNameMarker marker = typeof(Developer);
			inferrer.TypeName(marker).Should().Be("codemonkey");

			//Should use the custom type name inferrer that upper cases
			marker = typeof(NoopObject);
			inferrer.TypeName(marker).Should().Be("NOOPOBJECT");

		}

		[Test]
		public void AttributeTypeNamesTakePrecedenceOverDefaultTypeNameInferrer()
		{
			var clientSettings = new ConnectionSettings(UnitTestDefaults.Uri, "mydefaultindex")
				.SetDefaultTypeNameInferrer(t => t.Name.ToUpperInvariant())
				;

			var inferrer = new ElasticInferrer(clientSettings);
			TypeNameMarker marker = typeof(MyCustomAtrributeName);
			inferrer.TypeName(marker).Should().Be("custotypo");

		}

		[Test]
		public void MapTypeIndicesTakesPrecedenceOverAttributeName()
		{
			var clientSettings = new ConnectionSettings(UnitTestDefaults.Uri,"mydefaultindex")
				.MapDefaultTypeNames(dt=>dt
					.Add(typeof(MyCustomAtrributeName), "micutype")
				)
				.SetDefaultTypeNameInferrer(t => t.Name.ToUpperInvariant())
				;

			var inferrer = new ElasticInferrer(clientSettings);
			TypeNameMarker marker = typeof(MyCustomAtrributeName);
			inferrer.TypeName(marker).Should().Be("micutype");

		}
	}
}
