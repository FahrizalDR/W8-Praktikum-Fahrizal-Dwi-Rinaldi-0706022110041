using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace W8Praktikum
{
    public partial class FormHasilPertandingan : Form
    {
        public static string SqlConnection = "server = localhost; uid = root; pwd =; database = premier_league";
        public MySqlConnection sqlConnect = new MySqlConnection(SqlConnection);
        public MySqlCommand sqlCommand;
        public MySqlDataAdapter sqlAdapter;
        public string sqlQuery;

        public FormHasilPertandingan()
        {
            InitializeComponent();
        }        

        private void FormHasilPertandingan_Load(object sender, EventArgs e)
        {            
            DataTable dataTeamKiri = new DataTable();
            sqlQuery = "SELECT team_name as 'Team Name', team_id as 'ID Team' FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dataTeamKiri);            
            ComboBoxKiri.DisplayMember = "Team Name";
            ComboBoxKiri.ValueMember = "ID Team";
            ComboBoxKiri.DataSource = dataTeamKiri;

            DataTable dataTeamKanan = new DataTable();
            sqlQuery = "SELECT team_name as 'Team Name', team_id as 'ID Team' FROM team";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dataTeamKanan);
            ComboBoxKanan.DisplayMember = "Team Name";
            ComboBoxKanan.ValueMember = "ID Team";
            ComboBoxKanan.DataSource = dataTeamKanan;


        }

        private void ComboBoxKiri_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataManagerDanCaptainKiri = new DataTable();
            sqlQuery = "SELECT manager.manager_name as `Manager Name`, player.player_name as `Captain Name` FROM team, player, manager WHERE team.captain_id = player.player_id AND manager.manager_id = team.manager_id AND team.team_id = '"+ComboBoxKiri.SelectedValue.ToString()+"'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dataManagerDanCaptainKiri);
            OutputManagerKiri.Text = dataManagerDanCaptainKiri.Rows[0]["Manager Name"].ToString();
            OutputKaptenKiri.Text = dataManagerDanCaptainKiri.Rows[0]["Captain Name"].ToString();
            



            /*sqlQuery = "SELECT team_name,manager.manager_name, captain_id,home_stadium,capacity, player.player_name FROM team, player, manager WHERE team.captain_id = player.player_id AND team.manager_id = manager.manager_id AND team.team_name =  '"+ComboBoxKiri+"'";*/
            



        }

        private void ComboBoxKanan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataManagerDanCaptainKanan = new DataTable();
            sqlQuery = "SELECT manager.manager_name as `Manager Name`, player.player_name as `Captain Name` FROM team, player, manager WHERE team.captain_id = player.player_id AND manager.manager_id = team.manager_id AND team.team_id = '" + ComboBoxKanan.SelectedValue.ToString() + "'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dataManagerDanCaptainKanan);
            OutputManagerKanan.Text = dataManagerDanCaptainKanan.Rows[0]["Manager Name"].ToString();
            OutputKaptenKanan.Text = dataManagerDanCaptainKanan.Rows[0]["Captain Name"].ToString();

            DataTable Stadium = new DataTable();
            sqlQuery = "SELECT concat(home_stadium, ', ',team.city) as `Stadium`, capacity as `Capacity` FROM team WHERE team_id = '"+ComboBoxKiri.SelectedValue.ToString()+"'";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(Stadium);
            OutputStadium.Text = Stadium.Rows[0]["Stadium"].ToString();
            OutputCapacity.Text = Stadium.Rows[0]["Capacity"].ToString();
        }
    }
}
