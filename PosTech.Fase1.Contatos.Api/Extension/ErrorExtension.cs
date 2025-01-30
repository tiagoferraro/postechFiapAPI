using PosTech.Fase1.Contatos.Api.Model;

namespace PosTech.Fase1.Contatos.Api.Extension;

    public static class ErrorExtension
    {
        public static MensagemErro ConverteParaErro(this string? mensagem)
        {
            return new MensagemErro(new List<string>(new string?[] { mensagem }!));
        }

    }

