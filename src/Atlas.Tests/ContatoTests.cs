using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlas.UI.Domain;
using NUnit.Framework;

namespace Atlas.Tests
{
    [TestFixture]
    public class ContatoTests
    {
        [Test]
        public void Devo_informar_um_nome_ao_criar_um_novo_contato()
        {
            const string nome = "NovoContato";
            
            var contato = new Contato
                              {
                                  Nome = nome
                              };

            Assert.That(contato.Nome,
                Is.EqualTo(nome));
        }

        [Test]
        public void Posso_adicionar_telefones_a_um_contato()
        {
            const string nome = "NovoContato";

            var contato = new Contato
            {
                Nome = nome
            };

            contato.IncluiTelefone("011981234567", Operadora.Tim);

            Assert.That(contato.Telefones.Count(),
                Is.EqualTo(1));
        }

        [Test]
        public void Devo_informar_o_apelido_do_novo_contato()
        {
            const string nome = "NovoContato";
            const string apelido = "Novo1";

            var contato = new Contato
            {
                Nome = nome,
                Apelido = apelido
            };

            Assert.That(contato.Apelido,
                Is.EqualTo(apelido));
        }
    }
}
