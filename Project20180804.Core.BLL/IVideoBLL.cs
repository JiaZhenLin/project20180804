using System.Threading.Tasks;

namespace Project20180804.Core.BLL
{
    public interface IVideoBLL
    {
        Task<string> GetVideoUrlAsync();
        Task AddVideoAsync(string attachmentUrl);
    }
}
