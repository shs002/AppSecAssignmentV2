using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AppSecAssignmentV2
{
    public partial class Registration : System.Web.UI.Page
    {
        string AssignmentV2ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["AssignmentV2Connection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private int checkPassword(string password)
        {
            int score = 0;

            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }
            // score 2
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            else
            {
                score = 2;
            }
            // score 3
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            else
            {
                score = 3;
            }
            // score 4
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            else
            {
                score = 4;
            }
            // score 5
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            else
            {
                score = 5;
            }

            return score;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            int scores = checkPassword(tbPassword.Text);
            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }
            lblPwdCheck.Text = "Status : " + status;
            if (scores < 4)
            {
                lblPwdCheck.ForeColor = Color.Red;
                return;
            }
            lblPwdCheck.ForeColor = Color.Blue;


            string pwd = tbPassword.Text.ToString().Trim();
            // generate random salt
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            // fill array of bytes with a cryptographically strong sequence of random values
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            createAccount();
        }

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(AssignmentV2ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@FirstName, @LastName, @CreditCardInfo, @Email, @PasswordHash, @PasswordSalt, @DateOfBirth, @EmailVerified, @IV, @Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", tbFirstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", tbLastName.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreditCardInfo", Convert.ToBase64String(encryptData(tbCreditCardInfo.Text.Trim())));
                            cmd.Parameters.AddWithValue("@Email", tbEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@DateOfBirth", tbDateOfBirth.Text.Trim());
                            cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = ciper.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
    }
}