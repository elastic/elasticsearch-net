// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.Xunit;
using Tests.Domain;
using static Nest.Infer;

namespace Tests.Ingest
{
	using ProcFunc = Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>>;

	public interface IProcessorAssertion
	{
		ProcFunc Fluent { get; }
		IProcessor Initializer { get; }
		object Json { get; }
		string Key { get; }
	}

	public abstract class ProcessorAssertion : IProcessorAssertion
	{
		public abstract ProcFunc Fluent { get; }
		public abstract IProcessor Initializer { get; }
		public abstract object Json { get; }
		public abstract string Key { get; }
	}


	public static class ProcessorAssertions
	{
		public static IEnumerable<IProcessorAssertion> All =>
			from t in typeof(ProcessorAssertions).GetNestedTypes()
			where typeof(IProcessorAssertion).IsAssignableFrom(t) && t.IsClass
			let a = t.GetCustomAttributes(typeof(SkipVersionAttribute)).FirstOrDefault() as SkipVersionAttribute
			where a == null || !a.Ranges.Any(r => r.IsSatisfied(TestClient.Configuration.ElasticsearchVersion))
			select (IProcessorAssertion)Activator.CreateInstance(t);

		public static IProcessor[] Initializers => All.Select(a => a.Initializer).ToArray();

		public static Dictionary<string, object>[] AllAsJson =>
			All.Select(a => new Dictionary<string, object>
				{
					{ a.Key, a.Json }
				})
				.ToArray();

		public static IPromise<IList<IProcessor>> Fluent(ProcessorsDescriptor d)
		{
			foreach (var a in All) a.Fluent(d);
			return d;
		}
		
		public class Append : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Append<Project>(a => a.Field(p => p.State).Value(StateOfBeing.Stable, StateOfBeing.VeryActive));

			public override IProcessor Initializer => new AppendProcessor { Field = "state", Value = new object[] { StateOfBeing.Stable, StateOfBeing.VeryActive }};

			public override object Json => new
			{
				field = "state",
				value = new[] { "Stable", "VeryActive" }
			};

			public override string Key => "append";
		}

		[SkipVersion("<7.11.0", "Allow duplicates added in 7.11")]
		public class AppendWithAllowDuplicates : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Append<Project>(a => a.Field(p => p.State).Value(StateOfBeing.Stable, StateOfBeing.VeryActive).AllowDuplicates(false));

			public override IProcessor Initializer => new AppendProcessor { Field = "state", Value = new object[] { StateOfBeing.Stable, StateOfBeing.VeryActive }, AllowDuplicates = false };

			public override object Json => new { field = "state", value = new[] { "Stable", "VeryActive" }, allow_duplicates = false };

