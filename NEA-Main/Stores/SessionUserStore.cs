using NEA_Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEA_Main.Stores
{
    public class SessionUserStore
    {
        public AccountUser SessionUser { get; set; }
        public SessionUserStore(AccountUser user)
        {
            SessionUser = user;
        }
    }
}
