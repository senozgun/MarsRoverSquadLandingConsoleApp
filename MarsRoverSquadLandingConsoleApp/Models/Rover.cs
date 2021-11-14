using MarsRoverSquadLandingConsoleApp.Enums;

namespace MarsRoverSquadLandingConsoleApp.Models
{
    /// <summary>
	/// Rover object and its props
	/// </summary>
    public class Rover
    {
        public RoverPosition Position { get; set; }
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
        public int RoverX { get; set; }
        public int RoverY { get; set; }
        public CompassEnum RoverHeading { get; set; }

        public RoverPosition(int roverX, int roverY, CompassEnum heading)
        {
            this.RoverX = roverX;
            this.RoverY = roverY;
            this.RoverHeading = heading;
        }
    }
}
