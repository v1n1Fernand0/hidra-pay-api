# HidraPay

Uma API **plug-and-play** para múltiplos gateways de pagamento (Stripe, AbacatePay), construída com .NET 9 e adotando Clean Architecture.

---

## 🚀 Funcionalidades

- 📦 **Multi-gateway**: roteia requisições para Stripe (cartão & boleto) e AbacatePay (PIX & boleto/cartão)  
- 🎛️ **Clean Architecture**: camadas separadas (API, Application, Domain, Infrastructure, Messaging, Worker)  
- 🔌 **Dependência via DI**: módulos `AddDomain()`, `AddApplication()`, `AddInfrastructure()`, `AddMessaging()`  
- 🛠️ **Resiliência** com Polly (retry, circuit-breaker, timeout) nas chamadas HTTP  
- 🧪 **Testes**: stub de gateway, unit tests e estrutura para testes de integração  
- 🐇 **Background Worker**: consome eventos de pagamento (RabbitMQ) para pós-processamento  

---

## 📂 Estrutura

HidraPay/
├─ src/
│ ├─ HidraPay.Api/ ← Composition Root + Controllers + Models + OpenAPI
│ ├─ HidraPay.Application/ ← Serviços, UseCases, Interfaces, DI module
│ ├─ HidraPay.Domain/ ← Entidades, ValueObjects, Enums, Ports, DI module
│ ├─ HidraPay.Infrastructure/← Gateways (Stripe, AbacatePay), Configurações, DI module
│ ├─ HidraPay.Messaging/ ← RabbitMQ publisher/subscriber + DI module
│ └─ HidraPay.Worker/ ← Generic Host para consumo de filas
├─ tests/ ← UnitTests & IntegrationTests
├─ README.md
└─ LICENSE.txt

yaml
Copiar
Editar

---

## 🔧 Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)  
- Credenciais de sandbox:
  - **Stripe**: `STRIPE:ApiKey`  
  - **AbacatePay**: `ABACATEPAY:BaseUrl`, `ApiKey`, `ApiSecret`  
- RabbitMQ (opcional: para mensageria)  
- Docker (opcional: rodar RabbitMQ em `rabbitmq:3-management`)

---

## ⚙️ Configuração

1. Clone o repositório  
   ```bash
   git clone https://github.com/SEU_USUARIO/hidra-pay-api.git
   cd hidra-pay-api
Edite src/HidraPay.Api/appsettings.json, forneça suas chaves:

json
Copiar
Editar
{
  "Stripe": {
    "ApiKey": "sk_test_xxx"
  },
  "AbacatePay": {
    "BaseUrl": "https://api.sandbox.abacatepay.com",
    "ApiKey": "sua_api_key",
    "ApiSecret": "seu_api_secret"
  },
  "RabbitMq": {
    "HostName": "localhost",
    "Port": 5672,
    "UserName": "guest",
    "Password": "guest"
  }
}
(Opcional) Rode RabbitMQ com Docker:

bash
Copiar
Editar
docker run -d --hostname hidra --name rabbitmq \
  -p 5672:5672 -p 15672:15672 rabbitmq:3-management
▶️ Como rodar
API
bash
Copiar
Editar
cd src/HidraPay.Api
dotnet run
Swagger UI em https://localhost:5001/swagger

Endpoint de autorização:

bash
Copiar
Editar
POST /api/payments/authorize
Content-Type: application/json

{
  "orderId": "123",
  "amount": 150.0,
  "currency": "BRL",
  "method": "Pix",
  "expiresAt": "2025-05-08T18:00:00Z"
}
Worker (consumo de eventos)
bash
Copiar
Editar
cd src/HidraPay.Worker
dotnet run
🧪 Testes
bash
Copiar
Editar
cd tests/HidraPay.UnitTests
dotnet test

cd tests/HidraPay.IntegrationTests
dotnet test
✍️ Contribuindo
Fork este repositório

Crie uma branch para sua feature (git checkout -b feature/nova-coisa)

Commit suas mudanças (git commit -m "feat: descrição curta")

Push para sua branch (git push origin feature/nova-coisa)

Abra um Pull Request

📄 Licença
Este projeto está licenciado sob a MIT License. Veja o arquivo LICENSE.txt para mais detalhes.
