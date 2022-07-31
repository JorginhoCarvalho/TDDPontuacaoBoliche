using Xunit;
namespace TDDBoliche
{
    public class PontuacaoBolicheTest
    {
        [Theory]
        [MemberData(nameof(Rodadas))]
        public void RodadasComTodosValoresZeros(string[][] rodadasBoliche)
        {
            Assert.Equal(0, PontuacaoTotal(rodadasBoliche));
        }

        public static IEnumerable<string[][]> Rodadas()
        {
            yield return new string[][] { new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" }, new[] { "0 0" } };
        }

        int PontuacaoTotal(string[][] rodadas)
        {
            throw new NotImplementedException();
        }
    }
}