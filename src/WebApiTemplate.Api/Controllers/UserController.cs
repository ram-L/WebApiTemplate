using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Newtonsoft.Json;
using WebApiTemplate.Api.Authorization;
using WebApiTemplate.Application.Interfaces.Application;
using WebApiTemplate.Application.Models.User;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Exceptions;

namespace WebApiTemplate.Api.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="userService">The IUserService instance for managing user-related operations.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Retrieves a list of user summaries based on OData query options.
        /// </summary>
        /// <param name="options">The OData query options for filtering and paging.</param>
        /// <returns>An ActionResult containing a list of user summary DTOs.</returns>
        [HttpGet]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Read)]
        public async Task<ActionResult<IEnumerable<UserSummaryDto>>> GetUsers(ODataQueryOptions<UserSummaryDto> options)
        {
            try
            {
                // Create restrictions on allowed OData operations
                var validationSettings = new ODataValidationSettings();
                options.Validate(validationSettings);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException(ex.Message);
            }

            var users = await _userService.GetUsersAsync(options);

            if (users == null)
                throw new ResourceNotFoundException(PermissionResource.User, JsonConvert.SerializeObject(options.Filter));

            return Ok(users);
        }

        /// <summary>
        /// Retrieves a list of user details based on OData query options.
        /// </summary>
        /// <param name="options">The OData query options for filtering and paging.</param>
        /// <returns>An ActionResult containing a list of user detail DTOs.</returns>
        [HttpGet("detail")]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Read)]
        public async Task<ActionResult<IEnumerable<UserDetailDto>>> GetUserDetails(ODataQueryOptions<UserDetailDto> options)
        {
            try
            {
                // Create restrictions on allowed OData operations
                var validationSettings = new ODataValidationSettings();
                options.Validate(validationSettings);
            }
            catch (Exception ex)
            {
                throw new InvalidInputException(ex.Message);
            }

            var users = await _userService.GetUserDetailsAsync(options);

            if (users == null)
                throw new ResourceNotFoundException(PermissionResource.User, JsonConvert.SerializeObject(options.Filter));

            return Ok(users);
        }

        /// <summary>
        /// Retrieves a summary of a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An ActionResult of type UserSummaryDto.</returns>
        [HttpGet("{id}")]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Read)]
        public async Task<ActionResult<UserSummaryDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
                throw new ResourceNotFoundException(PermissionResource.User, id);

            return Ok(user);
        }

        /// <summary>
        /// Retrieves detailed information of a specific user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An ActionResult of type UserDetailDto.</returns>
        [HttpGet("detail/{id}")]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Read)]
        public async Task<ActionResult<UserDetailDto>> GetUserDetail(int id)
        {
            var user = await _userService.GetUserDetailByIdAsync(id);

            if (user == null)
                throw new ResourceNotFoundException(PermissionResource.User, id);

            return Ok(user);
        }

        /// <summary>
        /// Creates a new user based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing user information to add.</param>
        /// <returns>An ActionResult containing the ID of the added user.</returns>
        [HttpPost]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Create)]
        public async Task<ActionResult> CreateUser(UserAddDto dto)
        {
            var id = await _userService.AddUserAsync(dto);
            return Ok(await _userService.GetUserDetailByIdAsync(id));
        }

        /// <summary>
        /// Updates an existing user based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing updated user information.</param>
        /// <returns>An ActionResult containing the ID of the updated user.</returns>
        [HttpPut]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Update)]
        public async Task<ActionResult> UpdateUser(UserUpdateDto dto)
        {
            var id = await _userService.UpdateUserAsync(dto);
            return Ok(await _userService.GetUserDetailByIdAsync(dto.Id));
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An ActionResult indicating success or failure.</returns>
        [HttpDelete("{id}")]
        [ResourceAuthorize(PermissionResource.User, PermissionType.Delete)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
