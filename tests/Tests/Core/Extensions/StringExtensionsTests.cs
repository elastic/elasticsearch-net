// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Tests.Core.Extensions;

public class StringExtensionsTests
{
	[U]
	public void ToCamelCase()
	{
		"camelCase".ToCamelCase().Should().Be("camelCase");
		"CamelCase".ToCamelCase().Should().Be("camelCase");
		"CAMELCase".ToCamelCase().Should().Be("camelCase");
		"CamelCASE".ToCamelCase().Should().Be("camelCASE");
		"Camel Case".ToCamelCase().Should().Be("camel Case");
		"camel Case".ToCamelCase().Should().Be("camel Case");
	}
}
