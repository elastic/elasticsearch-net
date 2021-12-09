// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;

namespace Tests.Serialization.Scripting;

public class ScriptSerialisationTests : SourceSerializerTestBase
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
