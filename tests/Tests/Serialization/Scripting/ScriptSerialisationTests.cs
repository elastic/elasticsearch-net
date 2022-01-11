// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Threading.Tasks;
using Elastic.Clients.JsonNetSerializer;
using VerifyXunit;

namespace Tests.Serialization.Scripting;

[UsesVerify]
public class ScriptParamsSerializationTests : InstanceSerializerTestBase
{
	public ScriptParamsSerializationTests()
		: base(new ElasticsearchClientSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), sourceSerializer: JsonNetSerializer.Default)) { }

	[U]
	public async Task SerializesParamsUsingRequestResponseSerializer_WhenUseSourceSerializerForScriptParameters_IsTrue()
	{
		// In this test, we expect the null to be serialized by the default JsonNetSerializer

		var script = new InlineScript("source")
		{
			Params = new System.Collections.Generic.Dictionary<string, object> { { "person", new Person { Forename = "has_null_surname", Surname = null } } }
		};

		var json = SerializeAndGetJsonString(script, new ElasticsearchClientSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), sourceSerializer: JsonNetSerializer.Default)
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

public class ScriptSerialisationTests : SerializerTestBase
{
	[U]
	public void CanDeserialize_InlineScript_WhenParamsComeLast()
	{
		var stream = WrapInStream(@"{""script"":{""lang"":""painless"",""source"":""doc['field_name'].value * params.factor"",""options"":{""option0"":""option_0""},""params"":{""factor"":1.1}}}");

		var scriptHolder = _requestResponseSerializer.Deserialize<ScriptHolder>(stream);

		scriptHolder.Should().NotBeNull();
		var inlineScript = scriptHolder.Script.Should().BeOfType<InlineScript>().Subject;
		AssertInlineScript(inlineScript);
	}

	[U]
	public void CanDeserialize_InlineScript_WhenParamsComeFirst()
	{
		var stream = WrapInStream(@"{""script"":{""params"":{""factor"":1.1},""lang"":""painless"",""source"":""doc['field_name'].value * params.factor"",""options"":{""option0"":""option_0""}}}");

		var scriptHolder = _requestResponseSerializer.Deserialize<ScriptHolder>(stream);

		scriptHolder.Should().NotBeNull();
		var inlineScript = scriptHolder.Script.Should().BeOfType<InlineScript>().Subject;
		AssertInlineScript(inlineScript);
	}

	private void AssertInlineScript(InlineScript inlineScript)
	{
		inlineScript.Language.Should().Be(BuiltinScriptLanguage.Painless);
		inlineScript.Source.Should().Be("doc['field_name'].value * params.factor");
		inlineScript.Options.Should().HaveCount(1);
		inlineScript.Options.TryGetValue("option0", out var optionValue).Should().BeTrue();
		optionValue.Should().Be("option_0");
		inlineScript.Params.Should().HaveCount(1);
		inlineScript.Params.TryGetValue("factor", out var factor).Should().BeTrue();
		factor.Should().Be(1.1);
	}

	[U]
	public void CanDeserialize_StoredScript_WhenParamsComeLast()
	{
		var stream = WrapInStream(@"{""script"":{""id"":""calculate-score"",""params"":{""my_modifier"":2}}}");

		var scriptHolder = _requestResponseSerializer.Deserialize<ScriptHolder>(stream);

		scriptHolder.Should().NotBeNull();
		var storedScript = scriptHolder.Script.Should().BeOfType<StoredScriptId>().Subject;
		storedScript.Id.Should().Be("calculate-score");
	}

	[U]
	public void CanDeserialize_StoredScript_WhenParamsComeFirst()
	{
		var stream = WrapInStream(@"{""script"":{""params"":{""my_modifier"":2},""id"":""calculate-score""}}");

		var scriptHolder = _requestResponseSerializer.Deserialize<ScriptHolder>(stream);

		scriptHolder.Should().NotBeNull();
		var storedScript = scriptHolder.Script.Should().BeOfType<StoredScriptId>().Subject;
		storedScript.Id.Should().Be("calculate-score");
	}

	[U]
	public void CanDeserialize_ShortScriptForm()
	{
		var stream = WrapInStream(@"{""script"":""ctx._source.likes++""}");

		var scriptHolder = _requestResponseSerializer.Deserialize<ScriptHolder>(stream);

		scriptHolder.Should().NotBeNull();
		var inlineScript = scriptHolder.Script.Should().BeOfType<InlineScript>().Subject;
		inlineScript.Source.Should().Be("ctx._source.likes++");
	}

	private class ScriptHolder
	{
		public ScriptBase Script { get; set; }
	}
}
