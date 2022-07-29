using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Bank
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Bank ID")]
        public string BankID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Bank Info")]
        public string BankInfo { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Bank Account")]
        public string BankAccount { get; set; }

        [MaxLength(16)]
        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Bank Number")]
        public string BankNumber { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Status")]
        public byte Status { get; set; }

        [DisplayName("Channel")]
        public string Channel { get; set; }
    }
}
