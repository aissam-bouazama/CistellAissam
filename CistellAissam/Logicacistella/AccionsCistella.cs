using CistellAissam.Models;
namespace CistellAissam.Logicacistella

{
    public class AccionsCistella
    {
        private static List<Cistella> cistella = new List<Cistella>();

        public int AddProducte(string codeproducte)
        {
            //0 si ja existeix el producte a la cistella i s'ha incrementat la quantitat
            //1 si s'ha afegit correctament
            Cistella producte = new Cistella();
            
           
            int process = 1;
            if (cistella.Exists(x => x.codeproducte == codeproducte))
            {
              // producte =  cistella.Find(x => x.codeproducte.Equals(codeproducte));
              int posicion = cistella.FindIndex(x => x.codeproducte == codeproducte);
                if(posicion != -1) {
                    cistella[posicion].quantitat++;
                }
               
                process = 0;
            }
            else
            {
                producte.codeproducte = codeproducte;
                producte.quantitat = 1;
                cistella.Add(producte);

            }
            return process;
        }
        public List<Cistella> GetCistella()
        {
            return cistella;
        }
        public void DeleteProducte(string codeproducte)
        {
            cistella.RemoveAll(x => x.codeproducte == codeproducte);
        }
    }
}
