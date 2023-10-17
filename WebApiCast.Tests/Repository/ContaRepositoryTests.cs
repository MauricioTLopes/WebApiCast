using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApiCast.Entities;
using Xunit;

namespace WebApiCast.Tests.Context
{
    public class ContaRepositoryTests
    {
        private async Task<DataContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Contas.CountAsync() <= 0)
            {
                for(int i =  0; i < 10; i++)
                {
                    databaseContext.Contas.Add(new Conta()
                    {
                        Nome = "Nome teste 1",
                        Descricao = "Descrição teste 1"
                    });

                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext; 
        }

        [Fact]
        public async void ContaRepository_Conta_Adicionar_RetornaNãoNulo()
        {
            //Arrange
            var conta = new Conta()
            {
                Nome = "Nome Adicionar 1",
                Descricao = "Descrição Adicionar 1"
            };
            var dbContext = await GetDbContext();
            var contaRepository = new ContaRepository(dbContext);

            //Act
            var result = contaRepository.Adicionar(conta);

            //Assert
            result.Should().NotBeNull();

        }

        [Fact]
        public async void ContaRepository_Conta_Listar_RetornaTodos()
        {
            var dbContext = await GetDbContext();
            var contaRepository = new ContaRepository(dbContext);

            //Act
            var result = contaRepository.ListarTodos();

            //Assert

            result.Result.Should().BeEquivalentTo(dbContext.Contas);
           

        }

        [Fact]
        public async void ContaRepository_Conta_Deletar_RetornaNãoNulo()
        {
            //Arrange
            var conta = new Conta()
            {
                Id = 1
            };

            var dbContext = await GetDbContext();
            var contaRepository = new ContaRepository(dbContext);

            //Act
            var result = contaRepository.DeletarId(conta.Id);

            //Assert
            result.Should().NotBeNull();

        }

        [Fact]
        public async void ContaRepository_Conta_Editar_RetornaNãoNulo()
        {
            //Arrange
            var conta = new Conta()
            {
                Id = 1,
                Nome = "Nome Editar 1",
                Descricao = "Descrição Editar 1"
            };

            var dbContext = await GetDbContext();
            var contaRepository = new ContaRepository(dbContext);

            //Act
            var result = contaRepository.EditarId(conta.Id);

            //Assert
            result.Should().NotBeNull();


        }
    }
  
}
