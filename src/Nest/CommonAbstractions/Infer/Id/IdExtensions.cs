namespace Nest
{
	internal static class IdExtensions
	{
		internal static bool IsConditionless(this Id id) => id == null || id.Value == null && id.Document == null;
	}
}
