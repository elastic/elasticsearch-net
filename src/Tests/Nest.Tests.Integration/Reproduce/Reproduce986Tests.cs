using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace Nest.Tests.Integration.Reproduce
{
	public class ElasticCallCustomer
	{
		public int Id { get; set; }

		public string Name { get; set; }

	}
	public class ElasticCall
	{
		public int Id { get; set; }

		public long Duration { get; set; }

		public ElasticCallCustomer Customer { get; set; }
	}

	[TestFixture]
	public class Reproduce986Tests : IntegrationTests
	{
		[Test]
		public void PrefixQueryShouldReturnHits()
		{
			var customer1 = new ElasticCallCustomer { Id = 1, Name = "Martijn" };
			var customer2 = new ElasticCallCustomer { Id = 2, Name = "Thomas Tvedt" };

			var calls = new[]
			{
				new ElasticCall {Id = 1, Duration=12315, Customer = customer1 },
				new ElasticCall {Id = 2, Duration=345345, Customer = customer2 },
				new ElasticCall {Id = 3, Duration=231, Customer = customer1 },
				new ElasticCall {Id = 4, Duration=908, Customer = customer1 },
				new ElasticCall {Id = 5, Duration=1231, Customer = customer2 },
				new ElasticCall {Id = 6, Duration=2112, Customer = customer1 },
				new ElasticCall {Id = 7, Duration=99891, Customer = customer1 },
				new ElasticCall {Id = 8, Duration=29401281, Customer = customer1 },
				new ElasticCall {Id = 9, Duration=2, Customer = customer1 },
				new ElasticCall {Id = 10, Duration=21231, Customer = customer1 },
			};

			var result = this.Client.Bulk(b => b.IndexMany(calls).Refresh());
			result.IsValid.Should().BeTrue();

			var search = this.Client.Search<ElasticCall>(s => s
				.Aggregations(agg => agg
					.Terms("calls_per_customer", calls_per_customer => calls_per_customer
						.Field(call=>call.Customer.Id)
						.Size(10)
						.Aggregations(callsPerCustomerAggs => callsPerCustomerAggs
							.Terms("customer_name", customer_name => customer_name
								.Script("_source.customer.name")
								.Size(1)
							)
							.Sum("total_duration", total_duration => total_duration.Field("duration"))
							.Average("average_duration", average_duration => average_duration.Field("duration"))
						)
					)
				)
			);

			search.IsValid.Should().BeTrue();

			var callsPerCustomer = search.Aggs.Terms("calls_per_customer");
			callsPerCustomer.Items.Should().NotBeEmpty().And.HaveCount(2);

			foreach(var term in callsPerCustomer.Items)
			{
				var name = term.Terms("customer_name");
				name.Items.Should().NotBeEmpty().And.HaveCount(1);

				var sum = term.Sum("total_duration").Value;
				sum.Should().BeGreaterThan(10);

				var average = term.Average("average_duration").Value;
				average.Should().BeGreaterThan(10);
			}


		}
	}
}
