using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestioneSpeseEF.EF.RepositoryEF
{
    public class RepositorySpesa
    {
        private readonly Context ctx = new Context();

        public Spesa Add(Spesa s)
        {
            ctx.Add(s);
            ctx.SaveChanges();
            return s;
        }

        public bool Delete(Spesa s)
        {
            ctx.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        public bool Approve(int idSpesa)
        {
            ctx.Spese.Where(s => s.Id == idSpesa).FirstOrDefault().Approvato = true;
            ctx.SaveChanges();
            return true;
        }

        public IEnumerable<Spesa> Fetch(Func<Spesa,bool> lambda = null)
        {
            if (lambda != null)
                return ctx.Spese.Where(lambda);
            return ctx.Spese.ToList();
        }

        public List<Spesa> GetApproved()
        {
            return Fetch(s => s.Approvato == true).ToList();
        }

        public List<Spesa> GetByUser(string utente)
        {
            return Fetch(s => s.Utente == utente).ToList();
        }

        /*
        public List<int,int> FetchTotByCategory()
        {
            from s in ctx.Spese select 
        }*/

    }
}
