//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data.Services;
using System.Data.Services.Common;

namespace OrderEntryServices
{
    public class NorthwindDataService : DataService<NorthwindEntities>
    {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config)
        {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            // config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
            //config.SetServiceOperationAccessRule("Customer", ServiceOperationRights.All);

            config.SetEntitySetAccessRule("Customers", EntitySetRights.All);
            config.SetEntitySetAccessRule("Products", EntitySetRights.AllRead);
            config.SetEntitySetAccessRule("*", EntitySetRights.ReadSingle);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
