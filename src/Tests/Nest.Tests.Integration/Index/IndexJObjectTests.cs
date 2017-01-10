using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Nest.Tests.Integration.Index
{
	[TestFixture]
	public class IndexJObjectTests : IntegrationTests
	{
		private IEnumerable<JObject> _jobjects;

		public IEnumerable<JObject> JObjects
		{
			get
			{
				if (_jobjects == null)
				{
					_jobjects = Enumerable.Range(1, 1000)
						.Select(i =>
							new JObject
							{
								{ "id", i },
								{ "name", $"name {i}" },
								{ "value", Math.Pow(i, 2) },
								{ "date", new DateTime(2016, 1, 1) },
								{
									"child", new JObject
									{
										{ "child_name", $"child_name {i}{i}" },
										{ "child_value", 3 }
									}
								}
							});
				}

				return _jobjects;
			}
		}

		[Test]
		public void Index()
		{
			var jObject = JObjects.First();

			var indexResult = this.Client.Index(jObject, f => f
				.Id(jObject["id"].Value<int>())
			);

			indexResult.IsValid.Should().BeTrue();
			indexResult.RequestInformation.HttpStatusCode.Should().Be(201);
			indexResult.Created.Should().BeTrue();
			indexResult.Type.Should().Be("jobject");
		}

		[Test]
		public void Bulk()
		{
			var bulkResponse = this.Client.Bulk(b => b
				.IndexMany(JObjects.Skip(1), (bi, d) => bi
					.Document(d)
					.Id(d["id"].Value<int>())
				)
			);

			bulkResponse.IsValid.Should().BeTrue();
			bulkResponse.RequestInformation.HttpStatusCode.Should().Be(200);

			foreach (var response in bulkResponse.Items)
			{
				response.IsValid.Should().BeTrue();
				response.Status.Should().Be(201);
			}
		}
	}
}