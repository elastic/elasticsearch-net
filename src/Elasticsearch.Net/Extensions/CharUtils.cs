namespace Elasticsearch.Net.Extensions
{
	internal static class CharUtils
	{
		// https://referencesource.microsoft.com/#mscorlib/system/security/util/hex.cs,1bfe838f662feef3
		// converts number to hex digit. Does not do any range checks.
		internal static char HexDigit(int num) => (char)(num < 10 ? num + 48 : num + 55);
	}
}
