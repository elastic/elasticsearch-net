using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue2052
	{

		[U]
		public void BadSimpleJsonDeserialization()
		{

			var ex = this.GimmeACaughtException();

			var document = new Dictionary<string, object>{
				{ "message", "My message"},
				{ "exception", ex }
			};

			var pool = new StaticConnectionPool(new List<Uri> { new Uri("http://localhost:9200") });
			var memoryConnection = new InMemoryConnection();
			var connectionSettings = new ConnectionConfiguration(pool, memoryConnection)
				.DisableDirectStreaming();
			var client = new ElasticLowLevelClient(connectionSettings);


			var header = new { index = new { _index = "myIndex", _type = "myDocumentType" } };
			var payload = new List<object>{
				header,
				document
			};
			var response = client.Bulk<byte[]>(payload);


			var request = Encoding.UTF8.GetString(response.RequestBodyInBytes);
			var a = new AssemblyName(this.GetType().Assembly.FullName);

			var si = new SerializationInfo(ex.GetType(), new FormatterConverter());
			var sc = new StreamingContext();
			ex.GetObjectData(si, sc);
			PostData<object> postData = new List<object>
			{
				header,
				new
				{
					message = "My message",
					exception = new
					{
						ClassName = "System.Exception",
						Message = "Some exception",
						Source = "Tests",
						StackTraceString = ex.StackTrace,
						RemoteStackTraceString = si.GetString("RemoteStackTraceString"),
						RemoteStackIndex = 0,
						HResult = si.GetInt32("HResult"),
						HelpURL = si.GetString("HelpURL"),
						ExceptionMethod = new
						{
							Name = nameof(GimmeACaughtException),
							AssemblyName = a.Name,
							AssemblyVersion = a.Version.ToString(),
							AssemblyCulture = a.CultureName,
							ClassName = this.GetType().FullName,
							Signature = $"System.Exception {nameof(GimmeACaughtException)}()",
							MemberType = 8
						}
					},
				}
			};




			using (var ms = new MemoryStream())
			{
				postData.Write(ms, client.Settings);
				var expectedString = Encoding.UTF8.GetString(ms.ToArray());
				request.Should().Be(expectedString);
			}
		}

		private Exception GimmeACaughtException()
		{
			try
			{
				throw new Exception("Some exception");
			}
			catch (Exception e)
			{
				return e;
			}
		}

	}
}
