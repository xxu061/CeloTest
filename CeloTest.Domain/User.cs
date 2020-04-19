using System;

namespace CeloTest.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Title Title { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string OriginalProfileImageUrl { get; set; }
        public string ThumbnailProfileImageUrl { get; set; }
    }
}
