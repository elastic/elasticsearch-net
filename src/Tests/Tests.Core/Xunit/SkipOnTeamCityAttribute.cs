using System;
using Elastic.Xunit.XunitPlumbing;
using Tests.Configuration;

namespace Tests.Core.Xunit
{
	public class SkipOnTeamCityAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "Skip running this test on TeamCity, this is usually a sign this test is flakey?";

		public static bool RunningOnTeamCity => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TEAMCITY_VERSION"));
		public override bool Skip => RunningOnTeamCity;
	}
	//

	//TODO 7.0: this attribute and all its usages have to be scrubbed before we can do a 7.x release
	/// <summary> Indicates test can not run because of a presumed upstream bug </summary>
	public class BlockedByIssueAttribute : SkipTestAttributeBase
	{
		public BlockedByIssueAttribute(string url) => Url = url;

		public override string Reason => $"Blocked temporarily by {Url}";

		public override bool Skip => true;

		private string Url { get; }
	}
}
