using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Infrastructure;

namespace DiscountManagement.Infrastructure.Core.Permissions
{
    public class DiscountPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Discount", new List<PermissionDto>
                    {
                        new PermissionDto(60, "ListDiscount"),
                        new PermissionDto(61, "CreateDiscount"),
                        new PermissionDto(62, "CreateDiscount"),

                    }
                }
            };
        }
    }
}
