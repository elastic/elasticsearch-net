---
mapped_pages:
  - https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/breaking-changes-policy.html
---

# Breaking changes policy [breaking-changes-policy]

The .NET API Client source code is generated from a [formal specification of the Elasticsearch API](https://github.com/elastic/elasticsearch-specification). This API specification is large, and although it is tested against hundreds of Elasticsearch test files, it may have discrepancies with the actual API that result in issues in the .NET API Client.

Fixing these discrepancies in the API specification results in code changes in the .NET API Client, and some of these changes can require code updates in your applications.

This section explains how these breaking changes are considered for inclusion in .NET API Client releases.

## Breaking changes in patch releases [_breaking_changes_in_patch_releases]

Some issues in the API specification are properties that have an incorrect type, such as a `long` that should be a `string`, or a required property that is actually optional. These issues can cause the .NET API Client to not work properly or even throw exceptions.

When a specification issue is discovered and resolved, it may require code updates in applications using the .NET API Client. Such breaking changes are considered acceptable, *even in patch releases* (e.g. 7.17.0 → 7.17.1), as they introduce stability to APIs that may otherwise be unusable.

## Breaking changes in minor releases [_breaking_changes_in_minor_releases]

Along with these bug fixes, the API specification is constantly refined, more precise type definitions are introduced to improve developer comfort and remove ambiguities. The specification of often-used APIs is fairly mature, so these changes happen generally on less often used APIs. These changes can also cause breaking changes requiring code updates which are considered *acceptable in minor releases* (e.g. 8.0 → 8.1).

## Breaking changes in major releases [_breaking_changes_in_major_releases]

Major releases (e.g. 8.x → 9.x) can include larger refactorings of the API specification and the framework underlying the .NET API Client. These refactorings are considered carefully and done only when they unlock new important features or new developments.

## Elasticsearch API stability guarantees [_elasticsearch_api_stability_guarantees]

All Elasticsearch APIs have stability indicators, which imply potential changes. If an API is `stable` only additional non-breaking changes are added. In case of `experimental` APIs, breaking changes can be introduced any time, which means that these changes, will also be reflected in the .NET API Client.

## Breaking changes by release

For more information, refer to the [](../release-notes/breaking-changes.md).
