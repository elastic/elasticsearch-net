---
template: layout.jade
title: Aggregations
menusection: aggregations
menuitem: ipv4
---


# IPv4 Range aggregation

Just like the dedicated date range aggregation, there is also a dedicated range aggregation for IPv4 typed fields.

## Usage

### Fluent Syntax

	var result = client.Search<ElasticsearchProject>(s => s
		.Aggregations(a => a
			.IpRange("my_iprange_agg", d => d
				.Field(p => p.PingIP)
				.Ranges("10.0.0.0/25", "10.0.0.127/25")
			)
		)
	);

	var agg = result.Aggs.IpRange("my_iprange_agg");

### Object Initializer Syntax

	var request = new SearchRequest
	{
		Aggregations = new Dictionary<string, IAggregationContainer>
		{
			{ "my_iprange_agg", new AggregationContainer
				{
					IpRange = new Ip4RangeAggregator
					{
						Field = "pingIp",
						Ranges = new List<Ip4Range>
						{
							new Ip4Range().Mask("10.0.0.0/25"),
							new Ip4Range().Mask("10.0.0.0/127")
						}
					}
				}
			}
		}
	};

	var result = client.Search<ElasticsearchProject>(request);

	var agg = result.Aggs.IpRange("my_iprange_agg");

Refer to the [original docs](http://www.elasticsearch.org/guide/en/elasticsearch/reference/current/search-aggregations-bucket-iprange-aggregation.html) for more information.


