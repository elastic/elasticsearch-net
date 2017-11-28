using System;

namespace Tests.Framework
{
	/// <summary>
	/// Ignores all unit tests on from a class when we are running the unit tests against a PackageReference instead of ProjectReference
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ProjectReferenceOnlyAttribute : Attribute { }
}
