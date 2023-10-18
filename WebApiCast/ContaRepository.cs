using Microsoft.EntityFrameworkCore;
using WebApiCast.Entities;

namespace WebApiCast
{
    public class ContaRepository : IContaRepository
    {
        private readonly DataContext _context;

        public ContaRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Adicionar(Conta conta)
        {
            _context.Contas.Add(conta);
            return _context.SaveChanges() > 0;
        }

        public Task<List<Conta>> ListarTodos()
        {
            var query = from c in _context.Contas 
                        orderby c.Id 
                        select c;

            return Task.FromResult(query.ToList());
        }

        public async Task<bool> DeletarId(int id)
        {
            var query = from c in _context.Contas
                        where c.Id == id
                        select c;


            _context.Contas.Remove(query.First());

            return _context.SaveChanges() > 0;
        }

        public async Task<bool> Atualizar(Conta conta)
        {
            _context.Contas.Update(conta);

            return _context.SaveChanges() > 0;
        }

        public Task<Conta> RetornaId(int id)
        {
            var query = from c in _context.Contas
                        where c.Id == id
                        select c;

            return Task.FromResult(query.First());
        }
    }
}
