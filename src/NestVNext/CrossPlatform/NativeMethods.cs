// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// https://raw.githubusercontent.com/dotnet/core-setup/master/src/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Windows.cs

using System.Runtime.InteropServices;

namespace Nest
{
	internal static class NativeMethods
	{
		public static class Windows
		{
			// This call avoids the shimming Windows does to report old versions
			[DllImport("ntdll")]
			private static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);

			internal static string RtlGetVersion()
			{
				var osvi = new RTL_OSVERSIONINFOEX();
				osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
				if (RtlGetVersion(out osvi) == 0)
					return $"Microsoft Windows {osvi.dwMajorVersion}.{osvi.dwMinorVersion}.{osvi.dwBuildNumber}";

				return null;
			}

			[StructLayout(LayoutKind.Sequential)]
			// ReSharper disable once InconsistentNaming
			internal struct RTL_OSVERSIONINFOEX
			{
				internal uint dwOSVersionInfoSize;
				internal uint dwMajorVersion;
				internal uint dwMinorVersion;
				internal uint dwBuildNumber;
				internal uint dwPlatformId;

				[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
				internal string szCSDVersion;
			}
		}
	}
}
