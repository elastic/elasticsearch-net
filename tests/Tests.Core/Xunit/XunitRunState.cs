// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Concurrent;
using System.IO;

namespace Tests.Core.Xunit
{
	internal static class XunitRunState
	{
		public static ConcurrentBag<string> SeenDeprecations { get; } = new ConcurrentBag<string>();


		private static DirectoryInfo _tempDir;
		public static DirectoryInfo TemporaryDirectory
		{
			get
			{
				if (_tempDir != null) return _tempDir;
				var dir = Path.GetTempPath();
				//var testDir = Path.Combine(dir, "nest-tests-" + Guid.NewGuid().ToString("N").Substring(0, 8));
				var testDir = Path.Combine(dir, "nest-tests");
				_tempDir = Directory.CreateDirectory(testDir);
				return _tempDir;


			}
		}
	}
}
