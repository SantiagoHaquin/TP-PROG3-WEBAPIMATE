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
            // Obtener la lista de usuarios desde el repositorio
            var users = await _userRepository.ListAsync();

            // Verificar si la lista de usuarios es nula o vacía
            if (users == null || !users.Any())
            {
                return new List<UserDto>(); // Devolver una lista vacía
            }

            // Convertir cada usuario en un UserDto utilizando el método estático Create
            var userDtos = UserDto.CreateList(users);

            return userDtos;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            // Obtener el usuario desde el repositorio
            var user = await _userRepository.GetByIdAsync(id);

            // Verificar si el usuario es nulo
            if (user == null)
            {
                throw new Exception("User not found."); // Lanzar una excepción si el usuario no se encuentra
            }

            // Convertir la entidad User a UserDto utilizando el método estático Create
            var userDto = UserDto.Create(user);

            return userDto;
        }

        public async Task<User> CreateUser(UserRequest user)
        {
            var users = await _userRepository.ListAsync();

            // Verificar si ya existe un usuario con el mismo correo electrónico
            if (users.Any(u => u.Email == user.Email) || users.Any(u => u.UserName == user.UserName))
            {
                throw new Exception("A user with the same email or username already exists.");
            }
            User newUser;

            // Crear una instancia concreta basada en el tipo de usuario
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
            // Obtener el usuario desde el repositorio
            var user = await _userRepository.GetByIdAsync(id);

            // Verificar si el usuario existe
            if (user == null)
            {
                throw new Exception("User not found."); // Lanzar una excepción si el usuario no se encuentra
            }

            // Eliminar el usuario del repositorio
            await _userRepository.DeleteAsync(user);
        }

        public async Task UpdateUser(int id, UserRequest updatedUser)
        {
            // Obtener el usuario desde el repositorio
            var user = await _userRepository.GetByIdAsync(id);

            // Verificar si el usuario existe
            if (user == null)
            {
                throw new Exception("User not found."); // Lanzar una excepción si el usuario no se encuentra
            }

            // Actualizar las propiedades del usuario con los datos del usuario actualizado
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.UserType = updatedUser.UserType;

            // Guardar los cambios en el repositorio
            await _userRepository.UpdateAsync(user);
        }
    }
}
