using System.Windows.Forms;
using TableDependency.SqlClient;

namespace WindowsFormsAppSqlTableDependency
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /*
             * Crear un usuario con el mismo nombre de la base de datos para probar la conexión
             * CREATE USER [NORTHWND] 
             * FOR LOGIN [NORTHWND] WITH DEFAULT_SCHEMA=[dbo]
             * GO
             * ALTER ROLE [db_owner] ADD MEMBER [NORTHWND]
             * GO
             * Aquí lo estamos usando así porque en el proyecto de console Demo que publicamos aquí
             * https://www.youtube.com/watch?v=kEYTbprENag
             * usamos seguridad integrada de windows
             */
            string cnn = @"Data Source = .\SQLEXPRESS; Initial Catalog = NORTHWND; User Id = NORTHWND; Password = NORTHWND;";
            SqlTableDependency<Modelos.Categoria> dep = new SqlTableDependency<Modelos.Categoria>(cnn);

            dep.OnChanged += Dep_OnChanged;

            dep.Start();
        }

        private void Dep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Modelos.Categoria> e)
        {
            MessageBox.Show(e.Entity.Descripcion.Trim() + " Ha cambiado");
        }
    }
}
