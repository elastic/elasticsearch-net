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

#if NET461
using System;
using System.Linq;
using System.Reflection;

namespace Nest
{
	internal static class RuntimeInformation
	{
		private static string _frameworkDescription;
		private static string _osDescription;

		public static string FrameworkDescription
		{
			get
			{
				if (_frameworkDescription == null)
				{
					var assemblyFileVersionAttribute =
						((AssemblyFileVersionAttribute[])Attribute.GetCustomAttributes(
							typeof(object).Assembly,
							typeof(AssemblyFileVersionAttribute)))
						.OrderByDescending(a => a.Version)
						.First();
					_frameworkDescription = $".NET Framework {assemblyFileVersionAttribute.Version}";
				}
				return _frameworkDescription;
			}
		}

		public static string OSDescription
		{
			get
			{
				if (_osDescription == null)
				{
					var platform = (int)Environment.OSVersion.Platform;
					var isWindows = platform != 4 && platform != 6 && platform != 128;
					if (isWindows)
						_osDescription = NativeMethods.Windows.RtlGetVersion() ?? "Microsoft Windows";
					else
						_osDescription = Environment.OSVersion.VersionString;
				}
				return _osDescription;
			}
		}
	}
}
#endif
