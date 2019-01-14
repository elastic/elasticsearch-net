using System;
using System.Collections.Generic;
using System.IO;
using Elasticsearch.Net;

namespace Nest
{
	[MapsApi("xpack.security.put_privileges.json")]
	public partial interface IPutPrivilegesRequest : IProxyRequest
	{
		IAppPrivileges Applications { get; set; }
	}

	public partial class PutPrivilegesRequest
	{
		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);

		public IAppPrivileges Applications { get; set; }
	}

	public partial class PutPrivilegesDescriptor
	{
		void IProxyRequest.WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting formatting) =>
			sourceSerializer.Serialize(Self.Applications, stream, formatting);

		IAppPrivileges IPutPrivilegesRequest.Applications { get; set; }

		public PutPrivilegesDescriptor Applications(Func<AppPrivilegesDescriptor, IPromise<IAppPrivileges>> selector) =>
			Assign(a => a.Applications = selector?.Invoke(new AppPrivilegesDescriptor())?.Value);

	}

	public interface IAppPrivileges : IIsADictionary<string, IPrivileges> { }

	public class AppPrivileges : IsADictionaryBase<string, IPrivileges>, IAppPrivileges
	{
		public void Add(string name, IPrivileges privileges) => BackingDictionary.Add(name, privileges);
	}
	public class AppPrivilegesDescriptor : IsADictionaryDescriptorBase<AppPrivilegesDescriptor, IAppPrivileges, string, IPrivileges>
	{
		public AppPrivilegesDescriptor() : base(new AppPrivileges()) { }

		public AppPrivilegesDescriptor Application(string applicationName, IPrivileges privileges) => Assign(applicationName, privileges);

		public AppPrivilegesDescriptor Application(string applicationName, Func<PrivilegesDescriptor, IPromise<IPrivileges>> selector) =>
			Assign(applicationName, selector?.Invoke(new PrivilegesDescriptor())?.Value);
	}

	public interface IPrivileges : IIsADictionary<string, IPrivilegesActions> { }

	public class Privileges : IsADictionaryBase<string, IPrivilegesActions>, IPrivileges
	{
		public void Add(string name, IPrivilegesActions actions) => BackingDictionary.Add(name, actions);
	}
	public class PrivilegesDescriptor : IsADictionaryDescriptorBase<PrivilegesDescriptor, IPrivileges, string, IPrivilegesActions>
	{
		public PrivilegesDescriptor() : base(new Privileges()) { }

		public PrivilegesDescriptor Privilege(string privilegesName, IPrivilegesActions actions) => Assign(privilegesName, actions);

		public PrivilegesDescriptor Privilege(string privilegesName, Func<PrivilegesActionsDescriptor, IPrivilegesActions> selector) =>
			Assign(privilegesName, selector?.Invoke(new PrivilegesActionsDescriptor()));
	}

	public interface IPrivilegesActions
	{
		IEnumerable<string> Actions { get; set; }
		IDictionary<string, object> Metadata { get; set; }
	}
	public class PrivilegesActions : IPrivilegesActions
	{
		public IEnumerable<string> Actions { get; set; }
		public IDictionary<string, object> Metadata { get; set; }
	}
	public class PrivilegesActionsDescriptor : DescriptorBase<PrivilegesActionsDescriptor, IPrivilegesActions>, IPrivilegesActions
	{
		IEnumerable<string> IPrivilegesActions.Actions { get; set; }
		IDictionary<string, object> IPrivilegesActions.Metadata { get; set; }

		public PrivilegesActionsDescriptor Actions(params string[] actions) => Assign(a => a.Actions = actions);

		public PrivilegesActionsDescriptor Actions(IEnumerable<string> actions) => Assign(a => a.Actions = actions);

		public PrivilegesActionsDescriptor Metadata(IDictionary<string, object> meta) => Assign(a => a.Metadata = meta);

		public PrivilegesActionsDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> meta) =>
			Assign(a => a.Metadata = meta?.Invoke(new FluentDictionary<string, object>()));

	}
}
