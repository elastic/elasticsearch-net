namespace Elastic.SourceGenerator.Roslyn.Helpers;

internal static class CommonHelpers
{
	public static int CombineHashCodes(int h1, int h2)
	{
		// RyuJIT optimizes this to use the ROL instruction.
		// Related GitHub pull request: https://github.com/dotnet/coreclr/pull/1830.
		var rol5 = ((uint)h1 << 5) | ((uint)h1 >> 27);
		return ((int)rol5 + h1) ^ h2;
	}
}
