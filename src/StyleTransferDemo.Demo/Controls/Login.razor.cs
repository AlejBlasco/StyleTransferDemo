namespace StyleTransferDemo.Demo.Controls;


public partial class Login
{
    private string? username { get; set; }
    private string? password { get; set; }

    private void HandleSubmit()
    {
        if (!string.IsNullOrWhiteSpace(username)
            && !string.IsNullOrWhiteSpace(password))
            Console.WriteLine($"Username: {username}, Password: {password}");
        else
            Console.WriteLine("Username or password are empty");
    }
}
