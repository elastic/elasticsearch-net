// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

// Adapted from BenchmarkDotNet source https://github.com/dotnet/BenchmarkDotNet/blob/master/src/BenchmarkDotNet/Environments/Runtimes/CoreRuntime.cs
#region BenchmarkDotNet License https://github.com/dotnet/BenchmarkDotNet/blob/master/LICENSE.md
// The MIT License
// Copyright (c) 2013–2020.NET Foundation and contributors

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Diagnostics;
using System.Linq;
#if DOTNETCORE
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
#else
using Microsoft.Win32;
#endif

namespace Elasticsearch.Net
{
	/// <summary>
	/// Represents the current .NET Runtime version.
	/// </summary>
	internal sealed class RuntimeVersion
	{
		internal static readonly RuntimeVersion Default = new RuntimeVersion(new Version(0, 0, 0), false);

		private RuntimeVersion(Version version, bool isPrerelease)
		{
			var finalVersion = version;

			if (version.Minor == -1 || version.Build == -1)
			{
				finalVersion = new Version(version.Major, version.Minor > -1 ? version.Minor : 0, version.Build > -1 ? version.Build : 0);
			}

			Version = finalVersion;
			IsPrerelease = isPrerelease;
		}

		/// <summary>
		/// Determine the current runtime version.
		/// </summary>
		/// <returns>The identified <see cref="RuntimeVersion"/>.</returns>
		public static RuntimeVersion GetVersion() => GetRuntimeVersion();

		/// <summary>
		/// The version number of the runtime.
		/// </summary>
		public Version Version { get; }

		/// <summary>
		/// Indicates wheather the runtime is a prerelease version.
		/// </summary>
		public bool IsPrerelease { get; }

		public override string ToString() => IsPrerelease ? Version.ToString() + "p" : Version.ToString();

		private static RuntimeVersion GetRuntimeVersion() =>
#if !DOTNETCORE
			GetFullFrameworkRuntime();
#else
			GetNetCoreVersion();
#endif

#if DOTNETCORE
		public static RuntimeVersion GetNetCoreVersion()
		{
			// for .NET 5+ we can use Environment.Version
			if (Environment.Version.Major >= 5)
			{
				var isPrerelease = ContainsPrerelease(RuntimeInformation.FrameworkDescription);
				return new RuntimeVersion(Environment.Version, isPrerelease);
			}

			// next, try using file version info
			var systemPrivateCoreLib = FileVersionInfo.GetVersionInfo(typeof(object).Assembly.Location);
			if (TryGetVersionFromProductInfo(systemPrivateCoreLib.ProductVersion, systemPrivateCoreLib.ProductName, out var runtimeVersion))
			{
				return runtimeVersion;
			}

			var assembly = typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly;
			if (TryGetVersionFromAssemblyPath(assembly, out runtimeVersion))
			{
				return runtimeVersion;
			}

			// At this point, we can't identify whether this is a prerelease, but a version is better than nothing!

			var frameworkName = Assembly.GetEntryAssembly()?.GetCustomAttribute<TargetFrameworkAttribute>()?.FrameworkName;
			if (TryGetVersionFromFrameworkName(frameworkName, out runtimeVersion))
			{
				Console.WriteLine("NAME: " + runtimeVersion);
			}

			if (IsRunningInContainer
				&& (Version.TryParse(Environment.GetEnvironmentVariable("DOTNET_VERSION"), out var version)
					|| Version.TryParse(Environment.GetEnvironmentVariable("ASPNETCORE_VERSION"), out version)))
			{
				return new RuntimeVersion(version, false);
			}

			return RuntimeVersion.Default;
		}

		internal static bool TryGetVersionFromAssemblyPath(Assembly assembly, out RuntimeVersion runtimeVersion)
		{
			var assemblyPath = assembly.CodeBase.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
			var netCoreAppIndex = Array.IndexOf(assemblyPath, "Microsoft.NETCore.App");
			if (netCoreAppIndex > 0 && netCoreAppIndex < assemblyPath.Length - 2 && TryGetRuntimeVersion(assemblyPath[netCoreAppIndex + 1], out var version))
			{
				runtimeVersion = version;
				return true;
			}

			runtimeVersion = null;
			return false;
		}

		// NOTE: 5.0.1 FrameworkDescription returns .NET 5.0.1-servicing.20575.16, so we special case servicing as NOT prerelease
		private static bool ContainsPrerelease(string version) => version.Contains("-") && !version.Contains("-servicing");

		private static bool TryGetRuntimeVersion(string productVersion, out RuntimeVersion version)
		{
			var isPrerelase = ContainsPrerelease(productVersion);
			var versionPart = GetParsableVersionPart(productVersion);

			if (Version.TryParse(versionPart, out var parsedVersion))
			{
				version = new RuntimeVersion(parsedVersion, isPrerelase);
				return true;
			}

			version = null;
			return false;
		}

