using NerdyGadgets.Data;

namespace NerdyGadgets.Services
{
    public class BaseService
    {
        protected ApplicationDbContext _context;
        public BaseService(ApplicationDbContext context)
        {
            this._context = context;
        }
    }
}
