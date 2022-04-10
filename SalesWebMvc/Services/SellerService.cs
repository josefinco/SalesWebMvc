using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exeptions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }


        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Seller.Find(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasany = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasany)
            {
                throw new KeyNotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
