using CasimiroErrorException;
using Xunit;
using Xunit.Abstractions;

namespace Testes
{
    /// <summary>
    /// Usando Xunit mas n�o t� testando nada. Go Horse Go Horse!
    /// Projeto criado 03/12/2022 durante a espera do meu hamburguer
    /// Que apesar de tudo, chegou.
    /// </summary>
    /// 

    public class Main
    {
        private List<string> FrasesCasimiroValidacao = new List<string>()
        {
            "Meteu essa?", "Isso que d� gastar dinheiro com merda!",
            "DENTROOOOO! S� que n�o, n� doid�o?!",
            "Nerdola meteu o dado errado. kkkkkkk",
            "Cart�o amarelo, doid�o. Faz o teu que d� certo."
        };

        private List<string> FrasesCasimiroErrosLinkStackOverflow = new List<string>()
        {
            "hmmmmm, que papinho, hein?! T� te entendendo n�o. Quebrou o servidor bonito. Toma um link do StackOverFlow: {LINK_STACKOVERFLOW}",
            "Porra man�, nem se eu fosse um corsa capotava assim. Toma um link do StackOverFlow: {LINK_STACKOVERFLOW}"
        };

        private List<string> FrasesCasimiroNaoEncontrado = new List<string>()
        {
            "Caraca doid�o. N�o encontrei nada.",
            "Nada que n�o possa piorar. N�o encontrei isso.",
            "Capotou celta. Encontrei nada.",
            "EITAAAAAAAAAAAAA, rodou, man�!. Toma um 404 a�.",
        };

        private List<string> FrasesCasimiroSucesso = new List<string>()
        {
            "Deu bom.",
            "VAI VASCO DA GAMAAA!",
            "Boa, man�!",
            "Que papinhooooo. Bem que disseram que funcionava mesmo",
            "Que isso, melhor que celta � quando o request d� certo."
        };


        private readonly ITestOutputHelper output;
        public Main(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode400_ChamoObterMensagens_RetornaUmaMensagemStatusCode400Aleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.BadRequest)
            .AdicionarMensagens(FrasesCasimiroValidacao);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.BadRequest);
            Assert.Contains(FrasesCasimiroValidacao, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensDadoUmStatusCode200_ChamoObterMensagens_RetornaUmaMensagemStatusCode200Aleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensStatusCodeDiversos_ChamoObterMensagensParaStatusCode200EStatusCode400_RetornaUmaMensagemAleatoria()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado);

            var resultado = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultado));
        }

        [Fact]
        public void CrioListaDeMensagensDeDiversosStatusCode_ChamoObterMensagens_RetornaUmaMensagemAleatoriaDadoUmStatusCode()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.NotFound)
            .AdicionarMensagens(FrasesCasimiroNaoEncontrado)
            .Juntar(System.Net.HttpStatusCode.OK)
            .AdicionarMensagens(FrasesCasimiroSucesso);

            var resultadoNotFound = teste.ObterMensagensRandom(System.Net.HttpStatusCode.NotFound);
            Assert.Contains(FrasesCasimiroNaoEncontrado, item => item.Equals(resultadoNotFound));

            var resultadoOK = teste.ObterMensagensRandom(System.Net.HttpStatusCode.OK);
            Assert.Contains(FrasesCasimiroSucesso, item => item.Equals(resultadoOK));
        }

        [Fact]
        public void CrioListaDeMensagensStatusCode500_ChamoObterMensagens_RetornaUmaMensagemAleatoriaComLinkStackOverflowDadoUmStatusCode()
        {
            var teste = CasimiroErrorExceptionBuilder
            .Criar(System.Net.HttpStatusCode.InternalServerError)
            .UsarStackOverFlow(true, "Nginx large file content error")
            .AdicionarMensagens(FrasesCasimiroErrosLinkStackOverflow);
            var resultadoInternalServerError = teste.ObterMensagensRandom(System.Net.HttpStatusCode.InternalServerError);
            Assert.Contains("https://stackoverflow.com/search?q=Nginx+large+file+content+error", resultadoInternalServerError);
            output.WriteLine(resultadoInternalServerError);
        }
    }
}