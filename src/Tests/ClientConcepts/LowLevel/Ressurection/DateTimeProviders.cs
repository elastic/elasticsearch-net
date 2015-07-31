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

namespace Tests.ClientConcepts.LowLevel
{
	public class DateTimeProviders
	{
		/** # Date time providers
		 * Not typically something you'll have to pass to the client but all calls to DateTime.Now 
		 * in the client have been abstracted by IDateTimeProvider. This allows us to unit test timeouts and clusterfailover
		 * in run time not being bound to wall clock time.
		 */

		[U] public void DefaultNowBehaviour()
		{
			/** dates are always returned in UTC */
			var dateTimeProvider = new DateTimeProvider();
			dateTimeProvider.Now().Should().BeCloseTo(DateTime.UtcNow);
		}

		[U] public void DeadTimeoutCalculation()
		{
			/**  
			* The default timeout calculation is: `min(*timeout* * 2 ^ (*attempts* * 0.5 -1), *maxTimeout*)`
			* The default values for `*timeout*` and `*maxTimeout`are
			*/
			var timeout = TimeSpan.FromMinutes(1);
			var maxTimeout = TimeSpan.FromMinutes(30);

			/**
			* Plotting these defaults looks as followed:
			*
			*.Timeout plot
			* image::timeoutplot.png[dead timeout]	
			*
			* The goal here is that whenever a node is resurected and is found to still be offline we send it
			* back to the doghouse for an every increasingly long period untill we hit a bounded maximum.
			*/

			for (var attempt = 0; attempt < 30; attempt++)
			{

			}


			
		}

	}
}
