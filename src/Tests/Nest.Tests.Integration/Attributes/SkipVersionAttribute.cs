using NUnit.Framework;

namespace Nest.Tests.Integration
{
	public class SkipVersionAttribute : TestActionAttribute
	{
		private string _Version;
		private string _Reason;
		
		public SkipVersionAttribute(string version)
			: this(version, null)
		{
		}

		public SkipVersionAttribute(string version, string reason)
		{
			_Version = version;
			_Reason = reason;
		}

		public override void BeforeTest(TestDetails testDetails)
		{
			if (ElasticsearchConfiguration.CurrentVersion.Contains(_Version))
			{
				Assert.Pass(_Reason);
			}
		}
	}
}
