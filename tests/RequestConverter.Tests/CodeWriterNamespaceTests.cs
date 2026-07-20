// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace RequestConverter.Tests;

public class CodeWriterNamespaceTests
{
	[Fact]
	public void Unknown_namespace_disables_shortening_for_the_whole_snippet()
	{
		var writer = new global::RequestConverter.CodeWriter();
		writer.WriteTypeRef("Some.Unknown.Namespace.Foo");
		writer.Write(" ");
		writer.WriteTypeRef("System.Text.Json.JsonElement");

		// Membership of the unknown namespace is unknowable, so no name may shorten: any short name
		// could collide with a type in it once its using directive is added.
		Assert.Equal("global::Some.Unknown.Namespace.Foo global::System.Text.Json.JsonElement", writer.ToString());
	}

	[Fact]
	public void Known_unique_name_shortens()
	{
		var writer = new global::RequestConverter.CodeWriter();
		writer.WriteTypeRef("System.Text.Json.JsonElement");
		Assert.Equal("JsonElement", writer.ToString());
	}
}
