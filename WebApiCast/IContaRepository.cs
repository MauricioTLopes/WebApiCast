using Microsoft.EntityFrameworkCore;
using WebApiCast.Entities;

namespace WebApiCast
{
    public interface IContaRepository
    {
        Task<bool> Adicionar(Conta conta);
        Task<List<Conta>> ListarTodos();
        Task<bool> DeletarId(int id);
        Task<bool> EditarId(int id);
    }
}