			public override string Key => "append";
		}

		[SkipVersion("<7.9.0", "Description added in 7.9.0")]
		public class Csv : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Csv<Project>(c => c
					.Field(p => p.Name)
					.TargetFields(new[] { "targetField1", "targetField2" })
					.EmptyValue("empty")
					.Trim()
					.Description("parses CSV")
				);

			public override IProcessor Initializer => new CsvProcessor
			{
				Field = "name",
				TargetFields = new[] { "targetField1", "targetField2" },
				EmptyValue = "empty",
				Trim = true,
				Description = "parses CSV"
			};

			public override object Json => new
			{
				field = "name",
				target_fields = new[] { "targetField1", "targetField2" },
				empty_value = "empty",
				trim = true,
				description = "parses CSV"
			};

			public override string Key => "csv";
		}

		public class Convert : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Convert<Project>(c => c
					.Field(p => p.NumberOfCommits)
					.TargetField("targetField")
					.Type(ConvertProcessorType.String)
				);

			public override IProcessor Initializer => new ConvertProcessor
			{
				Field = "numberOfCommits",
				TargetField = "targetField",
				Type = ConvertProcessorType.String
			};

			public override object Json => new
			{
				field = "numberOfCommits",
				target_field = "targetField",
				type = "string"
			};

			public override string Key => "convert";
		}

		public class Date : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Date<Project>(dt => dt
					.Field(p => p.StartedOn)
					.TargetField("timestamp")
					.Formats("dd/MM/yyyy hh:mm:ss")
					.TimeZone("Europe/Amsterdam")
				);

			public override IProcessor Initializer => new DateProcessor
			{
				Field = "startedOn",
				TargetField = "timestamp",
				Formats = new[] { "dd/MM/yyyy hh:mm:ss" },
				TimeZone = "Europe/Amsterdam"
			};

			public override object Json => new
			{
				field = "startedOn",
				target_field = "timestamp",
				formats = new[] { "dd/MM/yyyy hh:mm:ss" },
				timezone = "Europe/Amsterdam"
			};

			public override string Key => "date";
		}

		[SkipVersion("<7.5.0", "Introduced in 7.5.0")]
		public class Enrich : ProcessorAssertion
		{
			public static string PolicyName = "enrich_processor_policy";

			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Enrich<Project>(f => f
				.PolicyName(PolicyName)
				.Field(f => f.Name)
				.TargetField("target_field")
			);

			public override IProcessor Initializer => new EnrichProcessor
			{
				PolicyName = PolicyName,
				Field = Field<Project>(f => f.Name),
				TargetField = "target_field"
			};

			public override object Json => new
			{
				policy_name = PolicyName,
				field = "name",
				target_field = "target_field"
			};
			public override string Key => "enrich";
		}

		public class Fail : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Fail(f => f.Message("an error message"));

			public override IProcessor Initializer => new FailProcessor { Message = "an error message" };

			public override object Json => new { message = "an error message" };
			public override string Key => "fail";
		}

		public class Foreach : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Foreach<Project>(fe => fe
					.Field(p => p.Tags)
					.Processor(pps => pps
						.Uppercase<Tag>(uc => uc
							.Field("_value.name")
						)
					)
				);

			public override IProcessor Initializer => new ForeachProcessor
			{
				Field = Field<Project>(p => p.Tags),
				Processor = new UppercaseProcessor
				{
					Field = "_value.name"
				}
			};

			public override object Json => new
			{
				field = "tags",
				processor = new
				{
					uppercase = new
					{
						field = "_value.name"
					}
				}
			};

			public override string Key => "foreach";
		}

		public class Grok : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Grok<Project>(gk => gk
					.Field(p => p.Description)
					.Patterns("my %{FAVORITE_DOG:dog} is colored %{RGB:color}")
					.PatternDefinitions(pds => pds
						.Add("FAVORITE_DOG", "border collie")
						.Add("RGB", "RED|BLUE|GREEN")
					)
				);

			public override IProcessor Initializer => new GrokProcessor
			{
				Field = "description",
				Patterns = new[] { "my %{FAVORITE_DOG:dog} is colored %{RGB:color}" },
				PatternDefinitions = new Dictionary<string, string>
				{
					{ "FAVORITE_DOG", "border collie" },
					{ "RGB", "RED|BLUE|GREEN" },
				}
			};

			public override object Json => new
			{
				field = "description",
				patterns = new[] { "my %{FAVORITE_DOG:dog} is colored %{RGB:color}" },
				pattern_definitions = new Dictionary<string, string>
				{
					{ "FAVORITE_DOG", "border collie" },
					{ "RGB", "RED|BLUE|GREEN" },
				}
			};

			public override string Key => "grok";
		}

		public class Gsub : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Gsub<Project>(gs => gs
					.Field(p => p.Name)
					.Pattern("-")
					.Replacement("_")
				);

			public override IProcessor Initializer => new GsubProcessor
			{
				Field = "name",
				Pattern = "-",
				Replacement = "_"
			};

			public override object Json => new { field = "name", pattern = "-", replacement = "_" };
			public override string Key => "gsub";
		}

		public class Join : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Join<Project>(j => j.Field(p => p.Branches).Separator(","));

			public override IProcessor Initializer => new JoinProcessor
			{
				Field = "branches",
				Separator = ","
			};

			public override object Json => new { field = "branches", separator = "," };
			public override string Key => "join";
		}

		public class Lowercase : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Lowercase<Project>(l => l.Field(p => p.Name));

			public override IProcessor Initializer => new LowercaseProcessor { Field = "name" };

			public override object Json => new { field = "name" };
			public override string Key => "lowercase";
		}

		public class Remove : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Remove<Project>(r => r.Field(p => p.Field(pp => pp.Suggest)));

			public override IProcessor Initializer => new RemoveProcessor { Field = "suggest" };

			public override object Json => new { field = new [] { "suggest" } };
			public override string Key => "remove";
		}

		public class Rename : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Rename<Project>(rn => rn.Field(p => p.LeadDeveloper).TargetField("projectLead"));

			public override IProcessor Initializer => new RenameProcessor
			{
				Field = "leadDeveloper",
				TargetField = "projectLead"
			};

			public override object Json => new { field = "leadDeveloper", target_field = "projectLead" };
			public override string Key => "rename";
		}

		public class Set : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Set<Project>(s => s.Field(p => p.Name).Value("foo"));

			public override IProcessor Initializer => new SetProcessor { Field = Field<Project>(p => p.Name), Value = "foo" };

			public override object Json => new { field = "name", value = "foo" };
			public override string Key => "set";
		}

		public class Split : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.Split<Project>(sp => sp.Field(p => p.Description).Separator("."));

			public override IProcessor Initializer => new SplitProcessor { Field = "description", Separator = "." };

			public override object Json => new { field = "description", separator = "." };
			public override string Key => "split";
		}

		public class Trim : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Trim<Project>(t => t.Field(p => p.Name));

			public override IProcessor Initializer => new TrimProcessor { Field = "name" };

			public override object Json => new { field = "name" };
			public override string Key => "trim";
		}

		public class Uppercase : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Uppercase<Project>(u => u.Field(p => p.Name));

			public override IProcessor Initializer => new UppercaseProcessor { Field = "name" };

			public override object Json => new { field = "name" };
			public override string Key => "uppercase";
		}

		public class DotExpander : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent =>
				d => d.DotExpander<Project>(de => de.Field("field.withDots"));

			public override IProcessor Initializer => new DotExpanderProcessor { Field = "field.withDots" };

			public override object Json => new { field = "field.withDots" };
			public override string Key => "dot_expander";
		}

		public class Script : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Script(s => s.Source("ctx.numberOfCommits++"));

			public override IProcessor Initializer => new ScriptProcessor { Source = "ctx.numberOfCommits++" };

			public override object Json => new { source = "ctx.numberOfCommits++" };
			public override string Key => "script";
		}

		[SkipVersion("<6.1.0", "uses url decode which was introduced in 6.1.0")]
		public class UrlDecode : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.UrlDecode<Project>(ud => ud
					.Field(p => p.Description)
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new UrlDecodeProcessor { Field = "description", IgnoreMissing = true };

			public override object Json => new { field = "description", ignore_missing = true };
			public override string Key => "urldecode";
		}

		[SkipVersion("<6.4.0", "")]
		public class Attachment : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Attachment<Project>(ud => ud
					.Field(p => p.Description)
					.IndexedCharacters(100_000)
					.Properties("title", "author")
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new AttachmentProcessor
			{
				Field = "description",
				Properties = new[] { "title", "author" },
				IndexedCharacters = 100_000,
				IgnoreMissing = true
			};

			public override object Json => new
			{
				field = "description",
				ignore_missing = true,
				properties = new[] { "title", "author" },
				indexed_chars = 100_000,
			};

			public override string Key => "attachment";
		}

		[SkipVersion("<7.11.0", "Resource name support was added in 7.11")]
		public class Attachment_WithResourceName : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Attachment<Project>(ud => ud
					.Field(p => p.Description)
					.IndexedCharacters(100_000)
					.Properties("title", "author")
					.IgnoreMissing()
					.ResourceName(n => n.Name)
				);

			public override IProcessor Initializer => new AttachmentProcessor
			{
				Field = "description",
				Properties = new[] { "title", "author" },
				IndexedCharacters = 100_000,
				IgnoreMissing = true,
				ResourceName = "name"
			};

			public override object Json => new
			{
				field = "description",
				ignore_missing = true,
				properties = new[] { "title", "author" },
				indexed_chars = 100_000,
				resource_name = "name"
			};

			public override string Key => "attachment";
		}

		[SkipVersion("<7.4.0", "Circle processor added in 7.4.0")]
		public class Circle : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Circle<Project>(ud => ud
					.Field(p => p.Description)
					.TargetField(p => p.ArbitraryShape)
					.ShapeType(ShapeType.Shape)
					.ErrorDistance(10d)
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new CircleProcessor
			{
				Field = "description",
				TargetField = "arbitraryShape",
				ShapeType = ShapeType.Shape,
				ErrorDistance = 10d,
				IgnoreMissing = true
			};

			public override object Json => new
			{
				field = "description",
				target_field = "arbitraryShape",
				shape_type = "shape",
				error_distance = 10.0,
				ignore_missing = true,
			};

			public override string Key => "circle";
		}

		[SkipVersion("<6.4.0", "")]
		public class Bytes : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Bytes<Project>(ud => ud
					.Field(p => p.Description)
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new BytesProcessor { Field = "description", IgnoreMissing = true };

			public override object Json => new { field = "description", ignore_missing = true };
			public override string Key => "bytes";
		}

		[SkipVersion("<6.5.0", "")]
		public class Dissect : ProcessorAssertion
		{
			private readonly string _pattern = "%{clientip} %{ident} %{auth} [%{@timestamp}] \"%{verb} %{request} HTTP/%{httpversion}\" %{status} %{size}";

			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Dissect<Project>(ud => ud
					.Field(p => p.Description)
					.IgnoreMissing()
					.Pattern(_pattern)
					.AppendSeparator(" ")
				);

			public override IProcessor Initializer => new DissectProcessor
			{
				Field = "description",
				IgnoreMissing = true,
				Pattern = _pattern,
				AppendSeparator = " "
			};

			public override object Json => new
			{
				field = "description",
				ignore_missing = true,
				pattern = _pattern,
				append_separator = " "
			};
			public override string Key => "dissect";
		}
		[SkipVersion("<6.5.0", "")]
		public class Drop : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Drop(ud => ud.If("true"));

			public override IProcessor Initializer => new DropProcessor
			{
				If = "true"
			};

			public override object Json => new
			{
				@if = "true"
			};
			public override string Key => "drop";
		}


		public class KeyValue : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Kv<Project>(ud => ud
					.Field(p => p.Description)
					.FieldSplit("_")
					.ValueSplit(" ")
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new KeyValueProcessor
			{
				Field = "description",
				FieldSplit = "_",
				ValueSplit = " ",
				IgnoreMissing = true
			};

			public override object Json => new
			{
				field = "description",
				ignore_missing = true,
				field_split = "_",
				value_split = " "
			};

			public override string Key => "kv";
		}

		[SkipVersion("<6.4.0", "trimming options were introduced later")]
		public class KeyValueTrimming : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Kv<Project>(ud => ud
					.Field(p => p.Description)
					.FieldSplit("_")
					.ValueSplit(" ")
					.TrimKey("xyz")
					.TrimValue("abc")
					.StripBrackets()
					.IgnoreMissing()
				);

			public override IProcessor Initializer => new KeyValueProcessor
			{
				Field = "description",
				FieldSplit = "_",
				ValueSplit = " ",
				TrimKey = "xyz",
				TrimValue = "abc",
				StripBrackets = true,
				IgnoreMissing = true
			};

			public override object Json => new
			{
				field = "description",
				ignore_missing = true,
				field_split = "_",
				value_split = " ",
				trim_key = "xyz",
				trim_value = "abc",
				strip_brackets = true
			};

			public override string Key => "kv";
		}

		[SkipVersion("<6.5.0", "")]
		public class SetSecurityUser: ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.SetSecurityUser<Project>(ud => ud
					.Field(p => p.Name)
				);

			public override IProcessor Initializer => new SetSecurityUserProcessor
			{
				Field = "name",
			};

			public override object Json => new
			{
				field = "name",
			};

			public override string Key => "set_security_user";
		}

		[SkipVersion("<6.5.0", "")]
		public class Pipeline : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Pipeline(ud => ud
					.ProcessorName("x")
				);

			public override IProcessor Initializer => new PipelineProcessor
			{
				ProcessorName = "x",
			};

			public override object Json => new
			{
				name = "x",
			};

			public override string Key => "pipeline";
		}

		[SkipVersion("<7.11.0", "Uses URI parts which was introduced in 7.11.0")]
		public class UriParts : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.UriParts<Project>(ud => ud
				.Field(p => p.Description)
				.KeepOriginal()
				.RemoveIfSuccessful());

			public override IProcessor Initializer => new UriPartsProcessor { Field = "description", KeepOriginal = true, RemoveIfSuccessful = true };
			public override object Json => new { field = "description", keep_original = true, remove_if_successful = true };
			public override string Key => "uri_parts";
		}

		[SkipVersion("<7.12.0", "Uses fingerprint processor which was introduced in 7.12.0")]
		public class Fingerprint : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Fingerprint<Project>(ud => ud
					.Fields(p => p.Fields(f => f.Labels))
					.Method("MD5")
					.Salt("ThisIsASalt!")
					.TargetField(p => p.Description)
					.IgnoreMissing());

			public override IProcessor Initializer => new FingerprintProcessor { Fields = "labels", Method = "MD5", Salt = "ThisIsASalt!", TargetField = "description", IgnoreMissing = true };
			public override object Json => new { fields =  new[] { "labels" }, method = "MD5", salt = "ThisIsASalt!", target_field = "description", ignore_missing = true };
			public override string Key => "fingerprint";
		}

		[SkipVersion("<7.12.0", "Uses the network community ID processor which was introduced in 7.12.0")]
		public class NetworkCommunityId : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.NetworkCommunityId<Project>(ud => ud
					.DestinationIp(f => f.LeadDeveloper.IpAddress)
					.DestinationPort("leadDeveloper.portNumber")
					.IanaNumber(f => f.Name)
					.IcmpType(f => f.Name)
					.IcmpCode(f => f.Name)
					.IgnoreMissing()
					.Seed(100)
					.SourceIp(f => f.LeadDeveloper.IpAddress)
					.SourcePort("leadDeveloper.portNumber")
					.TargetField(f => f.Description)
					.Transport(f => f.Name));

			public override IProcessor Initializer => new NetworkCommunityIdProcessor
			{
				DestinationIp = Field<Project>(f => f.LeadDeveloper.IpAddress),
				DestinationPort = "leadDeveloper.portNumber",
				IanaNumber = "name",
				IcmpType = "name",
				IcmpCode = "name",
				IgnoreMissing = true,
				Seed = 100,
				SourceIp = Field<Project>(f => f.LeadDeveloper.IpAddress),
				SourcePort = "leadDeveloper.portNumber",
				TargetField = "description",
				Transport = "name"
			};

			public override object Json => new
			{
				destination_ip = "leadDeveloper.ipAddress",
				destination_port = "leadDeveloper.portNumber",
				iana_number = "name",
				icmp_code = "name",
				icmp_type = "name",
				ignore_missing = true,
				seed = 100,
				source_ip = "leadDeveloper.ipAddress",
				source_port = "leadDeveloper.portNumber",
				target_field = "description",
				transport = "name"
			};

			public override string Key => "community_id";
		}

		[SkipVersion("<7.12.0", "Uses network direction processor which was introduced in 7.12.0")]
		public class NetworkDirection : ProcessorAssertion
		{
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.NetworkDirection<Project>(ud => ud
					.DestinationIp(f => f.LeadDeveloper.IpAddress)
					.SourceIp(f => f.LeadDeveloper.IpAddress)
					.InternalNetworks("network-a", "network-b")
					.TargetField(p => p.Description)
					.IgnoreMissing());

			public override IProcessor Initializer => new NetworkDirectionProcessor
			{
				DestinationIp = Field<Project>(f => f.LeadDeveloper.IpAddress),
				SourceIp = Field<Project>(f => f.LeadDeveloper.IpAddress),
				InternalNetworks = new [] { "network-a", "network-b" },
				TargetField = "description",
				IgnoreMissing = true
			};
			
			public override object Json => new
			{
				destination_ip = "leadDeveloper.ipAddress",
				internal_networks = new[]
				{
					"network-a", "network-b"
				},
				source_ip = "leadDeveloper.ipAddress",
				target_field = "description",
				ignore_missing = true
			};
			
			public override string Key => "network_direction";
		}
	}
}
