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

using System.Security.Cryptography.X509Certificates;

namespace Elasticsearch.Net.Extensions
{
	internal static class X509CertificateExtensions
	{
		// https://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs,318
		internal static string GetCertHashString(this X509Certificate certificate)
		{
			var bytes = certificate.GetCertHash();
			return EncodeHexString(bytes);
		}

		private static string EncodeHexString(byte[] sArray)
		{
			string result = null;

			if (sArray == null) return null;

			var hexOrder = new char[sArray.Length * 2];

			for (int i = 0, j = 0; i < sArray.Length; i++)
			{
				var digit = (sArray[i] & 0xf0) >> 4;
				hexOrder[j++] = CharUtils.HexDigit(digit);
				digit = sArray[i] & 0x0f;
				hexOrder[j++] = CharUtils.HexDigit(digit);
			}
			result = new string(hexOrder);
			return result;
		}
	}
}
