using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryBase<User> _userRepository;

        public UserService(IRepositoryBase<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.ListAsync();

            if (users == null || !users.Any())
            {
                return new List<UserDto>();
            }

            var userDtos = UserDto.CreateList(users);

            return userDtos;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found."); 
            }

            var userDto = UserDto.Create(user);

            return userDto;
        }

        public async Task<User> CreateUser(UserRequest user)
        {
            var users = await _userRepository.ListAsync();

            if (users.Any(u => u.Email == user.Email) || users.Any(u => u.UserName == user.UserName))
            {
                throw new Exception("A user with the same email or username already exists.");
            }
            User newUser;

            if (user.UserType == "Client")
            {
                newUser = new Client
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password= user.Password,
                    UserType = user.UserType,
                  
                };
            }
            else if (user.UserType == "Seller")
            {
                newUser = new Seller
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    UserType = user.UserType,
                    
                };
            }
            else if(user.UserType == "SysAdmin") 
            {
                newUser = new SysAdmin
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Password = user.Password,
                    UserType = user.UserType,

                };
            }
            else
            {
                throw new Exception("Invalid user type.");
            }
            return await _userRepository.AddAsync(newUser);
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found."); 
            }

            await _userRepository.DeleteAsync(user);
        }

        public async Task UpdateUser(int id, UserRequest updatedUser)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                throw new Exception("User not found."); 
            }

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.UserType = updatedUser.UserType;

            await _userRepository.UpdateAsync(user);
        }
    }
}
