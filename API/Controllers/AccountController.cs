using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(DataContext context) : BaseApiController
    {
        
        [HttpPost("register")]
        public async Task<ActionResult<AppUser>> Register(RegisterDTO registerDto)
        {

            if(await UserExists(registerDto.Username))
            {
                return BadRequest("Username is taken");
            }
            
            var username = registerDto.Username.ToLower();
            var password = registerDto.Password;
            
           
            
            
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                 PasswordSalt= hmac.Key
               
            };
            
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Ok(user);
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
       
        
    }
}