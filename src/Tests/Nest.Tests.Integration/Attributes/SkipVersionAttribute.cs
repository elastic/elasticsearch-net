using System;
using System.Linq;
using NUnit.Framework;

namespace Nest.Tests.Integration
{
	public class SkipVersionAttribute : TestActionAttribute
	{
		private Version _lowerBound;
		private Version _upperBound;
		private string _reason;
		private bool _inclusive;

		public SkipVersionAttribute(string range, string reason, bool inclusive = true)
		{
			_reason = reason;
			_inclusive = inclusive;

			ParseVersions(range);
		}

		public override void BeforeTest(TestDetails testDetails)
		{
			var currentVersion = ElasticsearchConfiguration.CurrentVersion;

			var skip = 
				   (_inclusive && _upperBound >= currentVersion && currentVersion >= _lowerBound) ||
				   (_upperBound > currentVersion && currentVersion > _lowerBound);
			
			if (skip)
				Assert.Pass(_reason);
		}

		private void ParseVersions(string range)
		{
			var bounds = range.Split('-').ToList();
			if (bounds.Count > 2)
				throw new ArgumentException("Failed to skip test: invalid range");

			_lowerBound = Version.Parse(NormalizeVersion(bounds[0]));

			var upperBound = bounds.Count == 2 ? bounds[1] : bounds[0];
			_upperBound = Version.Parse(NormalizeVersion(upperBound));
		}

		private string NormalizeVersion(string version)
		{
			var format = "{0}.{1}.{2}";
			var versionParts = version.Split('.').ToList();

			if (versionParts.Count == 1)
				return string.Format(format, versionParts[0], 0, 0);
			else if (versionParts.Count == 2)
				return string.Format(format, versionParts[0], versionParts[1], 0);
			else if (versionParts.Count == 3)
				return string.Format(format, versionParts[0], versionParts[1], versionParts[2]);
			else
				throw new ArgumentException("Failed to skip test: invalid version format");
		}
	}
}
