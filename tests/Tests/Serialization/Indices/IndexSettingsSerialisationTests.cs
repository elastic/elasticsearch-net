// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.IndexManagement;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class IndexSettingsSerializationTests : SerializerTestBase
{
	private const string IndexSettingsJson = @"{""creation_date"":""1655895084631""}";

	[U]
	public async Task CanSerialize_CreationDate()
	{
		var settings = new IndexSettings { CreationDate = 1655895084631 };
		var json = SerializeAndGetJsonString(settings);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task CanSerialize_NullCreationDate()
	{
		var settings = new IndexSettings();
		var json = SerializeAndGetJsonString(settings);
		await Verifier.VerifyJson(json);
	}

	[U]
	public void CanDeserialize_StringifiedCreationDate()
	{
		var stream = WrapInStream(IndexSettingsJson);
		var settings = _requestResponseSerializer.Deserialize<IndexSettings>(stream);
		settings.CreationDate.Should().Be(1655895084631);
	}
}
