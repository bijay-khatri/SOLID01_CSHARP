using MySql.Data.MySqlClient;
using SingleResposibility.Entity;
using SingleResposibility.Utility;
using System;
using System.Collections.Generic;


namespace SingleResposibility.Repository
{
    class MySqlStudentRepository : IRepository<Student>
    {
        /**
         * Table Structure (PK and AUTO INCREMENT NOT SHOWN)
         * CREATE TABLE `students` (
                                    `id` int(11) NOT NULL,
                                    `name` varchar(50) NOT NULL,
                                    `height` double NOT NULL
                                    );
         * 
         * 
         *  
         */
        private readonly string _connectionString = 
            @"server=localhost;user=root;password=;database=testdb";
        ILogger logger;
        private MySqlConnection _connection;

        public MySqlStudentRepository(ILogger logger)
        {
            this.logger = logger;
            if (_connection == null)
                _connection = new MySqlConnection(_connectionString);
        }

        public bool Save(Student student)
        {
            bool flag = true;
            this.OpenConnection();

            string query = "INSERT INTO Students(id, name,height) VALUES(@id,@name,@height)";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", student.Id);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@height", student.Height);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Exception while inserting");
                //checking the generics by specifying the MySQLException as T
                logger.Log<MySqlException>(e);
                flag = false;
            }
            finally
            {
                this.CloseConnection();
                
            }
            return flag;

        }

        public bool Delete(Student student)
        {
            bool flag = true;
            this.OpenConnection();

            string query = "DELETE FROM Students WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", student.Id);
            

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Exception while inserting");
                logger.Log<string>(e.Message);
                flag = false;
            }
            finally
            {
                this.CloseConnection();

            }
            return flag;
        }

        /**
         * read the database for all students
         * returns the list of students
         */
        public IList<Student> Read()
        {
            IList<Student> result = new List<Student>();
            this.OpenConnection();

            string query = "SELECT * from Students";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            try
            {
                using (MySqlDataReader dr = cmd.ExecuteReader()) { 
                    while (dr.Read())
                    {
                        result.Add(new Student
                        {
                            Id = int.Parse(dr["id"].ToString()),
                            Name = dr["name"].ToString(),
                            Height = double.Parse(dr["height"].ToString())
                        });
                    }
                }
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Error while reading");
                logger.Log<string>(e.Message);
               
            }
            finally
            {
                this.CloseConnection();
            }
           
            return result;
            
        }

        /**
         * Reads the single record form the database
         * @param id the integer value of key of student to be read
         * @return Student object 
         */ 
        public Student Read(int id)
        {
            this.OpenConnection();
            Student student = null;
            string query = "SELECT * from Students where id = @id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@id", id);
         
            try
            {
                using (MySqlDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow))
                {
                    if (dr.Read())
                    {
                        student = new Student
                        {
                            Id = int.Parse(dr["id"].ToString()),
                            Name = dr["name"].ToString(),
                            Height = double.Parse(dr["height"].ToString())
                        };
                    }
                    
                }
                    
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading");
                logger.Log<string>(e.Message);
            }
            finally
            {
                this.CloseConnection();
            }
            return student;
        }

        public bool Update(Student student)
        {
            bool flag = true;
            this.OpenConnection();

            string query = "UPDATE Students SET name = @name, height = @height WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            cmd.Parameters.AddWithValue("@name", student.Name);
            cmd.Parameters.AddWithValue("@height", student.Height);
            cmd.Parameters.AddWithValue("@id", student.Id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Exception while updating");
                logger.Log<string>(e.Message);
                flag = false;
            }
            finally
            {
                this.CloseConnection();

            }
            return flag;
        }

        private bool OpenConnection()
        {
           try
            {
                _connection.Open();
            }
            catch(MySqlException e)
            {
                Console.WriteLine("Error occured while opening...");
                logger.Log(e.Message);
                return false;
            }
            return true;
        }

        private bool CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error occured while closing...");
                logger.Log<string>(e.Message);
                return false;
            }
            return true;
        }
    }
}
