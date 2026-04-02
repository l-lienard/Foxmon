public class Carte
{
    string[] lignes = new[]
    {
        "#############################",
        "#.........*........*........#",
        "#..####....####..*...####...#",
        "#.....*.....*...............#",
        "#####..####..####..#####....#",
        "#....*....#...*....#.......*#",
        "#......####........#..*....*#",
        "#....#......#......#.......##",
        "#..*.........*.............*#",
        "#.................####..*...#",
        "#..####..####..............*#",
        "#..*..........######..####.*#",
        "#........*.................*#",
        "#####..########..#####..###*#",
        "#....*........#............*#",
        "#.........C...#....*.......*#",
        "#..####..####..####..####..*#",
        "#.....................*....##",
        "#....#........#............##",
        "#..*.........*.............*#",
        "#############################"
    };

    public int Height { get; private set; }
    public int Width  { get; private set; }
    public char[,] Grille { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Carte()
    {
        Height = lignes.Length;
        Width  = lignes[0].Length;
        Grille = new char[Width, Height];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                Grille[x, y] = lignes[y][x];
        X = 2;
        Y = 2;
    }

    public bool EstHauteHerbe(int x, int y) => Grille[x, y] == '*';
    public bool EstBatiment(int x, int y)   => Grille[x, y] == 'C';
}