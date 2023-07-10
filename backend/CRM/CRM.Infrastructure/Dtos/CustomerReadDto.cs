using CRM.Domain.ValueObjects;

namespace CRM.Infrastructure.Dtos
{
    public class CustomerReadDto
    {
        public Guid Id { get; set; }
        public Name FirstName { get; set; }
        public Name LastName { get; set; }
        public Email Email { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
        public Age Age { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
