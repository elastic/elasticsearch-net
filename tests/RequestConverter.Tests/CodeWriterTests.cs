// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;

using RequestConverter;

using Xunit;

namespace RequestConverter.Tests;

public class CodeWriterTests
{
	private static JsonElement Json(string text) => JsonSerializer.Deserialize<JsonElement>(text);

	private static CodeWriter Writer(FormattingOptions? options = null) => new(options);

	// ---- WriteObjectValue number fidelity (TryWriteObjectNumber) ----------

	[Theory]
	[InlineData("42", "42")]
	[InlineData("-7", "-7")]
	// G17 of 1.5 equals the token, so the double literal is faithful.
	[InlineData("1.5", "1.5d")]
	[InlineData("123.456", "123.456d")]
	public void WriteObjectValue_renders_faithful_numbers_as_literals(string token, string expected)
	{
		var writer = Writer();
		writer.WriteObjectValue(Json(token));
		Assert.Equal(expected, writer.ToString());
	}

	[Theory]
	// G17 of 1.1 is 1.1000000000000001, so a double literal would not reserialize identically.
	[InlineData("1.1")]
	// Exponent form: the boxed double reserializes as 100000, not 1e5.
	[InlineData("1e5")]
	// Dropped-zero form: G17 of 2.0 is 2. Conservatively rejected.
	[InlineData("2.0")]
	public void WriteObjectValue_keeps_unfaithful_numbers_as_JsonElement(string token)
	{
		var writer = Writer();
		writer.WriteObjectValue(Json(token));
		Assert.Contains("Deserialize<", writer.ToString());
		Assert.Contains(token, writer.ToString());
	}

	[Theory]
	[InlineData("\"hi\"", "\"hi\"")]
	[InlineData("true", "true")]
	[InlineData("false", "false")]
	[InlineData("null", "null")]
	public void WriteObjectValue_renders_scalars_plainly(string token, string expected)
	{
		var writer = Writer();
		writer.WriteObjectValue(Json(token));
		Assert.Equal(expected, writer.ToString());
	}

	// ---- WriteValue JsonElement rendering ---------------------------------

	[Fact]
	public void JsonElement_object_renders_as_raw_string_literal_with_reindented_json()
	{
		var writer = Writer();
		writer.WriteValue(Json("{\"a\":1}"));
		var code = writer.ToString();

		Assert.Contains("Deserialize<", code);
		Assert.Contains("\"\"\"", code);
		// Canonical 2-space re-indentation, independent of source formatting.
		Assert.Contains("  \"a\": 1", code);
	}

	[Fact]
	public void JsonElement_scalar_renders_compact()
	{
		var writer = Writer();
		writer.WriteValue(Json("\"x\""));
		var code = writer.ToString();

		Assert.Contains("Deserialize<", code);
		Assert.DoesNotContain("\"\"\"", code);
	}

	// ---- Fluent primitives: empty-body collapse ---------------------------

	[Fact]
	public void Empty_descriptor_body_collapses_to_no_arg_call()
	{
		var writer = Writer();
		writer.WriteFluentDescriptorCall("Query", static w => { });
		Assert.Equal("\n.Query()", writer.ToString());
	}

	[Fact]
	public void Empty_descriptor_body_uses_writeEmpty_fallback_when_supplied()
	{
		var writer = Writer();
		writer.WriteFluentDescriptorCall("Properties", static w => { }, static w => w.Write("new Properties() { }"));
		Assert.Equal("\n.Properties(new Properties() { })", writer.ToString());
	}

	[Fact]
	public void Non_empty_descriptor_body_wraps_and_closes_on_its_own_line()
	{
		var writer = Writer();
		writer.WriteFluentDescriptorCall("Query", static w => w.WriteFluentCall("Term"));
		Assert.Equal("\n.Query(d1 => d1\n    .Term()\n)", writer.ToString());
	}

	[Fact]
	public void Nested_descriptor_lambdas_allocate_distinct_parameters()
	{
		var writer = Writer();
		writer.WriteFluentDescriptorCall("Query", static w =>
			w.WriteFluentDescriptorCall("Match", static w2 => w2.WriteFluentCall("Field", static w3 => w3.WriteString("message"))));
		var code = writer.ToString();

		Assert.Contains("d1 => d1", code);
		Assert.Contains("d2 => d2", code);
		Assert.DoesNotContain("d1 => d1\n    .Match(d1", code);
	}

	[Fact]
	public void Empty_variant_add_collapses_to_key_only_overload()
	{
		var writer = Writer();
		writer.WriteFluentVariantAdd("Keyword", static w => w.WriteString("email"), static w => { });
		Assert.Equal("\n.Keyword(\"email\")", writer.ToString());
	}

	// ---- Object initializers ----------------------------------------------

	[Fact]
	public void Property_less_initializer_renders_constructor_only()
	{
		var writer = Writer();
		var initializer = writer.BeginObjectInitializer("Elastic.Clients.Elasticsearch.CountRequest");
		initializer.Dispose();
		Assert.Equal("new()", writer.ToString());
	}

