using System.Threading.Tasks;

namespace doremi.Services
{
    public interface IRoles
    {
        Task GenerateRolesFromPagesAsync();

        Task AddToRoles(string applicationUserId);

        Task AddRoleByName(string applicationUserId, string roleName);
    }
}
