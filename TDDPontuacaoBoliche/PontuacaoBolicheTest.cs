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

        [Theory]
        [MemberData(nameof(RodadasComJogadasQueTemStrike))]
        public void PontuacaoTotalDeTodasAsRodadasComStrike(string[] rodadasBoliche, int pontuacaoTotalEsperada)
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
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 6", "1 1", "2 / 9" }, 81 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "9 0", "1 5", "2 6", "3 /", "1 1", "2 / 9" }, 83 };
        }

        public static IEnumerable<object[]> RodadasComJogadasQueTemStrike()
        {
            yield return new object[] { new string[] { "1 /", "2 7", "3 0", "4 0", "5 0", "6 0", "7 0", "8 0", "9 0", "X 0 0" }, 73 };
            yield return new object[] { new string[] { "X", "0 1", "0 3", "0 5", "0 7", "0 9", "0 8", "0 6", "0 4", "0 2" }, 56 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "8 1", "X", "1 5", "2 6", "3 6", "1 1", "2 / 9" }, 88 };
            yield return new object[] { new string[] { "X", "X", "X", "X", "9 /", "1 5", "2 6", "3 /", "1 1", "2 / 9" }, 166 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 4", "X", "9 0", "1 5", "2 6", "3 /", "1 1", "X 1 /" }, 94 };
            yield return new object[] { new string[] { "3 1", "2 4", "5 /", "X", "9 0", "1 5", "2 6", "3 /", "1 1", "X 1 /" }, 105 };
            yield return new object[] { new string[] { "X", "X", "X", "X", "X", "X", "X", "X", "X", "X X X" }, 300 };
        }

        int PontuacaoTotal(string[] rodadas)
        {
            int pontuacaoTotal = 0;
            bool houveSpare = false;
            int contadorStrike = 0;

            foreach (var (rodada, i) in rodadas.Select((rodada, i) => (rodada, i)))
            {
                var valoresJogadas = rodada.Split(" ").Select(x => x).ToList();

                var primeiraJogada = valoresJogadas[0].Equals("X") ? 10 : int.Parse(valoresJogadas[0]);

                if(contadorStrike > 1)
                {
                    contadorStrike -= 1;
                    primeiraJogada += primeiraJogada;
                }

                if(i > 1 && contadorStrike > 1)
                {
                    contadorStrike -= 1;
                    primeiraJogada += valoresJogadas[0].Equals("X") ? 10 : int.Parse(valoresJogadas[0]);
                }

                if (houveSpare)
                    primeiraJogada += valoresJogadas[0].Equals("X") ? 10 : int.Parse(valoresJogadas[0]);

                if (!valoresJogadas[0].Equals("X"))
                {
                    var segundaJogada = valoresJogadas[1].Equals("/") ? 10 - int.Parse(valoresJogadas[0]) : int.Parse(valoresJogadas[1]);

                    if (contadorStrike > 0)
                    {
                        contadorStrike -= 1;
                        segundaJogada += segundaJogada;
                    }

                    pontuacaoTotal += primeiraJogada + segundaJogada;

                    houveSpare = valoresJogadas[1].Equals("/");

                    if (i == 9 && valoresJogadas[1].Equals("/"))
                        pontuacaoTotal += int.Parse(valoresJogadas[2]);
                }
                else
                {
                    houveSpare = false;

                    pontuacaoTotal += primeiraJogada;

                    if (i == 9 && valoresJogadas[0].Equals("X"))
                    {
                        if (contadorStrike > 0)
                        {
                            contadorStrike -= 1;
                            pontuacaoTotal += valoresJogadas[1].Equals("X") ? 10 : int.Parse(valoresJogadas[1]);
                        }

                        pontuacaoTotal += valoresJogadas[1].Equals("X") ? 10 : int.Parse(valoresJogadas[1]);
                        pontuacaoTotal += valoresJogadas[2].Equals("X") ? 10 : valoresJogadas[2].Equals("/") ? 10 - int.Parse(valoresJogadas[1]) : int.Parse(valoresJogadas[2]);
                    }
                }

                if (valoresJogadas[0].Equals("X"))
                    contadorStrike += 2;
            }

            return pontuacaoTotal;
        }
    }
}