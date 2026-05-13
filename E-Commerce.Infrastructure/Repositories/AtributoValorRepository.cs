using E_Commerce.Application.Interfaces.AtributoValores;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastructure.Datas;
using E_Commerce.Shared.DTOs.AtributoValores;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class AtributoValorRepository(ECommerceDBContext context) : IAtributoValorRepository
    {
        private readonly ECommerceDBContext _context = context;

        public async Task AddAsync(AtributoValor atributo)
            => await _context.atributoValores.AddAsync(atributo);

        public async Task<List<ListAtributoValorResponseDTO>> GetAllAsync()
            => await _context.atributoValores
            .AsNoTracking()
            .Select(av => new ListAtributoValorResponseDTO
            {
                Id = av.Id,
                IdAtributo = av.IdAtributo,
                Atributo = av.Atributo.Nombre,
                Valor = av.Valor
            })
            .ToListAsync();

        public async Task<AtributoValor?> GetByIdAsync(int id)
            => await _context.atributoValores
            .Where(av => av.Id == id)
            .SingleOrDefaultAsync();

        public async Task<int> GetIdByValorAsync(string valor)
        => await _context.atributoValores
            .AsNoTracking()
            .Where(v => v.Valor == valor)
            .Select(i => (int?)i.Id)
            .SingleOrDefaultAsync() ?? 0;

        public async Task<bool> ValidarAtributosValor(List<int> idsAtributoValor)
        {
            var existentes = await _context.atributoValores
                .Select(x => x.Id)
                .ToListAsync();

            return idsAtributoValor.All(id => existentes.Contains(id));
        }

    }
}
