using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Client;
using Tests.Domain;

namespace Tests.Ingest
{
	using ProcFunc = Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>>;
	public interface IProcessorAssertion
	{
		object Json { get; }
		string Key { get; }
		IProcessor Initializer { get; }
		ProcFunc Fluent { get; }
	}
	public abstract class ProcessorAssertion : IProcessorAssertion
	{
		public abstract string Key { get; }
		public abstract IProcessor Initializer { get; }
		public abstract ProcFunc Fluent { get; }
		public abstract object Json { get; }
	}


	public static class ProcessorAssertions
	{
		public static IEnumerable<IProcessorAssertion> All =>
			from t in typeof(ProcessorAssertions).GetNestedTypes()
			where typeof(IProcessorAssertion).IsAssignableFrom(t) && t.IsClass
			let a = t.GetCustomAttributes(typeof(SkipVersionAttribute)).FirstOrDefault() as SkipVersionAttribute
			where a == null || !a.Ranges.Any(r => r.IsSatisfied(TestClient.Configuration.ElasticsearchVersion))
			select (IProcessorAssertion) (Activator.CreateInstance(t));

		public static Dictionary<string, object>[] Json =>
			All.Select(a => new Dictionary<string, object>
			{
				{a.Key, a.Json}
			}).ToArray();

		public static IPromise<IList<IProcessor>> Fluent(ProcessorsDescriptor d)
		{
			foreach (var a in All) a.Fluent(d);
			return d;
		}

		public static IProcessor[] Initializer => All.Select(a => a.Initializer).ToArray();

		public class Append : ProcessorAssertion
		{
			public override string Key => "append";
			public override object Json => new {field = "state", value = new[] {"Stable", "VeryActive"}};

			public override IProcessor Initializer => new AppendProcessor
			{
				Field = "state",
				Value = new object[] {StateOfBeing.Stable, StateOfBeing.VeryActive}
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Append<Project>(a => a
					.Field(p => p.State)
					.Value(StateOfBeing.Stable, StateOfBeing.VeryActive)
				);

		}

		public class Convert : ProcessorAssertion
		{
			public override string Key => "convert";
			public override object Json => new
			{
				field = "numberOfCommits",
				target_field = "targetField",
				type = "string"
			};

			public override IProcessor Initializer => new ConvertProcessor
			{
				Field = "numberOfCommits",
				TargetField = "targetField",
				Type = ConvertProcessorType.String
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Convert<Project>(c => c
					.Field(p => p.NumberOfCommits)
					.TargetField("targetField")
					.Type(ConvertProcessorType.String)
				);
		}

		public class Date : ProcessorAssertion
		{
			public override string Key => "date";
			public override object Json => new
			{
				field = "startedOn",
				target_field = "timestamp",
				formats = new[] {"dd/MM/yyyy hh:mm:ss"},
				timezone = "Europe/Amsterdam"
			};

			public override IProcessor Initializer => new DateProcessor
			{
				Field = "startedOn",
				TargetField = "timestamp",
				Formats = new string[] {"dd/MM/yyyy hh:mm:ss"},
				Timezone = "Europe/Amsterdam"
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Date<Project>(dt => dt
					.Field(p => p.StartedOn)
					.TargetField("timestamp")
					.Formats("dd/MM/yyyy hh:mm:ss")
					.Timezone("Europe/Amsterdam")
				);
		}

		public class Fail : ProcessorAssertion
		{
			public override string Key => "fail";

			public override object Json => new { message = "an error message" };

			public override IProcessor Initializer => new FailProcessor {Message = "an error message"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Fail(f => f.Message("an error message"));
		}

		public class Foreach : ProcessorAssertion
		{
			public override string Key => "foreach";

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

			public override IProcessor Initializer => new ForeachProcessor
			{
				Field = Infer.Field<Project>(p => p.Tags),
				Processor = new UppercaseProcessor
				{
					Field = "_value.name"
				}
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Foreach<Project>(fe => fe
					.Field(p => p.Tags)
					.Processor(pps => pps
						.Uppercase<Tag>(uc => uc
							.Field("_value.name")
						)
					)
				);
		}

		public class Grok : ProcessorAssertion
		{
			public override string Key => "grok";

			public override object Json => new
			{
				field = "description",
				patterns = new[] {"my %{FAVORITE_DOG:dog} is colored %{RGB:color}"},
				pattern_definitions = new Dictionary<string, string>
				{
					{"FAVORITE_DOG", "border collie"},
					{"RGB", "RED|BLUE|GREEN"},
				}
			};

