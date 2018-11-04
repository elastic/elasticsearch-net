// this code contains a refactored version of DecodeRSAPrivateKey() found here http://www.jensign.com/opensslkey/opensslkey.cs
// Its license permits redistribution, the license is included here for reference.

/*
//
//OpenSSLKey
// .NET 2.0  OpenSSL Public & Private Key Parser
//
/*
Copyright (c) 2000  JavaScience Consulting,  Michel Gallant

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Elasticsearch.Net
{
//.NET core removed the setter for PrivateKey for X509Certificate, you'll have to manually convert to pfx/p12 or add the key to the machine store
#if !DOTNETCORE

	public class ClientCertificate
	{
		//https://tls.mbed.org/kb/cryptography/asn1-key-structures-in-der-and-pem
		private static RSACryptoServiceProvider DecodeRsaPrivateKey(byte[] privkey)
		{
			using (var mem = new MemoryStream(privkey))
			using (var binr = new BinaryReader(mem))
			{
				var twobytes = binr.ReadUInt16();
				switch (twobytes)
				{
					case 0x8130:
						binr.ReadByte(); //advance 1 byte
						break;
					case 0x8230:
						binr.ReadInt16(); //advance 2 bytes
						break;
					default:
						return null;
				}

				twobytes = binr.ReadUInt16();
				if (twobytes != 0x0102) return null; //version number

				var bt = binr.ReadByte();
				if (bt != 0x00) return null;

				// We make sure the provider typeString is compatible with RSA
				// ----
				// https://msdn.microsoft.com/en-us/library/system.security.cryptography.cspparameters.providertype(v=vs.110).aspx
				// https://msdn.microsoft.com/en-us/subscriptions/aa387431.aspx
				// https://blogs.msdn.microsoft.com/alejacma/2009/04/30/default-provider-typeString-for-cspparameters-has-changed/

				var serviceProvider = new RSACryptoServiceProvider(new CspParameters
				{
					Flags = CspProviderFlags.NoFlags,
					KeyContainerName = Guid.NewGuid().ToString(),
					ProviderType = 1
				});
				serviceProvider.ImportParameters(new RSAParameters
				{
					Modulus = ReadNext(binr),
					Exponent = ReadNext(binr),
					D = ReadNext(binr),
					P = ReadNext(binr),
					Q = ReadNext(binr),
					DP = ReadNext(binr),
					DQ = ReadNext(binr),
					InverseQ = ReadNext(binr)
				});
				return serviceProvider;
			}
		}

		private static byte[] ReadNext(BinaryReader br) => br.ReadBytes(GetSizeOfIntegerToReadNext(br));

		private static int GetSizeOfIntegerToReadNext(BinaryReader br)
		{
			var bt = br.ReadByte();
			if (bt != 0x02) return 0; //expect integer

			var count = 0;
			bt = br.ReadByte();
			switch (bt)
			{
				case 0x81:
					count = br.ReadByte(); // data size in next byte
					break;
				case 0x82:
					var highbyte = br.ReadByte();
					var lowbyte = br.ReadByte();
					byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
					count = BitConverter.ToInt32(modint, 0);
					break;
				default:
					count = bt; // we already have the data size
					break;
			}
			while (br.ReadByte() == 0x00) //remove high order zeros in data
				count -= 1;
			br.BaseStream.Seek(-1, SeekOrigin.Current); //last ReadByte wasn't a removed zero, so back up a byte
			return count;
		}

		private static byte[] ReadBytesFromPemFile(string fileContents, string typeString)
		{
			var header = $"-----BEGIN {typeString}-----";
			var footer = $"-----END {typeString}-----";
			var start = fileContents.IndexOf(header, StringComparison.Ordinal) + header.Length;
			var end = fileContents.IndexOf(footer, start, StringComparison.Ordinal) - start;
			var base64Der = fileContents.Substring(start, end);
			return Convert.FromBase64String(base64Der);
		}

		public static X509Certificate2 LoadWithPrivateKey(string publicCertificatePath, string privateKeyPath, string password)
		{
			var publicCert = File.ReadAllText(publicCertificatePath);
			var privateKey = File.ReadAllText(privateKeyPath);
			var certBuffer = ReadBytesFromPemFile(publicCert, "CERTIFICATE");
			var keyBuffer = ReadBytesFromPemFile(privateKey, "RSA PRIVATE KEY");
			var certificate = !string.IsNullOrEmpty(password) ? new X509Certificate2(certBuffer, password) : new X509Certificate2(certBuffer);
			var prov = DecodeRsaPrivateKey(keyBuffer);
			certificate.PrivateKey = prov;
			return certificate;
		}
	}
#endif
}
