using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace AppSecAssignmentV2
{
    public partial class Login : System.Web.UI.Page
    {
        string AssignmentV2ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentV2Connection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //
            //Response.Redirect("Homepage.aspx", false);
            //
            string userid = tbEmail.Text.ToString().Trim();
            string pwd = tbPwd.Text.ToString().Trim();

            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    //if (validateCaptcha())
                    //{
                    if (userHash.Equals(dbHash))
                    {
                        Session["UserID"] = userid;

                        //create GUID & save into the session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;
                        //create new cookie with guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Response.Redirect("Homepage.aspx", false);
                    }
                    else
                    {
                        lblMsg.Text = "Email or password is invalid. Please try again.";
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
        }
        protected string getDBHash(string userid)
        {
            string h = null;

            SqlConnection conn = new SqlConnection(AssignmentV2ConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email = @userid";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@Email", userid);

            try
            {
                conn.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["PasswordHash"] != null)
                        {
                            if (sdr["PasswordHash"] != DBNull.Value)
                            {
                                h = sdr["PasswordHash"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { conn.Close(); }
            return h;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;

            SqlConnection conn = new SqlConnection(AssignmentV2ConnectionString);
            string sql = "Select PasswordSalt FROM Account WHERE Email = @userid";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@Email", userid);

            try
            {
                conn.Open();

                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["PasswordSalt"] != null)
                        {
                            if (sdr["PasswordSalt"] != DBNull.Value)
                            {
                                s = sdr["PasswordSalt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            finally { conn.Close(); }
            return s;
        }
    }

    //public class MyObject
    //{
    //    public string success { get; set; }
    //    public List<string> ErrorMessage { get; set; }
    //}

    //public bool validateCaptcha()
    //{
    //    bool result = true;

    //    string captchaResponse = Request.Form["g-recaptcha-response"];
    //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create
    //        ("https://www.google.com/recaptcha/api/siteverify?secret=6Lc0qkgaAAAAAJmsX9eBoqthFbReT-aSJyNXl0by & response=" + captchaResponse);

    //    try
    //    {
    //        using (WebResponse wRes = req.GetResponse())
    //        {
    //            using (StreamReader sr = new StreamReader(wRes.GetResponseStream()))
    //            {
    //                string jsonResponse = sr.ReadToEnd();
    //                //lblGScore.Text = jsonResponse.ToString();

    //                JavaScriptSerializer js = new JavaScriptSerializer();

    //                MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);
    //                result = Convert.ToBoolean(jsonObject.success);
    //            }
    //        }
    //        return result;
    //    }
    //    catch (WebException ex)
    //    {
    //        throw ex;
    //    }
    //}

}