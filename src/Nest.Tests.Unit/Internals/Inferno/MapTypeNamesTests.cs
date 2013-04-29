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

		[Test]
		public void ResolveToSepcifiedTypeNames()
		{
			var clientSettings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex("mydefaultindex")
				.MapDefaultTypeNames(p => p
					.Add(typeof(Car), "automobile")
					.Add(typeof(Person), "human")
					.Add(typeof(Organization), "organisation")
					.Add(typeof(Developer), "codemonkey")
					.Add(typeof(MyGeneric<Developer>), "codemonkey-wrapped-in-bacon")
					.Add(typeof(MyGeneric<Organization>), "org-wrapped-in-bacon")
				);

			TypeNameMarker marker = typeof (Car);
			marker.Resolve(clientSettings).Should().Be("automobile");

			marker = typeof (Person);
			marker.Resolve(clientSettings).Should().Be("human");

			marker = typeof(Organization);
			marker.Resolve(clientSettings).Should().Be("organisation");

			marker = typeof(Developer);
			marker.Resolve(clientSettings).Should().Be("codemonkey");

			marker = typeof(MyGeneric<Developer>);
			marker.Resolve(clientSettings).Should().Be("codemonkey-wrapped-in-bacon");

			marker = typeof(MyGeneric<Organization>);
			marker.Resolve(clientSettings).Should().Be("org-wrapped-in-bacon");

			//Should fall back to the default lowercase pluralize since
			//it doesn't have an explicit default
			marker = typeof(NoopObject);
			marker.Resolve(clientSettings).Should().Be("noopobjects");

		}

		[Test]
		public void TypesShouldMakeItIntoPaths()
		{
			var clientSettings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex("mydefaultindex")
				.MapDefaultTypeNames(p => p
					.Add(typeof(Car), "automobile")
					.Add(typeof(Person), "human")
					.Add(typeof(Organization), "organisation")
					.Add(typeof(Developer), "codemonkey")
				);
			var c = new PathResolver(clientSettings);
			var searchPath = c.GetSearchPathForTyped(new SearchDescriptor<Person>());
			StringAssert.Contains("/human/", searchPath);
			searchPath = c.GetSearchPathForTyped(new SearchDescriptor<Developer>());
			StringAssert.Contains("/codemonkey/", searchPath);
		}

		[Test]
		public void DefaultTypeNamesTakePrecedenceOverCustomTypeNameInferrer()
		{
			var clientSettings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex("mydefaultindex")
				.MapDefaultTypeNames(p => p
					.Add(typeof(Developer), "codemonkey")
				)
				.SetDefaultTypeNameInferrer(t=>t.Name.ToUpperInvariant())
				;

			TypeNameMarker marker = typeof(Developer);
			marker.Resolve(clientSettings).Should().Be("codemonkey");

			//Should use the custom type name inferrer that upper cases
			marker = typeof(NoopObject);
			marker.Resolve(clientSettings).Should().Be("NOOPOBJECT");

		}
	}
}
