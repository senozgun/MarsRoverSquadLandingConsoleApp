using MarsRoverSquadLandingConsoleApp.Enums;
using MarsRoverSquadLandingConsoleApp.Models;

namespace MarsRoverSquadLandingConsoleApp.Helpers
{
    public class CompassHelper
    {
		/// <summary>
		/// Rotate the rover and set new heading by its heading and rotation value
		/// </summary>
		/// <returns>Returns the Rover object</returns>
		public Rover Rotate(Rover rover, RotationEnum rotation)
        {
			switch (rover.Position.RoverHeading)
			{
				case CompassEnum.N:
					rover.Position.RoverHeading = (rotation == RotationEnum.Left ? CompassEnum.W : CompassEnum.E);
					break;
				case CompassEnum.S:
					rover.Position.RoverHeading = (rotation == RotationEnum.Left ? CompassEnum.E : CompassEnum.W);
					break;
				case CompassEnum.W:
					rover.Position.RoverHeading = (rotation == RotationEnum.Left ? CompassEnum.S : CompassEnum.N);
					break;
				case CompassEnum.E:
					rover.Position.RoverHeading = (rotation == RotationEnum.Left ? CompassEnum.N : CompassEnum.S);
					break;
				default:
					break;
			}
			return rover;
        }
    }
}
