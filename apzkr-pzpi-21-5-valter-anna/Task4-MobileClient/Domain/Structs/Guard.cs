namespace Domain.Structs;

public readonly record struct Guard(string Name)
{
    public static Guard AuthenticatedOnly { get; } = new Guard("authenticated_only");
}