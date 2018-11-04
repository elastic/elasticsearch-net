using System;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.Core.Xunit
{
	public class ProjectReferenceOnlyAttribute : SkipTestAttributeBase
	{
		public override string Reason { get; } = "This test can only be run if client dependencies are project references";
		public override bool Skip => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TestPackageVersion"));
	}
}
