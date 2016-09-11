using SingleResposibility.Entity;
using SingleResposibility.Repository;
using SingleResposibility.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility.Utility
{
    static class StudentServiceFactory
    {
        public static IService<Student> CreateStudentService()
        {
            ILogger logger = new FileLogger();
            IRepository<Student> repo = new InMemoryStudentRepository(logger);
            IService<Student> studentService = new StudentService(repo);
            return studentService;
        }

    }
}
