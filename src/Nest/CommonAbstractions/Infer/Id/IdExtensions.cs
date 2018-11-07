namespace Nest
{
	internal static class IdExtensions
	{
		internal static bool IsConditionless(this Id id) => id == null || id.StringOrLongValue == null && id.Document == null;
	}
}
