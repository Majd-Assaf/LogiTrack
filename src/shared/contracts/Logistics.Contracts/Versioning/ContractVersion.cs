namespace Logistics.Contracts.Versioning;

public sealed record ContractVersion(int Major, int Minor)
{
    public override string ToString() => $"{Major}.{Minor}";
}
