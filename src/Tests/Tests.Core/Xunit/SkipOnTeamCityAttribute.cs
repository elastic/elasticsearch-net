using System;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class SkipOnTeamCityAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";

		public static bool RunningOnTeamCity => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public override bool Skip => RunningOnTeamCity;
	}
}
