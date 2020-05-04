// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
