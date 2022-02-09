// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization
{
	[UsesVerify]
	public class BulkSerialisationTests : SerializerTestBase
	{
		[U]
		public async Task BulkRequest_SerialisationTest()
		{
			var operations = new BulkOperationsCollection
			{
				new BulkIndexOperation<Project>(FixedProject) { Index = "project" },
				new BulkIndexOperation<Project>(FixedProject)
			};

			var request = new BulkRequest
			{
				Operations = operations
			};

			var serialisedJson = await SerializeAndGetJsonStringAsync(request);
			await Verifier.Verify(serialisedJson);
		}

		[U]
		public async Task BulkRequest_DescriptorSerialisationTest()
		{
			var request = new BulkRequestDescriptor();
			request.Index(FixedProject, b => b.Index("project"));
			request.Index(FixedProject);

			var serialisedJson = await SerializeAndGetJsonStringAsync(request);
			await Verifier.Verify(serialisedJson);
		}

		[U]
		public async Task BulkRequest_IndexMany_DescriptorSerialisationTest()
		{
			var request = new BulkRequestDescriptor();
			request.IndexMany(new [] { FixedProject, FixedProject });

			var serialisedJson = await SerializeAndGetJsonStringAsync(request);
			await Verifier.Verify(serialisedJson);
		}

		private static readonly Project FixedProject = new()
		{
			LeadDeveloper = new Developer
			{
				FirstName = "Steve",
				LastName = "Gordon",
				Gender = Gender.Male
			}
		};
	}
}
