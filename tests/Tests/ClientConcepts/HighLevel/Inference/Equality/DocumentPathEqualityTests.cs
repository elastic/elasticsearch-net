/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
