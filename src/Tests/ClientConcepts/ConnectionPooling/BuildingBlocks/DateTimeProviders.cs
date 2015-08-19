using System;
using System.Collections.Specialized;
using System.Net;
using Elasticsearch.Net;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Nest;
using System.Text;
using Elasticsearch.Net.Providers;
using FluentAssertions;
using Tests.Framework;
using System.Linq;

namespace Tests.ClientConcepts.LowLevel
{
	public class DateTimeProviders
	{

		/** = Date time providers
		 * 
		 * Not typically something you'll have to pass to the client but all calls `to DateTime.Now` 
		 * in the client have been abstracted by IDateTimeProvider. This allows us to unit test timeouts and clusterfailover
		 * in run time not being bound to wall clock time.
		 */

		[U] public void DefaultNowBehaviour()
		{
			var dateTimeProvider = new DateTimeProvider();
			/** dates are always returned in UTC */
			dateTimeProvider.Now().Should().BeCloseTo(DateTime.UtcNow);
		}

		/**
		* Another responsibility of this interface is to calculate the time a node has to taken out of rotation
		* based on the number of attempts to revive it. For very advance usecases this might be something of interest
		* to provide a custom implementation for.
		*/

		[U] public void DeadTimeoutCalculation()
		{
			var dateTimeProvider = new DateTimeProvider();
			/**  
			* The default timeout calculation is: `min(timeout * 2 ^ (attempts * 0.5 -1), maxTimeout)`
			* The default values for `timeout` and `maxTimeout` are
			*/
			var timeout = TimeSpan.FromMinutes(1);
			var maxTimeout = TimeSpan.FromMinutes(30);

			/**
			* Plotting these defaults looks as followed:
			*
			*[[timeout]]
			*.Default formula, x-axis time in minutes, y-axis number of attempts to revive
			*image::timeoutplot.png[dead timeout]	
			*
			* The goal here is that whenever a node is resurected and is found to still be offline we send it
			* back to the doghouse for an ever increasingly long period untill we hit a bounded maximum.
			*/

			var timeouts = Enumerable.Range(0, 30)
				.Select(attempt => dateTimeProvider.DeadTime(attempt, timeout, maxTimeout))
				.ToList();

			foreach (var increasedTimeout in timeouts.Take(10))
				increasedTimeout.Should().BeWithin(maxTimeout);
			
		}

	}
}
