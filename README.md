# Criptografia e Descriptografia no Console

[![GitHub stars](https://img.shields.io/github/stars/seu-usuario/seu-repositorio.svg)](https://github.com/seu-usuario/seu-repositorio/stargazers)
[![GitHub license](https://img.shields.io/github/license/seu-usuario/seu-repositorio.svg)](https://github.com/seu-usuario/seu-repositorio/blob/main/LICENSE)

Um sistema de criptografia e descriptografia no console desenvolvido em C# usando a biblioteca PgpCore. Este projeto permite criptografar arquivos em uma determinada pasta e descriptografá-los posteriormente.

## Dependências

- [PgpCore](https://www.nuget.org/packages/PgpCore/) (X.X.X ou superior)

## Uso

Certifique-se de ter a biblioteca PgpCore instalada antes de executar o projeto.

1. Clone o repositório:
git clone https://github.com/PatrickSoares-Dev/Cripto-Decript-Console

2. Instale as dependências:
dotnet restore

3. Execute o programa no console:
dotnet run


4. O programa possui duas funções principais:
   - `Crip()`: Criptografa os arquivos presentes na pasta "Arquivos CRIP" para um determinado cliente.
   - `DEcrip()`: Descriptografa todos os arquivos presentes na pasta "Arquivos DECRIP".

## Configuração

Antes de executar o programa, certifique-se de configurar corretamente as pastas e as chaves necessárias no código.

### Criptografia

No método `Crip()`, você precisa configurar as seguintes variáveis:

- `pastaArquivos`: O caminho para a pasta que contém os arquivos a serem criptografados.
- `pastaCriptografados`: O caminho para a pasta de destino onde os arquivos criptografados serão armazenados.
- `extensoesCriptografar`: Uma lista das extensões de arquivo que devem ser criptografadas.
- `chavesClientes`: Um dicionário que mapeia o nome do cliente para as chaves públicas correspondentes.
- `cliente`: O nome do cliente para o qual os arquivos serão criptografados.
- `chavePrivada`: O caminho para a chave privada usada para assinar os arquivos criptografados.
- `senha`: A senha associada à chave privada.

Certifique-se de configurar corretamente os caminhos das pastas e chaves para o seu ambiente.

### Descriptografia

No método `DEcrip()`, você precisa configurar as seguintes variáveis:

- `pastaArquivosDecrip`: O caminho para a pasta que contém os arquivos criptografados a serem descriptografados.
- `pastaDescriptografados`: O caminho para a pasta de destino onde os arquivos descriptografados serão armazenados.
- `chavePrivada`: O caminho para a chave privada usada para descriptografar os arquivos.
- `senha`: A senha associada à chave privada.

Certifique-se de configurar corretamente os caminhos das pastas e chaves para o seu ambiente.

## Contribuição

Contribuições são bem-vindas! Se você tiver alguma melhoria ou correção, sinta-se à vontade para abrir um pull request.
