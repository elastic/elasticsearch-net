// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Threading.Tasks;
using Tests.Core.Xunit;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
[SystemTextJsonOnly]
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

		var sr = new StringReader(serialisedJson);
		var count = 0;
		while(true)
		{
			var line = sr.ReadLine();
			if (line is not null)
			{
				await Verifier.VerifyJson(line).UseMethodName($"{nameof(BulkRequest_SerialisationTest)}_{++count}");
			}
			else
			{
				break;
			}
		}
	}

	[U]
	public async Task BulkRequest_DescriptorSerialisationTest()
	{
		var request = new BulkRequestDescriptor();
		request.Index(FixedProject, b => b.Index("project"));
		request.Index(FixedProject);

		var serialisedJson = await SerializeAndGetJsonStringAsync(request);

		var sr = new StringReader(serialisedJson);
		var count = 0;
		while (true)
		{
			var line = sr.ReadLine();
			if (line is not null)
			{
				await Verifier.VerifyJson(line).UseMethodName($"{nameof(BulkRequest_DescriptorSerialisationTest)}_{++count}");
			}
			else
			{
				break;
			}
		}
	}

	[U]
	public async Task BulkRequest_IndexMany_DescriptorSerialisationTest()
	{
		var request = new BulkRequestDescriptor();
		request.IndexMany(new [] { FixedProject, FixedProject });

		var serialisedJson = await SerializeAndGetJsonStringAsync(request);

		var sr = new StringReader(serialisedJson);
		var count = 0;
		while (true)
		{
			var line = sr.ReadLine();
			if (line is not null)
			{
				await Verifier.VerifyJson(line).UseMethodName($"{nameof(BulkRequest_IndexMany_DescriptorSerialisationTest)}_{++count}");
			}
			else
			{
				break;
			}
		}
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
