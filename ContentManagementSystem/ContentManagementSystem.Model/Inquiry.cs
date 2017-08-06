using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Model
{
    public class Inquiry
    {
        public int Id { get; set; }
        public int InquiryStatusId { get; set; }
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please Enter Your Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter a Message for Sarah")]
        public string Message { get; set; }
    }
}
