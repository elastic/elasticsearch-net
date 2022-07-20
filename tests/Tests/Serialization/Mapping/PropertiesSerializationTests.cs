// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Mapping;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization.Mapping;

[UsesVerify]
public class PropertiesSerializationTests : SerializerTestBase
{
	[U]
	public async Task CanSerialize_Properties_WithPropertyNameExpression()
	{
		var result = await RoundTripAndVerifyJson(new Properties<Project>
		{
			{ p => p.Name, new TextProperty { Boost = 1.2 } }
		});

		var textProperty = result["name"].Should().BeOfType<TextProperty>().Subject;
		textProperty.Boost.Should().Be(1.2);
	}

	[U]
	public async Task CanSerialize_MultipleProperties_WithPropertyNameExpression()
	{
		var result = await RoundTripAndVerifyJson(new Properties<Project>
		{
			{ p => p.Name, new TextProperty { Boost = 1.2 } },
			{ p => p.Description, new TextProperty { Boost = 1.4 } }
		});

		var nameTextProperty = result["name"].Should().BeOfType<TextProperty>().Subject;
		nameTextProperty.Boost.Should().Be(1.2);

		var descriptionTextProperty = result["description"].Should().BeOfType<TextProperty>().Subject;
		descriptionTextProperty.Boost.Should().Be(1.4);
	}
}
