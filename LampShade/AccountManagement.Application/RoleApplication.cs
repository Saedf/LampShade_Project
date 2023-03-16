using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Application
{
    public class RoleApplication:IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
           var operation=new OperationResult();
           if (_roleRepository.Exists(x=>x.Name==command.Name))
           {
               return operation.Faild(ApplicationMessage.DuplicatedRecord);
           }

           var role = new Role(command.Name);
           _roleRepository.Create(role);
           _roleRepository.SaveChanges();
           return operation.Succeeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operation = new OperationResult();
            var role = _roleRepository.GetBy(command.Id);
            if (role==null)
            {
                return operation.Faild(ApplicationMessage.RecordNotFound);
            }
            if (_roleRepository.Exists(x => x.Name == command.Name && x.Id!=command.Id))
            {
                return operation.Faild(ApplicationMessage.DuplicatedRecord);
            }
            role.Edit(command.Name);
            _roleRepository.SaveChanges();
            return operation.Succeeded();
        }

        public List<RoleViewModel> List()
        {
            return _roleRepository.List();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }
    }
}
