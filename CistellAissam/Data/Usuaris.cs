using CistellAissam.Models;

namespace CistellAissam.Data
{
    public class Usuaris
    {
        public static readonly List<UsuariLogin> _usuaris = [];

        static Usuaris()
        {
            _usuaris.Add(new UsuariLogin("12345", "joan23@gmail.com", false, false, new DateTime(2025, 1, 12, 16, 23, 45)));
            _usuaris.Add(new UsuariLogin("23456", "maria@gmail.com", false, false, new DateTime(2025, 1, 1, 23, 21, 5)));
            _usuaris.Add(new UsuariLogin("34567", "carme17@gmail.com", false, false, new DateTime(2025, 2, 11, 20, 00, 23)));
            _usuaris.Add(new UsuariLogin("45678", "super@gmail.com", true, false, new DateTime(2024, 1, 1, 0, 0, 0)));
        }
    }
}
