using Nest;

namespace Tests.Framework.MockData
{
	public static class AnonymizerExtensions
	{
		public static object ToAnonymousObject(this JoinField field) =>
			field.Match<object>(p => p.Name.Name, c => new
			{
				parent = c.Parent.ToString(),
				name = c.Name.Name
			});
	}
}