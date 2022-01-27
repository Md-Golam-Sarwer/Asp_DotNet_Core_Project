using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformationSystem.Models
{
    public class Salary
    {
        [Key]
        public int SalaryID { get; set; }
        public int? EmployeeID { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        public decimal TotalSalary { get { return (BasicSalary + HouseRent); } }
        public bool IsActive { get; set; }

        public virtual Employee Employee { get; private set; }
    }
}
