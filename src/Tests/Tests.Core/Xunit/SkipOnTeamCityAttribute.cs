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
	//

	/// <summary> Indicates test can not run because of a presumed upstream bug </summary>
	public class BlockedByIssueAttribute : SkipTestAttributeBase
	{
		public BlockedByIssueAttribute(string url) => Url = url;

		public override string Reason => $"Blocked temporarily by {Url}";

		public override bool Skip => true;

		private string Url { get; }
	}

	/// <summary>
	/// Indicates a test that is failing due to a change not related to request/response structure changes
	/// <pre>Warrants deeper investigation preferably as a separate PR</pre>
	/// </summary>
	public class SkipNonStructuralChangeAttribute : SkipTestAttributeBase
	{
		public override string Reason => $"Tests is failing due to change not related to a structural change in request/response";

		public override bool Skip => true;
	}
}
