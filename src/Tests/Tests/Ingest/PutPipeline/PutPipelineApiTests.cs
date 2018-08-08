using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;

namespace Tests.Ingest.PutPipeline
{
	[SkipVersion("<6.1.0", "uses url decode which was introduced in 6.1.0")]
	public class PutPipelineApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPutPipelineResponse, IPutPipelineRequest, PutPipelineDescriptor, PutPipelineRequest>
	{
		public PutPipelineApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _id = "pipeline-1";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.PutPipeline(_id, f),
			fluentAsync: (client, f) => client.PutPipelineAsync(_id, f),
			request: (client, r) => client.PutPipeline(r),
			requestAsync: (client, r) => client.PutPipelineAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_ingest/pipeline/{_id}";
		protected override bool SupportsDeserialization => false;
		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			description = "My test pipeline",
			processors = new object[]
			{
				new
				{
					append = new
					{
						field = "state",
						value = new [] { "Stable", "VeryActive" }
					}
				},
				new
				{
					convert = new
					{
						field = "numberOfCommits",
						target_field = "targetField",
						type = "string"
					}
				},
				new
				{
					date = new
					{
						field = "startedOn",
						target_field = "timestamp",
						formats = new [] { "dd/MM/yyyy hh:mm:ss" },
						timezone = "Europe/Amsterdam"
					}
				},
				new
				{
					fail = new
					{
						message = "an error message"
					}
				},
				new
				{
					@foreach = new
					{
						field = "tags",
						processor = new
						{
							uppercase = new
							{
								field = "_value.name"
							}
						}
					}
				},
				new
				{
					grok = new
					{
						field = "description",
						patterns = new[] { "my %{FAVORITE_DOG:dog} is colored %{RGB:color}" },
						pattern_definitions = new Dictionary<string, string>
						{
							{ "FAVORITE_DOG", "border collie" },
							{ "RGB", "RED|BLUE|GREEN" },
						}
					}
				},
				new
				{
					gsub = new
					{
						field = "name",
						pattern = "-",
						replacement = "_"
					}
				},
				new
				{
					join = new
					{
						field = "branches",
						separator = ","
					}
				},
				new
				{
					lowercase = new
					{
						field = "name"
					}
				},
				new
				{
					remove = new
					{
						field = "suggest"
					}
				},
				new
				{
					rename = new
					{
						field = "leadDeveloper",
						target_field = "projectLead"
					}
				},
				new
				{
					set = new
					{
						field = "name",
						value = "foo"
					}
				},
				new
				{
					split = new
					{
						field = "description",
						separator = "."
					}
				},
				new
				{
					trim = new
					{
						field = "name"
					}
				},
				new
				{
					uppercase = new
					{
						field = "name"
					}
				},
				new
				{
					dot_expander = new
					{
						field = "field.withDots"
					}
				},
				new
				{
					script = new
					{
						source = "ctx.numberOfCommits++"
					}
				},
				new
				{
					urldecode = new
					{
						field = "description",
						ignore_missing = true
					}
				}
			}
		};

		protected override PutPipelineDescriptor NewDescriptor() => new PutPipelineDescriptor(_id);

