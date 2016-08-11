using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Sistema_Integrador.Classes
{
    class cnxBanco
    {
        public SqlConnection cnxLogin; //conexão do formulario de login
        public string ControleAcesso, UsuarioAcesso, SenhaAcesso, DataAcesso, HoraAcesso, HoraSaida, IpAcesso, HostName; //Controle de acesso

        public Boolean Login_Usuario(string Usuario, string Senha)
        {
            //Validação de login

            SqlCommand cmdLogin = new SqlCommand();

            DataAcesso = DateTime.Now.ToShortDateString();
            HoraAcesso = DateTime.Now.ToShortTimeString();

            cnxLogin = new SqlConnection(Properties.Settings.Default.StrSQLSERVER);

            try
            {
                cnxLogin.Open();
                cmdLogin.CommandText = "select * from tblogin where usuario='" + Usuario + "'and senha='" + Senha + "'";
                cmdLogin.Connection = cnxLogin;
                SqlDataReader drLogin = cmdLogin.ExecuteReader();
                if ((drLogin.HasRows))
                {
                    while (drLogin.Read())
                    {
                        ControleAcesso = Convert.ToString(drLogin["acesso"]);
                        UsuarioAcesso = Usuario;
                        SenhaAcesso = Senha;
                    }
                    drLogin.Close();
                    drLogin.Dispose();
                    return true;
                }
                else
                {
                    drLogin.Close();
                    drLogin.Dispose();
                    return false;
                }
            }
            catch (Exception)
            {

            }
            MessageBox.Show("Banco de Dados indisponível");
            return false;
            cnxLogin.Close();
          }
    }
}
