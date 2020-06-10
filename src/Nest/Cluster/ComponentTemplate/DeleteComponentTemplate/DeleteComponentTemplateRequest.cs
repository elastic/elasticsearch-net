// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 namespace Nest
{
	/// <summary>
	/// Deletes a component template
	/// <para />
	/// Available in Elasticsearch 7.8.0+
	/// </summary>
	[MapsApi("cluster.delete_component_template.json")]
	public partial interface IDeleteComponentTemplateRequest { }

	/// <inheritdoc cref="IDeleteComponentTemplateRequest"/>
	public partial class DeleteComponentTemplateRequest { }

	/// <inheritdoc cref="IDeleteComponentTemplateRequest"/>
	public partial class DeleteComponentTemplateDescriptor { }
}
