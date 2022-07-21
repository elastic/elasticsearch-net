// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Domain;
using VerifyXunit;

namespace Tests.Serialization;

[UsesVerify]
public class SuggesterSerializationTests : SerializerTestBase
{
	[U]
	public async Task Suggester_ObjectInitializer_SerializesCorrectly()
	{
		var suggester = new Suggester
		{
			Text = "Trying out Elasticsearch",
			Suggesters = new System.Collections.Generic.Dictionary<string, FieldSuggester>
			{
				{ "my-suggester-1", FieldSuggester.Term(new TermSuggester{ Field = Infer.Field<Project>(f => f.Description) }) },
				{ "my-suggester-2", FieldSuggester.Term(new TermSuggester{ Field = Infer.Field<Project>(f => f.LeadDeveloper) }) }
			}
		};

		var result = await RoundTripAndVerifyJson(suggester);

		result.Text.Should().Be(suggester.Text);
		result.Suggesters.Should().HaveCount(2);
		var suggester1 = result.Suggesters["my-suggester-1"].Variant.Should().BeOfType<TermSuggester>().Subject;
		suggester1.Field.Name.Should().Be("description");
		var suggester2 = result.Suggesters["my-suggester-2"].Variant.Should().BeOfType<TermSuggester>().Subject;
		suggester2.Field.Name.Should().Be("leadDeveloper");
	}

	[U]
	public async Task Suggester_Descriptor_SerializesCorrectly()
	{
		// TODO: In a future release, we should ideally support descriptors for values in the fluent dictionary

		var suggester = new SuggesterDescriptor()
			.Text("Trying out Elasticsearch")
			.Suggesters(s => s
				.Add("my-suggester-1", FieldSuggester.Term(new TermSuggester { Field = Infer.Field<Project>(f => f.Description) }))
				.Add("my-suggester-2", FieldSuggester.Term(new TermSuggester { Field = Infer.Field<Project>(f => f.LeadDeveloper) })));

		await SerializeAndVerifyJson(suggester);
	}
}
