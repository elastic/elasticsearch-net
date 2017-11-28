using Nest;

namespace Tests.Framework
{
	internal static class NumericExtensions
	{
		public static string ToOrdinal(this int num)
		{
			if (num <= 0) return num.ToString();

			switch (num % 100)
			{
				case 11:
				case 12:
				case 13:
					return num + "th";
			}

			switch (num % 10)
			{
				case 1:
					return num + "st";
				case 2:
					return num + "nd";
				case 3:
					return num + "rd";
				default:
					return num + "th";
			}

		}
	}

	internal static class AnonymizerExtensions
	{
		public static object ToAnonymousObject(this JoinField field) =>
			field.Match<object>(p => TestClient.Default.Infer.RelationName(p.Name), c => new
			{
				parent = TestClient.Default.Infer.Id(c.Parent),
				name = TestClient.Default.Infer.RelationName(c.Name)

			});
	}
}
