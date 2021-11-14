using MarsRoverSquadLandingConsoleApp.Enums;

namespace MarsRoverSquadLandingConsoleApp.Models
{
    /// <summary>
	/// Rover object and its props
	/// </summary>
    public class Rover
    {
        /// <summary>
        /// Rover position object
        /// </summary>
        public RoverPosition Position { get; set; }
        /// <summary>
        /// Rover path
        /// </summary>
        public string Path { get; set; }

        public Rover(RoverPosition position, string path)
        {
            this.Position = position;
            this.Path = path;
        }
    }

    /// <summary>
    /// Rover Position object and its props
    /// </summary>
    public class RoverPosition
    {
        /// <summary>
        /// Rover position X value
        /// </summary>
        public int RoverX { get; set; }
        /// <summary>
        /// Rover position Y value
        /// </summary>
        public int RoverY { get; set; }
        /// <summary>
        /// Rover heading value
        /// </summary>
        public CompassEnum RoverHeading { get; set; }

        public RoverPosition(int roverX, int roverY, CompassEnum heading)
        {
            this.RoverX = roverX;
            this.RoverY = roverY;
            this.RoverHeading = heading;
        }
    }
}
