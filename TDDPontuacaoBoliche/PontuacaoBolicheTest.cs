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
        [MemberData(nameof(RodadasComJogadasDiferentesDeZero))]
        public void PontuacaoTotalDeTodasAsRodadasComValoresDiferentesDeZero(string[] rodadasBoliche, int pontuacaoTotalEsperada)
        {
            Assert.Equal(pontuacaoTotalEsperada, PontuacaoTotal(rodadasBoliche));
        }

        [Theory]
        [MemberData(nameof(RodadasComJogadasQueTemSpare))]
        public void PontuacaoTotalDeTodasAsRodadasComSpare(string[] rodadasBoliche, int pontuacaoTotalEsperada)
        {
            Assert.Equal(pontuacaoTotalEsperada, PontuacaoTotal(rodadasBoliche));
        }

        public static IEnumerable<object[]> RodadasComValoresZeros()
        {
            yield return new object[] { new string[] { "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0", "0 0" }, 0 };
        }

        public static IEnumerable<object[]> RodadasComJogadasDiferentesDeZero()
        {
            yield return new object[] { new string[] { "1 0", "2 0", "3 0", "4 0", "5 0", "6 0", "7 0", "8 0", "9 0", "0 0" }, 45 };
            yield return new object[] { new string[] { "0 0", "0 1", "0 3", "0 5", "0 7", "0 9", "0 8", "0 6", "0 4", "0 2" }, 45 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 6", "1 1", "2 5" }, 69};
        }

        public static IEnumerable<object[]> RodadasComJogadasQueTemSpare()
        {
            yield return new object[] { new string[] { "1 /", "2 7", "3 0", "4 0", "5 0", "6 0", "7 0", "8 0", "9 0", "0 0" }, 63 };
            yield return new object[] { new string[] { "0 /", "0 1", "0 3", "0 5", "0 7", "0 9", "0 8", "0 6", "0 4", "0 2" }, 55 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 6", "1 1", "2 / 9" }, 90 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 /", "1 1", "2 / 9" }, 92 };
        }

        int PontuacaoTotal(string[] rodadas)
        {
            int pontuacaoTotal = 0;
            int houveSpare = 1;

            foreach (var (rodada, i) in rodadas.Select((rodada, i) => (rodada, i)))
            {
                var valoresJogadas = rodada.Split(" ").Select(x => x).ToList();

                var primeiraJogada = int.Parse(valoresJogadas[0]);
                var segundaJogada = valoresJogadas[1].Equals("/") ? 10 - primeiraJogada : int.Parse(valoresJogadas[1]);

                pontuacaoTotal += primeiraJogada * houveSpare + segundaJogada;

                houveSpare = valoresJogadas[1].Equals("/") ? 2 : 1;

                if (i == 9 && valoresJogadas[1].Equals("/"))
                    pontuacaoTotal += int.Parse(valoresJogadas[2]) * houveSpare;
            }

            return pontuacaoTotal;
        }
    }
}