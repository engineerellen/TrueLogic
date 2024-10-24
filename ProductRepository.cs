using System.Data.Common;

namespace Test
{

    public class ProductRepository
    {
        private readonly Func<DbConnection> _connection;

        public ProductRepository(Func<DbConnection> connection) =>
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));



        public void RunExerciseLogic(int id, string name, decimal price)
        {
            try
            {
                Product prd = new Product() { Id = id, Name = name, Price = price };

                WriteProduct(prd);
                ReadProducts();
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_connection().State != System.Data.ConnectionState.Closed)
                    _connection().Close();
            }

        }

        private void ReadProducts()
        {
            using (var conn = _connection())
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "SELECT Id, Name, Price FROM Products";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = reader.GetInt32(0);
                    product.Name = reader.GetString(1);
                    product.Price = reader.GetDecimal(2);

                    Console.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
                }
            }
        }

        private void WriteProduct(Product product)
        {
            using (var conn = _connection())
            {
                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "INSERT INTO Products(Id, Name, Price) VALUES(@Id, @Name, @Price) ";

                DbParameter parameterID = command.CreateParameter();
                parameterID.ParameterName = "@Id";
                parameterID.Value = product.Id;
                command.Parameters.Add(parameterID);

                DbParameter parameterName = command.CreateParameter();
                parameterName.ParameterName = "@Name";
                parameterName.Value = product.Name;
                command.Parameters.Add(parameterName);

                DbParameter parameterPrice = command.CreateParameter();
                parameterPrice.ParameterName = "@Price";
                parameterPrice.Value = product.Price;
                command.Parameters.Add(parameterPrice);

                var reader = command.ExecuteNonQuery();

            }
        }
    }

}
