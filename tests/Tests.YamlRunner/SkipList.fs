// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

module Tests.YamlRunner.Skips

type SkipSection = All | Section of string | Sections of string list

type SkipFile = SkipFile of string 

let SkipList = dict<SkipFile,SkipSection> [

    // TODO: Needs investigation. "Setting upgrade mode to disabled from enabled" runs in isolation but not with the entire test file
    SkipFile "ml/set_upgrade_mode.yml", All
    SkipFile "ml/inference_crud.yml", Section "Test update model alias with model id referring to missing model"
    SkipFile "ml/start_stop_datafeed.yml", All
    SkipFile "ml/post_data.yml", All
    
    // These send empty strings for required parameters
    // TODO i THINK this is now supported
    SkipFile "ml/explain_data_frame_analytics.yml", Section "Test neither job id nor body"
    
    // funny looking dispatch /_security/privilege/app?name
    SkipFile "privileges/10_basic.yml", All

    // Sets a dictionary to null, we need to see if we can backport this from master
    SkipFile "search.aggregation/240_max_buckets.yml", All
    SkipFile "search.aggregation/180_percentiles_tdigest_metric.yml", Section "Invalid params test"
    SkipFile "search.aggregation/190_percentiles_hdr_metric.yml", Section "Invalid params test"

    // Test looks for "testnode.crt", but "ca.crt" is returned first
    SkipFile "ssl/10_basic.yml", Section "Test get SSL certificates"
    
    // Uses variables in strings e.g Bearer ${token} we can not due variable substitution in string yet
    SkipFile "token/10_basic.yml", All
    
    // We don't expose the overload with just id, warrants investigation in the code generator
    SkipFile "ml/delete_forecast.yml", Section "Test delete all where no forecast_id is set"
    
    SkipFile "rollup/put_job.yml", Section "Test put job with templates"
    
    SkipFile "change_password/11_token.yml", Section "Test user changing their password authenticating with token not allowed"

    SkipFile "change_password/10_basic.yml", Sections [
        // Changing password locks out tests
        "Test user changing their own password"
        // Uses variables in strings e.g Bearer ${token} we can not due variable substitution in string yet
        "Test user changing their password authenticating with token not allowed"
    ] 
    
    // Missing refreshes in the test
    SkipFile "data_frame/transforms_start_stop.yml", All
    SkipFile "ml/index_layout.yml", All
    
    // Todo investigate
    SkipFile "transform/transforms_start_stop.yml", Sections [
        "Verify start transform reuses destination index"
        "Test get multiple transform stats"
    ]
    
    SkipFile "transform/transforms_stats.yml", Sections [
        "Test get multiple transform stats"
        "Test get multiple transform stats where one does not have a task"
    ]
    // More QA tests than API tests
    SkipFile "data_frame/transforms_stats.yml", Sections [
        "Test get multiple transform stats"
        "Test get transform stats on missing transform"
        "Test get multiple transform stats where one does not have a task"
    ]
    // Invalid license makes subsequent tests fail
    SkipFile "license/20_put_license.yml", All

    // Various failures
    // Test tries to match on map from body, but Go keys are not sorted
    SkipFile "ml/jobs_crud.yml", Sections [
        "Test reopen job resets the finished time"
        "Test put job after closing state index"
        "Test close job with body params"
        "Test job with rules"
        "Test put job with model_memory_limit as number"
        "Test put job with model_memory_limit as string and lazy open"
        "Test reopen job resets the finished time"
    ]
    // Test gets stuck every time
    SkipFile "ml/jobs_get_stats.yml", All
    // status_exception, Cannot process data because job [post-data-job] does not have a corresponding autodetect process
    // resource_already_exists_exception, task with id {job-post-data-job} already exist
    // status_exception, Cannot open job [start-stop-datafeed-job-foo-1] because it has already been opened
    // Failed: Actions custom Setup actions Reason: Setup
    SkipFile "ml/post_data.yml", Sections [
        "Test flush with skip_time"
        "Test POST data job api, flush, close and verify DataCounts doc"
        "Test flush and close job WITHOUT sending any data"
        "Test open and close with non-existent job id"
    ]
    // Failed: Actions custom Setup actions Reason: Setup
    SkipFile "ml/stop_data_frame_analytics.yml", Sections [
        "Test stop given missing config and allow_no_match is true"
        "Test stop given missing config and allow_no_match is false"
    ]
    SkipFile "ml/start_stop_datafeed.yml", Section "Test stop given expression"
    SkipFile "transform/transforms_start_stop.yml", Sections [
        "Test start transform"  
        "Verify start transform reuses destination index"
    ]
    // Possible bad test setup, Cannot open job [start-stop-datafeed-job] because it has already been opened
    // resource_already_exists_exception, task with id {job-start-stop-datafeed-job-foo-2} already exist
    SkipFile "ml/start_stop_datafeed.yml", Sections [
        "Test start datafeed when persistent task allocation disabled"
        "Test start given field without mappings"
        "Test start datafeed given start is now"
    ]
    // Indexing step doesn't appear to work (getting total.hits=0)
    SkipFile "monitoring/bulk/10_basic.yml",
        Section "Bulk indexing of monitoring data on closed indices should throw an export exception"
        
    // Indexing step doesn't appear to work (getting total.hits=0)
    SkipFile "monitoring/bulk/20_privileges.yml", Section "Monitoring Bulk API"
    // Test tries to match on whole body, but map keys are unstable in Go
    SkipFile "rollup/security_tests.yml", All
    // Test tries to match on map key, but map keys are unstable in Go
    SkipFile "ml/data_frame_analytics_crud.yml", Sections [
        "Test put with description"
        "Test put valid config with custom outlier detection"
    ]
    // TEMPORARY: Missing 'body: { indices: "test_index" }' payload, TODO: PR
    SkipFile "snapshot/10_basic.yml", Section "Create a source only snapshot and then restore it"
    // illegal_argument_exception: Provided password hash uses [NOOP] but the configured hashing algorithm is [BCRYPT]
    SkipFile "users/10_basic.yml", Section "Test put user with password hash"
    // Slash in index name is not escaped (BUG)
    SkipFile "security/authz/13_index_datemath.yml", Section "Test indexing documents with datemath, when permitted"
    // Possibly a cluster health color mismatch...
    SkipFile "security/authz/14_cat_indices.yml", All
    // Test looks for "testnode.crt", but "ca.crt" is returned first
    SkipFile "ssl/10_basic.yml", Section "Test get SSL certificates"
    // class org.elasticsearch.xpack.vectors.query.VectorScriptDocValues$DenseVectorScriptDocValues cannot be cast to
    // class org.elasticsearch.xpack.vectors.query.VectorScriptDocValues$SparseVectorScriptDocValues ...
    SkipFile "vectors/30_sparse_vector_basic.yml", Section "Dot Product"
    // java.lang.IllegalArgumentException: No field found for [my_dense_vector] in mapping
    SkipFile "vectors/40_sparse_vector_special_cases.yml", Sections [
        "Vectors of different dimensions and data types"
        "Dimensions can be sorted differently"
        "Distance functions for documents missing vector field should return 0"
    ]
    // Cannot connect to Docker IP
    SkipFile "watcher/execute_watch/60_http_input.yml", All
    // Test tries to match on "tagline", which requires "human=false", which doesn't work in the Go API.
    // Also test does too much within a single test, so has to be disabled as whole, unfortunately.
    SkipFile "xpack/15_basic.yml", All
    
    // Snapshot testing requires local filesystem access
    SkipFile "snapshot.create/10_basic.yml", All
    SkipFile "snapshot.get/10_basic.yml", All
    SkipFile "snapshot.get_repository/10_basic.yml", All
    SkipFile "snapshot.restore/10_basic.yml", All
    SkipFile "snapshot.status/10_basic.yml", All
    
    // Datastreams are currently experimental
    SkipFile "indices.data_stream/10_basic.yml", All
    
    // uses $stashed id in match with object
    SkipFile "cluster.reroute/11_explain.yml", Sections [
        "Explain API for non-existent node & shard"
    ]
        
    // Additional entries in regex: Failed cat.templates 10_basic.yml: Assert operation Match $body RegexAssertion
    SkipFile "cat.templates/10_basic.yml", Sections [ "Multiple template"; "Sort templates"; "No templates" ]

    //TODO has dates without strings which trips up our yaml parser
    SkipFile "runtime_fields/40_date.yml", All
    // double / int in object comparison
    SkipFile "runtime_fields/60_boolean.yml", All
    
    // TODO fails to parse ulong dynamically in tests 
    SkipFile "unsigned_long/10_basic.yml", All
    SkipFile "unsigned_long/20_null_value.yml", All
    SkipFile "unsigned_long/30_multi_fields.yml", All
    SkipFile "unsigned_long/40_different_numeric.yml", All
    SkipFile "unsigned_long/50_script_values.yml", All
    SkipFile "unsigned_long/60_collapse.yml", All
    
    SkipFile "ml/inference_processor.yml", Section "Test simulate"

    // TODO: Review again soon once zip includes updated test file from https://github.com/elastic/elasticsearch/pull/71084
    SkipFile "nodes.info/10_basic.yml", Section "node_info role test"
   
    // TODO investigate
    // Failed: Assert operation Match snapshot.shards.failed Value 0 Reason: expected: 0.0 actual: 1.0
    // {"snapshot":{"snapshot":"snapshot","indices":["docs_shared_cache"],"shards":{"total":1,"failed":1,"successful":0}}}
    SkipFile "searchable_snapshots/10_usage.yml", Section "Tests searchable snapshots usage stats with full_copy and shared_cache indices"
]