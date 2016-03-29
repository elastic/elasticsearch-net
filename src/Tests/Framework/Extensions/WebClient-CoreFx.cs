#if DOTNETCORE
using System;
using System.IO;
using System.Net.Http;

namespace Tests.Framework
{
	public class WebClient
	{
		public void DownloadFile(string url, string file)
		{
			using (var client = new HttpClient())
			{
				using (var request = new HttpRequestMessage(HttpMethod.Get, url))
				using (var contentStream = client.SendAsync(request).Result.Content.ReadAsStreamAsync().Result)
				using (var stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
					contentStream.CopyTo(stream);
			}
		}
	}

	public static class ZipFile
	{
		public static void ExtractToDirectory(string file, string directory)
		{
			throw new NotImplementedException("TODO");
		}
	}
}
#endif