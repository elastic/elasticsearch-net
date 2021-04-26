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

using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.Xunit;
using Tests.Domain;
using Tests.Framework.DocumentationTests;

namespace Tests.Document.Single.Source
{
	public class SourceIntegrationTests : IntegrationDocumentationTestBase, IClusterFixture<ReadOnlyCluster>
	{
		public SourceIntegrationTests(ReadOnlyCluster cluster) : base(cluster) { }

		[I] public void SourceReturnsDocument()
		{
			var project = Client.Source<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;
			var p = Project.Projects.FirstOrDefault(i => i.Name == Project.Instance.Name);
			p.Should().NotBeNull("Test setup failure, project instance not found in projects indexed into readonly cluster");

			project.Name.Should().Be(p.Name);
			project.CuratedTags.Should().HaveCount(p.CuratedTags.Count());
			project.LastActivity.Should().Be(p.LastActivity);
			project.StartedOn.Should().Be(p.StartedOn);
		}

		[I]
		[JsonNetSerializerOnly]
		public void UseSourceSerializer()
		{
			var project = Client.Source<Project>(Project.Instance.Name, s => s.Routing(Project.Instance.Name)).Body;

			var sourceOnly = project.SourceOnly;
			sourceOnly.Should().NotBeNull();
			sourceOnly.NotReadByDefaultSerializer.Should().Be("read");
			sourceOnly.NotWrittenByDefaultSerializer.Should().Be("written");
		}
	}
}
