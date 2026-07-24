// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using Xunit;

namespace RequestConverter.Tests;

public sealed class ClientCallTests
{
	private static readonly global::Elastic.Transport.Serializer Serializer =
		global::RequestConverter.RequestConverter.DefaultSerializer;

	private static ConversionResult Convert(string api, string body, FormattingOptions options) =>
		global::RequestConverter.RequestConverter.Convert(Serializer, api, null, null, body, options);

	[Fact]
	public void Appends_awaited_namespaced_call()
	{
		var options = new FormattingOptions { EmitClientCall = true };
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.StartsWith("EsqlQueryRequest request = ", result.Code, StringComparison.Ordinal);
		Assert.EndsWith("var response = await client.Esql.QueryAsync(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Appends_root_client_call()
	{
		var options = new FormattingOptions { EmitVariableDeclaration = true, EmitClientCall = true };
		var result = Convert("count", "{}", options);

		Assert.EndsWith("var response = await client.CountAsync(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Sync_style_omits_await_and_async_suffix()
	{
		var options = new FormattingOptions
		{
			ClientCallStyle = ClientCallStyle.Sync,
			EmitClientCall = true,
			EmitVariableDeclaration = true
		};
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.EndsWith("var response = client.Esql.Query(request);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Response_only_generic_arity_spells_json_element()
	{
		var options = new FormattingOptions { EmitVariableDeclaration = true, EmitClientCall = true };
		var result = Convert("search", """{"query":{"match_all":{}}}""", options);

		Assert.EndsWith("var response = await client.SearchAsync<JsonElement>(request);", result.Code, StringComparison.Ordinal);
		Assert.Contains("System.Text.Json", result.Namespaces);
	}

	[Fact]
	public void Typed_document_mode_spells_the_document_type()
	{
		var options = new FormattingOptions
		{
			EmitClientCall = true,
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
			EmitClientCall = true,
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

		Assert.Equal(new ClientCallInfo("Esql", "Query", 0), result.ClientCall);
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
			ClientVariableName = "es",
			EmitClientCall = true,
			EmitVariableDeclaration = true,
			ResponseVariableName = "esqlResponse",
			VariableName = "esqlRequest"
		};
		var result = Convert("esql.query", """{"query":"FROM library"}""", options);

		Assert.EndsWith("var esqlResponse = await es.Esql.QueryAsync(esqlRequest);", result.Code, StringComparison.Ordinal);
	}

	[Fact]
	public void Host_options_map_to_formatting_options()
	{
		var options = new Hosting.ConvertOptions { ClientCallStyle = "sync", EmitClientCall = true };
		var formatting = Hosting.ConvertOptionsMapper.BuildFormattingOptions(options, "request");

		Assert.True(formatting.EmitClientCall);
		Assert.Equal(ClientCallStyle.Sync, formatting.ClientCallStyle);
	}

	[Fact]
	public void Host_options_default_to_no_client_call()
	{
		var formatting = Hosting.ConvertOptionsMapper.BuildFormattingOptions(new Hosting.ConvertOptions(), "request");

		Assert.False(formatting.EmitClientCall);
		Assert.Equal(ClientCallStyle.Async, formatting.ClientCallStyle);
	}
}
