using System;
using AutoMapper;
using System.Collections.Generic;
using WineDocumentation.Core.Domain;
using WineDocumentation.Infrastructure.DTO;
using WineDocumentation.Core.Repositoies;
using System.Threading.Tasks;

namespace WineDocumentation.Infrastructure.Service
{

    /// <summary>
    /// UserServices - UserRepository management and mapping their objects as DTO.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMapper _mapper;

        /// <summary>
        /// Init UserServices instance.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="encrypter"></param>
        /// <param name="mapper"></param>
        public UserService(
            IUserRepository userRepository, 
            // IEncrypter encrypter,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            // _encrypter = encrypter;
            _mapper = mapper;

            _encrypter = new Encrypter();
        }

        /// <summary>
        /// Get User from UserRepository by email address and map as UserDto.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);
            return _mapper.Map<User,UserDto>(user);
        }

        /// <summary>
        /// Get Users collection from UserRepository and map as UserDto IEnumerable collection.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// TODO! Login operation
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        public async Task<UserDto> LoginAsync(string email, string password)
        {
            try 
            {
                var user = await _userRepository.GetAsync(email);
                if(user == null)
                {
                    throw new Exception("Email is not recognized!");
                }
                
                // var hash = _encrypter.GetHash(password, user.Salt);
                if(password != user.Password)
                {
                    throw new Exception("Incalid passowrd.");
                }
                
                return _mapper.Map<User,UserDto>(user);;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Register a new User in repository.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        public async Task RegisterAsync(Guid userId, string email, string username, string password, string role)
        {
            try 
            {
                var user = await _userRepository.GetAsync(email);
                if(user != null)
                {
                    throw new Exception($"User with email: '{email}' already exists.");
                }

                // var salt = _encrypter.GetSalt(password);
                // var hash = _encrypter.GetHash(password, salt);
                user = new  User(userId, email, username, role, password, "salt");
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }
        }

        public async Task UpdateAsync(Guid userId, string email, string username, string password, string role)
        {
            var user = await _userRepository.GetAsync(userId);
            await user.UpdateAsync(email, username, password, role);
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _userRepository.RemoveAsync(userId);
        }
    }
}