	[Fact]
	public void Initializer_with_property_renders_brace_block()
	{
		var writer = Writer();
		var initializer = writer.BeginObjectInitializer();
		initializer.Property("Size", 10);
		initializer.Dispose();
		Assert.Equal("new()\n{\n    Size = 10\n}", writer.ToString());
	}

	[Fact]
	public void Explicit_constructor_style_names_the_type()
	{
		var writer = Writer(new FormattingOptions
		{
			ConstructorStyle = ConstructorStyle.Explicit,
			TypeNameStyle = TypeNameStyle.GlobalFqn,
		});
		var initializer = writer.BeginObjectInitializer("Elastic.Clients.Elasticsearch.CountRequest");
		initializer.Dispose();
		Assert.Equal("new global::Elastic.Clients.Elasticsearch.CountRequest()", writer.ToString());
	}

	// ---- Mode scoping ------------------------------------------------------

	[Fact]
	public void ForceObjectInitializer_overrides_descriptor_mode_for_the_scope()
	{
		var writer = Writer(new FormattingOptions { SyntaxMode = SyntaxMode.Descriptor });
		Assert.Equal(SyntaxMode.Descriptor, writer.EffectiveSyntaxMode);

		using (writer.ForceObjectInitializer())
		{
			Assert.Equal(SyntaxMode.ObjectInitializer, writer.EffectiveSyntaxMode);
		}

		Assert.Equal(SyntaxMode.Descriptor, writer.EffectiveSyntaxMode);
	}

	// ---- Type name styles ---------------------------------------------------

	[Fact]
	public void Fqn_style_renders_without_global_prefix()
	{
		var writer = Writer(new FormattingOptions { TypeNameStyle = TypeNameStyle.Fqn });
		writer.WriteTypeRef("System.Text.Json.JsonElement");
		Assert.Equal("System.Text.Json.JsonElement", writer.ToString());
	}

	[Fact]
	public void ToString_is_repeatable()
	{
		var writer = Writer(new FormattingOptions { TypeNameStyle = TypeNameStyle.GlobalFqn });
		writer.WriteTypeRef("System.Text.Json.JsonElement");
		var first = writer.ToString();
		var second = writer.ToString();
		Assert.Equal(first, second);
	}

	[Fact]
	public void Force_next_explicit_constructor_applies_to_the_next_initializer_only()
	{
		var writer = new CodeWriter();
		writer.ForceNextExplicitConstructor();
		writer.BeginObjectInitializer("Elastic.Clients.Elasticsearch.SearchRequest").Dispose();
		writer.BeginObjectInitializer("Elastic.Clients.Elasticsearch.SearchRequest").Dispose();

		Assert.Equal("new SearchRequest()new()", writer.ToString());
	}

	[Fact]
	public void Inline_descriptor_arguments_write_args_then_lambda()
	{
		var writer = new CodeWriter();
		writer.Write("M(");
		writer.WriteInlineDescriptorArguments(
			w => w.Write("\"idx\""),
			w => w.WriteFluentCall("Refresh", null));
		writer.Write(")");

		Assert.StartsWith("M(\"idx\", ", writer.ToString(), StringComparison.Ordinal);
		Assert.Contains(" => ", writer.ToString(), StringComparison.Ordinal);
		Assert.Contains(".Refresh()", writer.ToString(), StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_arguments_drop_an_empty_chain_and_its_separator()
	{
		var writer = new CodeWriter();
		writer.Write("M(");
		writer.WriteInlineDescriptorArguments(w => w.Write("\"idx\""), _ => { });
		writer.Write(")");

		Assert.Equal("M(\"idx\")", writer.ToString());
	}

	[Fact]
	public void Inline_descriptor_arguments_handle_empty_args_and_chain()
	{
		var writer = new CodeWriter();
		writer.Write("M(");
		writer.WriteInlineDescriptorArguments(_ => { }, _ => { });
		writer.Write(")");

		Assert.Equal("M()", writer.ToString());
	}

	[Fact]
	public void Inline_argument_label_writes_nothing_outside_an_inline_call()
	{
		var writer = new CodeWriter();
		writer.WriteInlineArgumentLabel("index");
		writer.Write("\"idx\"");

		Assert.Equal("\"idx\"", writer.ToString());
	}

	[Fact]
	public void Inline_argument_label_writes_the_name_for_hoisted_arguments_only()
	{
		var writer = new CodeWriter();
		writer.Write("M(");
		writer.WriteInlineDescriptorArguments(
			w => w.WriteInlineArgumentLabel("index").Write("\"idx\""),
			w => w.WriteInlineArgumentLabel("chain").WriteFluentCall("Refresh", null));
		writer.Write(")");

		Assert.StartsWith("M(index: \"idx\", ", writer.ToString(), StringComparison.Ordinal);
		Assert.DoesNotContain("chain: ", writer.ToString(), StringComparison.Ordinal);
	}
}
