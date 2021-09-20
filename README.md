# PrevisaoTempo

Para executar o projeto basta crie no localDB o Database "ClimaTempoSimples" e executar o script sistema_consulta_clima.sql

ou

Direcione a conexão no web.config para o SQLServer desejado e execute os script sistema_consulta_clima.sql

A previsão do tempo é programada para utilizar a data de hoje, porém os dados não estão atualizados para a data atual. Neste caso para acessar um dado coerente e conseguir testar o programa utilize na URL o caminho: https://localhost:XXXXX/ClimaTempo?hoje=2021-03-06 este parametro "hoje" é opcional, porém se omitir o mesmo, a função tentará executar a buscar utilizando a data atual (DateTime.Now)

