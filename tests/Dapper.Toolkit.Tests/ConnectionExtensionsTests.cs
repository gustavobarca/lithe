namespace Dapper.Toolkit.Tests;

public class ConnectionExtensionsTest
{
    class Teste
    {
        public Guid TesteId { get; set; }
        public string Name { get; set; }
    }

    [Fact]
    public void Test1()
    {
        var sql = ConnectionExtensions.BuildSelect<Teste>();
    }
}
