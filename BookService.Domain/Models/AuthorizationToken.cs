namespace BookService.Domain.Models;

public class AuthorizationToken : BaseModel
{
    private AuthorizationToken() {}
    
    public AuthorizationToken(long userId, string value, DateTime expiresAtUtc)
    {
        UserId = userId;
        Value = value;
        ExpiresAtUtc = expiresAtUtc;
    }

    public long UserId { get; private set; }
    public string Value { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public bool IsValid(DateTime utcNow)
    {
        return ExpiresAtUtc > utcNow;
    }
}