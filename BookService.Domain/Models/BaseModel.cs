using Ftsoft.Domain;

namespace BookService.Domain.Models;

public class BaseModel : Entity
{
    public long Id { get; private set; }
}