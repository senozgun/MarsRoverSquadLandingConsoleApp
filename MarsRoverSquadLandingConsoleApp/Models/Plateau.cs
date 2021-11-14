using System;

namespace MarsRoverSquadLandingConsoleApp.Models
{
    /// <summary>
    /// Plateau object and its props
    /// </summary>
    public class Plateau
    {
        /// <summary>
        /// Upper Right X value of the plateau
        /// </summary>
        public int MaxX { get; set; }

        /// <summary>
        /// Upper Right Y value of the plateau
        /// </summary>
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
