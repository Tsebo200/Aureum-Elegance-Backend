namespace Mystefy.Models;

public class VerifyCodeRequest
{
    public string Secret { get; set; } = string.Empty;
    public string Code   { get; set; } = string.Empty;
}
