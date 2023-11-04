using Microsoft.AspNetCore.OData.Query;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Application.Interfaces.Application
{
    /// <summary>
    /// Represents the service for managing users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets a paged list of user summaries based on OData query options.
        /// </summary>
        /// <param name="options">The OData query options for filtering and paging.</param>
        /// <returns>A paged result of user summary DTOs.</returns>
        Task<PagedResult<UserSummaryDto>> GetUsersAsync(ODataQueryOptions<UserSummaryDto> options);

        /// <summary>
        /// Gets a paged list of user details based on OData query options.
        /// </summary>
        /// <param name="options">The OData query options for filtering and paging.</param>
        /// <returns>A paged result of user detail DTOs.</returns>
        Task<PagedResult<UserDetailDto>> GetUserDetailsAsync(ODataQueryOptions<UserDetailDto> options);

        /// <summary>
        /// Gets a user summary by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user summary DTO.</returns>
        Task<UserSummaryDto> GetUserByIdAsync(int id);

        /// <summary>
        /// Gets a user detail by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The user detail DTO.</returns>
        Task<UserDetailDto> GetUserDetailByIdAsync(int id);

        /// <summary>
        /// Adds a new user based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing user information to add.</param>
        /// <returns>The ID of the added user.</returns>
        Task<int> AddUserAsync(UserAddDto dto);

        /// <summary>
        /// Updates an existing user based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing updated user information.</param>
        /// <returns>The ID of the updated user.</returns>
        Task<int> UpdateUserAsync(UserUpdateDto dto);

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        Task DeleteUserAsync(int id);
    }
}
