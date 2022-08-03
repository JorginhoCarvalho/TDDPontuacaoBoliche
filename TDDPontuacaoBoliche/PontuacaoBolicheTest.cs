using Xunit;
namespace TDDBoliche
{
    public class PontuacaoBolicheTest
    {
        [Theory]
        [MemberData(nameof(RodadasComValoresZeros))]
        public void PontuacaoTotalDeTodasAsRodadasComTodosValoresZeros(string[] rodadasBoliche, int pontuacaoTotalEsperada)
        {
            Assert.Equal(pontuacaoTotalEsperada, PontuacaoTotal(rodadasBoliche));
        }

        [Theory]
        [MemberData(nameof(RodadasComAPontuacaoDiferentesDeZero))]
        public void PontuacaoTotalDeTodasAsRodadasComTodosValoresDiferentesDeZero(string[] rodadasBoliche, int pontuacaoTotalEsperada)
        {
            Assert.Equal(pontuacaoTotalEsperada, PontuacaoTotal(rodadasBoliche));
        }

        public static IEnumerable<object[]> RodadasComValoresZeros()
        {
            yield return new object[] { new string[] { "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0" }, 0 };
        }

        public static IEnumerable<object[]> RodadasComAPontuacaoDiferentesDeZero()
        {
            yield return new object[] { new string[] { "1 0", "2 0", "3 0", "4 0", "5 0", "6 0", "7 0", "8 0", "9 0", "0 0" }, 45 };
            yield return new object[] { new string[] { "0 0", "0 1", "0 3", "0 5", "0 7", "0 9", "0 8", "0 6", "0 4", "0 2" }, 45 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 6", "1 1", "2 5" }, 69};
        }

        int PontuacaoTotal(string[] rodadas)
        {
            int pontuacaoTotal = 0;
            foreach (var (rodada, numeroRodada) in rodadas.Select((value, i) => (value, i)))
            {
                var valoresJogadas = rodada.Split(" ").Select(x => int.Parse(x)).ToList();

                var primeiraJogada = valoresJogadas[0];
                var segundaJogada = valoresJogadas[1];

                pontuacaoTotal += primeiraJogada + segundaJogada;
            }

            return pontuacaoTotal;
        }
    }
}