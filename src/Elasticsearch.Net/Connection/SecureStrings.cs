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
