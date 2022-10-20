using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canturi.Models.BusinessEntity.FrontEnd
{
    public class EmailServerModel 
    {
        [Required(ErrorMessage = "SMTP server required.")]
        [RegularExpression(@"^([a-zA-Z0-9' '\.&_-]+)$", ErrorMessage = "Name is not valid (Only a-z A-Z 0-9 @ & _ -).")]
        public string EmailServer { get; set; }

        [Required(ErrorMessage = "Network user id required.")]
        public string NetworkUserId { get; set; }

        [Required(ErrorMessage = "Network user password required.")]
        public string NetworkUserPassword { get; set; }

        [Required(ErrorMessage = "Sender display name required.")]
        public string SenderDisplayName { get; set; }

        [Required(ErrorMessage = "From name required.")]
        public string FromEmail { get; set; }

        [Required(ErrorMessage = "Port number required.")]
        public int Port { get; set; }

        [Required(ErrorMessage = "Email address required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email address must be at least 5 character long")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Valid email address is required.")]
        [DataType(DataType.EmailAddress)]
        public string EmailTo { get; set; }

        public bool EnableSsl { get; set; }

        public int Active { get; set; }

        public int Id { get; set; }
    }
}
