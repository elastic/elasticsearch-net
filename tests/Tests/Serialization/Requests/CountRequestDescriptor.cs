// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Requests;

[UsesVerify]
public class CountRequestDescriptor : SerializerTestBase
{
	[U]
	public async Task SerializesCountRequest()
	{
		var desciptor = new CountRequestDescriptor<Project>()
			.Query(q => q
				.Match(m => m
					.Field(f => f.Name)
					.Query("Elasticsearch")));

		var serialisedJson = await SerializeAndGetJsonStringAsync(desciptor);

		await Verifier.Verify(serialisedJson);
	}
}
