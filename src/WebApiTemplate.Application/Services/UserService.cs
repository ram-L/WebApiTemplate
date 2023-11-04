using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Serilog.Events;
using WebApiTemplate.Application.Extensions;
using WebApiTemplate.Application.Interfaces.Api;
using WebApiTemplate.Application.Interfaces.Application;
using WebApiTemplate.Application.Interfaces.Infrastructure;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.Application.Validations.User;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Exceptions;
using WebApiTemplate.SharedKernel.Helpers;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;
using ValidationException = WebApiTemplate.SharedKernel.Exceptions.ValidationException;

namespace WebApiTemplate.Application.Services
{
    /// <summary>
    /// Provides services for managing users.
    /// </summary>
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _userRepository;      

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="mapper">The mapper.</param>
        public UserService(
            IUserRepository userRepository,
            ICurrentUserContext currentUser,
            ICustomLogger logger,
            IMapper mapper)
            : base(currentUser, logger, mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <inheritdoc/>
        public async Task<PagedResult<UserSummaryDto>> GetUsersAsync(ODataQueryOptions<UserSummaryDto> options)
        {
            try
            {
                var query = _userRepository.GetBareQuery()
                    .Where(x => x.AccountType == AccountType.User)
                    .Include(x => x.UserProfile);

                // Project the entity to DTO
                var dto = query.ProjectTo<UserSummaryDto>(_mapper.ConfigurationProvider);

                return await QueryToPagedResultAsync(options, dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<PagedResult<UserDetailDto>> GetUserDetailsAsync(ODataQueryOptions<UserDetailDto> options)
        {
            try
            {
                var query = _userRepository.GetBareQuery()
                    .Where(x => x.AccountType == AccountType.User)
                    .Include(x => x.UserProfile);

                // Project the entity to DTO
                var dto = query.ProjectTo<UserDetailDto>(_mapper.ConfigurationProvider);

                return await QueryToPagedResultAsync(options, dto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<UserSummaryDto> GetUserByIdAsync(int id)
        {
            var entityLoader = new EntityLoader<Account>();
            entityLoader.AddIncludes(q => q.Include(u => u.UserProfile));

            var user = await _userRepository.FindByAsync(
                 x => x.AccountType == AccountType.User && x.Id == id,
                 entityLoader: entityLoader
            );

            return _mapper.Map<UserSummaryDto>(user);
        }

        /// <inheritdoc/>
        public async Task<UserDetailDto> GetUserDetailByIdAsync(int id)
        {
            var entityLoader = new EntityLoader<Account>();
            entityLoader.AddExplicits(u => u.UserProfile,
                        u => u.CreatedBy,
                        u => u.CreatedBy.UserProfile,
                        u => u.ModifiedBy,
                        u => u.ModifiedBy.UserProfile,
                        u => u.Owner,
                        u => u.Owner.UserProfile);

            var user = await _userRepository.FindByAsync(
                 x => x.AccountType == AccountType.User && x.Id == id,
                 entityLoader: entityLoader);

            return _mapper.Map<UserDetailDto>(user);
        }

        /// <inheritdoc/>
        public async Task<int> AddUserAsync(UserAddDto dto)
        {
            try
            {
                // Validate the input DTO
                await new UserAddDtoValidator(_logger).ValidateAsync(dto, PermissionResource.User.ToString());

                // Check if the username already exists
                var entityLoader = new EntityLoader<Account>();
                entityLoader.AddIncludes(q => q.Include(u => u.UserProfile));

                var existingUser = await _userRepository.FindByAsync(x => x.UserProfile.Username == dto.Username, entityLoader: entityLoader);
                if (existingUser != null)
                    throw new ValidationException("Username already exists");

                // Map the DTO to User entity and related entities
                var user = _mapper.Map<Account>(dto);
                user.Create(_currentUser.AccountId, AccountType.User);
                user.UserProfile.AddPassword(PasswordHelper.HashPassword(user, dto.Password));

                // Validate the user entity
                await new UserValidator(_logger).ValidateAsync(user, PermissionResource.User.ToString());

                //  Insert the user into the database
                var result = await _userRepository.InsertAsync(user);
                if (!result.IsSuccess)
                    throw new DatabaseException(result.Error);

                // Return the user's ID as the result
                return user.Id;
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the update process
                _logger.LogMessage("An error occurred while adding the user.", LogEventLevel.Fatal, ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<int> UpdateUserAsync(UserUpdateDto dto)
        {
            try
            {
                // Validate the user update DTO
                await new UserUpdateDtoValidator(_logger).ValidateAsync(dto, PermissionResource.User.ToString());

                // Find the user by ID, including loading related entity 'Info'
                var entityLoader = new EntityLoader<Account>();
                entityLoader.AddIncludes(q => q.Include(u => u.UserProfile));

                var user = await _userRepository.FindByAsync(x => x.Id == dto.Id,entityLoader: entityLoader);

                if (user == null)
                    throw new ValidationException("User does not exist");

                // Map the DTO properties to the user entity
                _mapper.Map(dto, user);
                user.SetUpdateAudit(_currentUser.AccountId);

                // Validate the user entity and related entities
                await new UserValidator(_logger).ValidateAsync(user, PermissionResource.User.ToString());

                // Update the user in the repository
                var result = await _userRepository.UpdateAsync(user);
                if (!result.IsSuccess)
                    throw new DatabaseException(result.Error);

                // Return the user's ID as the result
                return user.Id;
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the update process
                _logger.LogMessage("An error occurred while updating the user.", LogEventLevel.Fatal, ex);
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                // Find the user by ID
                var user = await _userRepository.FindByAsync(x => x.Id == id);
                if (user == null)
                    throw new ValidationException("User does not exist");

                user.SetDeleteAudit(_currentUser.AccountId);

                // Delete the user from the repository
                var result = await _userRepository.DeleteAsync(user);

                if (!result.IsSuccess)
                    throw new DatabaseException(result.Error);
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during the delete process
                _logger.LogMessage($"An error occurred while deleting the user {id}.", LogEventLevel.Fatal, ex);
                throw;
            }
        }
    }
}
