using Xunit;
namespace TDDBoliche
{
    public class PontuacaoBolicheTest
    {
        [Theory]
        [MemberData(nameof(Rodadas))]
        public void RodadasComTodosValoresZeros(string[] rodadasBoliche)
        {
            Assert.Equal(0, PontuacaoTotal(rodadasBoliche));
        }

        public static IEnumerable<object[]> Rodadas()
        {
            yield return new object[] { new string[] { "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0" } };
        }

        int PontuacaoTotal(string[] rodadas)
        {
            return 0;
        }
    }
}