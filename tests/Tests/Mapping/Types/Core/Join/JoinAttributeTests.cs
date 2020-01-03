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
