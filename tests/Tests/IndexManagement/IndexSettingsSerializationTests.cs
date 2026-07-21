// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Analysis;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Domain;
using Tests.Serialization;
using VerifyXunit;
using Xunit.Sdk;

namespace Tests.IndexManagement;

[UsesVerify]
public class IndexSettingsSerializationTests : SerializerTestBase
{
	private const string IndexSettingsJson = @"{""creation_date"":""1655895084631""}";

	[U]
	public async Task CanSerialize_IndexSettingsWithCustomAnalyzer()
	{
		// Test case for https://github.com/elastic/elasticsearch-net/issues/6739
		// Resolved after improved code-generation of internally-tagged untions to include
		// converters for the variant interfaces.

		var descriptor = new IndexSettingsDescriptor<Project>()
			.Analysis(a => a
				.Analyzers(a => a
					.Custom("whitespace_lowercase", wl => wl
						.Tokenizer("whitespace")
						.Filter(new[] { "lowercase" })
					)
				)
		 );

		var json = await SerializeAndGetJsonStringAsync(descriptor);
		await Verifier.VerifyJson(json);

		var indexSettings = DeserializeJsonString<IndexSettings>(json);
		var analyzer = indexSettings.Analysis.Analyzers["whitespace_lowercase"];
		var customAnalyzer = analyzer.Should().BeAssignableTo<CustomAnalyzer>().Subject;
		customAnalyzer.Tokenizer.Should().Be("whitespace");
		customAnalyzer.Filter.Should().ContainSingle("lowercase");

		var objectJson = await SerializeAndGetJsonStringAsync(indexSettings);
		objectJson.Should().Be(json);
	}

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
