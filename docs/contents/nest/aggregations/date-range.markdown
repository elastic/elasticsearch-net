---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: date-range
---


# Date Range aggregation

## Description

A range aggregation that is dedicated for date values. For more info, read the [docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-daterange-aggregation.html).

## Usage

	var result = _client.Search<ElasticsearchProject>(s => s
				.Aggregations(a => a
					.DateRange("date_range", date => date
						.Field("startedOn")
						.Format("MM-yyy")
						.Ranges(
							r => r.To("now-10M/M"),
							r => r.From("now-10M/M")
						))));

You can then access the result.Aggregations to get the data, i.e.

	var rangeAgg = result.Aggs.DateRange("date_range");
