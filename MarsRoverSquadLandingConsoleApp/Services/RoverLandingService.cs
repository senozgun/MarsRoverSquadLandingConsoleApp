using MarsRoverSquadLandingConsoleApp.Enums;
using MarsRoverSquadLandingConsoleApp.Helpers;
using MarsRoverSquadLandingConsoleApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarsRoverSquadLandingConsoleApp
{
	/// <summary>
	/// Rover Landing Service
	/// </summary>
	/// <inheritdoc cref="IRoverLandingService"/>
	public class RoverLandingService : IRoverLandingService
	{
		private readonly IConfiguration _config;
		private readonly ILogger<RoverLandingService> _logger;

		public RoverLandingService(ILogger<RoverLandingService> logger, IConfiguration config)
		{
			_logger = logger;
			_config = config;
		}

		/// <summary>
		/// Main Task for Landing Service
		/// </summary>
		public Task Run()
		{
			List<string> inputs = new List<string>();

			Console.WriteLine("Please enter upper-right coordinates of the plateau first");
			Console.WriteLine("Then enter rover's current position and exploration instructions respectively");
			Console.WriteLine("When you have completed the entries for all rovers, enter 'land' to start the landing operations");

			//Gathering Inputs
			string? input = Console.ReadLine();
			while (!String.IsNullOrEmpty(input) && !input.ToLower().Equals("land"))
			{
				inputs.Add(input.ToUpper());
				input = Console.ReadLine();
			}


			if (inputs == null || inputs.Count <= 0)
			{
				Log.Error("Invalid input");
			}
			else
			{
				List<Rover> roverSquad = new List<Rover>();
				Plateau plateau = new Plateau(1, 1);

				try
				{
					//Setting Plateaau
					Log.Information("Creating Plateau");
					plateau = SetPlateau(plateau, inputs);
					Log.Information("Plateau Created Successfully");

					//Creating Rover Squad List with position informations
					Log.Information("Setting Squad Informations");
					roverSquad = SetRoversInformation(inputs);
					Log.Information("Squad Initialized Successfully");


					//Calculating Rover Landing coordinates
					Log.Information("Rover Squad Landing Process Started");
					int roverIndex = 1;
					foreach (var rover in roverSquad)
					{
						var landingLocation = CalculateCoordinates(rover, plateau);
						Console.WriteLine("Rover {0} landed on {1}", roverIndex, landingLocation);
						roverIndex++;
					}
					Log.Information("Landing Finished");

				}
				catch (Exception ex)
				{
					Log.Error(ex.Message);
					return Task.CompletedTask;
				}
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// Sets Rover Squad Information
		/// </summary>
		/// <gets>
		/// List of input string
		/// </gets>
		/// <returns>
		/// Returns the Rover object
		/// </returns>
		public List<Rover> SetRoversInformation(List<string> inputs)
		{
			List<Rover> roverSquad = new List<Rover>();

			int locationIndex = 1;
			int pathIndex = 2;

			while (pathIndex <= inputs.Count)
			{
				string[] roverPosition = inputs[locationIndex].ToString().ToUpper().Split(' ');
				string roverPath = inputs[pathIndex].ToString().ToUpper();

				roverSquad.Add(new Rover(new RoverPosition(int.Parse(roverPosition[0]),
														   int.Parse(roverPosition[1]),
														   (CompassEnum)Enum.Parse(typeof(CompassEnum), roverPosition[2])),
														   roverPath));

				locationIndex = locationIndex + 2;
				pathIndex = pathIndex + 2;
			}

			ValidationHelper helper = new ValidationHelper();

			if (!helper.ValidateRoverPositions(roverSquad))
				throw new Exception("Invalid rover positions");

			if (!helper.ValidateRoverPaths(roverSquad))
				throw new Exception("Invalid rover paths");

			return roverSquad;
		}

		/// <summary>
		/// Sets Plateau
		/// </summary>
		/// <gets>
		/// Plateau object and list of input string
		/// </gets>
		/// <returns>
		/// Returns the Plateau object
		/// </returns>
		public Plateau SetPlateau(Plateau plateau, List<string> inputs)
		{
			string[] upperRightCoordinates = inputs[0].ToString().ToUpper().Split(' ');
			plateau.MaxX = Convert.ToInt32(upperRightCoordinates[0]);
			plateau.MaxY = Convert.ToInt32(upperRightCoordinates[1]);

			ValidationHelper helper = new ValidationHelper();

			if (!helper.ValidatePlateau(plateau))
				throw new Exception("Invalid upper-right coordinates of Plateau ");

			return plateau;
		}

		/// <summary>
		/// Calculates Rover Landing Position
		/// </summary>
		/// <gets>
		/// Rover object
		/// </gets>
		/// <returns>
		/// Returns the landing location as string
		/// </returns>
		public string CalculateCoordinates(Rover rover, Plateau plateau)
		{
			string landingLocation = "";
			CompassHelper compassHelper = new CompassHelper();
			MovementHelper movementHelper = new MovementHelper();

			foreach (char ch in rover.Path.ToString().ToCharArray())
			{
				switch (ch)
				{
					case 'L':
						compassHelper.Rotate(rover, RotationEnum.Left);
						break;
					case 'R':
						compassHelper.Rotate(rover, RotationEnum.Right);
						break;
					case 'M':
						movementHelper.Move(rover);
						break;
					default:
						break;
				}
			}

			landingLocation = rover.Position.RoverX.ToString() + " " + rover.Position.RoverY.ToString() + " " + rover.Position.RoverHeading.ToString();

			ValidationHelper helper = new ValidationHelper();

			if (!helper.ValidateRoverLandingLocation(rover, plateau))
				throw new Exception("Rover Landing Location is out of Plateau boundaries. Landing Aborted");

			return landingLocation;
		}
	}
}