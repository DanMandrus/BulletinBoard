using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Service.Controllers.Entities;

public class RegisterUserRequest : IValidatableObject
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Mail { get; set; }
    public string PasswordHash { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var errors = new List<ValidationResult>();
        return errors;
    }
}