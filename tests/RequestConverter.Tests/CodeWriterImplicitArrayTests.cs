// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;

using Xunit;

namespace RequestConverter.Tests;

public class CodeWriterImplicitArrayTests
{
	private static global::RequestConverter.CodeWriter CreateWriter() =>
		new(new global::RequestConverter.FormattingOptions { TypeNameStyle = global::RequestConverter.TypeNameStyle.GlobalFqn });

	[Fact]
	public void Empty_collection_reached_via_runtime_dispatch_uses_runtime_element_type()
	{
		var writer = CreateWriter();
		writer.WriteValue((object)new List<Guid>());
		Assert.Equal("global::System.Array.Empty<global::System.Guid>()", writer.ToString());
	}

	[Fact]
	public void Empty_collection_defaults_to_string_element_type()
	{
		var writer = CreateWriter();
		writer.WriteImplicitArray(Array.Empty<string>(), static (w, item) => w.WriteString(item));
		Assert.Equal("global::System.Array.Empty<string>()", writer.ToString());
	}

	[Fact]
	public void Non_empty_collection_renders_array_literal_unchanged()
	{
		var writer = CreateWriter();
		writer.WriteValue((object)new List<int> { 1, 2 });
		Assert.Equal("new[] { 1, 2 }", writer.ToString());
	}
}
