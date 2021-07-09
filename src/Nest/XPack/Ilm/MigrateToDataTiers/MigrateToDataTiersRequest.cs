// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("ilm.migrate_to_data_tiers")]
	[ReadAs(typeof(MigrateToDataTiersRequest))]
	public partial interface IMigrateToDataTiersRequest
	{
		/// <summary>
		/// The legacy index template name to delete. Defaults to none.
		/// </summary>
		[DataMember(Name = "legacy_template_to_delete")]
		string LegacyTemplateToDelete { get; set; }

		/// <summary>
		/// The name of the custom node attribute used for the indices and ILM policies allocation filtering. Defaults to data.
		/// </summary>
		[DataMember(Name = "node_attribute")]
		string NodeAttribute { get; set; }
	}

	/// <summary>
	/// Specifies properties for a migrate to data tiers request.
	/// </summary>
	public partial class MigrateToDataTiersRequest
	{
		/// <inheritdoc />
		public string LegacyTemplateToDelete { get; set; }

		/// <inheritdoc />
		public string NodeAttribute { get; set; }
	}

	public partial class MigrateToDataTiersDescriptor
	{
		string IMigrateToDataTiersRequest.LegacyTemplateToDelete { get; set; }
		string IMigrateToDataTiersRequest.NodeAttribute { get; set; }

		/// <inheritdoc cref="IMigrateToDataTiersRequest.LegacyTemplateToDelete" />
		public MigrateToDataTiersDescriptor LegacyTemplateToDelete(string legacyTemplateToDelete) =>
			Assign(legacyTemplateToDelete, (a, v) => a.LegacyTemplateToDelete = v);

		/// <inheritdoc cref="IMigrateToDataTiersRequest.NodeAttribute" />
		public MigrateToDataTiersDescriptor NodeAttribute(string nodeAttribute) => Assign(nodeAttribute, (a, v) => a.NodeAttribute = v);
	}
}
