using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApiCast;
using WebApiCast.Entities;
using WebApiCastMVCRazor.Models;

namespace WebApiCastMVCRazor.Controllers
{
    public class ContaController : Controller
    {
        private readonly IContaRepository _contaRepository;

        public ContaController()
        {
            _contaRepository = new ContaRepository(new DataContext());
        }

        public IActionResult Index()
        {
            var listaContasEntity = _contaRepository.ListarTodos();
            var listaContaModel = new List<ContaModel>();
            listaContasEntity.Result.ForEach(x =>
            {
                listaContaModel.Add(new ContaModel { Id = x.Id, Nome = x.Nome, Descricao = x.Descricao, });
            });
            return View(listaContaModel);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ContaModel obj)
		{
			if (ModelState.IsValid)
			{
				Conta conta = new Conta()
                { 
                    Nome = obj.Nome,
                    Descricao = obj.Descricao
                };
                _contaRepository.Adicionar(conta);
				TempData["success"] = "Categoria adicionada!";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var contaEntity = _contaRepository.RetornaId(id.Value);

            if (contaEntity == null)
            {
                return NotFound();
            }
			ContaModel contaModel = new ContaModel()
			{
				Nome = contaEntity.Result.Nome,
				Descricao = contaEntity.Result.Descricao
			};

			return View(contaModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContaModel obj)
        {
            if (ModelState.IsValid)
            {
				Conta conta = new Conta()
				{
                    Id = obj.Id,
                    Nome = obj.Nome,
					Descricao = obj.Descricao
				};

				_contaRepository.Atualizar(conta);
                TempData["success"] = "Conta atualizada!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var contaEntity = _contaRepository.RetornaId(id.Value);

            if (contaEntity == null)
            {
                return NotFound();
            }

			ContaModel contaModel = new ContaModel()
			{
				Id = id.Value,
				Nome = contaEntity.Result.Nome,
				Descricao = contaEntity.Result.Descricao
			};

			return View(contaModel);
        }

		//POST
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if (ModelState.IsValid)
            {
                _contaRepository.DeletarId(id.Value);
                TempData["success"] = "Conta excluída!";
            }
			return RedirectToAction("Index");
		}
    }
}