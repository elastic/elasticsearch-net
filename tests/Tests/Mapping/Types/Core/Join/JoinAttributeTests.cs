// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Domain;

namespace Tests.Mapping.Types.Core.Join
{
	public class JoinTest
	{
		[Join(typeof(Project), typeof(CommitActivity))]
		public JoinField JoinField { get; set; }
	}

	public class JoinAttributeTests : AttributeTestsBase<JoinTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				joinField = new
				{
					type = "join",
					relations = new
					{
						project = "commits"
					}
				}
			}
		};
	}
}
