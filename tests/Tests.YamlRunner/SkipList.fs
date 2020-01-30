module Tests.YamlRunner.Skips

type SkipSection = All | Section of string | Sections of string list

type SkipFile = SkipFile of string 

let SkipList = dict<SkipFile,SkipSection> [
    // These send empty strings for required parameters
    // TODO i THINK this is now supported
    SkipFile "ml/explain_data_frame_analytics.yml", Section "Test neither job id nor body"
    
    // funny looking dispatch /_security/privilege/app?name
    SkipFile "privileges/10_basic.yml ", Sections [
        "Test put and delete privileges"
        "Test put and get privileges"
    ]
    
    // - Failed: Assert operation NumericAssert Length invalidated_api_keys "Long" Reason: Expected 2.000000 = 3.000000        
    SkipFile "api_key/11_invalidation.yml", Section "Test invalidate api key by realm name"

    // tries to change current users password, not something we keep track of with later tests
    SkipFile "change_password/10_basic.yml", Section "Test user changing their own password"

    // Test looks for "testnode.crt", but "ca.crt" is returned first
    SkipFile "ssl/10_basic.yml", Section "Test get SSL certificates"
    
    // Uses variables in strings e.g Bearer ${token} we can not due variable substitution in string yet
    SkipFile "token/10_basic.yml", All
    
    // We don't expose the overload with just id, warrants investigation in the code generator
    SkipFile "ml/delete_forecast.yml", Section "Test delete all where no forecast_id is set"

    // Leaves .ml-state index closed after running
    (SkipFile "ml/jobs_crud.yml", Sections [
        "Test reopen job resets the finished time"
        "Test put job after closing state index"
    ])

    SkipFile "rollup/put_job.yml", Section "Test put job with templates"
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
    SkipFile "privileges/10_basic.yml", Section "Test put and delete privileges"
    
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
           
    SkipFile "ml/jobs_crud.yml", Section "Test reopen job resets the finished time"
    // Invalid license makes subsequent tests fail
    SkipFile "license/20_put_license.yml", All
    // Test tries to match on map from body, but Go keys are not sorted
    SkipFile "ml/jobs_crud.yml", Sections [
        "Test job with rules"
        "Test put job with model_memory_limit as number"
        "Test put job with model_memory_limit as string and lazy open"
    ]
    // Test gets stuck every time
    SkipFile "ml/jobs_get_stats.yml", All
    // status_exception, Cannot process data because job [post-data-job] does not have a corresponding autodetect process
    // resource_already_exists_exception, task with id {job-post-data-job} already exist
    // status_exception, Cannot open job [start-stop-datafeed-job-foo-1] because it has already been opened
    SkipFile "ml/post_data.yml", Sections [
        "Test flush with skip_time"
        "Test POST data job api, flush, close and verify DataCounts doc"
        "Test flush and close job WITHOUT sending any data"
    ]
    SkipFile "ml/start_stop_datafeed.yml", Section "Test stop given expression"
    SkipFile "transform/transforms_start_stop.yml", Sections [
        "Test start transform"  
        "Verify start transform reuses destination index"
    ]
    // Possible bad test setup, Cannot open job [start-stop-datafeed-job] because it has already been opened
    // resource_already_exists_exception, task with id {job-start-stop-datafeed-job-foo-2} already exist
    SkipFile "ml/start_stop_datafeed.yml",
        Section "Test start datafeed when persistent task allocation disabled"
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
]


