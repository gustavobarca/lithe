using System.ComponentModel.DataAnnotations.Schema;

namespace Lithe.Tests;

public class ConnectionExtensionsTest
{
    class Teste
    {
        public Guid TesteId { get; set; }
        public string Name { get; set; }
    }

    [Fact]
    public void BuildSelect()
    {
        var sql = ConnectionExtensions.BuildSelect<Teste>();
    }

    [Fact]
    public void BuildInsert()
    {
        var sql = ConnectionExtensions.BuildInsert<Teste>();
    }
}
