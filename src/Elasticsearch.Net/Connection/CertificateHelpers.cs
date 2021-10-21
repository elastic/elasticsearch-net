// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Elasticsearch.Net
{
	internal static class CertificateHelpers
	{
		private const string _colon = ":";
		private const string _hyphen = "-";

		/// <summary>
		/// Returns the result of validating the fingerprint of a certificate against an expected fingerprint string.
		/// </summary>
		public static bool ValidateCertificateFingerprint(X509Certificate certificate, string expectedFingerprint)
		{
			string sha256Fingerprint;

#if DOTNETCORE && !NETSTANDARD2_0
			sha256Fingerprint = certificate.GetCertHashString(HashAlgorithmName.SHA256);
#else
			using var alg = SHA256.Create();

			var sha256FingerprintBytes = alg.ComputeHash(certificate.GetRawCertData());
			sha256Fingerprint = BitConverter.ToString(sha256FingerprintBytes);
#endif

			sha256Fingerprint = ComparableFingerprint(sha256Fingerprint);
			return expectedFingerprint.Equals(sha256Fingerprint, StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Cleans the fingerprint by removing colons and dashes so that a comparison can be made
		/// without such characters affecting the result.
		/// </summary>
		public static string ComparableFingerprint(string fingerprint)
		{
			var finalFingerprint = fingerprint;

			if (fingerprint.Contains(_colon))
				finalFingerprint = fingerprint.Replace(_colon, string.Empty);
			else if (fingerprint.Contains(_hyphen))
				finalFingerprint = fingerprint.Replace(_hyphen, string.Empty);

			return finalFingerprint;
		}
	}
}