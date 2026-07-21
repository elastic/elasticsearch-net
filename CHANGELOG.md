# 7.x

## 7.17.0 [2022-02-01]

https://github.com/elastic/elasticsearch-net/compare/7.16.0...7.17.0

## Features & Enhancements

- #6070 Add `IgnoreUnmapped` to `GeoDistanceQuery`, `GeoBoundingBoxQuery` and `GeoPolygon` (issue: #6066)
- #6094 Add shard_stats to primary index stats (issue: #6079)
- #6101 Support index template v2 APIs in NEST

## Bug Fixes

- #6085 Support version on ingest pipelines (issue: #6082)
- #6088 Add missing fields for `IpProperty` (issue: #6067)
- #6090 Support deserialisation of simple scripts (issue: #5684)
- #6091 Add missing `TermVectorOption` (issue: #6078)
- #6095 Update boxplot to handle non-numeric values (issue: #6050)
- #6098 Move explain to body in search template API (issue: #5040)

## Breaking Changes

To align with naming elsewhere during the implementation of NEST APIs for index template v2, some of the low-level client types and methods have been renamed:
- ExistsIndexTemplateRequestParameters => IndexTemplateV2ExistsRequestParameters
- ExistsTemplateForAll => TemplateV2ExistsForAll
- ExistsTemplateForAllAsync => TemplateV2ExistsForAllAsync

## 7.16.0 [2021-12-08]

https://github.com/elastic/elasticsearch-net/compare/7.15.2...7.16.0

## Enhancements

- #6023 Add Component template APIs (issues: #4748, #4718)
- #6024 Support configuring a certificate fingerprint
- #6031 Add inference processor (issues: #4412, #4341)

## Bugs

- #6055 Fix cleanup repository patch and regen LL code (issue: #6054)

## 7.15.2 [2021-10-28]

https://github.com/elastic/elasticsearch-net/compare/7.15.1...7.15.2

## Bug Fixes

- #6038 Support `wildcard` field in `WildcardQuery` (issue: #6033)
- #6042 Support deprecated range query properties

## 7.15.1 [2021-10-21]

https://github.com/elastic/elasticsearch-net/compare/7.15.0...7.15.1

## Enhancements

- #6025 Add experimental config to disable TLS1.3
- #6027 Support params on SQL query & translate APIs (issue: #6022)