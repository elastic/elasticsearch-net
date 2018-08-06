using System;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.ClientConcepts.ConnectionPooling.BuildingBlocks
{
	public class DateTimeProviders
	{
		/**[[date-time-providers]]
         * === Date time providers
		 *
		 * Not typically something you'll have to pass to the client but all calls to `System.DateTime.UtcNow`
		 * in the client have been abstracted behind an `IDateTimeProvider` interface. 
         * This allows us to unit test timeouts and cluster failover without being bound to wall clock 
         * time as calculated by using `System.DateTime.UtcNow` directly.
		 */
		[U] public void DefaultNowBehaviour()
		{
			var dateTimeProvider = DateTimeProvider.Default;
			
			dateTimeProvider.Now().Should().BeCloseTo(DateTime.UtcNow);
        }

		/**
         * As you can see, dates are always returned in UTC from the default implementation.
         * 
		* Another responsibility of this interface is to calculate the time a node has to be taken out of rotation
		* based on the number of attempts to revive it. For very advanced use cases, this might be something of interest
		* to provide a custom implementation for.
		*/
		[U] public void DeadTimeoutCalculation()
		{
			/**
			* The default timeout calculation is
            * 
            * [source,sh]
            * ----
            * min(timeout * 2 ^ (attempts * 0.5 -1), maxTimeout)
            * ----
            * 
            * where the default values for `timeout` and `maxTimeout` are
			*/
			var timeout = TimeSpan.FromMinutes(1);
			var maxTimeout = TimeSpan.FromMinutes(30);

            /**
			* Plotting these defaults looks as follows
			*
			*.Default formula, x-axis number of attempts to revive, y-axis time in minutes
			*image::timeoutplot.png[dead timeout]
			*
			* The goal here is that whenever a node is resurrected and is found to still be offline, we send it
			* __back to the doghouse__ for an ever increasingly long period, until we hit a bounded maximum.
			*/
            var dateTimeProvider = DateTimeProvider.Default;

            var timeouts = Enumerable.Range(0, 30)
				.Select(attempt => dateTimeProvider.DeadTime(attempt, timeout, maxTimeout))
				.ToList();

			foreach (var increasedTimeout in timeouts.Take(10))
				increasedTimeout.Should().BeWithin(maxTimeout);
		}

	}
}
