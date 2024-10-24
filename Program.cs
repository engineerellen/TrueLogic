// CODING CHALLENGE: REFACTOR
//
// Instructions:
//
// - It's recommended to use Visual Studio, Rider or VS Code on your machine to do this challenge.
// - Create a Console App or a Class Library to do it.
// - Keep it simple, you have about 20~30 minutes.
// - Please share your screen with the interviewer while doing it.
//
// The code below grabs products from a database and lists them using the console. Please make the improvements listed below:
//
// 1. Make the DbConnection immutable and receive it on the constructor.
//     1.1. Optional: Replace by a Func<DbConnection> if you are familiar with it.
// 2. Use an 'using' clause where it's proper to use.
// 3. Handle possible errors in the execution of the code. If any error happens, notify by using Console.WriteLine;
// 4. Make sure the DB connection is always closed at the end of the RunExerciseLogic method.
// 5. Create separate private methods:
//     5.1. 1) One method for reading from the database that return a list of the products
//     5.2. 2) Another method to write the product list to the Console
//     5.3. Change the RunExerciseLogic method to only call both methods


using System.Data.SqlClient;
using Test;

var connection = () => new SqlConnection("connstring");
ProductRepository repository = new ProductRepository(connection);

repository.RunExerciseLogic(1, "Xiaomi 14 PRO", (decimal)1000.00);


//graduenz@truelogic.io
