using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infraestructure.Repository;

    public class PaisRepository : GenericRepository<Pis>, IPais
    {
        protected readonly APITiendaContext _context;
        
        public PaisRepository(APITiendaContext context) : base
        {
            _context = context;
        }

        public override async Task<IEnumerable<Pais>> GetTaskAsync()
        {
            return await _context.Paises
                .Include(p => p.Pais)
                .ToListAsync();
        }

        public override async Task<Pais> GetTaskAsync(int id)
        {
            return await _context.Paises
            .Include(p => p.Estados)
            .FirstOrDefaultAsync(p =>  p.Id == id);
        }
    }
