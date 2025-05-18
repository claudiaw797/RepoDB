namespace RepoDb.SqlServer.IntegrationTests.Models
{
    public record StringPkTable
    {
        public string Id { get; init; } = string.Empty;

        public string Value { get; init; } = string.Empty;

        public int IdLength => Id.Length;

        public int ValueLength => Value.Length;
    }
}
