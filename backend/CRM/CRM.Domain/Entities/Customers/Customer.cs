using CRM.Domain.Base;
using CRM.Domain.Exceptions;
using CRM.Domain.ValueObjects;

namespace CRM.Domain.Aggregates.UserAggregate
{
    public sealed class Customer : AggregateRoot
    {
        public Name FirstName { get; private set; }
        public Name LastName { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Age Age { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public void SetFirstName(Name firstName)
        {
            FirstName = firstName ?? throw new DomainValidationException("The firstname cannot be null.");
        }

        public void SetLastName(Name lastName)
        {
            LastName = lastName ?? throw new DomainValidationException("The lastname cannot be null.");
        }

        public void SetEmail(Email email)
        {
            Email = email ?? throw new DomainValidationException("The email cannot be null.");
        }

        public void SetPhoneNumber(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber ?? throw new DomainValidationException("The phone number cannot be null.");
        }

        public void SetAge(Age age)
        {
            Age = age ?? throw new DomainValidationException("The age cannot be null.");
        }
        public Customer()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Customer(string firstName, string lastName, string email, string phoneNumber, int age)
        {
            FirstName = new Name(firstName);
            LastName = new Name(lastName);
            Email = new Email(email);
            PhoneNumber = new PhoneNumber(phoneNumber);
            Age = new Age(age);
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
