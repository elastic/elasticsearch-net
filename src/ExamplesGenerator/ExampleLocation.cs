/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