			public override IProcessor Initializer => new GrokProcessor
			{
				Field = "description",
				Patterns = new[] {"my %{FAVORITE_DOG:dog} is colored %{RGB:color}"},
				PatternDefinitions = new Dictionary<string, string>
				{
					{"FAVORITE_DOG", "border collie"},
					{"RGB", "RED|BLUE|GREEN"},
				}
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Grok<Project>(gk => gk
					.Field(p => p.Description)
					.Patterns("my %{FAVORITE_DOG:dog} is colored %{RGB:color}")
					.PatternDefinitions(pds => pds
						.Add("FAVORITE_DOG", "border collie")
						.Add("RGB", "RED|BLUE|GREEN")
					)
				);
		}

		public class Gsub : ProcessorAssertion
		{
			public override string Key => "gsub";

			public override object Json => new { field = "name", pattern = "-", replacement = "_" };

			public override IProcessor Initializer => new GsubProcessor
			{
				Field = "name",
				Pattern = "-",
				Replacement = "_"
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.Gsub<Project>(gs => gs
					.Field(p => p.Name)
					.Pattern("-")
					.Replacement("_")
				);
		}

		public class Join : ProcessorAssertion
		{
			public override string Key => "join";

			public override object Json => new { field = "branches", separator = "," };

			public override IProcessor Initializer => new JoinProcessor
			{
				Field = "branches",
				Separator = ","
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Join<Project>(j => j.Field(p => p.Branches).Separator(","));
		}

		public class Lowercase : ProcessorAssertion
		{
			public override string Key => "lowercase";

			public override object Json => new { field = "name" };

			public override IProcessor Initializer => new LowercaseProcessor { Field = "name" };
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Lowercase<Project>(l => l.Field(p => p.Name));
		}

		public class Remove : ProcessorAssertion
		{
			public override string Key => "remove";

			public override object Json => new { field = "suggest" };

			public override IProcessor Initializer => new RemoveProcessor {Field = "suggest"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Remove<Project>(r => r.Field(p => p.Suggest));
		}

		public class Rename : ProcessorAssertion
		{
			public override string Key => "rename";

			public override object Json => new { field = "leadDeveloper", target_field = "projectLead" };

			public override IProcessor Initializer => new RenameProcessor
			{
				Field = "leadDeveloper",
				TargetField = "projectLead"
			};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Rename<Project>(rn => rn.Field(p => p.LeadDeveloper).TargetField("projectLead"));
		}

		public class Set : ProcessorAssertion
		{
			public override string Key => "set";

			public override object Json => new { field = "name", value = "foo" };

			public override IProcessor Initializer => new SetProcessor { Field = Infer.Field<Project>(p => p.Name), Value = "foo" };
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Set<Project>(s => s.Field(p => p.Name).Value("foo"));
		}

		public class Split : ProcessorAssertion
		{
			public override string Key => "split";

			public override object Json => new { field = "description", separator = "." };

			public override IProcessor Initializer => new SplitProcessor {Field = "description", Separator = "."};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Split<Project>(sp => sp.Field(p => p.Description).Separator("."));
		}

		public class Trim : ProcessorAssertion
		{
			public override string Key => "trim";

			public override object Json => new { field = "name" };

			public override IProcessor Initializer => new TrimProcessor {Field = "name"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Trim<Project>(t => t.Field(p => p.Name));
		}

		public class Uppercase : ProcessorAssertion
		{
			public override string Key => "uppercase";

			public override object Json => new { field = "name" };

			public override IProcessor Initializer => new UppercaseProcessor {Field = "name"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Uppercase<Project>(u => u.Field(p => p.Name));
		}

		public class DotExpander : ProcessorAssertion
		{
			public override string Key => "dot_expander";

			public override object Json => new { field = "field.withDots" };

			public override IProcessor Initializer => new DotExpanderProcessor {Field = "field.withDots"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.DotExpander<Project>(de => de.Field("field.withDots"));
		}

		public class Script : ProcessorAssertion
		{
			public override string Key => "script";

			public override object Json => new { source = "ctx.numberOfCommits++" };

			public override IProcessor Initializer => new ScriptProcessor {Source = "ctx.numberOfCommits++"};
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d.Script(s => s.Source("ctx.numberOfCommits++"));
		}

		[SkipVersion("<6.1.0", "uses url decode which was introduced in 6.1.0")]
		public class UrlDecode : ProcessorAssertion
		{
			public override string Key => "urldecode";

			public override object Json => new { field = "description", ignore_missing = true };

			public override IProcessor Initializer => new UrlDecodeProcessor { Field = "description", IgnoreMissing = true };
			public override Func<ProcessorsDescriptor, IPromise<IList<IProcessor>>> Fluent => d => d
				.UrlDecode<Project>(ud => ud
					.Field(p => p.Description)
					.IgnoreMissing()
				);
		}
	}
}
