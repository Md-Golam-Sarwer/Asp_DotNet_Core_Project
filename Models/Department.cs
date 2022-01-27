using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformationSystem.Models
{
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int? BranchID { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
