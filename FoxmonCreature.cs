using System;
using System.Collections.Generic;

namespace Foxmon
{
    public class Attaque
    {
        public string Nom { get; set; }
        public int Puissance { get; set; }
        public string Type { get; set; }

        public Attaque(string nom, int puissance, string type)
        {
            Nom = nom;
            Puissance = puissance;
            Type = type;
        }
    }

    public class FoxmonCreature
    {
        public string Nom { get; set; }
        public int PV { get; set; }
        public int PVMax { get; set; }
        public int Niveau { get; set; }
        public int Vitesse { get; set; }
        public string TypeElementaire { get; set; }
        public int Attaque { get; set; }
        public List<Attaque> Attaques { get; set; } = new List<Attaque>();

        public FoxmonCreature(string nom, int pv, int niveau, int vitesse, string type, int attaque)
        {
            Nom = nom;
            PV = pv;
            PVMax = pv;
            Niveau = niveau;
            Vitesse = vitesse;
            TypeElementaire = type;
            Attaque = attaque;
        }

        public int CalculerDegats(FoxmonCreature cible, Attaque? attaqueChoisie = null)
        {
            int puissance = attaqueChoisie?.Puissance ?? Attaque;
            string typeAttaque = attaqueChoisie?.Type ?? TypeElementaire;

            int degats = puissance;

            if (typeAttaque == "Eau"    && cible.TypeElementaire == "Feu")    degats *= 2;
            else if (typeAttaque == "Feu"    && cible.TypeElementaire == "Plante") degats *= 2;
            else if (typeAttaque == "Plante" && cible.TypeElementaire == "Eau")    degats *= 2;
            else if (typeAttaque == "Feu"    && cible.TypeElementaire == "Eau")    degats /= 2;
            else if (typeAttaque == "Eau"    && cible.TypeElementaire == "Plante") degats /= 2;
            else if (typeAttaque == "Plante" && cible.TypeElementaire == "Feu")    degats /= 2;

            return degats;
        }

        public void SubirDegats(int degats)
        {
            PV = Math.Max(0, PV - degats); // jamais négatif
        }

        public void Soigner()
        {
            PV = PVMax;
        }

        public bool EstKO() => PV <= 0;
    }
}