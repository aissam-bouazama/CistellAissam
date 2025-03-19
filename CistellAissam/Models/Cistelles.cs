using System.Text.Json.Serialization;

namespace CistellAissam.Models;


public class Cistelles
{
    [JsonInclude]
    private List<Cistella> cistella = [];


    public Cistelles(List<Cistella> cesta)
    {
        cistella = cesta;
    }
    public Cistelles()
    {
    }

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
            if (posicion != -1)
            {
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

    public Cistella? getProducte(string codiproducte)
    {
        foreach (Cistella pro in cistella)
        {
            if (pro.codeproducte == codiproducte)
            {
                return pro;
            }
        }
        return null;
    }
    public void EsborrarProducte(string codeproducte)
    {
        cistella.RemoveAll(x => x.codeproducte == codeproducte);
    }
    public void actualizarQuantitat(string codeproducte, int quantitat)
    {
        int index = cistella.FindIndex(x => x.codeproducte == codeproducte);
        if (index != -1)
        {
            cistella[index].quantitat = quantitat;
        }
    }
}
