using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeInformationSystem.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        //Required Valitation
        [Required(ErrorMessage = "Enter Employee Name")]
        public string EmployeeName { get; set; }

        public int? BranchID { get; set; }
        public virtual Branch Branch { get; set; }
        public int? DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        //Regular Expression Valitation
        [Required(ErrorMessage = "Email Must be Required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter Employee Valid Email")]
        public string Email { get; set; }

        //Compare Valitation
        [Compare("Email", ErrorMessage = "Your Email Doesnot Matched!!!")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "Enter Cellphone No.")]
        [MaxLength(11, ErrorMessage = "Contact Number Must be greater than 11 Digit")]
        [MinLength(11, ErrorMessage = "Contact Number Can't be less than 11 Digit")]
        public string CellPhone { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]

        //[Required(ErrorMessage = "The date value is mandatory")]
        //[DataType(DataType.DateTime)]
        //[Range(typeof(DateTime), "01/01/2010", "01/12/2030", ErrorMessage = " Must be between 1/1/2010 and 1/12/2030 ")]

        public DateTime LastUpdate { get; set; }
        //public string EmployeeImage { get; set; }
        //[NotMapped]
        //public HttpPostedFileBase UploadImage { get; set; }
        public string PhotoUrl { get; set; }

        [Required(ErrorMessage = "Please choose the Profile Photo")]
        [Display(Name = "Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

        public virtual List<Salary> Salaries { get; set; } = new List<Salary>();
    }
}
