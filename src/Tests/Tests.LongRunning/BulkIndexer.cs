using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.LongRunning.Models;
using IndexOptions = Tests.LongRunning.CommandLineArgs.IndexOptions;
using TaskStatus = System.Threading.Tasks.TaskStatus;

namespace Tests.LongRunning
{
    public class BulkIndexer
    {
        private const string PostsIndex = "posts";
        private const string UsersIndex = "users";
        private readonly IElasticClient _client;

        public BulkIndexer(IndexOptions opts)
        {
            var node = new SingleNodeConnectionPool(opts.ElasticsearchUrl);
            var settings = new ConnectionSettings(node)
                .RequestTimeout(TimeSpan.FromMinutes(10))
                .DefaultMappingFor(new[]
                {
                    new ClrTypeMapping(typeof(Post)) {IndexName = PostsIndex},
                    new ClrTypeMapping(typeof(Question)) {IndexName = PostsIndex, RelationName = "question"},
                    new ClrTypeMapping(typeof(Answer)) {IndexName = PostsIndex},
                    new ClrTypeMapping(typeof(User)) {IndexName = UsersIndex}
                })
                .OnRequestCompleted(response =>
                {
                    if (!response.Success)
                        Console.Error.WriteLine(response.DebugInformation);
                });

            if (!string.IsNullOrEmpty(opts.UserName) && !string.IsNullOrEmpty(opts.Password))
                settings.BasicAuthentication(opts.UserName, opts.Password);

            if (opts.AllowInsecure)
                settings.ServerCertificateValidationCallback(CertificateValidations.AllowAll);

            _client = new ElasticClient(settings);
        }

        public void IndexUsers(string usersPath, string badgesPath)
        {
            CreateUsersIndexIfNotExists();

            _client.Indices.UpdateSettings(UsersIndex, u => u
                .IndexSettings(i => i
                    .RefreshInterval("-1")
                )
            );

            var size = 1000;
            var seenPages = 0;
            var indexedDocs = 0;
            var totalDocs = 0;
            var handle = new ManualResetEvent(false);

            var users = StackOverflowData.GetUsers(usersPath);
            var observableBulk = _client.BulkAll(users, f => f
                .MaxDegreeOfParallelism(16)
                .BackOffTime(TimeSpan.FromSeconds(10))
                .BackOffRetries(2)
                .Size(size)
                .RefreshOnCompleted()
                .Index(UsersIndex)
            );

            Exception exception = null;
            var bulkObserver = new BulkAllObserver(
                onError: e =>
                {
                    exception = e;
                    handle.Set();
                },
                onCompleted: () => handle.Set(),
                onNext: b =>
                {
                    Interlocked.Add(ref indexedDocs, b.Items.Count(i => i.IsValid));
                    Interlocked.Add(ref totalDocs, b.Items.Count);
                    Interlocked.Increment(ref seenPages);
                    Log.WriteLine($"indexed users page {seenPages}, {indexedDocs} out of {totalDocs}");
                }
            );

            var stopWatch = Stopwatch.StartNew();
            observableBulk.Subscribe(bulkObserver);
            handle.WaitOne();

            if (exception != null)
                throw exception;

            Log.WriteLine($"Time taken to index users: {stopWatch.Elapsed}");

            // update user badges
            seenPages = 0;
            indexedDocs = 0;
            totalDocs = 0;
            handle = new ManualResetEvent(false);

            var badgeMetas = StackOverflowData.GetBadgeMetas(badgesPath);

            var observableBadgeBulk = _client.BulkAll(badgeMetas, f => f
                .Index<User>()
                .MaxDegreeOfParallelism(8)
                .Size(size)
                .BufferToBulk((bulk, badges) =>
                {
                    foreach (var badge in badges)
                        bulk.Update<User>(u => u
                            .Script(s => s
                                .Source(@"if (ctx._source.badges == null) { 
                                                    ctx._source.badges = [params.badge]; 
                                                } else if (ctx._source.badges.any(b -> b.name == params.badge.name) == false) { 
                                                    ctx._source.badges.add(params.badge); 
                                                }")
                                .Params(d => d
                                    .Add("badge", badge.Badge)
                                )
                            )
                            .Id(badge.UserId)
                            .RetriesOnConflict(10)
                        );
                })
                .RefreshOnCompleted()
            );

            bulkObserver = new BulkAllObserver(
                onError: e =>
                {
                    exception = e;
                    handle.Set();
                },
                onCompleted: () => handle.Set(),
                onNext: b =>
                {
                    Interlocked.Add(ref indexedDocs, b.Items.Count(i => i.IsValid));
                    Interlocked.Add(ref totalDocs, b.Items.Count);
                    Interlocked.Increment(ref seenPages);
                    Log.WriteLine($"indexed badges page {seenPages}, {indexedDocs} out of {totalDocs}");
                }
            );

            stopWatch.Restart();
            observableBadgeBulk.Subscribe(bulkObserver);
            handle.WaitOne();

            if (exception != null)
                throw exception;

            Log.WriteLine($"Time taken to index badges: {stopWatch.Elapsed}");

            _client.Indices.UpdateSettings(UsersIndex, u => u
                .IndexSettings(i => i
                    .RefreshInterval("30s")
                )
            );
        }

