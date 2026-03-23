# ConsultoriaDev — Plataforma de Consultoria em Desenvolvimento

> Plataforma web para contratação de serviços de desenvolvimento sob demanda, com painel administrativo para Tech Líder aprovar ou reprovar solicitações e notificar devs disponíveis.

---

## 👥 Alunos

| Nome | RM |
|---|---|
| Caio Freitas| RM553190|
| | |
| Caio Hideki | RM553630 |
| | |
| Jorge Booz | RM552700 |
| | |
| Mateus Tibão | RM553267 |
| | |
| Lana Andrade | RM552596 |

---

## 📋 Sobre o Projeto

O **ConsultoriaDev** é uma aplicação ASP.NET Core MVC que conecta clientes a desenvolvedores especializados. O cliente visualiza os serviços disponíveis, escolhe o que precisa e envia uma solicitação com seu nome e e-mail. O Tech Líder recebe a solicitação no painel administrativo, analisa, aprova ou reprova — e ao aprovar, a notificação é encaminhada ao dev disponível via WhatsApp.

---

## ✨ Funcionalidades

- **Tela pública de serviços** — cards com nome, descrição, tempo de resposta e preço
- **Modal de contratação** — cliente preenche nome e e-mail sem precisar de conta
- **Painel do Tech Líder** — listagem de todas as solicitações com status em tempo real
- **Aprovar / Reprovar** — Tech Líder toma a decisão e atribui um dev disponível
- **Notificação fake via WhatsApp** — card simulando o envio da mensagem ao dev atribuído
- **Autenticação por Claims** — rotas protegidas por Role (Cliente, TechLider, Dev, Admin)
- **Navbar dinâmica** — muda conforme o usuário autenticado e seu nível de acesso
- **Tema dark profissional** — layout escuro com tipografia técnica e azul celeste

---

## 🛠️ Stack

| Tecnologia | Uso |
|---|---|
| .NET 10 | Framework principal |
| ASP.NET Core MVC | Arquitetura da aplicação |
| Entity Framework Core | ORM e migrations |
| SQLite | Banco de dados local |
| Cookie Authentication | Autenticação stateful |
| Claims + Roles | Autorização granular |
| BCrypt.Net | Hash de senhas |
| Bootstrap 5.3 | Estilização e responsividade |
| Bootstrap Icons | Ícones da interface |

---

## 🏗️ Arquitetura

```
ConsultoriaDevApp/
├── Controllers/
│   ├── HomeController.cs          # Listagem pública de serviços
│   ├── SolicitacaoController.cs   # Fluxo de contratação do cliente
│   ├── AdminController.cs         # Painel do Tech Líder [Authorize]
│   └── AuthController.cs          # Login / Logout com Claims
├── Models/
│   ├── Servico.cs                 # Entidade de serviço
│   ├── Solicitacao.cs             # Entidade de solicitação (com enum Status)
│   └── Usuario.cs                 # Entidade de usuário (com enum Role)
├── Data/
│   └── AppDbContext.cs            # DbContext + seed de dados
├── Views/
│   ├── Shared/
│   │   └── _Layout.cshtml         # Layout global com navbar dinâmica
│   ├── Home/Index.cshtml          # Cards de serviços + modal
│   ├── Solicitacao/Confirmacao.cshtml
│   ├── Admin/
│   │   ├── Index.cshtml           # Tabela de solicitações + card WhatsApp
│   │   └── Detalhe.cshtml         # Tela de aprovação / reprovação
│   └── Auth/
│       ├── Login.cshtml
│       └── AcessoNegado.cshtml
└── wwwroot/css/site.css           # Tema dark customizado
```

---

## 🔐 Roles e Acessos

| Role | Acesso |
|---|---|
| **Cliente** | Apenas tela pública — visualiza serviços e envia solicitações |
| **TechLider** | Painel admin — aprova, reprova e atribui devs |
| **Dev** | Recebe notificação ao ser atribuído a uma solicitação |
| **Admin** | Acesso total à plataforma |

As policies são configuradas no `Program.cs`:

```csharp
options.AddPolicy("TechLiderOuAdmin", policy =>
    policy.RequireRole("TechLider", "Admin"));
```

---

## 🚀 Como rodar localmente

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 ou VS Code

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/ConsultoriaDevApp.git
cd ConsultoriaDevApp

# 2. Restaura os pacotes
dotnet restore

# 3. Aplica as migrations e cria o banco
dotnet ef database update

# 4. Roda o projeto
dotnet run
```

Acesse em `https://localhost:7xxx`

### Via Visual Studio

1. Abra o arquivo `.sln`
2. Pressione `F5` para rodar com o IIS Express
3. O banco SQLite (`consultoria.db`) é criado automaticamente

---

## 👤 Usuários padrão (seed)

| Nome | E-mail | Senha | Role |
|---|---|---|---|
| Tech Líder | techlider@consultoria.com | `123456` | TechLider |
| Admin | admin@consultoria.com | `admin123` | Admin |
| João Miguel | joaomiguel@consultoria.com | `123456` | Dev |
| Ana Alice | anaalice@consultoria.com | `123456` | Dev |
| Bento Oliveira | bentooliveira@consultoria.com | `123456` | Dev |
| Catarina Roberta | catarinaroberta@consultoria.com | `123456` | Dev |
| Ariel Mercedes | arielmercedes@consultoria.com | `123456` | Dev |

---

## 🔄 Fluxo da aplicação

```
Cliente acessa /
    └── Visualiza cards de serviços
        └── Clica em "Contratar"
            └── Preenche nome + e-mail no modal
                └── POST /Solicitacao/Criar
                    └── Salva com status "Pendente"
                        └── Tech Líder acessa /admin
                            ├── Reprova → status "Reprovada"
                            └── Aprova → status "Aprovada"
                                └── Card WhatsApp notifica o Dev atribuído
```

---

## 📦 Pacotes NuGet

```xml
<PackageReference Include="BCrypt.Net-Next" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" />
```

---

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais.
