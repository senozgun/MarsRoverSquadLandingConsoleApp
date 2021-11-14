using MarsRoverSquadLandingConsoleApp.Enums;
using MarsRoverSquadLandingConsoleApp.Models;

namespace MarsRoverSquadLandingConsoleApp.Helpers
{
    internal class MovementHelper
    {

        /// <summary>
        /// Move the rover 1 tile and sets the new position by its heading and current position
        /// </summary>
        /// <returns>Returns the Rover object</returns>
        public Rover Move(Rover rover)
        {
            switch (rover.Position.RoverHeading)
            {
                case CompassEnum.N:
                    rover.Position.RoverY++;
                    break;
                case CompassEnum.S:
                    rover.Position.RoverY--;
                    break;
                case CompassEnum.W:
                    rover.Position.RoverX--;
                    break;
                case CompassEnum.E:
                    rover.Position.RoverX++;
                    break;
                default:
                    break;
            }
            return rover;
        }
    }
}
