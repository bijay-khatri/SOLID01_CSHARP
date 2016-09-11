using SingleResposibility.Entity;
using SingleResposibility.Utility;
using System;
using System.Linq;
using System.Collections.Generic;


namespace SingleResposibility.Repository
{
    class InMemoryStudentRepository : IRepository<Student>
    {
        //Map as a in memomry database
        IDictionary<int, Student> db = new Dictionary<int, Student>();
        ILogger logger;

        public InMemoryStudentRepository(ILogger logger)
        {
            //Adhering to Dependency Injection Principle
            this.logger = logger;
        }
        
        public bool Save(Student student)
        {
            try
            {
                this.db.Add(student.Id, student);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot Save. Try again later");
                logger.Log(ex.Message);

            }
            return true;
        }

        public bool Delete(Student student)
        {
            //check if the student is actually in the database
            //can cause N+1 but it's okay now

            if (db[student.Id] != student) throw new Exception("Exception: trying to delete non existing object");
            try
            {
                this.db.Remove(student.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot Update. Try again later");
                logger.Log(ex.Message);

            }
            return true;
        }


        public bool Update(Student student)
        {
            //TODO: what if Object being updated in not in the database ?
            try
            {
                this.db[student.Id] = student;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot Update. Try again later");
                logger.Log(ex.Message);

            }
            return true;
        }

       
        public IList<Student> Read()
        {
            var result = db.Values.ToList();

            //Programming to interface or LSP
            IList<Student> students = new List<Student>();

            foreach (var student in result)
            {
                students.Add(student);
            }
            return students;
        }

        public Student Read(int id)
        {
            return db[id];
        }
    }

}
