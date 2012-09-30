
Objetivo
===

Este teste consiste na elaboração de um cadastro de contatos telefônicos que registre o nome, a operadora e o apelido.
O sistema rodará na web e as informações devem ser persistidas (pode ser txt, csv, xml, db relacional/noSql etc) sem duplicações e de forma padronizada.
Além de registrar os contatos, deve ser possível realizar alteração e pesquisa.
A solução deve ter testes unitários. O design e as tecnologias utilizadas serão de sua escolha.

Tecnologias / Frameworks
===

Microsoft .Net Framework 4.5: Foi escoliho por ser a versão mais recente do .Net framework contendo as ultimas melhorias da plataforma.
Aspnet MVC 4: Por se tratar de uma aplicação web, sem nenhum requisito complexo de UI optei pelo Aspnet Mvc devido a simplicidade de implementação e flexibilidade para desenvovlimento de funcionalidades mais complexas. A versão 4 foi escolhida pelo mesmo motivo do Framework 4.5
RavenDB: A aplicação não precisa de nenhum modelo transacional complexo nem nenhum tipo de relatorio, assim sendo optei por um opção NoSQL dada a facilidade de implementação de funcionalidades complexas como fulltext search e a simplicidade no uso de modelos OO, visto que não á preciso mapear um modelo Relacional ao seu Dominio
NUnit: Foi escolhido por afinidade.
NLog: Como o RavenDB já faz uso do NLog, mantive a mesma linha e utilizei o NLog como ferramenta para geração de log.
AutoFac: Foi escolhido por ser um dos melhores containers de IoC disponiveis e por possuir uma API muito simples e "viciante" como o proprio site diz.
AutoMapper: Foi escolhido por diminuir consideravelmente a quantidade de códido necessária para mapear uma Entidade para um ViewModel sem grandes problemas de performance.

Design
===

A aplicação é um site feito com Aspnet Mvc 4 padrão utilizando jQuery no lado cliente para: validação de formularios, resquisições ajax e alguns efeitos visuais. O repositorio de dados em tempo de desenvolimento é um RavenDB embedded self hosted, assim sendo é possível visualizar management studio do raven acessando localhost:8080. O acesso a dados é feito diretamente na DocumentSession do Raven visando manter a aplicação bem simples sem a adição de abstrações complexas e de funcionalidade duvidoso, dessa forma é bem simples também customizar as operações de acordo com as necessidades apresentadas. A injeção de dependecias é feita via construtor e configuradas no ApplicationModule que é um modulo do AutoFac, a DocumentStore do RavenDB é um Singleton e sua DocumentSession é única por requisição Http.