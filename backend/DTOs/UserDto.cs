using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Country { get; set; }
        public string DateOfBirth { get; set; }
        public string Token { get; set; }
    }
}