// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

// **********************************
// IMPORTANT: These tests have a secondary use as code snippets used in documentation.
// We disable formatting in sections of this file to ensure the correct indentation when tagged regions are
// included in the asciidocs. While hard to read, this formatting should be left as-is for docs generation.
// We also include using directives that are not required due to global using directives, but remain here
// so that can appear in the documentation.
// **********************************

#pragma warning disable CS0105 // Using directive appeared previously in this namespace
#pragma warning disable IDE0005 // Remove unnecessary using directives
//tag::using-directives[]
using System;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
//end::using-directives[]
using System.Text;
using System.Threading.Tasks;
using System.Linq;
#pragma warning restore IDE0005 // Remove unnecessary using directives
#pragma warning restore CS0105 // Using directive appeared previously in this namespace

namespace Tests.Documentation.Usage;

public class CrudExamplesTests
{
    [U]
    public async Task IndexingADocument()
    {
        // We're not really testing anything here. We have the code for inclusion in the docs and this ensures it compiles.

#pragma warning disable format
//tag::create-client[]
var client = new ElasticsearchClient(); // <1>
//end::create-client[]
#pragma warning restore format

        var jsonResponse = @"{""_index"":""my-tweet-index"",""_id"":""-7W0vIYBr7okFIdMVQuD"",""_version"":1,""result"":""created"",""_shards"":{""total"":2,""successful"":1,""failed"":0},""_seq_no"":4,""_primary_term"":3}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        // Needed for the test assertion as we should use the in memory connection and disable direct streaming.
        // We don't want to include those in the docs as it may mislead or confuse developers.
        // Any changes to the documentation code needs to be applied here also.
        client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 201)));

#pragma warning disable format
//tag::create-tweet[]
var tweet = new Tweet // <1>
{
    Id = 1, 
    User = "stevejgordon",
    PostDate = new DateTime(2009, 11, 15),
    Message = "Trying out the client, so far so good?"
};

var response = await client.IndexAsync(tweet, (IndexName)"my-tweet-index"); // <2>

if (response.IsValidResponse) // <3>
{
    Console.WriteLine($"Index document with ID {response.Id} succeeded."); // <4>
}
//end::create-tweet[]
#pragma warning restore format

        response.IsValidResponse.Should().BeTrue();
    }

    [U]
    public async Task GettingADocument()
    {
        var jsonResponse = @"{""_index"":""my-tweet-index"",""_id"":""1"",""_version"":1,""_seq_no"":0,""_primary_term"":1,""found"":true,""_source"":{""id"":1,""user"":""stevejgordon"",""postDate"":""2023-01-01T10:00:00"",""message"":""Test message""}}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        var client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 200)));

#pragma warning disable format
//tag::get-tweet[]
var response = await client.GetAsync<Tweet>(1, idx => idx.Index("my-tweet-index")); // <1>

if (response.IsValidResponse)
{
    var tweet = response.Source; // <2>
}
//end::get-tweet[]
#pragma warning restore format

        var tweetSource = response.Source;
        tweetSource.User.Should().Be("stevejgordon");
    }

    [U]
    public async Task SearchingForDocumentsFluent()
    {
        var jsonResponse = @"{""took"":9,""timed_out"":false,""_shards"":{""total"":1,""successful"":1,""skipped"":0,""failed"":0},""hits"":{""total"":{""value"":1,""relation"":""eq""},""max_score"":0.2876821,""hits"":[{""_index"":""my-tweet-index"",""_id"":""1"",""_score"":0.2876821,""_source"":{""id"":1,""user"":""stevejgordon"",""postDate"":""2023-01-01T10:00:00"",""message"":""Test message""}}]}}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        var client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 200)).EnableDebugMode());

#pragma warning disable format
//tag::search-tweet-fluent[]
var response = await client.SearchAsync<Tweet>(s => s // <1>
    .Index("my-tweet-index") // <2>
    .From(0)
    .Size(10)
    .Query(q => q
        .Term(t => t.User, "stevejgordon") // <3>
    )
);

if (response.IsValidResponse)
{
    var tweet = response.Documents.FirstOrDefault(); // <4>
}
//end::search-tweet-fluent[]
#pragma warning restore format

        response.Hits.Count().Should().Be(1);
    }

    [U]
    public async Task SearchingForDocumentsObject()
    {
        var jsonResponse = @"{""took"":9,""timed_out"":false,""_shards"":{""total"":1,""successful"":1,""skipped"":0,""failed"":0},""hits"":{""total"":{""value"":1,""relation"":""eq""},""max_score"":0.2876821,""hits"":[{""_index"":""my-tweet-index"",""_id"":""1"",""_score"":0.2876821,""_source"":{""id"":1,""user"":""stevejgordon"",""postDate"":""2023-01-01T10:00:00"",""message"":""Test message""}}]}}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        var client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 200)));

#pragma warning disable format
//tag::search-tweet-object-initializer[]
var request = new SearchRequest("my-tweet-index") // <1>
{
    From = 0,
    Size = 10,
    Query = new TermQuery("user") { Value = "stevejgordon" }
};

var response = await client.SearchAsync<Tweet>(request); // <2>

if (response.IsValidResponse)
{
    var tweet = response.Documents.FirstOrDefault(); 
}
//end::search-tweet-object-initializer[]
#pragma warning restore format

        response.Hits.Count().Should().Be(1);
    }

    [U]
    public async Task UpdatingADocument()
    {
        var jsonResponse = @"{""_index"":""my-tweet-index"",""_id"":""1"",""_version"":2,""result"":""updated"",""_shards"":{""total"":2,""successful"":1,""failed"":0},""_seq_no"":1,""_primary_term"":1}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        var client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 200)));

        var tweet = new Tweet
        {
            Id = 1,
            User = "stevejgordon",
            PostDate = new DateTime(2009, 11, 15),
            Message = "Trying out the client, so far so good?"
        };

#pragma warning disable format
//tag::update-tweet[]
tweet.Message = "This is a new message"; // <1>

var response = await client.UpdateAsync<Tweet, Tweet>("my-tweet-index", 1, u => u
    .Doc(tweet)); // <2>

if (response.IsValidResponse)
{
    Console.WriteLine("Update document succeeded.");
}
//end::update-tweet[]
#pragma warning restore format

        response.IsValidResponse.Should().BeTrue();
    }

    [U]
    public async Task DeletingADocument()
    {
        var jsonResponse = @"{""_index"":""my-tweet-index"",""_id"":""1"",""_version"":3,""result"":""deleted"",""_shards"":{""total"":2,""successful"":1,""failed"":0},""_seq_no"":2,""_primary_term"":1}";
        var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

        var client = new ElasticsearchClient(new ElasticsearchClientSettings(new InMemoryRequestInvoker(responseBytes, 200)));

#pragma warning disable format
//tag::delete-tweet[]
var response = await client.DeleteAsync("my-tweet-index", 1);

if (response.IsValidResponse)
{
    Console.WriteLine("Delete document succeeded.");
}
//end::delete-tweet[]
#pragma warning restore format

        response.IsValidResponse.Should().BeTrue();
    }
}

//tag::tweet-class[]
public class Tweet
{
    public int Id { get; set; } // <1>
    public string User { get; set; }
    public DateTime PostDate { get; set; }
    public string Message { get; set; }
}
//end::tweet-class[]
