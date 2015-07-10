using System;
using Bogus;

namespace Tests.Framework.MockData
{
	public class CommitActivity
	{
		public string Id { get; set; }
		public string ProjectName { get; set; }
		public string Message { get; set; }
		public long SizeInBytes { get; set; }
		public double ConfidenceFactor { get; set; }
		public Developer Committer { get; set; }

		public static Faker<CommitActivity> Generator { get; } =
			new Faker<CommitActivity>()
				.RuleFor(p => p.Id, p => Guid.NewGuid().ToString("N").Substring(0, 8))
				.RuleFor(p => p.ProjectName, p => Project.Projects[Gimme.Random.Number(0, Project.Projects.Count -1)].Name)
				.RuleFor(p => p.Committer, p => Developer.Developers[Gimme.Random.Number(0, Developer.Developers.Count -1)])
				.RuleFor(p => p.Message, p => p.Lorem.Paragraph(Gimme.Random.Number(1, 3)))
				.RuleFor(p => p.SizeInBytes, p => p.Random.Number(0, 100000))
				.RuleFor(p => p.ConfidenceFactor, p => p.Random.Double())
			;
	}
}