using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Module3
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiserDatas();

            // Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            List<Auteur> authors = ListeAuteurs.Where(n => n.Nom.StartsWith("G")).ToList();
            foreach (Auteur unAuteur in authors)
            {
                Console.WriteLine(unAuteur.Prenom);
            }
            Console.ReadKey();

            // Afficher l’auteur ayant écrit le plus de livres
            IGrouping<Auteur, Livre> auteur = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(g => g.Count())
                .FirstOrDefault();
            Console.WriteLine($"{auteur.Key.Nom} {auteur.Key.Prenom}");
            Console.ReadKey();

            // Afficher le nombre moyen de pages par livre par auteur
            IEnumerable<IGrouping<Auteur, Livre>> livresAuteur = ListeLivres.GroupBy(l => l.Auteur);
            foreach (IGrouping<Auteur, Livre> unAuteur in livresAuteur)
            {
                Console.WriteLine($"{unAuteur.Key.Prenom} {unAuteur.Key.Nom} : moyennes des pages={unAuteur.Average(l => l.NbPages)}");
            }
            Console.ReadKey();

            // Quel est le titre du livre avec le plus de pages
            Livre livre = ListeLivres.OrderByDescending(l => l.NbPages).FirstOrDefault();
            Console.WriteLine(livre.Titre);
            Console.ReadKey();

            // Combien ont gagné les auteurs en moyenne
            decimal salaire = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine(salaire);
            Console.ReadKey();

            // Afficher les auteurs et la liste de leurs livres
            IEnumerable<IGrouping<Auteur, Livre>> livresAuteur1 = ListeLivres.GroupBy(l => l.Auteur);
            foreach (IGrouping<Auteur, Livre> desLivres in livresAuteur1)
            {
                Console.WriteLine($"Auteur : {desLivres.Key.Prenom} {desLivres.Key.Nom} ");
                foreach (Livre unLivre in desLivres)
                {
                    Console.WriteLine($"Livre : {livre.Titre}");
                }
            }
            Console.ReadKey();

            // Afficher les titres de tous les livres triés par ordre alphabétique
            List<String> livresDesc = ListeLivres.Select(l => l.Titre)
                .OrderBy(t => t).ToList();
            foreach (string titreLivre in livresDesc)
            {
                Console.WriteLine(titreLivre);
            }
            // OR 
            // titreQ7s.ForEach(Console.WriteLine);
            Console.ReadKey();

            // Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
            double avgNbPage = ListeLivres.Average(l => l.NbPages);
            List<Livre> livres = ListeLivres.Where(x => x.NbPages > avgNbPage).ToList();
            foreach (Livre unLivre in livres)
            {
                Console.WriteLine(unLivre.Titre);
            }
            Console.ReadKey();

            // Afficher l'auteur ayant écrit le moins de livres
            IGrouping<Auteur, Livre> auteurQ2 = ListeLivres.GroupBy(l => l.Auteur)
                .OrderBy(g => g.Count())
                .FirstOrDefault();
            Console.WriteLine($"{auteurQ2.Key.Nom} {auteurQ2.Key.Prenom}");
            Console.ReadKey();
        }

        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }
    }
}
