// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Examples.Docs
{
	public class BulkPage : ExampleBase
	{
		[U]
		[Description("docs/bulk.asciidoc:11")]
		public void Line11()
		{
			// tag::ae9ccfaa146731ab9176df90670db1c2[]
			var bulkResponse = client.Bulk(b => b
				.Index<object>(i => i
					.Index("test")
					.Id("1")
					.Document(new { field1 = "value1" })
				)
				.Delete<object>(d => d
					.Index("test")
					.Id("2")
				)
				.Create<object>(c => c
					.Index("test")
					.Id("3")
					.Document(new { field1 = "value3" })
				)
				.Update<object>(u => u
					.Index("test")
					.Id("1")
					.Doc(new { field2 = "value2" })
				)
			);
			// end::ae9ccfaa146731ab9176df90670db1c2[]

			bulkResponse.MatchesExample(@"POST _bulk
			{ ""index"" : { ""_index"" : ""test"", ""_id"" : ""1"" } }
			{ ""field1"" : ""value1"" }
			{ ""delete"" : { ""_index"" : ""test"", ""_id"" : ""2"" } }
			{ ""create"" : { ""_index"" : ""test"", ""_id"" : ""3"" } }
			{ ""field1"" : ""value3"" }
			{ ""update"" : {""_id"" : ""1"", ""_index"" : ""test""} }
			{ ""doc"" : {""field2"" : ""value2""} }");
		}

		[U]
		[Description("docs/bulk.asciidoc:545")]
		public void Line545()
		{
			// tag::8cd00a3aba7c3c158277bc032aac2830[]
			var bulkResponse = client.Bulk(b => b
				.Update<object>(u => u
					.Index("index1")
					.Id("1")
					.RetriesOnConflict(3)
					.Doc(new { field = "value" })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("0")
					.RetriesOnConflict(3)
					.Script(s => s
						.Source("ctx._source.counter += params.param1")
						.Lang("painless")
						.Params(d => d
							.Add("param1", 1)
						)
					)
					.Upsert(new { counter = 1 })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("2")
					.RetriesOnConflict(3)
					.Doc(new { field = "value" })
					.DocAsUpsert(true)
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("3")
					.Source(true)
					.Doc(new { field = "value" })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("4")
					.Source(true)
					.Doc(new { field = "value" })
				)
			);
			// end::8cd00a3aba7c3c158277bc032aac2830[]

			bulkResponse.MatchesExample(@"POST _bulk
			{ ""update"" : {""_id"" : ""1"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""doc"" : {""field"" : ""value""} }
			{ ""update"" : { ""_id"" : ""0"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""script"" : { ""source"": ""ctx._source.counter += params.param1"", ""lang"" : ""painless"", ""params"" : {""param1"" : 1}}, ""upsert"" : {""counter"" : 1}}
			{ ""update"" : {""_id"" : ""2"", ""_index"" : ""index1"", ""retry_on_conflict"" : 3} }
			{ ""doc"" : {""field"" : ""value""}, ""doc_as_upsert"" : true }
			{ ""update"" : {""_id"" : ""3"", ""_index"" : ""index1"", ""_source"" : true} }
			{ ""doc"" : {""field"" : ""value""} }
			{ ""update"" : {""_id"" : ""4"", ""_index"" : ""index1""} }
			{ ""doc"" : {""field"" : ""value""}, ""_source"": true}",
				e =>
				{
					e.ApplyBulkBodyChanges(objects =>
					{
						objects[7].Add("_source", true);
						(objects[8]["update"] as JObject).Add("_source", true);
					});
				});
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/bulk.asciidoc:567")]
		public void Line567()
		{
			// tag::1aa91d3d48140d6367b6cabca8737b8f[]
			var bulkResponse = client.Bulk(b => b
				.Update<object>(u => u
					.Index("index1")
					.Id("5")
					.Doc(new { my_field = "foo" })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("6")
					.Doc(new { my_field = "foo" })
				)
				.Create<object>(c => c
					.Id("7")
					.Index("index1")
					.Document(new { my_field = "foo" })
				)
			);
			// end::1aa91d3d48140d6367b6cabca8737b8f[]

			bulkResponse.MatchesExample(@"POST /_bulk
			{ ""update"": {""_id"": ""5"", ""_index"": ""index1""} }
			{ ""doc"": {""my_field"": ""foo""} }
			{ ""update"": {""_id"": ""6"", ""_index"": ""index1""} }
			{ ""doc"": {""my_field"": ""foo""} }
			{ ""create"": {""_id"": ""7"", ""_index"": ""index1""} }
			{ ""my_field"": ""foo"" }");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/bulk.asciidoc:646")]
		public void Line646()
		{
			// tag::bfdad8a928ea30d7cf60d0a0a6bc6e2e[]
			var bulkResponse = client.Bulk(b => b
				.FilterPath("items.*.error")
				.Update<object>(u => u
					.Index("index1")
					.Id("5")
					.Doc(new { my_field = "baz" })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("6")
					.Doc(new { my_field = "baz" })
				)
				.Update<object>(c => c
					.Id("7")
					.Index("index1")
					.Doc(new { my_field = "baz" })
				)
			);
			// end::bfdad8a928ea30d7cf60d0a0a6bc6e2e[]

			bulkResponse.MatchesExample(@"POST /_bulk?filter_path=items.*.error
			{ ""update"": {""_id"": ""5"", ""_index"": ""index1""} }
			{ ""doc"": {""my_field"": ""baz""} }
			{ ""update"": {""_id"": ""6"", ""_index"": ""index1""} }
			{ ""doc"": {""my_field"": ""baz""} }
			{ ""update"": {""_id"": ""7"", ""_index"": ""index1""} }
			{ ""doc"": {""my_field"": ""baz""} }");
		}
	}
}
