namespace OneHub360.DB
{
    public interface IUserInfo
    {
        string LoginName { get; set; }
        string Password { get; set; }
        string PasswordSalt { get; set; }
        string ArabicFullName { get; set; }
        string LatinFullName { get; set; }
        string Email { get; set; }
        string Mobile { get; set; }
    }
}
