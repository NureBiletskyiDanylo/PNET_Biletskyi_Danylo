using BookCatalog_API.DTOs;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers
{
    public class MembersController(IUserRepository repository) : BaseApiController
    {
        [HttpGet("get-users")]
        public async Task<ActionResult> GetUsers()
        {
            var users = await repository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id:int}/update-role")]
        public async Task<ActionResult> UpdateUserRole(int id, [FromBody] UpdateUserRoleDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Updated role was not received");
            }

            bool success = await repository.UpdateUserRoleAsync(id, dto.Role);
            if (success)
            {
                return Ok();
            }
            return BadRequest("User role was not updated");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            bool success = await repository.RemoveUserByIdAsync(id);
            if (success)
            {
                return Ok();
            }
            return BadRequest("User was not removed");
        }
    }
}
