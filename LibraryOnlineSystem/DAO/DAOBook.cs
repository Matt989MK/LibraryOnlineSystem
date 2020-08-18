using LibraryOnlineSystem.Connection;
using LibraryOnlineSystem.Models;
using LibraryOnlineSystem.Queries;
using System;
using System.Data.SqlClient;
namespace LibraryOnlineSystem
{
    public class DAOBook
    {
        public Book getSelectedBook(int id)
        {
            Book book = new Book();

            // Create a variable for the connection string
            String connectionUrl = ConnectionToDatabase.CONNECTIONURL;
            SqlConnection con = new SqlConnection(connectionUrl);
            con.Open();
            String SQL = QueriesBook.getSelectedBook(id);
            SqlCommand sqlCommand = new SqlCommand(SQL, con);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {

                //   [AuthorId],[AuthorId] ,[Name] ,[DateOfPublication],[Genre],[Overview],[Publisher]"
                book.BookId = sqlDataReader.GetInt32(0);
                //  book.AuthorList = sqlDataReader.GetInt32(0)
                book.Name = sqlDataReader.GetString(2);
                book.DateOfPublication = sqlDataReader.GetDateTime(3);
                // book.Genre = sqlDataReader.GetString(4);
                book.Overview = sqlDataReader.GetString(5);
                book.Publisher = sqlDataReader.GetString(6);




            }
            con.Close();

            return book;

        }

        //public bool DoesItCellExists(int idreport, int column, int row)
        //{
        //    // Create a variable for the connection string
        //    String connectionUrl = ConnectionReport.CONNECTIONURL;
        //    SqlConnection con = new SqlConnection(connectionUrl);
        //    con.Open();
        //    String SQL = QueriesCellArticleQuantity.doesSelectedCellArticleQuantityExists(idreport, column, row);
        //    SqlCommand sqlCommand = new SqlCommand(SQL, con);
        //    int numberOfCells = (int)sqlCommand.ExecuteScalar();
        //    if (numberOfCells > 0)
        //    {
        //        return true;
        //    }

        //    else
        //    {
        //        return false;
        //    }

        //}

        //public void InsertArticleQuantity(int idreport, int column, int row, string quantity)
        //{
        //    // Create a variable for the connection string
        //    String connectionUrl = ConnectionReport.CONNECTIONURL;
        //    SqlConnection con = new SqlConnection(connectionUrl);
        //    con.Open();
        //    String SQL = QueriesCellArticleQuantity.insertCellArticleQuantity(quantity, idreport, column, row);
        //    SqlCommand sqlCommand = new SqlCommand(SQL, con);
        //    sqlCommand.ExecuteNonQuery();
        //    con.Close();

        //}
        //public void UpdateArticleQuantity(int idreport, int column, int row, string quantity)
        //{
        //    // Create a variable for the connection string
        //    String connectionUrl = ConnectionReport.CONNECTIONURL;
        //    SqlConnection con = new SqlConnection(connectionUrl);
        //    con.Open();
        //    String SQL = QueriesCellArticleQuantity.UpdateCellArticleQuantity(quantity, idreport, column, row);
        //    SqlCommand sqlCommand = new SqlCommand(SQL, con);
        //    sqlCommand.ExecuteNonQuery();
        //    con.Close();

        //}
        //public void DeleteArticleQuantity(int idreport, int column, int row)
        //{
        //    // Create a variable for the connection string
        //    String connectionUrl = ConnectionReport.CONNECTIONURL;
        //    SqlConnection con = new SqlConnection(connectionUrl);
        //    con.Open();
        //    String SQL = QueriesCellArticleQuantity.DeleteCellArticleQuantity(idreport, column, row);
        //    SqlCommand sqlCommand = new SqlCommand(SQL, con);
        //    sqlCommand.ExecuteNonQuery();
        //    con.Close();

        //}
    }
}
