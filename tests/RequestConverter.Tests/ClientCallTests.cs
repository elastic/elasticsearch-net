// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Xunit;

namespace RequestConverter.Tests;

public sealed class ClientCallTests
{
	private static readonly global::Elastic.Transport.Serializer Serializer =
		global::RequestConverter.RequestConverter.DefaultSerializer;

	private static ConversionResult Convert(string api, string body, FormattingOptions options,
		IReadOnlyDictionary<string, string>? pathParameters = null) =>
		global::RequestConverter.RequestConverter.Convert(Serializer, api, pathParameters, null, body, options);

	[Fact]
	public void Appends_awaited_namespaced_call()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Statement };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.StartsWith("EsqlQueryRequest request = ", result.Code, StringComparison.Ordinal);
		Assert.EndsWith("var response = await client.Esql.QueryAsync(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Appends_root_client_call()
	{
		var options = new FormattingOptions { EmitVariableDeclaration = true, ClientCallFormat = ClientCallFormat.Statement };
		var result = Convert("count", "{}", options);

		Assert.EndsWith("var response = await client.CountAsync(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Sync_style_omits_await_and_async_suffix()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Statement,
			ClientCallStyle = ClientCallStyle.Sync,
			EmitVariableDeclaration = true
		};
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.EndsWith("var response = client.Esql.Query(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Response_only_generic_arity_spells_json_element()
	{
		var options = new FormattingOptions { EmitVariableDeclaration = true, ClientCallFormat = ClientCallFormat.Statement };
		var result = Convert("search", """{"query":{"match_all":{}}}""", options);

		Assert.EndsWith("var response = await client.SearchAsync<JsonElement>(request);", result.Code, StringComparison.Ordinal);
		Assert.Contains("System.Text.Json", result.Namespaces);
	}

	[Fact]
	public void Typed_document_mode_spells_the_document_type()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Statement,
			EmitVariableDeclaration = true,
			UseStronglyTypedDocument = true
		};
		var result = Convert("search", """{"query":{"match_all":{}}}""", options);

		Assert.EndsWith("var response = await client.SearchAsync<MyDocument>(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Descriptor_mode_appends_the_same_call()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Statement,
			EmitVariableDeclaration = true,
			SyntaxMode = SyntaxMode.Descriptor
		};
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.EndsWith("var response = await client.Esql.QueryAsync(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Client_call_info_is_reported_without_opt_in()
	{
		var result = Convert("esql.query", """{"query":"FROM library"}""", new FormattingOptions());

		Assert.Equal(new ClientCallInfo("Esql", "Query", 0, 0), result.ClientCall);
	}

	[Fact]
	public void Default_options_do_not_emit_a_client_call()
	{
		var result = Convert("esql.query", """{"query":"FROM library"}""", new FormattingOptions { EmitVariableDeclaration = true });

		Assert.DoesNotContain("client.", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Custom_variable_names_are_used()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Statement,
			ClientVariableName = "es",
			EmitVariableDeclaration = true,
			ResponseVariableName = "esqlResponse",
			VariableName = "esqlRequest"
		};
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.EndsWith("var esqlResponse = await es.Esql.QueryAsync(esqlRequest);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Host_options_suffix_the_response_variable()
	{
		var formatting = Hosting.ConvertOptionsMapper.BuildFormattingOptions(new Hosting.ConvertOptions(), "request1", "response1");

		Assert.Equal("request1", formatting.VariableName);
		Assert.Equal("response1", formatting.ResponseVariableName);
	}

	[Fact]
	public void Host_options_map_to_formatting_options()
	{
		var options = new Hosting.ConvertOptions { ClientCallStyle = "sync", ClientCallFormat = "statement" };
		var formatting = Hosting.ConvertOptionsMapper.BuildFormattingOptions(options, "request");

		Assert.Equal(ClientCallFormat.Statement, formatting.ClientCallFormat);
		Assert.Equal(ClientCallStyle.Sync, formatting.ClientCallStyle);
	}

	[Fact]
	public void Host_options_default_to_no_client_call()
	{
		var formatting = Hosting.ConvertOptionsMapper.BuildFormattingOptions(new Hosting.ConvertOptions(), "request");

		Assert.Equal(ClientCallFormat.None, formatting.ClientCallFormat);
		Assert.Equal(ClientCallStyle.Async, formatting.ClientCallStyle);
	}

	[Fact]
	public void Inline_format_inlines_the_request_argument()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.StartsWith("var response = await client.Esql\n    .QueryAsync(new EsqlQueryRequest()", result.Code, StringComparison.Ordinal);
		Assert.EndsWith("});", result.Code, StringComparison.Ordinal);
		Assert.DoesNotContain("request =", result.Code, StringComparison.Ordinal);
		Assert.Contains("Elastic.Clients.Elasticsearch.Esql", result.Namespaces);
	}

	[Fact]
	public void Inline_format_spells_response_generics()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline };
		var result = Convert("search", """{"query":{"match_all":{}}}""", options);

		Assert.StartsWith("var response = await client\n    .SearchAsync<JsonElement>(new SearchRequest()", result.Code, StringComparison.Ordinal);
		Assert.Contains("System.Text.Json", result.Namespaces);
	}

	[Fact]
	public void Inline_sync_style_omits_await()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, ClientCallStyle = ClientCallStyle.Sync };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.StartsWith("var response = client.Esql\n    .Query(new EsqlQueryRequest()", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_format_ignores_variable_declaration()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, EmitVariableDeclaration = true };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.StartsWith("var response = ", result.Code, StringComparison.Ordinal);
		Assert.DoesNotContain("EsqlQueryRequest request", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_format_names_the_bulk_request_type()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline };
		var result = Convert("bulk", "{\"index\":{\"_id\":\"1\"}}\n{\"field\":1}\n", options);

		Assert.StartsWith("var response = await client\n    .BulkAsync(new BulkRequest", result.Code, StringComparison.Ordinal);
		Assert.Contains("Elastic.Clients.Elasticsearch", result.Namespaces);
	}

	[Fact]
	public void Inline_descriptor_emits_a_configuration_lambda()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		// Spelled with explicit "\n" (the FormattingOptions default) so the expectation does not depend on the
		// checkout's line endings. The non-empty chain wraps the method onto its own line and closes ");" on its own.
		Assert.Equal(
			"var response = await client.Esql\n"
			+ "    .QueryAsync(d1 => d1\n"
			+ "        .Query(\"FROM library\")\n"
			+ "    );",
			result.Code);
	}

	[Fact]
	public void Inline_descriptor_hoists_required_path_arguments()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("indices.create", """{"settings":{"number_of_shards":1}}""", options,
			new Dictionary<string, string> { ["index"] = "my-index" });

		Assert.StartsWith("var response = await client.Indices\n    .CreateAsync(index: \"my-index\", d1 => d1\n", result.Code, StringComparison.Ordinal);
		Assert.Contains(".Settings(", result.Code, StringComparison.Ordinal);
		Assert.EndsWith("\n    );", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_drops_the_lambda_for_an_empty_chain()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("indices.create", "{}", options, new Dictionary<string, string> { ["index"] = "my-index" });

		Assert.Equal("var response = await client.Indices.CreateAsync(index: \"my-index\");", result.Code);
	}

	[Fact]
	public void Inline_descriptor_uses_the_actionless_overload_when_everything_is_empty()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("search", "{}", options);

		Assert.Equal("var response = await client.SearchAsync<JsonElement>();", result.Code);
	}

	[Fact]
	public void Inline_descriptor_falls_back_to_the_request_form_for_bulk()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("bulk", "{\"index\":{\"_id\":\"1\"}}\n{\"field\":1}\n", options);

		Assert.StartsWith("var response = await client\n    .BulkAsync(new BulkRequest", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_labels_hoisted_arguments()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("index", """{"name":"book"}""", options,
			new Dictionary<string, string> { ["index"] = "books" });

		// Spelled with explicit "\n" (the FormattingOptions default) so the expectation does not depend on the
		// checkout's line endings.
		Assert.Equal(
			"var response = await client.IndexAsync<JsonElement>(document: JsonSerializer.Deserialize<JsonElement>(\"\"\"\n"
			+ "{\n"
			+ "  \"name\": \"book\"\n"
			+ "}\n"
			+ "\"\"\"), index: \"books\", id: null);",
			result.Code);
	}

	[Fact]
	public void Inline_descriptor_spells_the_descriptor_overload_generics()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("eql.search", """{"query":"any where true"}""", options,
			new Dictionary<string, string> { ["index"] = "books" });

		// Every descriptor-action overload is SearchAsync<TDocument, TEvent>, so both arguments are spelled even
		// though the request overload leaves only TEvent open.
		Assert.StartsWith("var response = await client.Eql\n    .SearchAsync<JsonElement, JsonElement>(", result.Code, StringComparison.Ordinal);
		Assert.Contains("indices: new[] { \"books\" }", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_spells_generics_the_request_overload_infers()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("update", """{"doc":{"name":"book"}}""", options,
			new Dictionary<string, string> { ["index"] = "books", ["id"] = "1" });

		Assert.StartsWith("var response = await client\n    .UpdateAsync<JsonElement, JsonElement>(index: \"books\", id: \"1\", ", result.Code, StringComparison.Ordinal);
		Assert.Contains(" => ", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_typed_document_mode_spells_the_descriptor_overload_generics()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Inline,
			SyntaxMode = SyntaxMode.Descriptor,
			UseStronglyTypedDocument = true
		};
		var result = Convert("eql.search", """{"query":"any where true"}""", options,
			new Dictionary<string, string> { ["index"] = "books" });

		Assert.StartsWith("var response = await client.Eql\n    .SearchAsync<MyDocument, MyDocument>(", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_descriptor_falls_back_without_a_matching_descriptor_overload()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.Descriptor };
		var result = Convert("put_script", """{"script":{"lang":"painless","source":"ctx._source.counter += 1"}}""", options,
			new Dictionary<string, string> { ["id"] = "my-script" });

		Assert.StartsWith("var response = await client\n    .PutScriptAsync(new PutScriptRequest", result.Code, StringComparison.Ordinal);
		Assert.DoesNotContain(" => ", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Inline_request_form_closes_the_generic_request_type()
	{
		var options = new FormattingOptions { ClientCallFormat = ClientCallFormat.Inline, SyntaxMode = SyntaxMode.ObjectInitializer };
		var result = Convert("index", """{"name":"book"}""", options,
			new Dictionary<string, string> { ["index"] = "books" });

		Assert.Contains("new IndexRequest<JsonElement>", result.Code, StringComparison.Ordinal);
		Assert.Contains("System.Text.Json", result.Namespaces);
	}

	[Fact]
	public void Inline_descriptor_typed_document_mode_spells_the_document_type()
	{
		var options = new FormattingOptions
		{
			ClientCallFormat = ClientCallFormat.Inline,
			SyntaxMode = SyntaxMode.Descriptor,
			UseStronglyTypedDocument = true
		};
		var result = Convert("search", """{"query":{"match_all":{}}}""", options);

		Assert.StartsWith("var response = await client\n    .SearchAsync<MyDocument>(", result.Code, StringComparison.Ordinal);
	}
}
