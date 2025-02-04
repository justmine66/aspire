// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting.Azure;

/// <summary>
/// A resource that represents an Azure Application Insights resource.
/// </summary>
/// <param name="name">The resource name.</param>
public class AzureApplicationInsightsResource(string name) :
    AzureBicepResource(name, templateResourceName: "Aspire.Hosting.Azure.Bicep.appinsights.bicep"),
    IResourceWithConnectionString
{
    /// <summary>
    /// Gets the "appInsightsConnectionString" output reference for the Azure Application Insights resource.
    /// </summary>
    public BicepOutputReference ConnectionString => new("appInsightsConnectionString", this);

    /// <summary>
    /// Gets the connection string template for the manifest for the Azure Application Insights resource.
    /// </summary>
    public string ConnectionStringExpression => ConnectionString.ValueExpression;

    /// <summary>
    /// Gets the connection string for the Azure Application Insights resource.
    /// </summary>
    /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
    /// <returns>The connection string for the Azure Application Insights resource.</returns>
    public ValueTask<string?> GetConnectionStringAsync(CancellationToken cancellationToken = default)
        => ConnectionString.GetValueAsync(cancellationToken);

    // UseAzureMonitor is looks for this specific environment variable name.
    string IResourceWithConnectionString.ConnectionStringEnvironmentVariable => "APPLICATIONINSIGHTS_CONNECTION_STRING";
}
