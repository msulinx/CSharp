namespace Business.Helpers;

public static class IdGenerator
{
    public static string GenerateId()
    {
        return Guid.NewGuid().ToString();
    }
}