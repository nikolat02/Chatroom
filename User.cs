using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1    //NameSpace ConsoleApplication1 is the ChatRoomClient side of the project
{
    public class User
    {
        private string ipAddress;
        private string userName;
        public string UserName
        {
            get { return userName; }
            private set
            {
                userName = value;
            }
        }

        public string IPAddress
        {
            get { return ipAddress; }
            private set
            {
                ipAddress = value;
            }
        }


        public User(string user)
        {
            UserName = user;
        }
    }

}
