using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tests.Framework.MockData
{
	public class CommitActivity
	{
		private string _projectName;
		public JoinField Join { get; set; }
		public string Id { get; set; }

		public string ProjectName
		{
			get => _projectName;
			set
			{
				this.Join = JoinField.Link<CommitActivity>(value);
				_projectName = value;
			}
		}

		public string Message { get; set; }
		public long SizeInBytes { get; set; }
		public double ConfidenceFactor { get; set; }
		public Developer Committer { get; set; }
		public TimeSpan? Duration { get; set; }

		[Text]
		[StringTimeSpan, JsonConverter(typeof(StringTimeSpanConverter))]
		public TimeSpan? StringDuration
		{
			get => Duration;
			set => Duration = value;
		}

		public static Faker<CommitActivity> Generator { get; } =
			new Faker<CommitActivity>()
				.UseSeed(TestClient.Configuration.Seed)
				.RuleFor(p => p.Id, p => Guid.NewGuid().ToString("N").Substring(0, 8))
				.RuleFor(p => p.ProjectName, p => Project.Projects[Gimme.Random.Number(0, Project.Projects.Count -1)].Name)
				.RuleFor(p => p.Committer, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Message, p => p.Lorem.Paragraph(Gimme.Random.Number(1, 3)))
				.RuleFor(p => p.SizeInBytes, p => p.Random.Number(0, 100000))
				.RuleFor(p => p.ConfidenceFactor, p => p.Random.Double())
				.RuleFor(p => p.Duration, p => p.Random.ArrayElement(new TimeSpan?[]
				{
					TimeSpan.MinValue,
					TimeSpan.MaxValue,
					TimeSpan.FromMinutes(7.5),
					TimeSpan.Zero,
					null,
					TimeSpan.FromHours(4.23),
					TimeSpan.FromDays(5),
				}))
			;

		public static IList<CommitActivity> CommitActivities { get; } = Generator.Clone().Generate(1000);
	}
}
