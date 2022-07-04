// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Tests.Core.Xunit;
// using VerifyXunit;

// namespace Tests.Serialization;

// [UsesVerify]
// [SystemTextJsonOnly]
// public class DictionarySerializationTests : SerializerTestBase
// {
// 	[U]
// 	public async Task SerializesDictionaryWithBuiltInTypeKey()
// 	{
// 		var dictionary = new Dictionary<int, string>() { { 1, "text" } };

// 		var serialisedJson = await SerializeAndGetJsonStringAsync(dictionary);
// 	}

// 	[U]
// 	public async Task SerializesDictionaryWithFieldKey()
// 	{
// 		var dictionary = new Dictionary<Field, string>() { { "field-name", "text" } };

// 		var serialisedJson = await SerializeAndGetJsonStringAsync(dictionary);
// 	}

// 	[U]
// 	public async Task SerializesKvpWithFieldKey()
// 	{
// 		var kvp = new KeyValuePair<Field, string>("field-name", "text");

// 		var serialisedJson = await SerializeAndGetJsonStringAsync(kvp);
// 	}

// 	private class WithDictionaryKey
// 	{
// 		public Dictionary<Field, string> Data { get; set; }
// 	}
// }
