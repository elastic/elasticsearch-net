// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Elasticsearch.Net
{
	/// <summary>
	/// Methods for working with <see cref="SecureString"/>
	/// </summary>
	public static class SecureStrings
	{
		/// <summary>
		/// Creates a string from a secure string
		/// </summary>
		public static string CreateString(this SecureString secureString)
		{
			if (secureString == null || secureString.Length == 0)
				return string.Empty;

			var num = IntPtr.Zero;

			try
			{
				num = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				return Marshal.PtrToStringUni(num);
			}
			finally
			{
				if (num != IntPtr.Zero)
					Marshal.ZeroFreeGlobalAllocUnicode(num);
			}
		}

		/// <summary>
		/// Creates a secure string from a string
		/// </summary>
		public static SecureString CreateSecureString(this string plainString)
		{
			var secureString = new SecureString();

			if (plainString == null)
				return secureString;

			foreach (var ch in plainString)
				secureString.AppendChar(ch);

			return secureString;
		}
	}
}
