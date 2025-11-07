using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RotinaEstudo.Data;
using RotinaEstudo.Models;

namespace RotinaEstudo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(AppDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<TarefaEstudo> Tarefas { get; set; } = new();

        public async Task OnGetAsync()
        {
            await CarregarTarefas();
        }

        // Handler para adicionar nova tarefa
        public async Task<IActionResult> OnPostAsync(string materia, string tema, int tempoEstudoMinutos, string prioridade)
        {
            if (!ModelState.IsValid)
            {
                await CarregarTarefas();
                return Page();
            }

            try
            {
                var novaTarefa = new TarefaEstudo
                {
                    Materia = materia,
                    Tema = tema,
                    TempoEstudoMinutos = tempoEstudoMinutos,
                    Prioridade = prioridade,
                    DataCriacao = DateTime.Now,
                    Concluida = false
                };

                _context.TarefasEstudo.Add(novaTarefa);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Tarefa adicionada com sucesso: {Materia}", materia);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar tarefa");
                ModelState.AddModelError("", "Erro ao adicionar tarefa. Tente novamente.");
                await CarregarTarefas();
                return Page();
            }
        }

        // Handler para marcar como concluída
        public async Task<IActionResult> OnPostConcluirAsync(int id)
        {
            try
            {
                var tarefa = await _context.TarefasEstudo.FindAsync(id);
                if (tarefa != null)
                {
                    tarefa.Concluida = true;
                    tarefa.DataConclusao = DateTime.Now;
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Tarefa {Id} marcada como concluída", id);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao concluir tarefa {Id}", id);
                ModelState.AddModelError("", "Erro ao marcar tarefa como concluída.");
                await CarregarTarefas();
                return Page();
            }
        }

        // Handler para excluir tarefa
        public async Task<IActionResult> OnPostExcluirAsync(int id)
        {
            try
            {
                var tarefa = await _context.TarefasEstudo.FindAsync(id);
                if (tarefa != null)
                {
                    _context.TarefasEstudo.Remove(tarefa);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Tarefa {Id} excluída com sucesso", id);
                }
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao excluir tarefa {Id}", id);
                ModelState.AddModelError("", "Erro ao excluir tarefa.");
                await CarregarTarefas();
                return Page();
            }
        }

        private async Task CarregarTarefas()
        {
            Tarefas = await _context.TarefasEstudo
                .OrderByDescending(t => t.DataCriacao)
                .ToListAsync();
        }
    }
}