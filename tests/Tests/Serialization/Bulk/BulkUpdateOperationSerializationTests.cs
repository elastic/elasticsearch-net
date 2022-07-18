// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Bulk;

[UsesVerify]
public class BulkUpdateOperationSerializationTests : SerializerTestBase
{
	[U]
	public async Task Serializes_With_StoredScriptId()
	{
		var operation = new BulkUpdateOperationDescriptor<Project, Project>("doc-id")
			.Script(s => s
				.Id("script-id")
				.Params(p => p.Add("param1", "value1").Add("param2", "value2")));

		var jsonString = SerializeAndGetJsonString(operation);

		await Verifier.Verify(jsonString);
	}
}
