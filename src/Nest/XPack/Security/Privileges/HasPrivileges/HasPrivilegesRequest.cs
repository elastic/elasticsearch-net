using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.security.has_privileges.json")]
	public partial interface IHasPrivilegesRequest
	{
		/// <summary>
		///	A list of the cluster privileges that you want to check.
		/// </summary>
		[JsonProperty("cluster")]
		IEnumerable<string> Cluster { get; set; }

		/// <summary>
		/// A list of indices and a list of the privileges that you want to check for the specified indices.
		/// </summary>
		[JsonProperty("index")]
		IEnumerable<IndexPrivilegesCheck> Index { get; set; }

		/// <summary>
		/// The name of the application, a list of the privileges that you want to check for the specified resources,
		/// (may be either application privilege names, or the names of actions that are granted by those privileges,
		/// a list of resource names against which the privileges should be checked.
		/// </summary>
		[JsonProperty("application")]
		IEnumerable<IApplicationPrivilegesCheck> Application { get; set; }
	}

	public partial class HasPrivilegesRequest
	{
		/// <inheritdoc cref="IHasPrivilegesRequest.Cluster"/>
		public IEnumerable<string> Cluster { get; set; }

		/// <inheritdoc cref="IHasPrivilegesRequest.Index"/>
		public IEnumerable<IndexPrivilegesCheck> Index { get; set; }

		/// <inheritdoc cref="IHasPrivilegesRequest.Application"/>
		public IEnumerable<IApplicationPrivilegesCheck> Application { get; set; }
	}

	[DescriptorFor("XpackSecurityHasPrivileges")]
	public partial class HasPrivilegesDescriptor
	{
		/// <inheritdoc cref="IHasPrivilegesRequest.Cluster"/>
		IEnumerable<string> IHasPrivilegesRequest.Cluster { get; set; }

		/// <inheritdoc cref="IHasPrivilegesRequest.Index"/>
		IEnumerable<IndexPrivilegesCheck> IHasPrivilegesRequest.Index { get; set; }

		/// <inheritdoc cref="IHasPrivilegesRequest.Application"/>
		IEnumerable<IApplicationPrivilegesCheck> IHasPrivilegesRequest.Application { get; set; }

		/// <inheritdoc cref="IHasPrivilegesRequest.Cluster"/>
		public HasPrivilegesDescriptor Cluster(IEnumerable<string> cluster) => Assign(a => a.Cluster = cluster);

		/// <inheritdoc cref="IHasPrivilegesRequest.Cluster"/>
		public HasPrivilegesDescriptor Cluster(params string[] cluster) => Assign(a => a.Cluster = cluster);

		/// <inheritdoc cref="IHasPrivilegesRequest.Index"/>
		public HasPrivilegesDescriptor Indices(Func<ApplicationPrivilegesChecksDescriptor, IPromise<List<IApplicationPrivilegesCheck>>> selector) =>
			Assign(a => a.Application = selector?.Invoke(new ApplicationPrivilegesChecksDescriptor())?.Value);

		/// <inheritdoc cref="IHasPrivilegesRequest.Application"/>
		public HasPrivilegesDescriptor Applications(Func<ApplicationPrivilegesChecksDescriptor, IPromise<List<IApplicationPrivilegesCheck>>> selector
		) => Assign(a => a.Application = selector?.Invoke(new ApplicationPrivilegesChecksDescriptor())?.Value);
	}

	public interface IIndexPrivilegesCheck
	{
		/// <summary>
		/// A list of indices.
		/// </summary>
		IEnumerable<string> Names { get; set; }

		/// <summary>
		/// A list of the privileges that you want to check for the specified indices.
		/// </summary>
		IEnumerable<string> Privileges { get; set; }
	}

	public class IndexPrivilegesCheck : IIndexPrivilegesCheck
	{
		/// <inheritdoc cref="IIndexPrivilegesCheck.Names"/>
		public IEnumerable<string> Names { get; set; }

		/// <inheritdoc cref="IIndexPrivilegesCheck.Privileges"/>
		public IEnumerable<string> Privileges { get; set; }
	}

	public class IndexPrivilegesChecksDescriptor : DescriptorPromiseBase<IndexPrivilegesChecksDescriptor, List<IIndexPrivilegesCheck>>
	{
		public IndexPrivilegesChecksDescriptor() : base(new List<IIndexPrivilegesCheck>()) { }

		public IndexPrivilegesChecksDescriptor Index(Func<IndexPrivilegesCheckDesciptor, IIndexPrivilegesCheck> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new IndexPrivilegesCheckDesciptor())));

		public class IndexPrivilegesCheckDesciptor : DescriptorBase<IndexPrivilegesCheckDesciptor, IIndexPrivilegesCheck>, IIndexPrivilegesCheck
		{
			/// <inheritdoc cref="IIndexPrivilegesCheck.Names"/>
			IEnumerable<string> IIndexPrivilegesCheck.Names { get; set; }

			/// <inheritdoc cref="IIndexPrivilegesCheck.Privileges"/>
			IEnumerable<string> IIndexPrivilegesCheck.Privileges { get; set; }

			/// <inheritdoc cref="IIndexPrivilegesCheck.Privileges"/>
			public IndexPrivilegesCheckDesciptor Privileges(params string[] privileges) => Assign(a => a.Privileges = privileges);

			/// <inheritdoc cref="IIndexPrivilegesCheck.Privileges"/>
			public IndexPrivilegesCheckDesciptor Privileges(IEnumerable<string> privileges) => Assign(a => a.Privileges = privileges);

			/// <inheritdoc cref="IIndexPrivilegesCheck.Names"/>
			public IndexPrivilegesCheckDesciptor Names(params string[] names) => Assign(a => a.Names = names);

			/// <inheritdoc cref="IIndexPrivilegesCheck.Names"/>
			public IndexPrivilegesCheckDesciptor Names(IEnumerable<string> names) => Assign(a => a.Names = names);
		}
	}

	public interface IApplicationPrivilegesCheck
	{
		/// <summary>
		/// The name of the application.
		/// </summary>
		[JsonProperty("application")]
		string Name { get; set; }

		/// <summary>
		/// A list of the privileges that you want to check for the specified resources.
		/// May be either application privilege names, or the names of actions
		/// that are granted by those privileges.
		/// </summary>
		IEnumerable<string> Privileges { get; set; }

		/// <summary>
		/// A list of resource names against which the privileges should be checked.
		/// </summary>
		IEnumerable<string> Resources { get; set; }
	}

	public class ApplicationPrivilegesCheck : IApplicationPrivilegesCheck
	{
		/// <inheritdoc cref="IApplicationPrivilegesCheck.Name"/>
		public string Name { get; set; }

		/// <inheritdoc cref="IApplicationPrivilegesCheck.Privileges"/>
		public IEnumerable<string> Privileges { get; set; }

		/// <inheritdoc cref="IApplicationPrivilegesCheck.Resources"/>
		public IEnumerable<string> Resources { get; set; }
	}

	public class ApplicationPrivilegesChecksDescriptor
		: DescriptorPromiseBase<ApplicationPrivilegesChecksDescriptor, List<IApplicationPrivilegesCheck>>
	{
		public ApplicationPrivilegesChecksDescriptor() : base(new List<IApplicationPrivilegesCheck>()) { }

		public ApplicationPrivilegesChecksDescriptor Application(Func<ApplicationPrivilegesCheckDescriptor, IApplicationPrivilegesCheck> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new ApplicationPrivilegesCheckDescriptor())));

		public class ApplicationPrivilegesCheckDescriptor
			: DescriptorBase<ApplicationPrivilegesCheckDescriptor, IApplicationPrivilegesCheck>, IApplicationPrivilegesCheck
		{
			/// <inheritdoc cref="IApplicationPrivilegesCheck.Name"/>
			string IApplicationPrivilegesCheck.Name { get; set; }

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Privileges"/>
			IEnumerable<string> IApplicationPrivilegesCheck.Privileges { get; set; }

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Resources"/>
			IEnumerable<string> IApplicationPrivilegesCheck.Resources { get; set; }

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Name"/>
			public ApplicationPrivilegesCheckDescriptor Name(string name) => Assign(a => a.Name = name);

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Privileges"/>
			public ApplicationPrivilegesCheckDescriptor Privileges(params string[] privileges) => Assign(a => a.Privileges = privileges);

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Privileges"/>
			public ApplicationPrivilegesCheckDescriptor Privileges(IEnumerable<string> privileges) => Assign(a => a.Privileges = privileges);

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Resources"/>
			public ApplicationPrivilegesCheckDescriptor Resources(params string[] resources) => Assign(a => a.Resources = resources);

			/// <inheritdoc cref="IApplicationPrivilegesCheck.Resources"/>
			public ApplicationPrivilegesCheckDescriptor Resources(IEnumerable<string> resources) => Assign(a => a.Resources = resources);
		}
	}
}
