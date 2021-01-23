<h1 align="center">Automação Web - Mantis</h1>


<b>Projeto desenvolvido em C# - VS2019.</b> 

<b>Status do Projeto: Concluido :heavy_check_mark:</b> 

<b>Para configuração do mantis, mariadb, selenium grid, node chrome e mozilla usei como referência os passos sugeridos pelo Saymon em: </b>
https://github.com/saymowan/Mantis4Testers-Docker

<b>Metas do projeto:</b>

<p align="justify">
<b>1) Implementar 50 scripts de testes que manipulem uma aplicação web (sugestões: Mantis ou TestLink) com Page Objects.
Foram criados 59 scripts de testes distribuídos nas classes:</b> LoginTests, ManageCustomFieldTests, ManageProfMenuTests, ManageProjCatEditTests
ManageProjTests, ManageTagsTests e ManageUserTests.</p>
<p align="justify">
<b>2) Alguns scripts devem ler dados de uma planilha Excel para implementar Data-Driven.
Quem utilizar Cucumber, SpecFlow ou outra ferramenta de BDD não precisa implementar lendo de uma planilha Excel. Pode ser a implementação de um Scenario Outline.</b>
Item implementado na classe: ManageUserTests.</p>
<p align="justify">
<b>3) Notem que 50 scripts podem cobrir mais de 50 casos de testes se usarmos Data-Driven. Em outras palavras, implementar 50 CTs usando data-driven não é a 
mesma coisa que implementar 50 scripts.</b></p>
<p align="justify">
<b>4) Os casos de testes precisam ser executados em no mínimo três navegadores. Utilizando o Selenium Grid.
Não é necessário executar em paralelo. Pode ser demonstrada a execução dos browsers separadamente.
Não é uma boa prática executar os testes em todos os browsers em uma única execução. A melhor forma é controlar o browser através de um arquivo de configuração.
.NET: normalmente utiliza-se app.config
Java: normalmente utiliza-se a classe Properties e cria-se um arquivo chamado environment.properties.</b></p>
<p align="justify">
<b>5) Gravar screenshots ou vídeo automaticamente dos casos de testes.</b></p>
<p align="justify">
<b>6) O projeto deverá gerar um relatório de testes automaticamente com screenshots ou vídeos embutidos:</b> foi o utilizado o framework ExtentReports.</p>
<p align="justify">
<b>7) A massa de testes deve ser preparada neste projeto, seja com scripts carregando massa nova no BD ou com restore de banco de dados.</b></p>
<p align="justify">
<b>8) Um dos scripts deve injetar Javascript para executar alguma operação na tela. O objetivo aqui é exercitar a injeção de Javascript dentro do código do Selenium.
Sugestão: fazer o login usando Javascript ao invés do código do Selenium.</b>
Item implementado na classe: LoginTests (método: RealizarLoginComSucessoComJavaScript)</p>
<p align="justify">
<b>9) Testes deverão ser agendados pelo Jenkins, CircleCI, TFS, TeamCity ou outra ferramenta de CI que preferir:</b> foi utilizado o Azure DevOps. O arquivo com a configuração realizada se encontra no repositório, caminho: AutomacaoMantis\AutomacaoMantis\Resources\Configuracao_Azure_DevOps.pdf</p> 
<a href="https://dev.azure.com/janainasantos033/DesafioWeb/_build/results?buildId=38&view=results">Execução no Azure DevOps<a/>
