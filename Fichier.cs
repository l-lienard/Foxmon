using System;
using System.Collections.Generic;

public class Carte
{
    string[] lignes = new[]
    {
        "####################",
        "#.........*....*...#",
        "#..####....####..*.#",
        "#.....*.....*......#",
        "#####..####..####..#",
        "#....*....#...*....#",
        "#......####........#",
        "#....#......#......#",
        "#..*.........*.....#",
        "####################",
    };

    int height;
    int width;
    char[,] grille;

    public int X { get; set; }
    public int Y { get; set; }

    public Carte()
    {
        height = lignes.Length;
        width = lignes[0].Length;
        grille = new char[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grille[x, y] = lignes[y][x];
            }
        }

        X = 2;
        Y = 2;
    }

    public void Afficher()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grille[x, y]);
            }
            Console.WriteLine();
        }
    }


}


public class Dresseur
{
    public string Nom { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<Foxmon> Equipe { get; set; }

    public Dresseur(string nom, int x, int y)
    {
        Nom = nom;
        X = x;
        Y = y;
        Equipe = new List<Foxmon>();
    }
}

public class Foxmon
{
    public string Nom { get; set; }
    public int PV { get; set; }
    public int PVMax { get; set; }
    public int Niveau { get; set; }
    public int Vitesse { get; set; }
    public string TypeElementaire { get; set; }

    public Foxmon(string nom, int pv, int pvmax, int niveau, int vitesse, string typeelementaire)
    {
        Nom = nom;
        PV = pv;
        PVMax = pvmax;
        Niveau = niveau;
        Vitesse = vitesse;
        TypeElementaire = typeelementaire;
    }
}

public class Program
{
    public static void Main()
    {
        Carte carte = new Carte();

        while (true)
        {
            int newX = carte.X;
            int newY = carte.Y;

            Console.Clear();
            carte.Afficher();
            Console.SetCursorPosition(newX,newY);

            ConsoleKey key = Console.ReadKey(true).Key;

            if(key == ConsoleKey.UpArrow){
                carte.Y--;
            };

            if(key == ConsoleKey.LeftArrow)
            {
                carte.X--;  
            };

            if(key == ConsoleKey.RightArrow)
            {
                carte.X++;
            };

            if(key == ConsoleKey.DownArrow)
            {
                carte.Y++;  
            };

        }
    }
}
