using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class IndexNameInference
	{
		[U]
		public void DefaultIndexIsInferred()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex");
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("defaultindex");
		}

		[U]
		public void ExplicitMappingIsInferred()
		{
			var settings = new ConnectionSettings()
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		[U]
		public void ExplicitMappingTakesPrecedence()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("defaultindex")
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "projects")
				);
			var resolver = new IndexNameResolver(settings);
			var index = resolver.Resolve<Project>();
			index.Should().Be("projects");
		}

		[U]
		public void UppercaseCharacterThrowsResolveException()
		{
			var settings = new ConnectionSettings()
				.DefaultIndex("Default")
				.MapDefaultTypeIndices(m => m
					.Add(typeof(Project), "myProjects")
				);

			var resolver = new IndexNameResolver(settings);

			var e = Assert.Throws<ResolveException>(() => resolver.Resolve<Project>());
			e.Message.Should().Be($"Index names cannot contain uppercase characters: myProjects.");
			e = Assert.Throws<ResolveException>(() => resolver.Resolve<Tag>());
			e.Message.Should().Be($"Index names cannot contain uppercase characters: Default.");
			e = Assert.Throws<ResolveException>(() => resolver.Resolve("Foo"));
			e.Message.Should().Be($"Index names cannot contain uppercase characters: Foo.");
		}

		[U]
		public void NoIndexThrowsResolveException()
		{
			var settings = new ConnectionSettings();
			var resolver = new IndexNameResolver(settings);
			var e = Assert.Throws<ResolveException>(() => resolver.Resolve<Project>());
			e.Message.Should().Contain("Index name is null");
		}

		[U]
		public void ResolveExceptionBubblesOut()
		{
			var client = TestClient.GetInMemoryClient(s => new ConnectionSettings());
			var e = Assert.Throws<ResolveException>(() => client.Search<Project>());

		}
	}
}
