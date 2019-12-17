using Elastic.Xunit.XunitPlumbing;
using Nest;

namespace Examples.Docs
{
	public class BulkPage : ExampleBase
	{
		[U]
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

		[U(Skip = "Example not implemented")]
		public void Line405()
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
					// TODO: missing
					//.Source(true)
					.Doc(new { field = "value" })
				)
				.Update<object>(u => u
					.Index("index1")
					.Id("4")
					// TODO: missing
					//.Source(true)
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
			{ ""doc"" : {""field"" : ""value""}, ""_source"": true}");
		}
	}
}
