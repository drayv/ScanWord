using System.IO;
using System.Threading.Tasks;
using WatchWord.Domain.Entity;

namespace WatchWord.Application.EntityServices.Abstract
{
    /// <summary>The material service interface.</summary>
    public interface IMaterialsService
    {
        Material CreateMaterial(Stream stream, MaterialType type, string name, string description, int userId);

        Task<int> SaveMaterial(Material material);
    }
}