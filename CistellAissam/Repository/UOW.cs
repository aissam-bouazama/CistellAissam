using CistellAissam.Data;
using CistellAissam.Repository.Interfaces;
using MySql.Data.MySqlClient;
using CistellAissam.Repository;
namespace CistellAissam.Repository

{
	public class UOW
	{
		MySqlConnection? _conection;
		MySqlTransaction trans;
		
		public void Open()
		{
			_conection = DB.Open();
			if(_conection == null)
			{
				throw new Exception("No s'ha pogut obrir la connexió a la base de dades.");
			}
			trans = _conection.BeginTransaction();
		}

		public void Close()
		{
			if (_conection != null)
			{
				_conection.Close();
				_conection = null;
			}
		}
		public IProducteRepository productes = new ProducteRepository();
	//	public IUsuariRepository usuaris = new UsuariRepositoryBD(_conection,trans);

		public void Commit() { 
			trans.Commit();
		}
		public void Rollback()
		{
			trans.Rollback();
		}
	}
}
