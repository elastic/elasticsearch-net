// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using System.Text.Json.Serialization;

namespace Tests.Document.Single.Update;

public class UpdateDocumentsCoordinatedTests : CoordinatedIntegrationTestBase<WritableCluster>
{
	private const string IndexDocumentStep = nameof(IndexDocumentStep);
	private const string GetAfterScriptedUpdateStep = nameof(GetAfterScriptedUpdateStep);
	private const string UpdateWithScriptStep = nameof(UpdateWithScriptStep);
	private const string UpdateWithPartialStep = nameof(UpdateWithPartialStep);
	private const string GetAfterPartialUpdateStep = nameof(GetAfterPartialUpdateStep);
	private const string UpdateWithPartialStepTwo = nameof(UpdateWithPartialStepTwo);
	private const string UpsertExistingStep = nameof(UpsertExistingStep);
	private const string GetAfterUpsertExistingStep = nameof(GetAfterUpsertExistingStep);
	private const string UpsertNewStep = nameof(UpsertNewStep);
	private const string GetAfterUpsertNewStep = nameof(GetAfterUpsertNewStep);

	private static Script TestScript => new(new InlineScript
	{
		Source = "ctx._source.counter += params.count",
		Language = ScriptLanguage.Painless,
		Params = new() { { "count", 4 } }
	});

	public UpdateDocumentsCoordinatedTests(WritableCluster cluster, EndpointUsage usage) : base(
		new CoordinatedUsage(cluster, usage)
		{
			{
				IndexDocumentStep, u =>
					u.Calls<IndexRequestDescriptor<UpdateTestDocument>, IndexRequest<UpdateTestDocument>, IndexResponse>(
						v => new IndexRequest<UpdateTestDocument>(UpdateTestDocument.InitialValues, v),
						(v, d) => d,
						(v, c, f) => c.Index(UpdateTestDocument.InitialValues, Infer.Index<UpdateTestDocument>(), r => r.Id(v)),
						(v, c, f) => c.IndexAsync(UpdateTestDocument.InitialValues, Infer.Index<UpdateTestDocument>(),r => r.Id(v)),
						(_, c, r) => c.Index(r),
						(_, c, r) => c.IndexAsync(r)
					)
			},
			{
				UpdateWithScriptStep, u =>
					u.Calls<UpdateRequestDescriptor<UpdateTestDocument, object>, UpdateRequest<UpdateTestDocument, object>, UpdateResponse<UpdateTestDocument>>(
						v => new UpdateRequest<UpdateTestDocument, object>(typeof(UpdateTestDocument), v) { Script = TestScript },
						(v, d) => d.Script(TestScript),
						(v, c, f) => c.Update(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.UpdateAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Update(r),
						(_, c, r) => c.UpdateAsync(r)
					)
			},
			{
				GetAfterScriptedUpdateStep, u =>
					u.Calls<GetRequestDescriptor<UpdateTestDocument>, GetRequest, GetResponse<UpdateTestDocument>>(
						v => new GetRequest(typeof(UpdateTestDocument), v),
						(v, d) => d,
						(v, c, f) => c.Get(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.GetAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Get<UpdateTestDocument>(r),
						(_, c, r) => c.GetAsync<UpdateTestDocument>(r)
					)
			},
			{
				UpdateWithPartialStep, u =>
					u.Calls<UpdateRequestDescriptor<UpdateTestDocument, UpdateTestDocumentPartial>, UpdateRequest<UpdateTestDocument, UpdateTestDocumentPartial>, UpdateResponse<UpdateTestDocument>>(
						v => new UpdateRequest<UpdateTestDocument, UpdateTestDocumentPartial>(typeof(UpdateTestDocument), v) { Doc = new UpdateTestDocumentPartial() },
						(v, d) => d.Doc(new UpdateTestDocumentPartial()),
						(v, c, f) => c.Update(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.UpdateAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Update(r),
						(_, c, r) => c.UpdateAsync(r)
					)
			},
			{
				GetAfterPartialUpdateStep, u =>
					u.Calls<GetRequestDescriptor<UpdateTestDocument>, GetRequest, GetResponse<UpdateTestDocument>>(
						v => new GetRequest(typeof(UpdateTestDocument), v),
						(v, d) => d,
						(v, c, f) => c.Get(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.GetAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Get<UpdateTestDocument>(r),
						(_, c, r) => c.GetAsync<UpdateTestDocument>(r)
					)
			},
			{
				UpdateWithPartialStepTwo, u =>
					u.Calls<UpdateRequestDescriptor<UpdateTestDocument, UpdateTestDocumentPartial>, UpdateRequest<UpdateTestDocument, UpdateTestDocumentPartial>, UpdateResponse<UpdateTestDocument>>(
						v => new UpdateRequest<UpdateTestDocument, UpdateTestDocumentPartial>(typeof(UpdateTestDocument), v) { Doc = new UpdateTestDocumentPartial() },
						(v, d) => d.Doc(new UpdateTestDocumentPartial()),
						(v, c, f) => c.Update(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.UpdateAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Update(r),
						(_, c, r) => c.UpdateAsync(r)
					)
			},
			{
				UpsertExistingStep, u =>
					u.Calls<UpdateRequestDescriptor<UpdateTestDocument, object>, UpdateRequest<UpdateTestDocument, object>, UpdateResponse<UpdateTestDocument>>(
						v => new UpdateRequest<UpdateTestDocument, object>(typeof(UpdateTestDocument), v) { Script = TestScript, Upsert = new UpdateTestDocument { Counter = 100, Name = "Newly inserted" } },
						(v, d) => d.Script(TestScript).Upsert(new UpdateTestDocument { Counter = 100, Name = "Newly inserted" }),
						(v, c, f) => c.Update(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.UpdateAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Update(r),
						(_, c, r) => c.UpdateAsync(r)
					)
			},
			{
				GetAfterUpsertExistingStep, u =>
					u.Calls<GetRequestDescriptor<UpdateTestDocument>, GetRequest, GetResponse<UpdateTestDocument>>(
						v => new GetRequest(typeof(UpdateTestDocument), v),
						(v, d) => d,
						(v, c, f) => c.Get(Infer.Index<UpdateTestDocument>(), v, f),
						(v, c, f) => c.GetAsync(Infer.Index<UpdateTestDocument>(), v, f),
						(_, c, r) => c.Get<UpdateTestDocument>(r),
						(_, c, r) => c.GetAsync<UpdateTestDocument>(r)
					)
			},
			{
				UpsertNewStep, u =>
					u.Calls<UpdateRequestDescriptor<UpdateTestDocument, object>, UpdateRequest<UpdateTestDocument, object>, UpdateResponse<UpdateTestDocument>>(
						v => new UpdateRequest<UpdateTestDocument, object>(typeof(UpdateTestDocument), $"{v}-new") { Script = TestScript, Upsert = new UpdateTestDocument { Counter = 100, Name = "Newly inserted" } },
						(v, d) => d.Script(TestScript).Upsert(new UpdateTestDocument { Counter = 100, Name = "Newly inserted" }),
						(v, c, f) => c.Update(Infer.Index<UpdateTestDocument>(), $"{v}-new", f),
						(v, c, f) => c.UpdateAsync(Infer.Index<UpdateTestDocument>(), $"{v}-new", f),
						(_, c, r) => c.Update(r),
						(_, c, r) => c.UpdateAsync(r)
					)
			},
			{
				GetAfterUpsertNewStep, u =>
					u.Calls<GetRequestDescriptor<UpdateTestDocument>, GetRequest, GetResponse<UpdateTestDocument>>(
						v => new GetRequest(typeof(UpdateTestDocument), $"{v}-new"),
						(v, d) => d,
						(v, c, f) => c.Get(Infer.Index<UpdateTestDocument>(), $"{v}-new", f),
						(v, c, f) => c.GetAsync(Infer.Index<UpdateTestDocument>(), $"{v}-new", f),
						(_, c, r) => c.Get<UpdateTestDocument>(r),
						(_, c, r) => c.GetAsync<UpdateTestDocument>(r)
					)
			},
		})
	{
	}

	[I]
	public async Task UpdateWithScriptResponse() => await Assert<UpdateResponse<UpdateTestDocument>>(UpdateWithScriptStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Result.Should().Be(Result.Updated);
	});

	[I]
	public async Task GetResponseAfterScriptedUpdateStep() => await Assert<GetResponse<UpdateTestDocument>>(GetAfterScriptedUpdateStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Source.Name.Should().Be("Initial"); // The name should not have changed
		r.Source.Counter.Should().Be(5); // The script updates by 4
		r.Source.RenamedField.Should().BeNull(); // This hasn't been set yet and should not exist on the source
	});

	[I]
	public async Task UpdateWithPartialResponse() => await Assert<UpdateResponse<UpdateTestDocument>>(UpdateWithPartialStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Result.Should().Be(Result.Updated);
	});

	[I]
	public async Task GetResponseAfterPartialUpdateStep() => await Assert<GetResponse<UpdateTestDocument>>(GetAfterPartialUpdateStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Source.Name.Should().Be("Initial"); // The name should not have changed
		r.Source.Counter.Should().Be(5); // The count should still be five
		r.Source.RenamedField.Should().Be("Partial"); // The partial update should have added the partial value for RenamedField
	});

	[I]
	public async Task UpdateWithPartialNoOpResponse() => await Assert<UpdateResponse<UpdateTestDocument>>(UpdateWithPartialStepTwo, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Result.Should().Be(Result.NoOp);
	});

	[I]
	public async Task UpsertExistingResponse() => await Assert<UpdateResponse<UpdateTestDocument>>(UpsertExistingStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Result.Should().Be(Result.Updated);
	});

