namespace Nest
{
	internal static class FieldLookupExtensions
	{
		internal static bool IsConditionless(this IFieldLookup fieldLookup) =>
			fieldLookup == null ||
			fieldLookup.Id == null ||
			fieldLookup.Index == null ||
			fieldLookup.Type == null ||
			fieldLookup.Path == null;
	}
}
