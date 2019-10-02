using System;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class SkipOnCiAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";

		public static bool RunningOnTeamCity => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public static bool RunningOnAzureDevops => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD"));
		public static bool RunningOnAppVeyor => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("APPVEYOR_BUILD_VERSION"));
		public override bool Skip => RunningOnTeamCity || RunningOnAppVeyor || RunningOnAzureDevops;
	}

	public class SkipAttribute : SkipTestAttributeBase
	{
		public SkipAttribute(string reason) => Reason = reason;

		public override bool Skip => true;
		public override string Reason { get; }
	}
}
