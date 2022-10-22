using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;

namespace LibraryManageSystem
{
    internal class DatabaseCon
    {
        public static String serverPath = "server=" + Server.IP + ";port=" + Server.PORT + ";user=" + Server.USER + ";password=" + Server.PASSWORD + "; database=" + Server.DATABASE + ";";
        public static String GetMD5(ref String pw)
        {
            String res = "";
            MD5 mD5=MD5.Create();
            byte[] data=mD5.ComputeHash(Encoding.UTF8.GetBytes(pw));
            foreach (byte b in data)
            {
                res += b.ToString("x2");
            }
            return res;
        }
        public static bool LoginDect(ref String user,ref String password, ref bool userType) //1-admin 0-reader
        {
            String typeStr = userType ? "admin" : "reader";
            String pwMD5 = GetMD5(ref password);

            MySqlConnection conn = new MySqlConnection(serverPath);
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                return false;
            }

            String sql = "select * from account where username = @para1 and pwMD5 = @para2 and userType = @para3";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user);
            cmd.Parameters.AddWithValue("para2", pwMD5);
            cmd.Parameters.AddWithValue("para3", typeStr);

            MySqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                dr.Close();
                conn.Close();
                return true;
            }
            else
            {
                dr.Close();
                conn.Close();
                return false;
            }
        }
        public static bool CreateAccount(ref String user, ref String password)
        {
            String pwMD5 = GetMD5(ref password);

            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "Insert INTO account values(@para1,@para2,'reader')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", user);
            cmd.Parameters.AddWithValue("para2", pwMD5);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool UpdatePW(ref String user, ref String password, ref string userType)
        {
            String pwMD5 = GetMD5(ref password);

            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "update account set pwMD5 = @para";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para",pwMD5);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static void BookInfo(ref ObservableCollection<Book> books)
        {
            books.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from books";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                Book book = new Book();

                book.ID = dr.GetString(0);
                book.Category = dr.GetString(1);
                book.Name=dr.GetString(2);
                book.Press = dr.GetString(3);
                book.Year = dr.GetInt32(4);
                book.Author = dr.GetString(5);
                book.ISBN = dr.GetString(6);
                book.Price = dr.GetDecimal(7);
                book.totalNum = dr.GetInt32(8);
                book.curNum = dr.GetInt32(9);

                books.Add(book);
            }

            dr.Close();
            conn.Close();
        }
        public static bool AddBook(ref Book book)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "Insert INTO books values(@para1,@para2,@para3,@para4,@para5,@para6,@para7,@para8,@para9,@para10)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", book.ID);
            cmd.Parameters.AddWithValue("para2", book.Category);
            cmd.Parameters.AddWithValue("para3", book.Name);
            cmd.Parameters.AddWithValue("para4", book.Press);
            cmd.Parameters.AddWithValue("para5", book.Year);
            cmd.Parameters.AddWithValue("para6", book.Author);
            cmd.Parameters.AddWithValue("para7", book.ISBN);
            cmd.Parameters.AddWithValue("para8", book.Price);
            cmd.Parameters.AddWithValue("para9", book.totalNum);
            cmd.Parameters.AddWithValue("para10", book.curNum);

            try
            { 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool RemoveBook(string bID)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "delete from books where book_ID = @para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", bID);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool SelectSingleBook(ref Book book)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from books where book_ID = @para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", book.ID);
            
            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                dr.Close();
                conn.Close();
                return false;
            }
            while (dr.Read())
            {
                book.Category = dr.GetString(1);
                book.Name = dr.GetString(2);
                book.Press = dr.GetString(3);
                book.Year = dr.GetInt32(4);
                book.Author = dr.GetString(5);
                book.ISBN = dr.GetString(6);
                book.Price = dr.GetDecimal(7);
                book.totalNum = dr.GetInt32(8);
                book.curNum = dr.GetInt32(9);
            }
            dr.Close();
            conn.Close();
            return true;
        }
        public static bool UpdateBook(ref Book book)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "update books set category = @para1, book_name = @para2, press = @para3, year = @para4, author = @para5, ISBN = @para6, price = @para7, totalNum = @para8, curNum = @para9 where book_ID = @para10";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para10", book.ID);
            cmd.Parameters.AddWithValue("para1", book.Category);
            cmd.Parameters.AddWithValue("para2", book.Name);
            cmd.Parameters.AddWithValue("para3", book.Press);
            cmd.Parameters.AddWithValue("para4", book.Year);
            cmd.Parameters.AddWithValue("para5", book.Author);
            cmd.Parameters.AddWithValue("para6", book.ISBN);
            cmd.Parameters.AddWithValue("para7", book.Price);
            cmd.Parameters.AddWithValue("para8", book.totalNum);
            cmd.Parameters.AddWithValue("para9", book.curNum);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static void BookStatInfo(ref List<int> res)
        {
            res.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sqlT = "select count(*) from books";
            String sqlA = "select count(*) from books where book_type = 'FREE'";
            String sqlB = "select count(*) from books where book_type = 'BORROWED'";
            String sqlN = "select count(*) from books where book_type = 'N/A'";

            MySqlCommand cmd = new MySqlCommand(sqlT, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            res.Add(dr.GetInt32(0));
            dr.Close();
            
            cmd = new MySqlCommand(sqlA, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            res.Add((int)dr.GetInt32(0));
            dr.Close();

            cmd = new MySqlCommand(sqlB, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            res.Add((int)dr.GetInt32(0));
            dr.Close();

            cmd = new MySqlCommand(sqlN, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            res.Add((int)dr.GetInt32(0));
            dr.Close();

            conn.Close();
        }





        public static void ReaderInfo(ref ObservableCollection<Reader> readers)
        {
            readers.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from readers";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Reader reader = new Reader();

                reader.ID = dr.GetString(0);
                reader.Name = dr.GetString(1);
                reader.Depart = dr.GetString(2);

                String t = dr.GetString(3);
                if(t=="T")
                {
                    reader.Type = "教师";
                }
                else
                {
                    reader.Type = "学生";
                }
                reader.Num = dr.GetInt32(4);
                reader.Credit = dr.GetInt32(5);

                readers.Add(reader);
            }

            dr.Close();
            conn.Close();
        }
        public static bool AddReader(ref Reader reader)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "Insert INTO readers values(@para1,@para2,@para3,@para4,@para5,@para6)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", reader.ID);
            cmd.Parameters.AddWithValue("para2", reader.Name);
            cmd.Parameters.AddWithValue("para3", reader.Depart);
            cmd.Parameters.AddWithValue("para4", reader.Type);
            cmd.Parameters.AddWithValue("para5", reader.Num);
            cmd.Parameters.AddWithValue("para6", reader.Credit);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool RemoveReader(string rID)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "delete from readers where reader_ID = @para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", rID);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool SelectSingleReader(ref Reader reader)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from readers where reader_ID = @para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", reader.ID);

            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                dr.Close();
                conn.Close();
                return false;
            }
            while (dr.Read())
            {
                reader.Name = dr.GetString(1);
                reader.Depart = dr.GetString(2);
                reader.Type = dr.GetString(3);
                reader.Num = dr.GetInt32(4);
                reader.Credit = dr.GetInt32(5);
            }
            dr.Close();
            conn.Close();
            return true;
        }
        public static bool UpdateReader(ref Reader reader)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "update readers set reader_name = @para1, department = @para2, reader_Type = @para3, cur_num = @para4, credit = @para5 where reader_ID = @para6";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para6", reader.ID);
            cmd.Parameters.AddWithValue("para1", reader.Name);
            cmd.Parameters.AddWithValue("para2", reader.Depart);
            cmd.Parameters.AddWithValue("para3", reader.Type);
            cmd.Parameters.AddWithValue("para4", reader.Num);
            cmd.Parameters.AddWithValue("para5", reader.Credit);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }



        public static void BorrowInfo(ref ObservableCollection<Borrow> borrows)
        {
            borrows.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from borrow";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Borrow borrow = new Borrow();

                borrow.ID = dr.GetInt32(0);
                borrow.reader_ID = dr.GetString(1);
                borrow.book_ID = dr.GetString(2);
                borrow.bDate = dr.GetDateTime(3);
                borrow.pDate = dr.GetDateTime(4);
                borrow.BorrowDate = borrow.bDate.ToShortDateString().ToString();
                borrow.PlanDate = borrow.pDate.ToShortDateString().ToString();
                if(dr.IsDBNull(5))
                {
                    borrow.AcutalDate = "未归还";
                    borrow.aDate = DateTime.MaxValue;
                }
                else
                {
                    borrow.aDate = dr.GetDateTime(5);
                    borrow.AcutalDate = borrow.aDate.ToShortDateString().ToString();
                }

                borrows.Add(borrow);
            }

            dr.Close();
            conn.Close();
        }
        public static bool AddBorrow(ref Borrow borrow)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "Insert INTO borrow values(NULL, @para2, @para3, @para4, @para5, @para6)";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para2", borrow.reader_ID);
            cmd.Parameters.AddWithValue("para3", borrow.book_ID);
            cmd.Parameters.AddWithValue("para4", borrow.bDate.ToShortDateString().ToString());
            cmd.Parameters.AddWithValue("para5", borrow.pDate.ToShortDateString().ToString());
            cmd.Parameters.AddWithValue("para6", borrow.aDate == DateTime.MaxValue ? DBNull.Value : borrow.aDate);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public static bool SelectSingleBorrow(ref Borrow borrow)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from borrow where borrow_ID = @para1";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", borrow.ID);

            MySqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                dr.Close();
                conn.Close();
                return false;
            }
            while (dr.Read())
            {
                borrow.reader_ID = dr.GetString(1);
                borrow.book_ID = dr.GetString(2);
                borrow.bDate = dr.GetDateTime(3);
                borrow.pDate = dr.GetDateTime(4);

                if (dr.IsDBNull(5))
                {
                    borrow.aDate = DateTime.MaxValue;
                }
                else
                {
                    borrow.aDate = dr.GetDateTime(5);
                }
            }
            dr.Close();
            conn.Close();
            return true;
        }
        public static bool UpdateBorrow(ref Borrow borrow)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "update borrow set reader_ID = @para1, book_ID = @para2, borrow_date = @para3, planned_date = @para4, acutal_date = @para5 where borrow_ID = @para6";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para6", borrow.ID);
            cmd.Parameters.AddWithValue("para1", borrow.reader_ID);
            cmd.Parameters.AddWithValue("para2", borrow.book_ID);
            cmd.Parameters.AddWithValue("para3", borrow.bDate.ToShortDateString().ToString());
            cmd.Parameters.AddWithValue("para4", borrow.pDate.ToShortDateString().ToString());
            cmd.Parameters.AddWithValue("para5", borrow.aDate == DateTime.MaxValue ? DBNull.Value : borrow.aDate);

            try
            {
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }




        public static void BorrowReaderInfo(ref ObservableCollection<Borrow> borrows,string ID)
        {
            borrows.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from borrow where reader_ID = @para";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para", ID);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Borrow borrow = new Borrow();

                borrow.ID = dr.GetInt32(0);
                borrow.reader_ID = dr.GetString(1);
                borrow.book_ID = dr.GetString(2);
                borrow.bDate = dr.GetDateTime(3);
                borrow.pDate = dr.GetDateTime(4);
                borrow.BorrowDate = borrow.bDate.ToShortDateString().ToString();
                borrow.PlanDate = borrow.pDate.ToShortDateString().ToString();
                if (dr.IsDBNull(5))
                {
                    borrow.AcutalDate = "未归还";
                    borrow.aDate = DateTime.MaxValue;
                }
                else
                {
                    borrow.aDate = dr.GetDateTime(5);
                    borrow.AcutalDate = borrow.aDate.ToShortDateString().ToString();
                }

                borrows.Add(borrow);
            }

            dr.Close();
            conn.Close();
        }
        public static bool ReaderBorrowBook(ref Reader reader, ref Book book)
        {
            if (reader.Num < 10 && reader.Credit >= 2 && book.curNum > 0)
            {
                Borrow borrow = new Borrow();

                borrow.reader_ID = reader.ID;
                borrow.book_ID = book.ID;
                borrow.bDate = DateTime.Now;
                borrow.pDate = DateTime.Now.AddDays(30);
                borrow.aDate = DateTime.MaxValue;

                if(AddBorrow(ref borrow))
                {
                    reader.Num++;
                    book.curNum--;
                    if(UpdateBook(ref book)&&UpdateReader(ref reader))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool ReaderReturnBook(ref Borrow borrow)
        {
            if (borrow.aDate == DateTime.MaxValue)
            {
                borrow.aDate = DateTime.Now;
                if (UpdateBorrow(ref borrow))
                {
                    Reader reader = new Reader();
                    Book book = new Book();
                    reader.ID = borrow.reader_ID;
                    book.ID = borrow.book_ID;
                    if (SelectSingleReader(ref reader) && SelectSingleBook(ref book))
                    {
                        book.curNum++;
                        reader.Num--;
                        if (borrow.aDate > borrow.pDate)  //late
                        {
                            if (reader.Credit != 0)
                            {
                                reader.Credit--;
                            }
                        }
                        else
                        {
                            if (reader.Credit != 5)
                            {
                                reader.Credit++;
                            }
                        }
                        if(UpdateBook(ref book)&& UpdateReader(ref reader))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static void StatData(ref StatPrint stat)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            int total, atv;
            int card, tea;
            String sql1 = "select sum(totalNum) from books";
            String sql2 = "select sum(curNum) from books";

            String sql4 = "select count(*) from readers";
            String sql5 = "select count(*) from readers where reader_Type = 'T'";


            MySqlCommand cmd = new MySqlCommand(sql1, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.IsDBNull(0))
            {
                total = 0;
            }
            else
            {
                total = dr.GetInt32(0);
            }
            stat.total = "馆藏书籍总数：" + total.ToString();
            dr.Close();

            cmd = new MySqlCommand(sql2, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                atv = 0;
            }
            else
            {
                atv = dr.GetInt32(0);
            }
            stat.atv = "在架书籍总数：" + atv.ToString();
            dr.Close();

            stat.brw = "借阅书籍总数：" + (total - atv).ToString();

            cmd = new MySqlCommand(sql4, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                card = 0;
            }
            else
            {
                card = dr.GetInt32(0);
            }
            stat.card = "借书卡总数：" + card.ToString();
            dr.Close();

            cmd = new MySqlCommand(sql5, conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.IsDBNull(0))
            {
                tea = 0;
            }
            else
            {
                tea = dr.GetInt32(0);
            }
            stat.tea = "教师总数：" + tea.ToString();
            dr.Close();

            stat.stu = "学生总数：" + (card - tea).ToString();

            conn.Close();
        }
        public static void QueryBook(ref ObservableCollection<Book> books, string type, string keyWord)
        {
            books.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            keyWord = "%" + keyWord + "%";
            String sql = "select * from books where " + type + " like '" + keyWord + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Book book = new Book();

                book.ID = dr.GetString(0);
                book.Category = dr.GetString(1);
                book.Name = dr.GetString(2);
                book.Press = dr.GetString(3);
                book.Year = dr.GetInt32(4);
                book.Author = dr.GetString(5);
                book.ISBN = dr.GetString(6);
                book.Price = dr.GetDecimal(7);
                book.totalNum = dr.GetInt32(8);
                book.curNum = dr.GetInt32(9);

                books.Add(book);
            }

            dr.Close();
            conn.Close();
        }
        public static void QueryYearMinMax(ref int min, ref int max)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select min(year),max(year) from books";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                dr.Read();
                min = dr.GetInt32(0);
                max = dr.GetInt32(1);
            }

            dr.Close();
            conn.Close();
        }
        public static void QueryPriceMinMax(ref decimal min, ref decimal max)
        {
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select min(price),max(price) from books";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                min = dr.GetDecimal(0);
                max = dr.GetDecimal(1);
            }

            dr.Close();
            conn.Close();
        }
        public static void QueryYear(ObservableCollection<Book> res, int start, int end)
        {
            res.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from books where year between @para1 and @para2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", start);
            cmd.Parameters.AddWithValue("para2", end);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Book book = new Book();

                book.ID = dr.GetString(0);
                book.Category = dr.GetString(1);
                book.Name = dr.GetString(2);
                book.Press = dr.GetString(3);
                book.Year = dr.GetInt32(4);
                book.Author = dr.GetString(5);
                book.ISBN = dr.GetString(6);
                book.Price = dr.GetDecimal(7);
                book.totalNum = dr.GetInt32(8);
                book.curNum = dr.GetInt32(9);

                res.Add(book);
            }

            dr.Close();
            conn.Close();
        }
        public static void QueryPrice(ObservableCollection<Book> res, decimal start, decimal end)
        {
            res.Clear();
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            String sql = "select * from books where price between @para1 and @para2";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para1", start);
            cmd.Parameters.AddWithValue("para2", end);
            MySqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Book book = new Book();

                book.ID = dr.GetString(0);
                book.Category = dr.GetString(1);
                book.Name = dr.GetString(2);
                book.Press = dr.GetString(3);
                book.Year = dr.GetInt32(4);
                book.Author = dr.GetString(5);
                book.ISBN = dr.GetString(6);
                book.Price = dr.GetDecimal(7);
                book.totalNum = dr.GetInt32(8);
                book.curNum = dr.GetInt32(9);

                res.Add(book);
            }

            dr.Close();
            conn.Close();
        }


        public static int MultiInsert(string filePath, string table)
        {
            String newPath = filePath.Replace("\\\\", "\\\\\\\\");
            MySqlConnection conn = new MySqlConnection(serverPath);
            conn.Open();

            string sql = "LOAD DATA INFILE @para REPLACE INTO TABLE " + table + " FIELDS TERMINATED BY ','";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("para", newPath);


            try
            {
                int res = cmd.ExecuteNonQuery();
                conn.Close();
                return res;
            }
            catch (Exception)
            { 
                conn.Close();
                return -1;
            }
        }

        public static void AdminRefreshData()
        {
            UserControlBookInfo.RefreshData();
            UserControlReaderInfo.RefreshData();
            UserControlBorrowInfo.RefreshData();
            StatInfo.RefreshData();
        }

        public static void UserRefreshData()
        {
            UserControlBookInfo.RefreshData();
            UserBorrowInfo.RefreshData();
            UserCardInfo.RefreshData();
        }
    }
}
