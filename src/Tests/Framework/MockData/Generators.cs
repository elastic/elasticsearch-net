namespace Tests.Framework.MockData
{
	/// <summary>
	/// Class that materializes generators in static context.
	/// </summary>
	public static class Generators
	{
		static Generators()
		{
			var r = Gimme.Random.Number(1, 3);
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
