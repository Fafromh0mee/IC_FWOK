public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; } // Added Email property
}
