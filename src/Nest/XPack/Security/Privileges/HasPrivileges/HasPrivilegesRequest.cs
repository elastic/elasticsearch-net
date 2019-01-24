using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.security.has_privileges.json")]
	public partial interface IHasPrivilegesRequest
	{
		[JsonProperty("cluster")]
		IEnumerable<string> Cluster { get; set; }

		[JsonProperty("index")]
		IEnumerable<IndexPrivilegesCheck> Index { get; set; }

		[JsonProperty("application")]
		IEnumerable<IApplicationPrivilegesCheck> Application { get; set; }
	}

	public partial class HasPrivilegesRequest
	{
		public IEnumerable<string> Cluster { get; set; }
		public IEnumerable<IndexPrivilegesCheck> Index { get; set; }
		public IEnumerable<IApplicationPrivilegesCheck> Application { get; set; }
	}

	public partial class HasPrivilegesDescriptor
	{
		IEnumerable<string> IHasPrivilegesRequest.Cluster { get; set; }
		IEnumerable<IndexPrivilegesCheck> IHasPrivilegesRequest.Index { get; set; }
		IEnumerable<IApplicationPrivilegesCheck> IHasPrivilegesRequest.Application { get; set; }

		public HasPrivilegesDescriptor Cluster(IEnumerable<string> cluster) => Assign(a => a.Cluster = cluster);

		public HasPrivilegesDescriptor Cluster(params string[] cluster) => Assign(a => a.Cluster = cluster);

		public HasPrivilegesDescriptor Indicees(Func<ApplicationPrivilegesChecksDescriptor, IPromise<List<IApplicationPrivilegesCheck>>> selector) =>
			Assign(a => a.Application = selector?.Invoke(new ApplicationPrivilegesChecksDescriptor())?.Value);

		public HasPrivilegesDescriptor Applications(Func<ApplicationPrivilegesChecksDescriptor, IPromise<List<IApplicationPrivilegesCheck>>> selector
		) =>
			Assign(a => a.Application = selector?.Invoke(new ApplicationPrivilegesChecksDescriptor())?.Value);
	}

	public interface IIndexPrivilegesCheck
	{
		IEnumerable<string> Names { get; set; }
		IEnumerable<string> Privileges { get; set; }
	}

	public class IndexPrivilegesCheck : IIndexPrivilegesCheck
	{
		public IEnumerable<string> Names { get; set; }
		public IEnumerable<string> Privileges { get; set; }
	}

	public class IndexPrivilegesChecksDescriptor : DescriptorPromiseBase<IndexPrivilegesChecksDescriptor, List<IIndexPrivilegesCheck>>
	{
		public IndexPrivilegesChecksDescriptor() : base(new List<IIndexPrivilegesCheck>()) { }

		public IndexPrivilegesChecksDescriptor Index(Func<IndexPrivilegesCheckDesciptor, IIndexPrivilegesCheck> selector) =>
			Assign(a => a.Add(selector.InvokeOrDefault(new IndexPrivilegesCheckDesciptor())));

		public class IndexPrivilegesCheckDesciptor : DescriptorBase<IndexPrivilegesCheckDesciptor, IIndexPrivilegesCheck>, IIndexPrivilegesCheck
		{
			IEnumerable<string> IIndexPrivilegesCheck.Names { get; set; }
			IEnumerable<string> IIndexPrivilegesCheck.Privileges { get; set; }

			public IndexPrivilegesCheckDesciptor Privileges(params string[] privileges) => Assign(a => a.Privileges = privileges);

			public IndexPrivilegesCheckDesciptor Privileges(IEnumerable<string> privileges) => Assign(a => a.Privileges = privileges);

			public IndexPrivilegesCheckDesciptor Names(params string[] names) => Assign(a => a.Names = names);

			public IndexPrivilegesCheckDesciptor Names(IEnumerable<string> names) => Assign(a => a.Names = names);

		}
	}

	public interface IApplicationPrivilegesCheck
	{
		[JsonProperty("application")]
		string Name { get; set; }
		IEnumerable<string> Privileges { get; set; }
		IEnumerable<string> Resources { get; set; }
	}

	public class ApplicationPrivilegesCheck : IApplicationPrivilegesCheck
	{
		public string Name { get; set; }
		public IEnumerable<string> Privileges { get; set; }
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
			string IApplicationPrivilegesCheck.Name { get; set; }
			IEnumerable<string> IApplicationPrivilegesCheck.Privileges { get; set; }
			IEnumerable<string> IApplicationPrivilegesCheck.Resources { get; set; }

			public ApplicationPrivilegesCheckDescriptor Name(string name) => Assign(a => a.Name = name);

			public ApplicationPrivilegesCheckDescriptor Privileges(params string[] privileges) => Assign(a => a.Privileges = privileges);

			public ApplicationPrivilegesCheckDescriptor Privileges(IEnumerable<string> privileges) => Assign(a => a.Privileges = privileges);

			public ApplicationPrivilegesCheckDescriptor Resources(params string[] resources) => Assign(a => a.Resources = resources);

			public ApplicationPrivilegesCheckDescriptor Resources(IEnumerable<string> resources) => Assign(a => a.Resources = resources);
		}
	}
}
