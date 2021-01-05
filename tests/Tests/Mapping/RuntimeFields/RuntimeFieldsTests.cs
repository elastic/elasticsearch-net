// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.RuntimeFields
{
	[SkipVersion("<7.11.0", "Runtime fields introduced in 7.11.0")]
	public class RuntimeFieldsTests : CoordinatedIntegrationTestBase<WritableCluster>
	{
		private const string ScriptValue = "emit(doc['lastActivity'].value.dayOfWeekEnum.getDisplayName(TextStyle.FULL, Locale.ROOT))";
		private const string DateFormat = "yyyy-MM-dd";
		private const string RuntimeFieldNameOne = "runtimeFieldOne";
		private const string RuntimeFieldNameTwo = "runtimeFieldTwo";

		private const string CreateIndexWithMappingStep = nameof(CreateIndexWithMappingStep);
		private const string GetCreatedIndexMappingStep = nameof(GetCreatedIndexMappingStep);
		private const string DeleteIndexStep = nameof(DeleteIndexStep);
		private const string CreateIndexWithoutMappingStep = nameof(CreateIndexWithoutMappingStep);
		private const string CreateMappingStep = nameof(CreateMappingStep);
		private const string GetMappingStep = nameof(GetMappingStep);

		public RuntimeFieldsTests(WritableCluster cluster, EndpointUsage usage) : base(new CoordinatedUsage(cluster, usage)
		{
			{
				CreateIndexWithMappingStep, u =>
					u.Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
						v => new CreateIndexRequest(IndexName(v))
						{
							Mappings = new TypeMapping
							{
								RuntimeFields = new Nest.RuntimeFields
								{
									{RuntimeFieldNameOne, new RuntimeField
									{
										Type = FieldType.Keyword,
										Script = new PainlessScript(ScriptValue)
									}},
									{RuntimeFieldNameTwo, new RuntimeField
									{
										Type = FieldType.Date,
										Format = DateFormat
									}}
								}
							}
						},
						(v, d) => d.Index(IndexName(v)).Map(mapping => mapping
							.RuntimeFields(rtf => rtf
								.RuntimeField(RuntimeFieldNameOne, FieldType.Keyword, f1 => f1
									.Script(ScriptValue))
								.RuntimeField(RuntimeFieldNameTwo, FieldType.Date, f2 => f2.Format(DateFormat)))),
						(v, c, f) => c.Indices.Create(IndexName(v), f),
						(v, c, f) => c.Indices.CreateAsync(IndexName(v), f),
						(v, c, r) => c.Indices.Create(r),
						(v, c, r) => c.Indices.CreateAsync(r)
					)
			},
			{
				GetCreatedIndexMappingStep, u =>
					u.Calls<GetMappingDescriptor<Project>, GetMappingRequest, IGetMappingRequest, GetMappingResponse>(
						v => new GetMappingRequest(IndexName(v)),
						(v, d) => d.Index(IndexName(v)),
						(v, c, f) => c.Indices.GetMapping(f),
						(v, c, f) => c.Indices.GetMappingAsync(f),
						(v, c, r) => c.Indices.GetMapping(r),
						(v, c, r) => c.Indices.GetMappingAsync(r)
					)
			},
			{
				DeleteIndexStep, u =>
					u.Calls<DeleteIndexDescriptor, DeleteIndexRequest, IDeleteIndexRequest, DeleteIndexResponse>(
						v => new DeleteIndexRequest(IndexName(v)),
						(v, d) => d,
						(v, c, f) => c.Indices.Delete(IndexName(v), f),
						(v, c, f) => c.Indices.DeleteAsync(IndexName(v), f),
						(v, c, r) => c.Indices.Delete(r),
						(v, c, r) => c.Indices.DeleteAsync(r)
					)
			},
			{
				CreateIndexWithoutMappingStep, u =>
					u.Calls<CreateIndexDescriptor, CreateIndexRequest, ICreateIndexRequest, CreateIndexResponse>(
						v => new CreateIndexRequest(IndexName(v)),
						(v, d) => d,
						(v, c, f) => c.Indices.Create(IndexName(v), f),
						(v, c, f) => c.Indices.CreateAsync(IndexName(v), f),
						(v, c, r) => c.Indices.Create(r),
						(v, c, r) => c.Indices.CreateAsync(r)
					)
			},
			{
				CreateMappingStep, u =>
					u.Calls<PutMappingDescriptor<Project>, PutMappingRequest, IPutMappingRequest, PutMappingResponse>(
						v => new PutMappingRequest(IndexName(v))
						{
							RuntimeFields = new Nest.RuntimeFields
							{
								{RuntimeFieldNameOne, new RuntimeField
								{
									Type = FieldType.Keyword,
									Script = new PainlessScript(ScriptValue)
								}},
								{RuntimeFieldNameTwo, new RuntimeField
								{
									Type = FieldType.Date,
									Format = DateFormat
								}}
							}
						},
						(v, d) => d.Index(IndexName(v))
							.RuntimeFields(rtf => rtf
								.RuntimeField(RuntimeFieldNameOne, FieldType.Keyword, f1 => f1
									.Script(ScriptValue))
								.RuntimeField(RuntimeFieldNameTwo, FieldType.Date, f2 => f2.Format(DateFormat))),
						(v, c, f) => c.Indices.PutMapping(f),
						(v, c, f) => c.Indices.PutMappingAsync(f),
						(v, c, r) => c.Indices.PutMapping(r),
						(v, c, r) => c.Indices.PutMappingAsync(r)
					)
			},
			{
				GetMappingStep, u =>
					u.Calls<GetMappingDescriptor<Project>, GetMappingRequest, IGetMappingRequest, GetMappingResponse>(
						v => new GetMappingRequest(IndexName(v)),
						(v, d) => d.Index(IndexName(v)),
						(v, c, f) => c.Indices.GetMapping(f),
						(v, c, f) => c.Indices.GetMappingAsync(f),
						(v, c, r) => c.Indices.GetMapping(r),
						(v, c, r) => c.Indices.GetMappingAsync(r)
					)
			}
		})
		{ }

		private static string IndexName(string uniqueId) => $"runtime-{uniqueId}";

		[I] public async Task CreateIndexWithRuntimeFieldsMapping() => await Assert<CreateIndexResponse>(CreateIndexWithMappingStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task GetIndexMappingWithRuntimeFields() => await Assert<GetMappingResponse>(GetCreatedIndexMappingStep, (v, r) => AssertRuntimeFields(r));

		[I] public async Task PutMappingWithRuntimeFields() => await Assert<PutMappingResponse>(CreateMappingStep, (v, r) =>
		{
			r.ShouldBeValid();
			r.Acknowledged.Should().BeTrue();
		});

		[I] public async Task GetMappingWithRuntimeFields() => await Assert<GetMappingResponse>(GetMappingStep, (v, r) => AssertRuntimeFields(r));

		private static void AssertRuntimeFields(GetMappingResponse response)
		{
			response.ShouldBeValid();
			var runtimeFields = response.Indices.First().Value.Mappings.RuntimeFields;

			runtimeFields.Count.Should().Be(2);
			runtimeFields.TryGetValue(RuntimeFieldNameOne, out var fieldOne).Should().BeTrue();
			runtimeFields.TryGetValue(RuntimeFieldNameTwo, out var fieldTwo).Should().BeTrue();

			fieldOne!.Type.Should().Be(FieldType.Keyword);
			fieldOne.Script.Lang.Should().Be("painless");
			fieldOne.Script.Source.Should().Be(ScriptValue);

			fieldTwo!.Type.Should().Be(FieldType.Date);
			fieldTwo.Format.Should().Be(DateFormat);
		}
	}
}
