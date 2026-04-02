using System.Collections.Generic;

namespace Foxmon
{
    public class Dresseur
    {
        public string Nom { get; set; }
        public string Emoji { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<FoxmonCreature> Equipe { get; set; }

        public Dresseur(string nom, string emoji, int x = 2, int y = 2)
        {
            Nom = nom;
            Emoji = emoji;
            X = x;
            Y = y;
            Equipe = new List<FoxmonCreature>();
        }
    }
}