	[I]
	public async Task GetResponseAfterUpsertExistingStep() => await Assert<GetResponse<UpdateTestDocument>>(GetAfterUpsertExistingStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Source.Name.Should().Be("Initial"); // The name should not have changed
		r.Source.Counter.Should().Be(9); // The count should have been updated by the script
		r.Source.RenamedField.Should().Be("Partial"); // The RenamedField should not have changed
	});

	[I]
	public async Task UpsertNewResponse() => await Assert<UpdateResponse<UpdateTestDocument>>(UpsertNewStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Result.Should().Be(Result.Created);
	});

	[I]
	public async Task GetResponseAfterUpsertNewStep() => await Assert<GetResponse<UpdateTestDocument>>(GetAfterUpsertNewStep, (v, r) =>
	{
		r.IsValid.Should().BeTrue();
		r.Source.Name.Should().Be("Newly inserted");
		r.Source.Counter.Should().Be(100);
		r.Source.RenamedField.Should().BeNull();
	});

	private class UpdateTestDocument
	{
		[JsonPropertyName("another_field")]
		public string RenamedField { get; set; }
		public int Counter { get; set; }
		public string Name { get; set; }

		public static UpdateTestDocument InitialValues => new() { Counter = 1, Name = "Initial" };
	}

	private class UpdateTestDocumentPartial
	{
		[JsonPropertyName("another_field")]
		public string RenamedField { get; set; } = "Partial";
	}
}
