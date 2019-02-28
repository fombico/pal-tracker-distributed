using System.Threading.Tasks;

namespace Expenses
{
    public interface IProjectClient
    {
        Task<ProjectInfo> Get(long projectId);
    }
}