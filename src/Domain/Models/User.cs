namespace Domain.Models;

public class User
{
#pragma warning disable CS8618
    public User()
#pragma warning restore CS8618
    {
    }

    public User(string name, string email, string nickname, DateTime birthDate, string cep, string password,
        string photo)
    {
        Name = name;
        Email = email;
        Nickname = nickname;
        BirthDate = birthDate;
        Cep = cep;
        Password = password;
        Photo = photo;
    }

    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? Nickname { get; set; }
    public DateTime BirthDate { get; private set; }
    public string Cep { get; private set; }
    public string Password { get; private set; }
    public string? Photo { get; set; }
}