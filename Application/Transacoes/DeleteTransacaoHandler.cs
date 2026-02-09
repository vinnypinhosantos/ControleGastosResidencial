using ControleGastosResidencial.Common;
using ControleGastosResidencial.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastosResidencial.Application.Transacoes;

public class DeleteTransacaoHandler (AppDbContext _context)
{
    public async Task<Result> HandleAsync(
        int id
    )
    {
        var transacao = await _context.Transacoes
            .FirstOrDefaultAsync(p => p.Id == id);
        
        if (transacao is null)
            return Result.Failure("Transacao não encontrada");
        
        _context.Transacoes.Remove(transacao);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
