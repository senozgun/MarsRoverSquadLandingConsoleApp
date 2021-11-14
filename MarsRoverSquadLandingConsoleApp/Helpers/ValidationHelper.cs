using MarsRoverSquadLandingConsoleApp.Models;

namespace MarsRoverSquadLandingConsoleApp.Helpers
{
    public class ValidationHelper
    {
        /// <summary>
        /// Validates the plateau
        /// </summary>
        /// <gets>
        /// Plateau object
        /// </gets>
        /// <returns>True if valid, else False</returns>
        public bool ValidatePlateau(Plateau plateau)
        {
            if (plateau.MaxX <= 0 || plateau.MaxY <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// Validates the rover first positions
        /// </summary>
        /// <gets>
        /// List of Rover object
        /// </gets>
        /// <returns>True if valid, else False</returns>
        public bool ValidateRoverPositions(List<Rover> roverSquad)
        {
            string[] directions = { "N", "S", "E", "W" };

            foreach (Rover rover in roverSquad)
            {
                if (rover.Position.RoverX <= 0 || rover.Position.RoverY <= 0 || !directions.Any(d => rover.Position.RoverHeading.ToString().Contains(d)))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
		/// Validates the rover paths
		/// </summary>
		/// <gets>
        /// List of Rover object
        /// </gets>
		/// <returns>True if valid, else False</returns>
        public bool ValidateRoverPaths(List<Rover> roverSquad)
        {
            string[] movements = { "L", "R", "M" };

            foreach (Rover rover in roverSquad)
            {
                foreach (char ch in rover.Path.ToString().ToCharArray())
                {
                    if (!movements.Any(m=>ch.ToString().Contains(m)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Validates the rover landing location
        /// </summary>
        /// <gets>
        /// Rover object and Plateau object
        /// </gets>
        /// <returns>True if valid, else False</returns>
        public bool ValidateRoverLandingLocation(Rover rover, Plateau plateau)
        {
            if(rover.Position.RoverX > plateau.MaxX || rover.Position.RoverY > plateau.MaxY)
                return false;

            return true;
        }
    }
}
