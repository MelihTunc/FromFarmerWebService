using System;

namespace FromFarmer.Entities.ViewModel
{
    public class UserVM
    {
        public int IdentityNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkAddress { get; set; }
        public int? IsFarmer { get; set; }
    }
}
