using System.Security.Cryptography.X509Certificates;

namespace Elasticsearch.Net
{
	internal static class X509CertificateExtensions
	{

		// https://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs,318
		internal static string GetCertHashString(this X509Certificate certificate)
		{
			var bytes = certificate.GetCertHash();
			return EncodeHexString(bytes);

		}
		// https://referencesource.microsoft.com/#mscorlib/system/security/util/hex.cs,1bfe838f662feef3
		// converts number to hex digit. Does not do any range checks.
		private static char HexDigit(int num) => (char)((num < 10) ? (num + '0') : (num + ('A' - 10)));

		private static string EncodeHexString(byte[] sArray)
		{
			string result = null;

			if (sArray == null) return result;
			var hexOrder = new char[sArray.Length * 2];

			for(int i = 0, j = 0; i < sArray.Length; i++) {
				var digit = (sArray[i] & 0xf0) >> 4;
				hexOrder[j++] = HexDigit(digit);
				digit = sArray[i] & 0x0f;
				hexOrder[j++] = HexDigit(digit);
			}
			result = new string(hexOrder);
			return result;
		}
	}
}
