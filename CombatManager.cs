using System;
using System.Collections.Generic;

namespace Foxmon
{
    public class CombatManager
    {
        private static Random rng = new Random();

        public static List<FoxmonCreature> BestiaireSauvage = new List<FoxmonCreature>
        {
            new FoxmonCreature("Aquarox",   30, 5,  8,  "Eau",    8),
            new FoxmonCreature("Flamby",    28, 4,  10, "Feu",    10),
            new FoxmonCreature("Feuillos",  25, 3,  6,  "Plante", 7),
            new FoxmonCreature("Braskor",   35, 6,  7,  "Feu",    12),
            new FoxmonCreature("Marékor",   32, 5,  9,  "Eau",    9),
            new FoxmonCreature("Voltix",    27, 4,  12, "Foudre", 11),
            new FoxmonCreature("Glacius",   30, 5,  7,  "Glace",  9),
            new FoxmonCreature("Terrakos",  38, 6,  5,  "Roche",  13),
            new FoxmonCreature("Venimos",   24, 3,  11, "Poison", 8),
            new FoxmonCreature("Psykor",    26, 4,  9,  "Psy",    10),
            new FoxmonCreature("Ombralis",  29, 5,  10, "Ombre",  11),
            new FoxmonCreature("Drakos",    40, 7,  8,  "Dragon", 14),
            new FoxmonCreature("Aéryon",    22, 3,  14, "Vol",    8),
            new FoxmonCreature("Normios",   20, 2,  6,  "Normal", 6),
            new FoxmonCreature("Ferrox",    36, 6,  6,  "Acier",  12),
        };

        public static bool TenterRencontre() => rng.Next(0, 100) < 20;

        public static FoxmonCreature GenererSauvage()
        {
            var m = BestiaireSauvage[rng.Next(BestiaireSauvage.Count)];
            return new FoxmonCreature(m.Nom, m.PVMax, m.Niveau, m.Vitesse, m.TypeElementaire, m.Attaque);
        }

        public static bool TenterCapture(FoxmonCreature cible)
        {
            double chance = 1.0 - ((double)cible.PV / cible.PVMax);
            return rng.NextDouble() < chance;
        }

        public static bool TenterFuite() => rng.Next(0, 100) < 50;
    }
}