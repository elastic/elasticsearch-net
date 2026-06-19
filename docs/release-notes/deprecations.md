---
navigation_title: "Deprecations"
---

# Elasticsearch .NET Client deprecations [elasticsearch-net-client-deprecations]
Over time, certain Elastic functionality becomes outdated and is replaced or removed. To help with the transition, Elastic deprecates functionality for a period before removal, giving you time to update your applications.

Review the deprecated functionality for Elasticsearch .NET Client. While deprecations have no immediate impact, we strongly encourage you update your implementation after you upgrade. To learn how to upgrade, check out [Upgrade](docs-content://deploy-manage/upgrade.md).

% ## Next version [elasticsearch-net-client-versionnext-deprecations]

% ::::{dropdown} Deprecation title
% Description of the deprecation.
% For more information, check [PR #](PR link).
% **Impact**<br> Impact of deprecation. 
% **Action**<br> Steps for mitigating deprecation impact.
% ::::

## 9.3.4 [elasticsearch-net-client-934-deprecations]

### Overview

- [1. ES|QL `QueryAsObjectsAsync`](#1-esql-query-as-objects)

### Deprecations

#### 1. ES|QL `QueryAsObjectsAsync` [#1-esql-query-as-objects]

**Impact**: Medium.

The ESQL `QueryAsObjectsAsync` method should no longer be used.

The new recommended approach is [LINQ to ES|QL](../reference/linq-to-esql.md), which provides type-safe queries with automatic result mapping. Refer to the [LINQ to ES|QL](../reference/linq-to-esql.md) documentation for details.

## 9.0.0 [elasticsearch-net-client-900-deprecations]

_No deprecations_