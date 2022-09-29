// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Tests.Core.Xunit;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
[SystemTextJsonOnly]
public class KvpSerializationTests : SerializerTestBase
{
	[U]
	public async Task SerializesKvpWithFieldKey()
	{
		var dictionary = new KeyValuePair<Field, TestData>("field-name", new TestData { Name = "Steve", Age = 30 });

		var json = await SerializeAndGetJsonStringAsync(dictionary);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesKvp()
	{
		var dictionary = new KeyValuePair<string, int>("field-name", 100);

		var json = await SerializeAndGetJsonStringAsync(dictionary);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task DeserializesKvpWithFieldKey()
	{
		var json = @"{""field-name"":{""name"":""Steve"",""age"":30}}";

		var result = DeserializeJsonString<KeyValuePair<Field, TestData>>(json);

		await Verifier.Verify(result);
	}

	[U]
	public async Task DeserializesKvp()
	{
		var json = @"{""key"": ""field-name"",""value"": 100}";

		var result = DeserializeJsonString<KeyValuePair<string, int>>(json);

		await Verifier.Verify(result);
	}


	private class TestData
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
}
