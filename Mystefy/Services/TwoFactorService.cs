using OtpNet;

namespace Mystefy.Services;

/// <summary>
/// Handles all TOTP (Google-Authenticator) operations.
/// </summary>
public class TwoFactorService
{
    public string GenerateSecret()
    {
        // 20-byte random key → Base32
        var bytes = KeyGeneration.GenerateRandomKey(20);
        return Base32Encoding.ToString(bytes);
    }

    public string GetQrCodeUri(string email, string secret, string issuer = "Mystefy")
    {
        // otpauth://totp/Mystefy:user@example.com?secret=XXXX&issuer=Mystefy&digits=6
        return $"otpauth://totp/{issuer}:{email}?secret={secret}&issuer={issuer}&digits=6";
    }

    public bool ValidateCode(string secret, string code)
    {
        var totp = new Totp(Base32Encoding.ToBytes(secret));
        return totp.VerifyTotp(code, out _, new VerificationWindow(2, 2));
    }
}
