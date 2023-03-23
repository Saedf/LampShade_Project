using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Core.Permissions
{
    public class InventoryPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Inventory", new List<PermissionDto>
                    {
                        new PermissionDto(InventoryPermissions.ListInventories,"ListInventory"),
                        new PermissionDto(InventoryPermissions.SearchInventories,"SearchInventory"),
                        new PermissionDto(InventoryPermissions.CreateInventory,"CreateInventory"),
                        new PermissionDto(InventoryPermissions.EditInventory,"EditInventory"),
                        new PermissionDto(InventoryPermissions.Increase,"Increase"),
                        new PermissionDto(InventoryPermissions.Reduce,"Reduce"),
                        new PermissionDto(InventoryPermissions.OperationLog,"OperationLog"),

    }
                }
            };
        }
    }
}
