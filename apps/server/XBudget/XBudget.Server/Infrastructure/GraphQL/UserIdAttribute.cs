namespace XBudget.Server.Infrastructure.GraphQL;

public class UserIdAttribute : GlobalStateAttribute
{
    public UserIdAttribute() : base("UserId")
    {

    }
}