		// sample input:
		// 2.0: 4.6.26614.01 @BuiltBy: dlab14-DDVSOWINAGE018 @Commit: a536e7eec55c538c94639cefe295aa672996bf9b, Microsoft .NET Framework
		// 2.1: 4.6.27817.01 @BuiltBy: dlab14-DDVSOWINAGE101 @Branch: release/2.1 @SrcCode: https://github.com/dotnet/coreclr/tree/6f78fbb3f964b4f407a2efb713a186384a167e5c, Microsoft .NET Framework
		// 2.2: 4.6.27817.03 @BuiltBy: dlab14-DDVSOWINAGE101 @Branch: release/2.2 @SrcCode: https://github.com/dotnet/coreclr/tree/ce1d090d33b400a25620c0145046471495067cc7, Microsoft .NET Framework
		// 3.0: 3.0.0-preview8.19379.2+ac25be694a5385a6a1496db40de932df0689b742, Microsoft .NET Core
		// 5.0: 5.0.0-alpha1.19413.7+0ecefa44c9d66adb8a997d5778dc6c246ad393a7, Microsoft .NET Core
		private static bool TryGetVersionFromProductInfo(string productVersion, string productName, out RuntimeVersion version)
		{
			if (string.IsNullOrEmpty(productVersion) || string.IsNullOrEmpty(productName))
			{
				version = null;
				return false;
			}

			// yes, .NET Core 2.X has a product name == .NET Framework...
			if (productName.IndexOf(".NET Framework", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				const string releaseVersionPrefix = "release/";
				var releaseVersionIndex = productVersion.IndexOf(releaseVersionPrefix);
				if (releaseVersionIndex > 0)
				{
					var releaseVersion = productVersion.Substring(releaseVersionIndex + releaseVersionPrefix.Length);
					if (TryGetRuntimeVersion(releaseVersion, out version))
					{
						return true;
					}
				}
			}

			// matches .NET Core and also .NET 5+
			if (productName.IndexOf(".NET", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				var releaseVersion = GetParsableVersionPart(productVersion);
				if (TryGetRuntimeVersion(releaseVersion, out version))
				{
					return true;
				}
			}

			version = null;
			return false;
		}

		// sample input:
		// .NETCoreApp,Version=v2.0
		// .NETCoreApp,Version=v2.1
		internal static bool TryGetVersionFromFrameworkName(string frameworkName, out RuntimeVersion runtimeVersion)
		{
			const string versionPrefix = ".NETCoreApp,Version=v";
			if (!string.IsNullOrEmpty(frameworkName) && frameworkName.StartsWith(versionPrefix))
			{
				var frameworkVersion = GetParsableVersionPart(frameworkName.Substring(versionPrefix.Length));

				if (TryGetRuntimeVersion(frameworkVersion, out runtimeVersion))
				{
					return true;
				}
			}

			runtimeVersion = null;
			return false;
		}

		public static bool IsRunningInContainer => string.Equals(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), "true");

		// sample input:
		// for dotnet run: C:\Program Files\dotnet\shared\Microsoft.NETCore.App\2.1.12\
		// for dotnet publish: C:\Users\adsitnik\source\repos\ConsoleApp25\ConsoleApp25\bin\Release\netcoreapp2.0\win-x64\publish\
		internal static bool TryGetVersionFromRuntimeDirectory(string runtimeDirectory, out string version)
		{
			if (!string.IsNullOrEmpty(runtimeDirectory))
			{
				version = GetParsableVersionPart(new DirectoryInfo(runtimeDirectory).Name);
				return true;
			}

			version = null;
			return false;
		}

		private static string GetParsableVersionPart(string fullVersionName) =>
			new string(fullVersionName.TakeWhile(c => char.IsDigit(c) || c == '.').ToArray());
#endif

#if !DOTNETCORE
		private static RuntimeVersion GetFullFrameworkRuntime()
		{
			const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

			using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
			{
				if (ndpKey != null && ndpKey.GetValue("Release") != null)
				{
					var version = CheckFor45PlusVersion((int)ndpKey.GetValue("Release"));

					if (!string.IsNullOrEmpty(version) && Version.TryParse(version, out var finalVersion))
						return new RuntimeVersion(finalVersion, false);
				}
			}

			var fullName = RuntimeInformation.FrameworkDescription;
			var servicingVersion = new string(fullName.SkipWhile(c => !char.IsDigit(c)).ToArray());
			var servicingVersionRelease = MapToReleaseVersion(servicingVersion);

			if (Version.TryParse(servicingVersionRelease, out var runtimeVersion))
				return new RuntimeVersion(runtimeVersion, false);

			// We should never get here
			Debug.Assert(false, "Unable to determine runtime version for full framework");
			return Default;

			static string MapToReleaseVersion(string servicingVersion)
			{
				// the following code assumes that .NET 4.6.1 is the oldest supported version
				if (string.Compare(servicingVersion, "4.6.2") < 0)
					return "4.6.1";
				if (string.Compare(servicingVersion, "4.7") < 0)
					return "4.6.2";
				if (string.Compare(servicingVersion, "4.7.1") < 0)
					return "4.7";
				if (string.Compare(servicingVersion, "4.7.2") < 0)
					return "4.7.1";
				if (string.Compare(servicingVersion, "4.8") < 0)
					return "4.7.2";

				return "4.8.0"; // most probably the last major release of Full .NET Framework
			}

			// Checking the version using >= enables forward compatibility.
			static string CheckFor45PlusVersion(int releaseKey)
			{
				if (releaseKey >= 528040)
					return "4.8.0";
				if (releaseKey >= 461808)
					return "4.7.2";
				if (releaseKey >= 461308)
					return "4.7.1";
				if (releaseKey >= 460798)
					return "4.7";
				if (releaseKey >= 394802)
					return "4.6.2";
				if (releaseKey >= 394254)
					return "4.6.1";
				if (releaseKey >= 393295)
					return "4.6";
				if (releaseKey >= 379893)
					return "4.5.2";
				if (releaseKey >= 378675)
					return "4.5.1";
				if (releaseKey >= 378389)
					return "4.5.0";
				// This code should never execute. A non-null release key should mean
				// that 4.5 or later is installed.
				return null;
			}
		}
#endif
	}
}
