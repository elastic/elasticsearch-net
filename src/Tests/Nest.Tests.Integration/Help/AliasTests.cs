using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{

	/// <summary>
	/// I use this class to answer questions on github issues/stackoverflow
	/// Tests that are written here are subject to removal at any time
	/// </summary>
	[TestFixture]
	public class HelpTests : IntegrationTests
	{
		[ElasticType(Name = "Entry", IdProperty = "Id")]
		public class Entry
		{
			public string Id { get; set; }
			public string Title { get; set; }
			public string Description { get; set; }
			public string Award { get; set; }
			public int Year { get; set; }
		}
	}
}