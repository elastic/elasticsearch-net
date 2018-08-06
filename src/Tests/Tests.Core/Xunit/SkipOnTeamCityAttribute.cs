using System;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class SkipOnTeamCityAttribute : SkipTestAttributeBase
	{
		public override bool Skip => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";
	}

}
