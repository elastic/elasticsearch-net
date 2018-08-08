using Tests.Domain;

namespace Tests.Core.Xunit
{
	/// <summary>
	/// Class that materializes generators in static context.
	/// </summary>
	public static class Generators
	{
		static Generators()
		{
			var tags = Tag.Generator;
			var messages = Message.Generator;
			var people = Person.People;
			var developers = Developer.Developers;
			var projects = Project.Projects;
			var commits = CommitActivity.CommitActivities;
		}

		public static void Initialize() { }
	}
}
