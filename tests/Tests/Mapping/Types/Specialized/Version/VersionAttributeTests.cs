// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;

namespace Tests.Mapping.Types.Specialized.Version
{
	public class VersionTest
	{
		[Version]
		public string VersionNumber { get; set; }
	}

	public class VersionAttributeTests : AttributeTestsBase<VersionTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				versionNumber = new
				{
					type = "version"
				}
			}
		};
	}
}
