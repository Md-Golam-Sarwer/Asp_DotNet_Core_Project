using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeInformationSystem.Models;
using EmployeeInformationSystem.ViewModel;

namespace EmployeeInformationSystem.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchVM>();
            CreateMap<BranchVM, Branch>();
        }

    }
}
