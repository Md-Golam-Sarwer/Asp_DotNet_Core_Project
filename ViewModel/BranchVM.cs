using EmployeeInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformationSystem.ViewModel
{
    public class BranchVM
    {
        [Key]
        public int BranchID { get; set; }
        public string BranchName { get; set; }
        public string BranchLocation { get; set; }
        public string Division { get; set; }

       

        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
