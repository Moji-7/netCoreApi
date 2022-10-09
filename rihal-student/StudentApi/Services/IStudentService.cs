

using StudentApi.Models;

namespace StudentApi.Services;
public interface IStudentService
{

    UserDTO GetById(Guid Id);

}