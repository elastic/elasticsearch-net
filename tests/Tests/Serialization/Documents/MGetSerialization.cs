// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Documents;

[UsesVerify]
public class MGetSerialization : SerializerTestBase
{
	[U]
	public async Task DeserializesError()
	{
		var json = @"{""docs"":[{""_index"":""devs"",""_id"":""1001"",""error"":{""root_cause"":[{""type"":""routing_missing_exception"",""reason"":""routing is required for [devs]/[1001]"",""index_uuid"":""_na_"",""index"":""devs""}],""type"":""routing_missing_exception"",""reason"":""routing is required for [devs]/[1001]"",""index_uuid"":""_na_"",""index"":""devs""}}]}";

		var stream = WrapInStream(json);

		var search = _requestResponseSerializer.Deserialize<MultiGetResponse<Developer>>(stream);

		search.Docs.Count.Should().Be(1);
		var error = search.Docs.First().Item2;
		error.Should().NotBeNull();

		error.Id.Should().Be("1001");
		error.Index.Should().Be("devs");

		await Verifier.Verify(error.Error);
	}

	[U]
	public async Task SerializesRequestWithSingleIds()
	{
		var request = new MultiGetRequest()
		{
			Ids = new Ids("single-value")
		};

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesRequestWithMultipleIds()
	{
		var request = new MultiGetRequest()
		{
			Ids = new Ids(new[] { "value-1", "value-2" })
		};

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesRequestWithSingleDocs()
	{
		var request = new MultiGetRequest()
		{
			Docs = new[] { new Operation { Index = "test-index", Id = "single-value" } }
		};

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesRequestWithMultipleDocs()
	{
		var request = new MultiGetRequest()
		{
			Docs = new[]
			{
				new Operation { Index = "test-index", Id = "value-a" },
				new Operation { Index = "test-index", Id = "value-b" }
			}
		};

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithSingleIds()
	{
		var request = new MultiGetRequestDescriptor()
			.Ids("single-value");

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	public async Task SerializesDescriptorWithMultipleIds()
	{
		var request = new MultiGetRequestDescriptor()
			.Ids(new[] { "value-1", "value-2" });

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithSingleDocsAsValue()
	{
		var request = new MultiGetRequestDescriptor()
			.Docs(new[] { new Operation { Index = "test-index", Id = "single-value" } });

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithMultipleDocsAsValue()
	{
		var request = new MultiGetRequestDescriptor()
			.Docs(new[]
			{
				new Operation { Index = "test-index", Id = "value-a" },
				new Operation { Index = "test-index", Id = "value-b" }
			});

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithSingleDocsAsDescriptorAction()
	{
		var request = new MultiGetRequestDescriptor()
			.Docs(d => d.Index("test-index").Id("single-value"));

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithSingleDocsAsDescriptor()
	{
		var request = new MultiGetRequestDescriptor()
			.Docs(new OperationDescriptor().Index("test-index").Id("single-value"));

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesDescriptorWithMultipleDocsAsDescriptorActions()
	{
		var request = new MultiGetRequestDescriptor()
			.Docs(
				d => d.Index("test-index").Id("value-a"),
				d => d.Index("test-index").Id("value-b")
			);

		var json = SerializeAndGetJsonString(request);
		await Verifier.VerifyJson(json);
	}
}
