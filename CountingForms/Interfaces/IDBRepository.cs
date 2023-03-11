using CountingDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingForms.Interfaces
{
    public interface IDBRepository
    {
        Task<List<t_g_role>> GetRoles();
    }
}
