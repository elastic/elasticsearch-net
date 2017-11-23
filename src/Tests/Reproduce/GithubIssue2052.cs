using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
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
		private const string _objectMessage = "My message";
		private static object _bulkHeader =
			 new { index = new { _index = "myIndex", _type = "myDocumentType" } };
		private readonly ElasticLowLevelClient _client;

		public GithubIssue2052()
		{

			var pool = new StaticConnectionPool(new List<Uri> { new Uri("http://localhost:9200") });
			var memoryConnection = new InMemoryConnection();
			var connectionSettings = new ConnectionConfiguration(pool, memoryConnection)
				.DisableDirectStreaming();
			this._client = new ElasticLowLevelClient(connectionSettings);
		}


		[U] public void SingleThrownExceptionCanBeSerializedUsingSimpleJson()
		{

			var ex = this.GimmeACaughtException();

			var request = this.CreateRequest(ex);
			var postData = this.CreatePostData(ex);

			this.AssertRequestEquals(request, postData);
		}

		[U] public void MultipleThrownExceptionCanBeSerializedUsingSimpleJson()
		{

			var ex = this.GimmeAnExceptionWithInnerException();

			var request = this.CreateRequest(ex);
			var postData = this.CreatePostData(ex);

			this.AssertRequestEquals(request, postData);
		}

		private PostData CreatePostData(Exception e)
		{
			PostData postData = PostData.MultiJson(new List<object>
			{
				_bulkHeader,
				new
				{
					message = "My message",
					exception = this.ExceptionJson(e).ToArray(),
				}
			});
			return postData;
		}

		private IEnumerable<object> ExceptionJson(Exception e)
		{
			int depth = 0;
			int maxExceptions = 20;
			do
			{
#if !DOTNETCORE
				var si = new SerializationInfo(e.GetType(), new FormatterConverter());
				var sc = new StreamingContext();
				e.GetObjectData(si, sc);

				var helpUrl = si.GetString("HelpURL");
				var stackTrace = si.GetString("StackTraceString");
				var remoteStackTrace = si.GetString("RemoteStackTraceString");
				var remoteStackIndex = si.GetInt32("RemoteStackIndex");
				var exceptionMethod = si.GetString("ExceptionMethod");
				var hresult = si.GetInt32("HResult");
				var source = si.GetString("Source");
				var className = si.GetString("ClassName");
#else
				var helpUrl = e.HelpLink;
				var stackTrace = e.StackTrace;
				var remoteStackTrace = string.Empty;
				var remoteStackIndex = string.Empty;
				var exceptionMethod = string.Empty;
				var hresult = e.HResult;
				var source = e.Source;
				var className = string.Empty;
#endif

				yield return new
				{
					Depth = depth,
					ClassName = className,
					Message = e.Message,
					Source = source,
					StackTraceString = stackTrace,
					RemoteStackTraceString = remoteStackTrace,
					RemoteStackIndex = remoteStackIndex,
					HResult = hresult,
					HelpURL = helpUrl,
#if !DOTNETCORE && !__MonoCS__
					ExceptionMethod = this.WriteStructuredExceptionMethod(exceptionMethod)
#endif
				};
				depth++;
				e = e.InnerException;

			}
			while (depth < maxExceptions && e != null);
		}

		private object WriteStructuredExceptionMethod(string exceptionMethodString)
		{
			if (string.IsNullOrWhiteSpace(exceptionMethodString)) return null;

			var args = exceptionMethodString.Split('\0', '\n');

			if (args.Length != 5) return null;

			var memberType = Int32.Parse(args[0], CultureInfo.InvariantCulture);
			var name = args[1];
			var assemblyName = args[2];
			var className = args[3];
			var signature = args[4];
			var an = new AssemblyName(assemblyName);
			return new
			{
				Name = name,
				AssemblyName = an.Name,
				AssemblyVersion = an.Version.ToString(),
				AssemblyCulture = an.CultureName,
				ClassName = className,
				Signature = signature,
				MemberType = memberType,
			};
		}

		private string CreateRequest(Exception ex)
		{
			var document = new Dictionary<string, object>{
				{ "message", _objectMessage},
				{ "exception", ex }
			};


			var payload = new List<object>{
				_bulkHeader,
				document
			};
			var response = this._client.Bulk<byte[]>(PostData.MultiJson(payload));


			var request = Encoding.UTF8.GetString(response.RequestBodyInBytes);
			return request;
		}

		private void AssertRequestEquals(string request, PostData postData)
		{
			using (var ms = new MemoryStream())
			{
				postData.Write(ms, this._client.Settings);
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


		private Exception GimmeAnExceptionWithInnerException()
		{
			try
			{
				var e = this.GimmeACaughtException();
				throw new Exception("Some exception", e);
			}
			catch (Exception e)
			{
				return e;
			}
		}
	}
}