		protected override Func<PutPipelineDescriptor, IPutPipelineRequest> Fluent => d => d
			.Description("My test pipeline")
			.Processors(ps => ps
				.Append<Project>(a => a
					.Field(p => p.State)
					.Value(StateOfBeing.Stable, StateOfBeing.VeryActive)
				)
				.Convert<Project>(c => c
					.Field(p => p.NumberOfCommits)
					.TargetField("targetField")
					.Type(ConvertProcessorType.String)
				)
				.Date<Project>(dt => dt
					.Field(p => p.StartedOn)
					.TargetField("timestamp")
					.Formats("dd/MM/yyyy hh:mm:ss")
					.Timezone("Europe/Amsterdam")
				)
				.Fail(f => f
					.Message("an error message")
				)
				.Foreach<Project>(fe => fe
					.Field(p => p.Tags)
					.Processor(pps => pps
						.Uppercase<Tag>(uc => uc
							.Field("_value.name")
						)
					)
				)
				.Grok<Project>(gk => gk
					.Field(p => p.Description)
					.Patterns("my %{FAVORITE_DOG:dog} is colored %{RGB:color}")
					.PatternDefinitions(pds => pds
						.Add("FAVORITE_DOG", "border collie")
						.Add("RGB", "RED|BLUE|GREEN")
					)
				)
				.Gsub<Project>(gs => gs
					.Field(p => p.Name)
					.Pattern("-")
					.Replacement("_")
				)
				.Join<Project>(j => j
					.Field(p => p.Branches)
					.Separator(",")
				)
				.Lowercase<Project>(l => l
					.Field(p => p.Name)
				)
				.Remove<Project>(r => r
					.Field(p => p.Suggest)
				)
				.Rename<Project>(rn => rn
					.Field(p => p.LeadDeveloper)
					.TargetField("projectLead")
				)
				.Set<Project>(s => s
					.Field(p => p.Name)
					.Value("foo")
				)
				.Split<Project>(sp => sp
					.Field(p => p.Description)
					.Separator(".")
				)
				.Trim<Project>(t => t
					.Field(p => p.Name)
				)
				.Uppercase<Project>(u => u
					.Field(p => p.Name)
				)
				.DotExpander<Project>(de => de
					.Field("field.withDots")
				)
				.Script(s => s
					.Source("ctx.numberOfCommits++")
				)
				.UrlDecode<Project>(ud => ud
					.Field(p => p.Description)
					.IgnoreMissing()
				)
			);

		protected override PutPipelineRequest Initializer => new PutPipelineRequest(_id)
		{
			Description = "My test pipeline",
			Processors = new IProcessor[]
			{
				new AppendProcessor
				{
					Field = "state",
					Value = new object [] { StateOfBeing.Stable, StateOfBeing.VeryActive }
				},
				new ConvertProcessor
				{
					Field = "numberOfCommits",
					TargetField = "targetField",
					Type = ConvertProcessorType.String
				},
				new DateProcessor
				{
					Field = "startedOn",
					TargetField = "timestamp",
					Formats = new string [] { "dd/MM/yyyy hh:mm:ss"},
					Timezone = "Europe/Amsterdam"
				},
				new FailProcessor
				{
					Message = "an error message"
				},
				new ForeachProcessor
				{
					Field = Infer.Field<Project>(p => p.Tags),
					Processor = new UppercaseProcessor
					{
						Field = "_value.name"
					}
				},
				new GrokProcessor
				{
					Field = "description",
					Patterns = new [] { "my %{FAVORITE_DOG:dog} is colored %{RGB:color}" },
					PatternDefinitions = new Dictionary<string, string>
					{
						{ "FAVORITE_DOG", "border collie" },
						{ "RGB", "RED|BLUE|GREEN" },
					}
				},
				new GsubProcessor
				{
					Field = "name",
					Pattern = "-",
					Replacement = "_"
				},
				new JoinProcessor
				{
					Field = "branches",
					Separator = ","
				},
				new LowercaseProcessor
				{
					Field = "name"
				},
				new RemoveProcessor
				{
					Field = "suggest"
				},
				new RenameProcessor
				{
					Field = "leadDeveloper",
					TargetField = "projectLead"
				},
				new SetProcessor
				{
					Field = Infer.Field<Project>(p => p.Name),
					Value = "foo"
				},
				new SplitProcessor
				{
					Field = "description",
					Separator = "."
				},
				new TrimProcessor
				{
					Field = "name"
				},
				new UppercaseProcessor
				{
					Field = "name"
				},
				new DotExpanderProcessor
				{
					Field = "field.withDots"
				},
				new ScriptProcessor
				{
					Source = "ctx.numberOfCommits++"
				},
				new UrlDecodeProcessor
				{
					Field = "description",
					IgnoreMissing = true
				}
			}
		};
	}
}
