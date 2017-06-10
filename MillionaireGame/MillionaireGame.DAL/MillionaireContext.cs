using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionaireGame.DAL.Entities;

namespace MillionaireGame.DAL
{
    public class MillionaireContext : DbContext
    {
        public MillionaireContext(): base("MillionaireDb")
        {
        }

        public DbSet<Question> Questions { get; set; }
    }
}
