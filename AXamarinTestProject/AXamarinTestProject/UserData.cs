using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace AXamarinTestProject
    {
    [Table("UsersData")]
   public class UserData
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id {get; set;}
            public string Name {get; set;}
            
           [Unique]
            public string Login
            {
            get;
            set;
            }

            public string Password
            {
            get;
            set;
            }
            
        }
    }
