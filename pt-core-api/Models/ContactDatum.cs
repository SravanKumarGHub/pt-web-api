using System;
using System.Collections.Generic;

#nullable disable

namespace pt_core_api.Models
{
    public partial class ContactDatum
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
