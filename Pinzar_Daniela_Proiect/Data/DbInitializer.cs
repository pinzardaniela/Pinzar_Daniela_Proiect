using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pinzar_Daniela_Proiect.Models;

namespace Pinzar_Daniela_Proiect.Data
{
    public class DbInitializer
    {
        public static void Initialize(MagazinContext context)
        {
            context.Database.EnsureCreated();

            if (context.Produse.Any())
            {
                return; // BD a fost creata anterior
            }

            var produse = new Produs[]
            {
                new Produs{Denumire="Lapte de cocos Bio",Furnizor="SanoVita",Pret=Decimal.Parse("25")},
                new Produs{Denumire="Unt de arahide Ecologic",Furnizor="NIAVIS",Pret=Decimal.Parse("22")},
                new Produs{Denumire="Migdale BIO",Furnizor="BioVegan",Pret=Decimal.Parse("18")},
                new Produs{Denumire="Miere de salcam ecologica",Furnizor="Republica BIO",Pret=Decimal.Parse("46")},
                new Produs{Denumire="Seminte Chia",Furnizor="BioVegan",Pret=Decimal.Parse("16")},
                new Produs{Denumire="Fulgi ovaz",Furnizor="Melora",Pret=Decimal.Parse("19")},
                new Produs{Denumire="Scortisoara ecologica",Furnizor="SanoVita",Pret=Decimal.Parse("21")},
            };

            foreach (Produs s in produse)
            {
                context.Produse.Add(s);
            }
            context.SaveChanges();

            var clienti = new Client[]
                {

                    new Client{ClientID=1050,Nume="Pop Adrian",DataNasterii=DateTime.Parse("1988-10-22")},
                    new Client{ClientID=1045,Nume="Nicoara Mihaela",DataNasterii=DateTime.Parse("1970-09-18")}

                };

            foreach (Client c in clienti)
            {
                context.Clienti.Add(c);
            }
            context.SaveChanges();

            var comenzi = new Comanda[]
                {
                    new Comanda{ProdusID=1,ClientID=1050, DataComenzii=DateTime.Parse("12-18-2020")},
                    new Comanda{ProdusID=3,ClientID=1045, DataComenzii=DateTime.Parse("11-23-2020")},
                    new Comanda{ProdusID=1,ClientID=1045, DataComenzii=DateTime.Parse("09-28-2020")},
                    new Comanda{ProdusID=2,ClientID=1050, DataComenzii=DateTime.Parse("10-28-2020")},
                    new Comanda{ProdusID=4,ClientID=1050, DataComenzii=DateTime.Parse("12-09-2020")},
                    new Comanda{ProdusID=5,ClientID=1045, DataComenzii=DateTime.Parse("07-29-2020")},
                 };
            foreach (Comanda e in comenzi)
            {
                context.Comenzi.Add(e);
            }
            context.SaveChanges();

            var distribuitori = new Distribuitor[]
                {

                    new Distribuitor{NumeDistribuitor="SAMMILLS DISTRIBUTION SRL",Adresa="Str. Mioritei, nr. 151,Agris"},
                    new Distribuitor{NumeDistribuitor="SAFF TRADING SRL",Adresa="Str. Libertatii, nr. 36,Bucuresti"},
                    new Distribuitor{NumeDistribuitor="Dorsan Impex",Adresa="Str. Cernavoda, nr.5, Cluj-Napoca"},
                };
            foreach (Distribuitor p in distribuitori)
            {
                context.Distribuitori.Add(p);
            }
            context.SaveChanges();

            var distribuitorproduse = new DistribuitorProdus[]
            {
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Lapte de cocos Bio").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "SAFF TRADING SRL").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Unt de arahide Ecologic").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "Dorsan Impex").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Migdale BIO").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "SAMMILLS DISTRIBUTION SRL").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Miere de salcam ecologica").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "SAFF TRADING SRL").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Seminte Chia").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "Dorsan Impex").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Fulgi ovaz").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "SAMMILLS DISTRIBUTION SRL").ID },
                new DistribuitorProdus { ProdusID = produse.Single(c => c.Denumire == "Scortisoara ecologica").ID, DistribuitorID = distribuitori.Single(i => i.NumeDistribuitor == "Dorsan Impex").ID },
            };

            foreach (DistribuitorProdus pb in distribuitorproduse)
            {
                context.DistribuitorProduse.Add(pb);
            }
            context.SaveChanges();

        }
    }
}
