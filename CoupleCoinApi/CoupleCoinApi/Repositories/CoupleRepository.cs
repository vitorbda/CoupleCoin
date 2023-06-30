using CoupleCoinApi.dbContext;
using CoupleCoinApi.Models;
using CoupleCoinApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoupleCoinApi.Repositories
{
    public class CoupleRepository : ICoupleRepository
    {
        #region Constructor
        private readonly AppDbContext _context;

        public CoupleRepository(AppDbContext context)
        {
            _context = context;
        }
        #endregion

        public bool CreateCouple(User user, User user2)
        {
            try
            {
                Couple couple = new Couple
                {
                    User1 = user,
                    User2 = user2,
                    IsActive = true
                };
                _context.Couple.Add(couple);
                _context.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public Couple GetActiveCoupleByUser(User user)
        {
            var couple = _context.Couple
                .Where(_ => _.User1 == user || _.User2 == user)
                .Where(_ => _.IsActive == true)
                .FirstOrDefault();

            return couple;
        }       

        public Couple GetActiveCoupleByUsername(string username)
        {
            var couple = _context.Couple
                .Include(c => c.User1)
                .Where(c => c.User1.UserName == username || c.User2.UserName == username)
                .Where(_ => _.IsActive == true)
                .FirstOrDefault();

            return couple;
        }

    }
}
