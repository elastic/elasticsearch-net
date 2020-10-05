// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// https://raw.githubusercontent.com/dotnet/core-setup/master/src/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Windows.cs

#if NET461
using System.Runtime.InteropServices;

namespace Elastic.Transport.Extensions
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
				return RtlGetVersion(out osvi) == 0
					? $"Microsoft Windows {osvi.dwMajorVersion}.{osvi.dwMinorVersion}.{osvi.dwBuildNumber}"
					: null;
			}

			[StructLayout(LayoutKind.Sequential)]
			// ReSharper disable once MemberCanBePrivate.Global
			// ReSharper disable InconsistentNaming
			// ReSharper disable FieldCanBeMadeReadOnly.Global
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
			// ReSharper restore InconsistentNaming
			// ReSharper restore FieldCanBeMadeReadOnly.Global
		}
	}
}
#endif
