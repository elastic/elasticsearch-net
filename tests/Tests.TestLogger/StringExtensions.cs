using System;

namespace Tests.Core.VsTest
{
	internal static class StringExtensions
	{
		public static string CreateRelativePath(this string filePath)
		{
			var file = new Uri(filePath, UriKind.Absolute);

			var relativeFile = Uri.UnescapeDataString(PrettyLogger.RootUri.MakeRelativeUri(file).ToString());
			return relativeFile;
		}

	}
}
