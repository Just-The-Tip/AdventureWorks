using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorksSandbox.Models
{
    public class Person : BusinessEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonType { get; set; }
    }
}