        private void CreateUsersIndexIfNotExists()
        {
            if (!_client.Indices.Exists(UsersIndex).Exists)
            {
                var createIndexResponse = _client.Indices.Create(UsersIndex, c => c
                    .Settings(s => s
                        .NumberOfShards(3)
                        .NumberOfReplicas(0)
                        .Analysis(a => a
                            .Analyzers(an => an
                                .Custom("html", ca => ca
                                    .CharFilters("html_strip")
                                    .Tokenizer("standard")
                                    .Filters("lowercase", "stop")
                                )
                            )
                        )
                    )
                    .Map<User>(mm => mm
                        .AutoMap()
                        .Properties(p => p
                            .Text(s => s
                                .Name(n => n.AboutMe)
                                .Analyzer("html")
                                .SearchAnalyzer("standard")
                            )
                            .Keyword(s => s
                                .Name(n => n.ProfileImageUrl)
                            )
                            .Keyword(s => s
                                .Name(n => n.WebsiteUrl)
                            )
                            .Object<Badge>(o => o
                                .Name(n => n.Badges)
                                .AutoMap()
                                .Properties(op => op
                                    .Keyword(s => s
                                        .Name(n => n.Name)
                                    )
                                    .Keyword(s => s
                                        .Name(n => n.Class)
                                    )
                                )
                            )
                        )
                    )
                );

                if (!createIndexResponse.IsValid)
                    throw new Exception(
                        $"invalid response creating users index: {createIndexResponse.DebugInformation}");
            }
        }

        public void UpdateAnswersWithQuestionTags(string path, int size)
        {
            if (!_client.Indices.Exists(PostsIndex).Exists)
                throw new Exception($"{PostsIndex} index does not exist. You must run the 'posts' command to index posts first");

            var postIdsAndTags = StackOverflowData.GetPostTagsWithAnswers(path);
            long totalAnswersUpdated = 0;
            var totalQuestions = 0;
            var stopWatch = Stopwatch.StartNew();

            foreach (var batch in postIdsAndTags.Batch(size))
            {
                var tasks = batch.Select(b =>
                {
                    var (id, tags) = b;
                    return _client.UpdateByQueryAsync<Answer>(u => u
                        .Routing(id)
                        .Query(q => +q
                            .ParentId(p => p
                                .Id(id)
                                .Type<Answer>()
                            )
                        )
                        .Conflicts(Conflicts.Proceed)
                        .Index(PostsIndex)
                        .Timeout(TimeSpan.FromMinutes(1))
                        .WaitForCompletion()
                        .Script(ss => ss
                            .Source(@"if (ctx._source.tags == null) { 
                                        ctx._source.tags = params.tags; 
                                    } else { 
                                        ctx.op = ""noop"";
                                    }")
                            .Params(p => p
                                .Add("tags", tags)
                            )
                        )
                    );
                }).ToArray();

                var task = Task.WhenAll(tasks);
                task.Wait();

                if (task.Status == TaskStatus.Faulted)
                    throw task.Exception.Flatten();

                totalQuestions += tasks.Length;
                totalAnswersUpdated += tasks.Sum(t => t.Result.Updated);
                Log.WriteLine($"Updated {totalAnswersUpdated} answers for {totalQuestions} questions");
            }

            Log.WriteLine($"time taken to update answers: {stopWatch.Elapsed}");
        }

