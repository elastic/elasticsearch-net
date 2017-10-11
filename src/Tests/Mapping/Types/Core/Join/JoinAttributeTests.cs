using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using Tests.Framework.MockData;

namespace Tests.Mapping.Types.Core.Join
{
	public class JoinTest
	{
		[Join(typeof(Project), typeof(CommitActivity))]
		public QueryContainer Query { get; set; }
	}

	public class JoinAttributeTests : AttributeTestsBase<JoinTest>
	{
		protected override object ExpectJson => new
		{
			properties = new
			{
				query = new
				{
					type = "join",
					relations = new {
						project = "commits"
					}
				}
			}
		};
	}
}
