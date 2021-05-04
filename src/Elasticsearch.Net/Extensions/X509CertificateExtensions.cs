// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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

			if (sArray == null) return result;

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
