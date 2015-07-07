using System;
using System.Collections.Generic;

namespace Tests.Framework.MockData
{
	public class Project
	{
		public string Name { get; set; }
		public DateTime StartedOn { get; set; }
		public DateTime LastActivity { get; set; }
		public Developer LeadDeveloper { get; set; }
		public IEnumerable<Tag> Tags { get; set; } 
	}

	public class Tag
	{
		public DateTime Added { get; set; }
		public string Name { get; set; }
	}


	public class CommitActivity
	{
		public string Id { get; set; }
		public string ProjectName { get; set; }
		public string Message { get; set; }
		public Developer Committer { get; set; }
	}

	public enum Gender
	{
		Male, Female, NoneOfYourBeeswax
	}

	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

	public class Developer : Person
	{
		public string OnlineHandle { get; set; }
		public Gender Gender { get; set; }
		public string PrivateValue { get; set; }
		//public GeoLocation Location { get; set; }
	}

}
