using Microsoft.Practices.Unity;
using SingleResposibility.Entity;
using SingleResposibility.Service;
using SingleResposibility.Repository;
using System;
using SingleResposibility.Utility;

namespace SingleResposibility
{
    class Program
    {
        private IService<Student> _studentService;


        /**
         * Constructor used by Unity DI
         */
        public Program(IService<Student> studentService)
        {
            this._studentService = studentService;
        }

        /**
         * Constructor used by Manual DI
         */ 
        public Program()
        {
            _studentService = StudentServiceFactory.CreateStudentService(ServiceType.MySQL);
        }

        static void Main(string[] args)
        {
            Print("Solid Principle in C#");
            ManualDI();

            UnityDI();
            Console.ReadKey();
        }

        private static void UnityDI()
        {
            Print("\nUnity DI");
            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IService<Student>, StudentService>();
            unityContainer.RegisterType<IRepository<Student>, MySqlStudentRepository>();
            unityContainer.RegisterType<ILogger, FileLogger>();
            var obj = unityContainer.Resolve<Program>();
            obj.DisplayList();
        }

        private static void ManualDI()
        {
            var obj = new Program();
            Print("Manula DI.....");
            Print("Creating four instances of students");
            Student student1 = new Student { Id = 1, Name = "Bijay", Height = 5.8d };
            Student student2 = new Student { Id = 2, Name = "Ajay", Height = 5.7d };
            Student student3 = new Student { Id = 3, Name = "Jhon", Height = 6.0d };
            Student student4 = new Student { Id = 4, Name = "Doe", Height = 5.0d };

            Print("Saving two instances of students");
            obj.Save(student1);
            obj.Save(student2);

            obj.DisplayList();

            Print("Saving another two instances of students");
            obj.Save(student3);
            obj.Save(student4);

            obj.DisplayList();

            Print("Updating the value of Height for student1 ");
            student1.Height = 9.9d;
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
