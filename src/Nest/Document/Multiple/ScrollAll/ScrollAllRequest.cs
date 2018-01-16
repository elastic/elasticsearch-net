using System;
using System.Linq.Expressions;

namespace Nest
{
	public interface IScrollAllRequest
	{
		/// <summary>
		/// The ammount of time to keep the scroll alive on the server
		/// </summary>
		Time ScrollTime { get; set; }
		/// <summary>
		/// An optional search request that describes the search we want to scroll over.
		/// Defaults to matchall on the index and type of T in the <see cref="ScrollAllObserver{T}"/>.
		/// Note: both scroll and slice information WILL be overriden.
		/// </summary>
		ISearchRequest Search { get; set; }
		/// <summary>
		/// The maximum number of slices to partition the scroll over
		/// </summary>
		int Slices { get; set; }
		/// <summary>
		/// The maximum degree of parallelism we should drain the sliced scroll, defaults to the value of <see cref="Slices"/>
		/// </summary>
		int? MaxDegreeOfParallelism { get; set; }

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then a given back pressure factor
		/// When set each scroll request will additionally wait on <see cref="ProducerConsumerBackPressure.WaitAsync"/> as well as
		/// <see cref="MaxDegreeOfParallelism"/> if set. Not that the consumer has to call <see cref="ProducerConsumerBackPressure.Release"/>
		/// on the same instance every time it is done.
		/// </summary>
		ProducerConsumerBackPressure BackPressure { get; set; }

		/// <summary>
		/// Set a different routing field, has to have doc_values enabled
		/// </summary>
		Field RoutingField { get; set; }
	}

	public class ScrollAllRequest : IScrollAllRequest
	{
		int IScrollAllRequest.Slices { get; set; }
		Time IScrollAllRequest.ScrollTime { get; set; }

		/// <inheritdoc/>
		public ISearchRequest Search { get; set; }
		/// <inheritdoc/>
		public Field RoutingField { get; set; }
		/// <inheritdoc/>
		public int? MaxDegreeOfParallelism { get; set; }
		/// <inheritdoc/>
		public ProducerConsumerBackPressure BackPressure { get; set; }

		public ScrollAllRequest(Time scrollTime, int numberOfSlices)
		{
			var i = ((IScrollAllRequest)this);
			i.ScrollTime = scrollTime;
			i.Slices = numberOfSlices;
		}
		public ScrollAllRequest(Time scrollTime, int numberOfSlices, Field routingField) : this(scrollTime, numberOfSlices)
		{
			this.RoutingField = routingField;
		}
	}

	public class ScrollAllDescriptor<T> : DescriptorBase<ScrollAllDescriptor<T>, IScrollAllRequest>, IScrollAllRequest where T : class
	{
		ISearchRequest IScrollAllRequest.Search { get; set; }
		int IScrollAllRequest.Slices { get; set; }
		Field IScrollAllRequest.RoutingField { get; set; }
		Time IScrollAllRequest.ScrollTime { get; set; }
		int? IScrollAllRequest.MaxDegreeOfParallelism { get; set; }
		ProducerConsumerBackPressure IScrollAllRequest.BackPressure { get; set; }

		public ScrollAllDescriptor(Time scrollTime, int numberOfSlices)
		{
			Assign(a =>
			{
				a.ScrollTime = scrollTime;
				a.Slices = numberOfSlices;
			});
		}

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> MaxDegreeOfParallelism(int? maxDegreeOfParallelism) =>
			Assign(a => a.MaxDegreeOfParallelism = maxDegreeOfParallelism);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> RoutingField(Field field) => Assign(a => a.RoutingField = field);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> RoutingField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.RoutingField = objectPath);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector) =>
			Assign(a => a.Search = selector?.Invoke(new SearchDescriptor<T>()));

		/// <summary>
		/// Simple back pressure implementation that makes sure the minimum max concurrency between producer and consumer
		/// is not amplified by the greedier of the two by more then a given back pressure factor
		/// When set each bulk request will call <see cref="ProducerConsumerBackPressure.Release"/>
		/// </summary>
		/// <param name="maxConcurrency">The minimum maximum concurrency which would be the bottleneck of the producer consumer pipeline</param>
		/// <param name="backPressureFactor">The maximum amplification back pressure of the greedier part of the producer consumer pipeline</param>
		public ScrollAllDescriptor<T> BackPressure(int maxConcurrency, int? backPressureFactor = null) =>
			Assign(a => a.BackPressure = new ProducerConsumerBackPressure(backPressureFactor, maxConcurrency));
	}
}
