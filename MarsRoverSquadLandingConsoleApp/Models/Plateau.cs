using System;

namespace MarsRoverSquadLandingConsoleApp.Models
{
    /// <summary>
    /// Plateau object and its props
    /// </summary>
    public class Plateau
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public Plateau(int maxX, int maxY)
        {
            this.MaxX = maxX;
            this.MaxY = maxY;

            if (MaxX <= 0 || MaxY <= 0)
                throw new ArgumentOutOfRangeException();
        }
    }
}
