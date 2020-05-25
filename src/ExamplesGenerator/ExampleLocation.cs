// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;

namespace ExamplesGenerator
{
	public static class ExampleLocation
	{
		private static string _root;

		public static DirectoryInfo ExamplesAsciiDocDir { get; } = new DirectoryInfo($@"{Root}../../examples");
		public static DirectoryInfo ExamplesCSharpProject { get; } = new DirectoryInfo($@"{Root}../../tests/Examples");

		private static string Root
		{
			get
			{
				if (_root != null) return _root;

				var directoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

				var runningAsNetCore =
					directoryInfo.Name == "ExamplesGenerator" &&
					directoryInfo.Parent != null &&
					directoryInfo.Parent.Name == "src";

				_root = runningAsNetCore ? "" : @"../../../";
				return _root;
			}
		}
	}
}
