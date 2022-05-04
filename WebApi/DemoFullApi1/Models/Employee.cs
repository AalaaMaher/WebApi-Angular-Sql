using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoFullApi1.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
       public string   EmployeeName { get; set; }
      public string   Department { get; set; }
     public string   HirringDate { get; set; }
     public string  PersonalImageName { get; set; }
    }
}