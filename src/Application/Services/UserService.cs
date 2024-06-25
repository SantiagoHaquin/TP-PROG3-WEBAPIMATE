using Application.Interfaces;
using Application.Models;
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

        public async Task<User> CreateUser(User user)
        {
            // Crear un nuevo usuario en el repositorio
            return await _userRepository.AddAsync(user);
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

        public async Task UpdateUser(int id, User updatedUser)
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
