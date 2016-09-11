using SingleResposibility.Entity;
using SingleResposibility.Repository;
using System;
using System.Collections.Generic;


namespace SingleResposibility.Service
{
    class StudentService : IService<Student>
    {
        private IRepository<Student> _studentRepository;

        public StudentService(IRepository<Student> repository)
        {
            this._studentRepository = repository;
        }
        public bool Delete(Student newObject)
        {
            return _studentRepository.Delete(newObject);
        }

        public IList<Student> Read()
        {
            return _studentRepository.Read();
        }

        public Student Read(int id)
        {
            return _studentRepository.Read(id);
        }

        public bool Save(Student newObject)
        {
            return _studentRepository.Save(newObject);
        }

        public bool Update(Student updatedObject)
        {
            return _studentRepository.Update(updatedObject);
        }
    }
}
