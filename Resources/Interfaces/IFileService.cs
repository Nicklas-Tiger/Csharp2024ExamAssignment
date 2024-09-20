using Resources.Models;

namespace Resources.Interfaces;

public interface IFileService
{
    public ResponseResult<string> SaveToFile(string content);
    public ResponseResult<string> GetFromFile();
}
