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
using System.IO;

namespace AppSecAssignmentV2
{
    public partial class Homepage : System.Web.UI.Page
    {
        string AssignmentV2ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentV2Connection"].ConnectionString;
        static byte[] Key = null;
        static byte[] IV = null;
        static string userid = null;
        static byte[] creditcardinfo = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    userid = Session["UserId"].ToString();
                    displayUserProfile(userid);
                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            //INCOMPLETE: session clear
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }
            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }

        protected string decryptData(byte[] cipherText)
        {
            string plainText = null;

            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                // create decryptor to perfrom stream transform
                ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptTransform, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return plainText;
        }

        protected void displayUserProfile(string userid)
        {
            SqlConnection conn = new SqlConnection(AssignmentV2ConnectionString);
            string sql = "select * FROM Account where Email=@userId";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@userId", userid);

            try
            {
                conn.Open();
                using (SqlDataReader sdr = command.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["Email"] != DBNull.Value)
                        {
                            lblUserID.Text = sdr["Email"].ToString();
                        }
                        if (sdr["CreditCardInfo"] != DBNull.Value)
                        {
                            creditcardinfo = Convert.FromBase64String(sdr["CreditCardIndo"].ToString());
                        }
                        if (sdr["IV"] != DBNull.Value)
                        {
                            IV = Convert.FromBase64String(sdr["IV"].ToString());
                        }
                        if (sdr["Key"] != DBNull.Value)
                        {
                            Key = Convert.FromBase64String(sdr["Key"].ToString());
                        }
                    }
                    lblCreditCardInfo.Text = decryptData(creditcardinfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}