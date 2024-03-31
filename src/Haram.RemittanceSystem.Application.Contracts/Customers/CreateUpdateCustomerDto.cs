using Haram.RemittanceSystem.Genders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Haram.RemittanceSystem.Customers
{
    public class CreateUpdateCustomerDto 
    {
    
        public Guid? Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string FatherName { get; set; }
        [Required]
        [MaxLength(100)]
        public string MotherName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(100)]
        public string Phone { get; set; }
        [Required]
        public Gender CustGender { get; set; }


    }
}
