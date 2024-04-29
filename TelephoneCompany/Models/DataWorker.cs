using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
namespace TelephoneCompany.Models
{
    public static class DataWorker
    {
        private static string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        public static string connectionString = $@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={baseDirectory}\DataBase\TelephoneCompanyDataBase.mdb";
        
        public static List<MainWindowData> GetMainData()
        {
            using (IDbConnection db = new OleDbConnection(connectionString))
            {
                db.Open();
                var sql = @"
                SELECT 
                    Abonent.AbonentID, 
                    Abonent.SecondName, 
                    Abonent.Name, 
                    Abonent.Surname,
                    Streets.Street, 
                    Address.HouseNumber, 
                    PhoneNumber.HomeNumber, 
                    PhoneNumber.WorkNumber, 
                    PhoneNumber.MobileNumber
                FROM 
                    (((Abonent
                    INNER JOIN Address ON Abonent.AbonentID = Address.AbonentID)
                    INNER JOIN Streets ON Address.Street = Streets.StreetID)
                    INNER JOIN PhoneNumber ON Abonent.AbonentID = PhoneNumber.AbonentID)";
                var result = db.Query<MainWindowData>(sql).ToList();
                db.Close();
                return result;
            }
        }
        public static List<StreetsWindowData> GetStreets()
        {
            using (IDbConnection db = new OleDbConnection(connectionString))
            {
                db.Open();
                var sql = @"
                SELECT        
                    Streets.Street, COUNT(Address.Street) AS AddressCount
                FROM
                    (Streets INNER JOIN
                    Address ON Streets.StreetID = Address.Street)
                GROUP BY 
                    Streets.Street";
                var result = db.Query<StreetsWindowData>(sql).ToList();
                db.Close();
                return result;
            }
        }
        public static List<NumberSearchWindowData> SearchNumber(string number)
        {
            using (IDbConnection db = new OleDbConnection(connectionString))
            {
                db.Open();
                var sql = $@"
                SELECT        
                    Abonent.SecondName, Abonent.Name, Abonent.Surname
                FROM            
                    (PhoneNumber INNER JOIN
                    Abonent ON PhoneNumber.AbonentID = Abonent.AbonentID)
                WHERE  
                    (PhoneNumber.MobileNumber LIKE '{number}') OR
                    (PhoneNumber.WorkNumber LIKE '{number}') OR
                    (PhoneNumber.HomeNumber LIKE '{number}')";
                var result = db.Query<NumberSearchWindowData>(sql).ToList();
                db.Close();
                return result;
            }
        }
        public static void SaveToCSV(string[] str)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = $@"{baseDirectory}\Report\report_{DateTime.Now:yyyy.MM.dd_HH.mm.ss}.csv";
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                for (int i = 0; i < str.GetLength(0); i++)
                {
                    writer.WriteLine(str[i]);
                }
                writer.Close();
            }
        }
    }
}
