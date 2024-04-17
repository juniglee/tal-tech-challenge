using TAL.TechTest.DAL.Interface;
using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.DAL.Repository
{
    public class BlockoutRepository : IBlockoutRepository
    {
        private AppDbContext _db;

        public BlockoutRepository(AppDbContext db)
        {
            _db = db;
        }

        public Blockout Get(TimeOnly time)
        {
            return _db.Blockouts.FirstOrDefault(x => x.StartTime == time);
        }

        public IEnumerable<Blockout> Get()
        {
            return _db.Blockouts;
        }

        public Blockout Create(Blockout blockout)
        {
            _db.Blockouts.Add(blockout);
            _db.SaveChanges();
            return blockout;
        }
    }
}
