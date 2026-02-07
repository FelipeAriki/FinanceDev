💰 FinanceDev

API financeira desenvolvida em .NET / C# para gerenciamento de transações e categorias, com suporte à exportação de dados em CSV e Excel.

Projeto voltado para estudos, uso pessoal ou como base para aplicações financeiras.

🚀 Tecnologias

C# / .NET

ASP.NET Core Web API

Entity Framework Core

SQL Server

Exportação CSV / Excel

Arquitetura em camadas (DDD)

📁 Estrutura
FinanceDev
 ┣ Finance.API
 ┣ FinanceDev.Application
 ┣ FinanceDev.Core
 ┣ FinanceDev.Infrastructure
 ┣ criarPopularTabelas.sql

⚙️ Funcionalidades

CRUD de Transações

CRUD de Categorias

Relacionamento entre categorias e transações

Exportação de dados em CSV e Excel

▶️ Como executar
git clone https://github.com/FelipeAriki/FinanceDev.git
cd FinanceDev
dotnet restore
dotnet run --project Finance.API


Configure a connection string no appsettings.json e execute o script criarPopularTabelas.sql antes de rodar a API.

👤 Autor

Felipe Ariki
🔗 https://github.com/FelipeAriki
