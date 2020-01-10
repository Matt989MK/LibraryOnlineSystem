using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryOnlineSystem.Queries
{
    public class QueriesBook
    {
        public static String getSelectedBook(int id)
        {

            String strSelectedBook = "SELECT   [AuthorId],[AuthorId] ,[Name] ,[DateOfPublication],[Genre],[Overview],[Publisher]"
                    + " FROM [dbLibraryOnlineSystem].[dbo].[TbBook] where [AuthorId]=" + (id).ToString();
            return strSelectedBook;
        }

        public static String doesBookExist(int name)
        {
            String doesBookExist = "SELECT count ([AuthorId]) as total FROM [DbLibraryOnlineSystem].[dbo].[tbBook]"
                                   + " where [Name] =" + (name).ToString();
                   return doesBookExist;
        }

        //TODO
        //insert

        public static String insertBook(int AuthorId,string Name, DateTime DateOfPublication, string Genre, string Overview,string Publisher)
        {
            String _insertBook = "INSERT INTO  [DbLibraryOnlineSystem].[dbo].[tbBook]"
                    + " ([AuthorId] ,[Name] ,[DateOfPublication],[Genre],[Overview],[Publisher]) "
                    + " VALUES (" + (AuthorId).ToString() + "," + (Name).ToString() + "," + (DateOfPublication).ToString() + (Genre).ToString() + "," +
                    "," + (Overview).ToString() + "," + (Publisher).ToString() + ") ";
            return _insertBook;
        }
        //update
        public static String UpdateBook(int AuthorId, string Name, DateTime DateOfPublication, string Genre, string Overview, string Publisher)
        {
            String _updateBook = "Update [DbLibraryOnlineSystem].[dbo].[tbBook]"
                   + " SET [AuthorId]=" + (AuthorId).ToString() + ",Name = "+ (Name).ToString() + ",DateOfPublication = " + (DateOfPublication).ToString() + 
                   ",Genre = " + (Genre).ToString() + ",Overview = " + (Overview).ToString() + ",Publisher = " + (Publisher).ToString() + " WHERE [wiersz] =";

            //+ " VALUES ("+ Integer.toString(row)+ ","+ Integer.toString(column) + ","+Float.toString(quantity)+

            return _updateBook;
        }
        public static String DeleteBook(int Id)
        {
            String _deleteBook = "DELETE FROM [DbLibraryOnlineSystem].[dbo].[tbBook]"
                                 + "  WHERE [AuthorId] =" + (Id).ToString();
                   

            //+ " VALUES ("+ Integer.toString(row)+ ","+ Integer.toString(column) + ","+Float.toString(quantity)+

            return _deleteBook;
        }
    }
}
