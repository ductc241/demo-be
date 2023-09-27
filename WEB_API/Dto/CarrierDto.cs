using System.ComponentModel.DataAnnotations;

namespace WEB_API.Dto
{
    public class CarrierDto
    {
        public string Name { get; set; }
        public string Contact_Person { get; set; }
        public string Phone_Number { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string? Note { get; set; }
        public bool Status { get; set; }
    }
}
