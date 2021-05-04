// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Elastic.Transport;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	/// TODO ignored for now failing unrelated to current branch or target branch.
	/// currently this exception behaviour is low level client only and a refactor
	/// pulled in the high level serializer here.
	public class GithubIssue2052
	{
		private const string ObjectMessage = "My message";

		private static readonly object BulkHeader =
			new { index = new { _index = "myIndex", } };

		private readonly ElasticLowLevelClient _client;

		public GithubIssue2052()
		{
			var connectionSettings = TestClient.DisabledStreaming.ConnectionSettings;
			_client = new ElasticLowLevelClient(connectionSettings);
		}

		public void SingleThrownExceptionCanBeSerializedUsingSimpleJson()
		{
			var ex = GimmeACaughtException();

			var request = CreateRequest(ex);
			var postData = CreatePostData(ex);

			AssertRequestEquals(request, postData);
		}

		 public void MultipleThrownExceptionCanBeSerializedUsingSimpleJson()
		{
			var ex = GimmeAnExceptionWithInnerException();

			var request = CreateRequest(ex);
			var postData = CreatePostData(ex);

			AssertRequestEquals(request, postData);
		}

		private PostData CreatePostData(Exception e)
		{
			var postData = PostData.MultiJson(new List<object>
			{
				BulkHeader,
				new
				{
					message = "My message",
					exception = ExceptionJson(e).ToArray(),
				}
			});
			return postData;
		}

		private IEnumerable<object> ExceptionJson(Exception e)
		{
			var depth = 0;
			var maxExceptions = 20;
			do
			{
				var helpUrl = e.HelpLink;
				var stackTrace = e.StackTrace;
				var remoteStackTrace = string.Empty;
				var remoteStackIndex = string.Empty;
				var hresult = e.HResult;
				var source = e.Source;
				var className = string.Empty;

				yield return new
				{
					// ReSharper disable RedundantAnonymousTypePropertyName
					depth = depth,
					source = source,
					remoteStackIndex = remoteStackIndex,
					className = className,
					// ReSharper restore RedundantAnonymousTypePropertyName
					message = e.Message,
					stackTraceString = stackTrace,
					remoteStackTraceString = remoteStackTrace,
					hResult = hresult,
					helpURL = helpUrl,
					//ExceptionMethod = this.WriteStructuredExceptionMethod(exceptionMethod)
				};

				depth++;
				e = e.InnerException;
			} while (depth < maxExceptions && e != null);
		}

		// ReSharper disable once UnusedMember.Local
		private object WriteStructuredExceptionMethod(string exceptionMethodString)
		{
			if (string.IsNullOrWhiteSpace(exceptionMethodString)) return null;

			var args = exceptionMethodString.Split('\0', '\n');

			if (args.Length != 5) return null;

			var memberType = int.Parse(args[0], CultureInfo.InvariantCulture);
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
			var document = new Dictionary<string, object>
			{
				{ "message", ObjectMessage },
				{ "exception", ex }
			};


			var payload = new List<object>
			{
				BulkHeader,
				document
			};
			var response = _client.Bulk<BytesResponse>(PostData.MultiJson(payload));


			var request = Encoding.UTF8.GetString(response.RequestBodyInBytes);
			return request;
		}

		private void AssertRequestEquals(string request, PostData postData)
		{
			using (var ms = new MemoryStream())
			{
				postData.Write(ms, _client.Settings);
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
				var e = GimmeACaughtException();
				throw new Exception("Some exception", e);
			}
			catch (Exception e)
			{
				return e;
			}
		}
	}
}
