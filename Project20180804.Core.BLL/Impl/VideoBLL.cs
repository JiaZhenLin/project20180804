using Microsoft.EntityFrameworkCore;
using Project20180804.Core.DAL.Repositorys;
using Project20180804.Core.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Project20180804.Core.BLL.Impl
{
    public class VideoBLL: IVideoBLL
    {
        private readonly IRepository<Video> _videoRepository;
        private readonly IRepository<Attachment> _attachmentRepository;

        public VideoBLL(IRepository<Video> videoRepository, IRepository<Attachment> attachmentRepository)
        {
            _videoRepository = videoRepository;
            _attachmentRepository = attachmentRepository;
        }

        public async Task<string> GetVideoUrlAsync()
        {
            var video = await _videoRepository.Table.Include(x=>x.Attachment).OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

            if(video != null)
            {
                return video.Attachment.Url;
            }

            return string.Empty;
        }

        public async Task AddVideoAsync(string attachmentUrl)
        {
            var attachment = new Attachment()
            {
                Url = attachmentUrl
            };

            var video = new Video()
            {
                AttachmentID = attachment.ID
            };

            await _attachmentRepository.InsertAsync(attachment);

            await _videoRepository.InsertAsync(video);
        }
    }
}
