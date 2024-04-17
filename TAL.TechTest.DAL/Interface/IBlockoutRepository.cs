using TAL.TechTest.DAL.Model;

namespace TAL.TechTest.DAL.Interface
{
    public interface IBlockoutRepository
    {
        public Blockout Get(TimeOnly time);
        public IEnumerable<Blockout> Get();
        public Blockout Create(Blockout blockout);
    }
}
