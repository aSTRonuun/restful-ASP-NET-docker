using RestWithASPNETUdemy.Data.VO;

namespace RestWithASPNETUdemy.Business;

public interface IFileBusiness
{
    public byte[] GetFile(string fileName);

    public Task<FileDatailsVO> SaveFileToDisk(IFormFile file);
    public Task<List<FileDatailsVO>> SaveFilesToDisk(IList<IFormFile> files);
}