        public void IndexPosts(string path)
        {
            CreatePostsIndexIfNotExists();

            _client.Indices.UpdateSettings(PostsIndex, u => u
                .IndexSettings(i => i
                    .RefreshInterval("-1")
                )
            );

            var handle = new ManualResetEvent(false);
            var size = 1000;
            var posts = StackOverflowData.GetPosts(path);
            var observableBulk = _client.BulkAll(posts, f => f
                .MaxDegreeOfParallelism(Environment.ProcessorCount * 2)
                .BackOffTime(TimeSpan.FromSeconds(10))
                .BackOffRetries(2)
                .Size(size)
                .BufferToBulk((bulk, buffer) =>
                {
                    foreach (var post in buffer)
                        if (post is Question question)
                        {
                            var item = new BulkIndexOperation<Question>(question);
                            bulk.AddOperation(item);
                        }
                        else
                        {
                            var answer = (Answer) post;
                            var item = new BulkIndexOperation<Answer>(answer);
                            bulk.AddOperation(item);
                        }
                })
                .RefreshOnCompleted()
                .Index(PostsIndex)
            );

            var seenPages = 0;
            var indexedDocs = 0;
            var totalDocs = 0;

            Exception exception = null;
            var bulkObserver = new BulkAllObserver(
                onError: e =>
                {
                    exception = e;
                    handle.Set();
                },
                onCompleted: () => handle.Set(),
                onNext: b =>
                {
                    Interlocked.Add(ref indexedDocs, b.Items.Count(i => i.IsValid));
                    Interlocked.Add(ref totalDocs, b.Items.Count);
                    Interlocked.Increment(ref seenPages);
                    Log.WriteLine($"indexed page {seenPages} of questions and answers, {indexedDocs} out of {totalDocs}");
                }
            );

            var stopWatch = Stopwatch.StartNew();
            observableBulk.Subscribe(bulkObserver);
            handle.WaitOne();

            if (exception != null)
                throw exception;

            Log.WriteLine($"time taken to index posts: {stopWatch.Elapsed}");

            _client.Indices.UpdateSettings(PostsIndex, u => u
                .IndexSettings(i => i
                    .RefreshInterval("30s")
                )
            );
        }

        private void CreatePostsIndexIfNotExists()
        {
            if (!_client.Indices.Exists(PostsIndex).Exists)
            {
                var characterFilterMappings = CreateCharacterFilterMappings();

                var createIndexResponse = _client.Indices.Create(PostsIndex, c => c
                    .Settings(s => s
                        .NumberOfShards(3)
                        .NumberOfReplicas(0)
                        .Analysis(a => a
                            .CharFilters(cf => cf
                                .Mapping("programming_language", mca => mca
                                    .Mappings(characterFilterMappings)
                                )
                            )
                            .Analyzers(an => an
                                .Custom("html", ca => ca
                                    .CharFilters("html_strip", "programming_language")
                                    .Tokenizer("standard")
                                    .Filters("lowercase", "stop")
                                )
                                .Custom("expand", ca => ca
                                    .CharFilters("programming_language")
                                    .Tokenizer("standard")
                                    .Filters("lowercase", "stop")
                                )
                            )
                        )
                    )
                    .Map<Post>(u => u
                        .RoutingField(r => r.Required())
                        .AutoMap<Question>()
                        .AutoMap<Answer>()
                        .SourceField(s => s
                            .Excludes(new[] {"titleSuggest"})
                        )
                        .Properties<Question>(p => p
                            .Join(j => j
                                .Name(f => f.ParentId)
                                .Relations(r => r
                                    .Join<Question, Answer>()
                                )
                            )
                            .Text(s => s
                                .Name(n => n.Title)
                                .Analyzer("expand")
                                .Norms(false)
                                .Fields(f => f
                                    .Keyword(ss => ss
                                        .Name("raw")
                                    )
                                )
                            )
                            .Keyword(s => s
                                .Name(n => n.OwnerDisplayName)
                            )
                            .Keyword(s => s
                                .Name(n => n.LastEditorDisplayName)
                            )
                            .Keyword(s => s
                                .Name(n => n.Tags)
                            )
                            .Keyword(s => s
                                .Name(n => n.Type)
                            )
                            .Text(s => s
                                .Name(n => n.Body)
                                .Analyzer("html")
                                .SearchAnalyzer("expand")
                            )
                            .Completion(co => co
                                .Name(n => n.TitleSuggest)
                            )
                        )
                    )
                );

                if (!createIndexResponse.IsValid)
                    throw new Exception(
                        $"invalid response creating posts index: {createIndexResponse.DebugInformation}");
            }
        }

        private static IList<string> CreateCharacterFilterMappings()
        {
            var mappings = new List<string>();
            foreach (var c in new[] {"c", "f", "m", "j", "s", "a", "k", "t"})
            {
                mappings.Add($"{c}# => {c}sharp");
                mappings.Add($"{c.ToUpperInvariant()}# => {c}sharp");
            }

            foreach (var c in new[] {"g", "m", "c", "s", "a", "d"})
            {
                mappings.Add($"{c}++ => {c}plusplus");
                mappings.Add($"{c.ToUpperInvariant()}++ => {c}plusplus");
            }

            return mappings;
        }
    }
}
