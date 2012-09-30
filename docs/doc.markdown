
Objetivo
===

Este teste consiste na elabora��o de um cadastro de contatos telef�nicos que registre o nome, a operadora e o apelido.
O sistema rodar� na web e as informa��es devem ser persistidas (pode ser txt, csv, xml, db relacional/noSql etc) sem duplica��es e de forma padronizada.
Al�m de registrar os contatos, deve ser poss�vel realizar altera��o e pesquisa.
A solu��o deve ter testes unit�rios. O design e as tecnologias utilizadas ser�o de sua escolha.

Tecnologias / Frameworks
===

Microsoft .Net Framework 4.5: Foi escoliho por ser a vers�o mais recente do .Net framework contendo as ultimas melhorias da plataforma.
Aspnet MVC 4: Por se tratar de uma aplica��o web, sem nenhum requisito complexo de UI optei pelo Aspnet Mvc devido a simplicidade de implementa��o e flexibilidade para desenvovlimento de funcionalidades mais complexas. A vers�o 4 foi escolhida pelo mesmo motivo do Framework 4.5
RavenDB: A aplica��o n�o precisa de nenhum modelo transacional complexo nem nenhum tipo de relatorio, assim sendo optei por um op��o NoSQL dada a facilidade de implementa��o de funcionalidades complexas como fulltext search e a simplicidade no uso de modelos OO, visto que n�o � preciso mapear um modelo Relacional ao seu Dominio
NUnit: Foi escolhido por afinidade.
NLog: Como o RavenDB j� faz uso do NLog, mantive a mesma linha e utilizei o NLog como ferramenta para gera��o de log.
AutoFac: Foi escolhido por ser um dos melhores containers de IoC disponiveis e por possuir uma API muito simples e "viciante" como o proprio site diz.
AutoMapper: Foi escolhido por diminuir consideravelmente a quantidade de c�dido necess�ria para mapear uma Entidade para um ViewModel sem grandes problemas de performance.

Design
===

A aplica��o � um site feito com Aspnet Mvc 4 padr�o utilizando jQuery no lado cliente para: valida��o de formularios, resquisi��es ajax e alguns efeitos visuais. O repositorio de dados em tempo de desenvolimento � um RavenDB embedded self hosted, assim sendo � poss�vel visualizar management studio do raven acessando localhost:8080. O acesso a dados � feito diretamente na DocumentSession do Raven visando manter a aplica��o bem simples sem a adi��o de abstra��es complexas e de funcionalidade duvidoso, dessa forma � bem simples tamb�m customizar as opera��es de acordo com as necessidades apresentadas. A inje��o de dependecias � feita via construtor e configuradas no ApplicationModule que � um modulo do AutoFac, a DocumentStore do RavenDB � um Singleton e sua DocumentSession � �nica por requisi��o Http.