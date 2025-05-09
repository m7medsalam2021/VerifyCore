using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static List<User> _users = new List<User>();
        private static int _nextId = 1;

        public void Add(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
        }

        public User GetByUsername(string username)
        {
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}
