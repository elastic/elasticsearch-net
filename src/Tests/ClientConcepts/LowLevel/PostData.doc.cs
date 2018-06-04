using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Threading;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.ClientConcepts.LowLevel
{
	public class PostingData
	{
		/**[[post-data]]
         * === Post data
		 * The low level client allows you to post a `string` or `byte[]` array directly. On top of this,
		 * if you pass a collection of `string` or `object` they will be serialized
		 * using Elasticsearch's special bulk/multi format.
		 */
		private readonly string @string = "fromString";
		private readonly byte[] bytes = Utf8Bytes("fromByteArray");
		private List<string> collectionOfStrings = Enumerable.Range(0, 5).Select(i => i.ToString()).ToList();
		private List<object> collectionOfObjects;
		private object @object;

		private byte[] utf8BytesOfListOfStrings;
		private byte[] utf8BytesOfCollectionOfObjects;
		private byte[] utf8ObjectBytes;

		public PostingData()
		{
			this.@object = new { my_object = "value" };
			this.collectionOfObjects = Enumerable.Range(0, 5).Select(i => @object).Cast<object>().ToList();
			var json = "{\"my_object\":\"value\"}";
			this.utf8ObjectBytes = Utf8Bytes(json);
			this.utf8BytesOfListOfStrings = Utf8Bytes(string.Join("\n", collectionOfStrings) + "\n");
			this.utf8BytesOfCollectionOfObjects = Utf8Bytes(string.Join("\n", collectionOfObjects.Select(o=> json)) + "\n");
		}

		[U] public void ImplicitConversions()
		{
			/**[float]
			* === Implicit Conversion
			* Even though the argument for PostData on the low level client takes a `PostData<object>`,
			* You can rely on implicit conversion to abstract the notion of PostData completely.
			* You can implicitly convert from the following types
			*
			* - A `string`
			* - A collection of `string`
			* - An `object`
			* - A collection of `object`
			* - A `byte[]` array
			*
			* Let's demonstrate each with some assertive examples
			*/

			PostData<object> fromString = @string;
			PostData<object> fromByteArray = bytes;
			PostData<object> fromListOfString = collectionOfStrings;
			PostData<object> fromListOfObject = collectionOfObjects;
			PostData<object> fromObject = @object;

			fromByteArray.WrittenBytes.Should().BeSameAs(bytes); // <1> `WrittenBytes` will always be set if it originated from `byte[]`

			/** The `Type` property is representative of the original type from which post data is constructed */
			fromString.Type.Should().Be(PostType.LiteralString);
			fromByteArray.Type.Should().Be(PostType.ByteArray);
			fromListOfString.Type.Should().Be(PostType.EnumerableOfString);
			fromListOfObject.Type.Should().Be(PostType.EnumerableOfObject);
			fromObject.Type.Should().Be(PostType.Serializable);

			/** and passing a `PostData<object>` instance to a method that accepts `PostData<object>`
			 * as an argument does not wrap it again
			 */
			fromString = MethodThatAcceptsPostData(fromString);
			fromByteArray = MethodThatAcceptsPostData(fromByteArray);
			fromListOfString = MethodThatAcceptsPostData(fromListOfString);
			fromListOfObject = MethodThatAcceptsPostData(fromListOfObject);
			fromObject = MethodThatAcceptsPostData(fromObject);

			fromString.Type.Should().Be(PostType.LiteralString);
			fromByteArray.Type.Should().Be(PostType.ByteArray);
			fromListOfString.Type.Should().Be(PostType.EnumerableOfString);
			fromListOfObject.Type.Should().Be(PostType.EnumerableOfObject);
			fromObject.Type.Should().Be(PostType.Serializable);
		}

		//hide
		[U] public async Task WritesCorrectlyUsingBothLowAndHighLevelSettings()
		{
			await this.AssertOn(new ConnectionSettings());
			await this.AssertOn(new ConnectionConfiguration());
		}

		private async Task AssertOn(IConnectionConfigurationValues settings)
		{
			/**
			 * Each of the implicitly converted types behaves _slightly_ differently.
			 *
			 * For `string`, the UTF-8 bytes are sent in the request and the `WrittenBytes` property is assigned
			 * the bytes
			 */
			await Post(() => @string, writes: Utf8Bytes(@string), writtenBytesIsSet: true, settings: settings);

			/**
			 * Similarly, for `byte[]`, the bytes are sent verbatim and the `WrittenBytes` property is assigned
			 * the bytes
			 */
			await Post(() => bytes, writes: bytes, writtenBytesIsSet: true, settings: settings);

			/**
			 * When passing a collection of `string`, the client assumes that it's a collection of valid serialized json,
			 * so joins each with newline feeds, ensuring there is a trailing linefeed. As with `string` and `byte[]`,
			 * the `WrittenBytes` property is assigned the UTF-8 bytes of the collection of strings
			 */
			await Post(() => collectionOfStrings, writes: utf8BytesOfListOfStrings, writtenBytesIsSet: true, settings: settings);

			/**
			* When passing a collection of `object`, the client assumes that it's a collection of objects
			* that needs to be serialized individually to json and joined with newline feeds. As with the collection of strings, the client ensures that
			* there is a trailing linefeed.
			*/
			await Post(() => collectionOfObjects, writes: utf8BytesOfCollectionOfObjects, writtenBytesIsSet: false, settings: settings);

			/**
			 * In all other cases, Post data is serialized as is and `WrittenBytes` is not assigned
			 */
			await Post(() => @object, writes: utf8ObjectBytes, writtenBytesIsSet: false, settings: settings);

			/**
			* ==== Forcing WrittenBytes to be set
			*
			* If you want to maintain a copy of the request that went out, you can set `DisableDirectStreaming`  on `ConnectionConfiguration`.
			* In doing so, the serialized bytes are first written to a private `MemoryStream` so that the client can get hold of the serialized bytes
			*/
			settings = new ConnectionConfiguration().DisableDirectStreaming();

			await Post(() => collectionOfObjects, writes: utf8BytesOfCollectionOfObjects, writtenBytesIsSet: true, settings: settings);

			/** This behavior can also be observed when serializing a simple object using `DisableDirectStreaming` enabled
			 */
			await Post(() => @object, writes: utf8ObjectBytes, writtenBytesIsSet: true, settings: settings);
		}

		//hide
		private static async Task Post(Func<PostData<object>> postData, byte[] writes, bool writtenBytesIsSet, IConnectionConfigurationValues settings)
		{
			PostAssert(postData(), writes, writtenBytesIsSet, settings);
			await PostAssertAsync(postData(), writes, writtenBytesIsSet, settings);
		}

		//hide
		private static void PostAssert(PostData<object> postData, byte[] writes, bool storesBytes, IConnectionConfigurationValues settings)
		{
			using (var ms = new MemoryStream())
			{
				postData.Write(ms, settings);
				var sentBytes = ms.ToArray();
				sentBytes.Should().Equal(writes);
				if (storesBytes)
					postData.WrittenBytes.Should().NotBeNull();
				else
					postData.WrittenBytes.Should().BeNull();
			}
		}

		//hide
		private static async Task PostAssertAsync(PostData<object> postData, byte[] writes, bool storesBytes, IConnectionConfigurationValues settings)
		{
			using (var ms = new MemoryStream())
			{
				await postData.WriteAsync(ms, settings, default(CancellationToken));
				var sentBytes = ms.ToArray();
				sentBytes.Should().Equal(writes);
				if (storesBytes)
					postData.WrittenBytes.Should().NotBeNull();
				else
					postData.WrittenBytes.Should().BeNull();
			}
		}

		//hide
		private static byte[] Utf8Bytes(string s)
		{
			return string.IsNullOrEmpty(s) ? null : Encoding.UTF8.GetBytes(s);
		}

		//hide
		private PostData<object> MethodThatAcceptsPostData(PostData<object> postData) => postData;

	}
}
