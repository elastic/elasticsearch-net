// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace RequestConverter.Tests;

public class CodeWriterEscapingTests
{
	[Theory]
	// C# line terminators: a raw occurrence inside a "..." literal is a compile error.
	[InlineData("a\u2028b", "\"a\\u2028b\"")]
	[InlineData("a\u2029b", "\"a\\u2029b\"")]
	[InlineData("a\u0085b", "\"a\\u0085b\"")]
	// Control characters: legal in a literal but unreadable; escape for robustness.
	[InlineData("a\u0001b", "\"a\\u0001b\"")]
	[InlineData("a\u007Fb", "\"a\\u007Fb\"")]
	// Private-use area: contains the writer's type-ref placeholder sentinels.
	[InlineData("a\uE000b", "\"a\\uE000b\"")]
	[InlineData("a\uE001b", "\"a\\uE001b\"")]
	// Existing short escapes must be unchanged.
	[InlineData("a\"b\\c\nd\re\tf", "\"a\\\"b\\\\c\\nd\\re\\tf\"")]
	// Plain text must pass through untouched, including non-ASCII letters.
	[InlineData("héllo wörld", "\"héllo wörld\"")]
	public void WriteString_escapes_invalid_literal_characters(string input, string expected)
	{
		var writer = new global::RequestConverter.CodeWriter();
		writer.WriteString(input);
		Assert.Equal(expected, writer.ToString());
	}

	[Fact]
	public void Private_use_sentinel_in_user_string_does_not_corrupt_type_ref_resolution()
	{
		var writer = new global::RequestConverter.CodeWriter(
			new global::RequestConverter.FormattingOptions { TypeNameStyle = global::RequestConverter.TypeNameStyle.GlobalFqn });
		writer.WriteTypeRef("System.Text.Json.JsonElement");
		writer.Write(" x = ");
		// Before the fix this raw U+E000/U+E001 pair makes ToString() misparse the placeholder stream and throw.
		writer.WriteString("\uE000 42 \uE001");

		var code = writer.ToString();

		Assert.Equal("global::System.Text.Json.JsonElement x = \"\\uE000 42 \\uE001\"", code);
	}
}
