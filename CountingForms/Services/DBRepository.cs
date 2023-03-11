using CountingDB.Entities;
using CountingForms.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Services
{
    public class DBRepository : IDBRepository, IDisposable
    {
        DBContext context = new DBContext();
        protected virtual void Dispose(bool disposing)
        {
            context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<List<t_g_role>> GetRoles()
        {
            return await context.t_g_role.ToListAsync();            
        }
    }
}
