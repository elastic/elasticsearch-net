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

		public ScrollAllDescriptor(Time scrollTime, int numberOfSlices)
		{
			Assign(a =>
			{
				a.ScrollTime = scrollTime;
				a.Slices = numberOfSlices;
			});
		}

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> MaxDegreeOfParallelism(int maxDegreeOfParallelism) =>
			Assign(a => a.MaxDegreeOfParallelism = maxDegreeOfParallelism);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> RoutingField(Field field) => Assign(a => a.RoutingField = field);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> RoutingField(Expression<Func<T, object>> objectPath) =>
			Assign(a => a.RoutingField = objectPath);

		/// <inheritdoc/>
		public ScrollAllDescriptor<T> Search(Func<SearchDescriptor<T>, ISearchRequest> selector) =>
			Assign(a => a.Search = selector?.Invoke(new SearchDescriptor<T>()));

	}
}
