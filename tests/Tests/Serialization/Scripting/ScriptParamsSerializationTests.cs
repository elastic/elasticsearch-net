// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Clients.JsonNetSerializer;
using Tests.Core.Xunit;
using VerifyXunit;

namespace Tests.Serialization.Scripting;

[UsesVerify]
[SystemTextJsonOnly]
public class ScriptParamsSerializationTests : InstanceSerializerTestBase
{
	public ScriptParamsSerializationTests()
		: base(new ElasticsearchClientSettings(new SingleNodePool(new Uri("http://localhost:9200")), sourceSerializer: JsonNetSerializer.Default)) { }

	[U]
	public async Task SerializesParamsUsingRequestResponseSerializer_WhenUseSourceSerializerForScriptParameters_IsTrue()
	{
		// In this test, we expect the null to be serialized by the default JsonNetSerializer

		var script = new InlineScript("source")
		{
			Params = new System.Collections.Generic.Dictionary<string, object> { { "person", new Person { Forename = "has_null_surname", Surname = null } } }
		};

		var json = SerializeAndGetJsonString(script, new ElasticsearchClientSettings(new SingleNodePool(new Uri("http://localhost:9200")), sourceSerializer: JsonNetSerializer.Default)
				.Experimental(new ExperimentalSettings { UseSourceSerializerForScriptParameters = true }));

		await Verifier.VerifyJson(json);
	}

	[U]
	public async Task SerializesParamsUsingRequestResponseSerializer_WhenUseSourceSerializerForScriptParameters_IsFalse()
	{
		// In this test, we expect the nulls to be ignore when the setting is disabled so the request response serializer is used.

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
		// In this test, we expect the nulls to be ignore when the setting is disabled so the request response serializer is used.

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
