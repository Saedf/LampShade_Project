using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public string Name { get; private set; }
        public List<Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }
        protected Role()
        {
            
        }
        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Accounts = new List<Account>();
        }
        public void Edit(string name,List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;

        }

    }
}
