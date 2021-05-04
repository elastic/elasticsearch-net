// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Domain;

namespace Tests.ClientConcepts.HighLevel.Inference.Equality
{
	public class DocumentPathEqualityTests
	{
		[U] public void Eq()
		{
			var project = new Project { Name = "x" };
			DocumentPath<Project> path = project;
			DocumentPath<Project> pathOther = project;
			(pathOther == path).Should().BeTrue();
			pathOther.Should().Be(path);

			path = new DocumentPath<Project>(2);
			pathOther = new DocumentPath<Project>(2);
			(pathOther == path).Should().BeTrue();
			pathOther.Should().Be(path);
		}

		[U] public void NotEq()
		{
			var project = new Project { Name = "x" };
			DocumentPath<Project> path = project;
			DocumentPath<Project> pathOther = new Project { Name = "x" };
			(pathOther != path).Should().BeTrue();
			pathOther.Should().NotBe(path);

			path = new DocumentPath<Project>(2);
			pathOther = new DocumentPath<Project>(2).Index("x");
			(pathOther != path).Should().BeTrue();
			pathOther.Should().NotBe(path);
		}

		[U] public void Null()
		{
			var value = new DocumentPath<Project>(2);
			(value == null).Should().BeFalse();
			(null == value).Should().BeFalse();
		}
	}
}
