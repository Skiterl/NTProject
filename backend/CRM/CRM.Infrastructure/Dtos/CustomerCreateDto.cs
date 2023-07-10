namespace CRM.Infrastructure.Dtos
{
    public class CustomerCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
