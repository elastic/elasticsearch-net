namespace Tests.Framework.MockData
{
	/// <summary>
	/// Class that materializes generators in static context.
	/// Seeing dead locks often since we moved to Randomizer.Seed but we can't move to Bogus 2.0 which has instance seeds on
	/// Generator because it moved to 2.0: https://github.com/bchavez/Bogus/pull/94
	/// </summary>
	public static class Generators
	{
		static Generators()
		{
			var people = Person.People;
			var developers = Developer.Developers;
			var projects = Project.Projects;
			var commits = CommitActivity.CommitActivities;
		}

		public static void Initialize() { }
	}
}
