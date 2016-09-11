using SingleResposibility.Entity;
using SingleResposibility.Repository;
using SingleResposibility.Service;
using SingleResposibility.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResposibility
{
    class Program
    {
        private IService<Student> _studentService;
        

        public Program()
        {
            _studentService = StudentServiceFactory.CreateStudentService();
        }
        
        static void Main(string[] args)
        {
            Print("Solid Principle in C#");
            var obj = new Program();

            Student student1 = new Student { Id = 1, Name = "Bijay", height = 5.8d};
            Student student2 = new Student { Id = 2, Name = "Ajay", height = 5.7d };
            Student student3 = new Student { Id = 3, Name = "Jhon", height = 6.0d };
            Student student4 = new Student { Id = 4, Name = "Doe", height = 5.0d };

            obj.Save(student1);
            obj.Save(student2);

            obj.DisplayList();

            obj.Save(student3);
            obj.Save(student4);

            obj.DisplayList();

            Print("Updating the value of height for student1 ");
            student1.height = 9.9d;
            obj.Update(student1);

            obj.DisplayList();


            Print("Deleting the 3rd Student");

            obj.Delete(student3);

            obj.DisplayList();


            //Print("Trying to delete non existing student");
            //obj.Delete(new Student { Id = 1 });
            //obj.DisplayList();


            Print("Info of student 2");
            obj.Display(2);
            Console.ReadKey();
        }

        private void Display(int v)
        {
            Print(this._studentService.Read(v));
        }

        static void Print(object s)
        {
            Console.WriteLine(s);
        }

        private void Save(Student s)
        {
            if (this._studentService.Save(s))
            {
                //success message
            }
        }

        private void Update(Student s)
        {
            if (this._studentService.Update(s))
            {
                //success message
            }
        }


        private void Delete(Student s)
        {
            if (this._studentService.Delete(s))
            {
                //success message
            }
        }

        private void DisplayList()
        {
            Print("Displaying All Students");
            var students = this._studentService.Read();
            foreach (var student in students)
            {
                Print(student);
            }
            Print("");
        }

       
    }
}
