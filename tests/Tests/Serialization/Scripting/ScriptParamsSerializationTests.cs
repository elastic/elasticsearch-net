// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VerifyXunit;

namespace Tests.Serialization.Scripting;

[UsesVerify]
public class ScriptParamsSerializationTests : InstanceSerializerTestBase
{
	private class TestSerializer : Serializer
	{
		public override object Deserialize(Type type, Stream stream) => throw new NotImplementedException();

		public override T Deserialize<T>(Stream stream) => throw new NotImplementedException();

		public override ValueTask<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) => throw new NotImplementedException();

		public override void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None) => stream.Write(Encoding.UTF8.GetBytes(@"{""fromSourceSerializer"":true}"));

		public override Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None, CancellationToken cancellationToken = default) => throw new NotImplementedException();
	}

	public ScriptParamsSerializationTests()
		: base(new ElasticsearchClientSettings(new SingleNodePool(new Uri("http://localhost:9200")), sourceSerializer: (_, _) => new TestSerializer() )) { }

	[U]
	public async Task SerializesParamsUsingRequestResponseSerializer_WhenUseSourceSerializerForScriptParameters_IsTrue()
	{
		// In this test, we expect the TestSerializer to be used because we've enabled the experimental setting

		var script = new InlineScript("source")
		{
			Params = new System.Collections.Generic.Dictionary<string, object> { { "person", new Person { Forename = "has_null_surname", Surname = null } } }
		};

		var json = SerializeAndGetJsonString(script, new ElasticsearchClientSettings(new SingleNodePool(new Uri("http://localhost:9200")), (_, _) => new TestSerializer())
				.Experimental(new ExperimentalSettings { UseSourceSerializerForScriptParameters = true }));

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesParamsUsingRequestResponseSerializer_WhenUseSourceSerializerForScriptParameters_IsFalse()
	{
		// In this test, we expect the nulls to be ignored when the setting is disabled so the request response serializer is used.

		var script = new InlineScript("source")
		{
			Params = new System.Collections.Generic.Dictionary<string, object> { { "person", new Person { Forename = "has_null_surname", Surname = null } } }
		};

		var json = SerializeAndGetJsonString(script);

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesRawJson()
	{
		// In this test, we expect the nulls to be ignored when the setting is disabled so the request response serializer is used.

		var script = new InlineScript("source")
		{
			Params = new System.Collections.Generic.Dictionary<string, object> { { "person", new RawJsonString(@"{ ""forename"": ""raw_json"" }") } }
		};

		var json = SerializeAndGetJsonString(script);

		await Verifier.VerifyJson(json);
	}

	private class Person
	{
		public string Forename { get; set; }
		public string Surname { get; set; }
	}
}
