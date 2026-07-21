// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using VerifyXunit;

namespace Tests.Serialization.Types;

[UsesVerify]
public class SortOptionsSerializationTests : SerializerTestBase
{
	[U]
	public void DeserializesSortCombinations_WithFieldSorts_AsSortOptions()
	{
		var json = @"{""sort"":[{""post_date"":{""order"":""asc"",""format"":""strict_date_optional_time_nanos""}},""user"",{""name"":""desc""},{""age"":""desc""},""_score""]}";

		var testClass = DeserializeJsonString<TestClass>(json);

		var sort = testClass.Sort[0];
		sort.VariantName.Should().Be("post_date");
		var fieldSortVariant = sort.Variant.Should().BeOfType<FieldSort>().Subject;
		fieldSortVariant.Format.Should().Be("strict_date_optional_time_nanos");
		fieldSortVariant.Missing.Should().BeNull();
		fieldSortVariant.Mode.Should().BeNull();
		fieldSortVariant.Nested.Should().BeNull();
		fieldSortVariant.NumericType.Should().BeNull();
		fieldSortVariant.Order.Should().Be(SortOrder.Asc);
		fieldSortVariant.UnmappedType.Should().BeNull();

		sort = testClass.Sort[1];
		sort.AdditionalPropertyName.Should().Be("user");
		fieldSortVariant = sort.Variant.Should().BeOfType<FieldSort>().Subject;
		fieldSortVariant.Format.Should().BeNull();
		fieldSortVariant.Missing.Should().BeNull();
		fieldSortVariant.Mode.Should().BeNull();
		fieldSortVariant.Nested.Should().BeNull();
		fieldSortVariant.NumericType.Should().BeNull();
		fieldSortVariant.Order.Should().BeNull();
		fieldSortVariant.UnmappedType.Should().BeNull();

		sort = testClass.Sort[2];
		sort.VariantName.Should().Be("name");
		fieldSortVariant = sort.Variant.Should().BeOfType<FieldSort>().Subject;
		fieldSortVariant.Format.Should().BeNull();
		fieldSortVariant.Missing.Should().BeNull();
		fieldSortVariant.Mode.Should().BeNull();
		fieldSortVariant.Nested.Should().BeNull();
		fieldSortVariant.NumericType.Should().BeNull();
		fieldSortVariant.Order.Should().Be(SortOrder.Desc);
		fieldSortVariant.UnmappedType.Should().BeNull();

		sort = testClass.Sort[3];
		sort.VariantName.Should().Be("age");
		fieldSortVariant = sort.Variant.Should().BeOfType<FieldSort>().Subject;
		fieldSortVariant.Format.Should().BeNull();
		fieldSortVariant.Missing.Should().BeNull();
		fieldSortVariant.Mode.Should().BeNull();
		fieldSortVariant.Nested.Should().BeNull();
		fieldSortVariant.NumericType.Should().BeNull();
		fieldSortVariant.Order.Should().Be(SortOrder.Desc);
		fieldSortVariant.UnmappedType.Should().BeNull();

		sort = testClass.Sort[4];
		sort.AdditionalPropertyName.Should().Be("_score");
		fieldSortVariant = sort.Variant.Should().BeOfType<FieldSort>().Subject;
		fieldSortVariant.Format.Should().BeNull();
		fieldSortVariant.Missing.Should().BeNull();
		fieldSortVariant.Mode.Should().BeNull();
		fieldSortVariant.Nested.Should().BeNull();
		fieldSortVariant.NumericType.Should().BeNull();
		fieldSortVariant.Order.Should().BeNull();
		fieldSortVariant.UnmappedType.Should().BeNull();
	}

	private class TestClass
	{
		public IList<SortOptions> Sort { get; set; }
	}
}
