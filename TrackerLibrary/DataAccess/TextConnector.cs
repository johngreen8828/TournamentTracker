using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {

        //Convert the text to a list for the model
        //Find the max id 
        //add the new record with new id max + 1
        //convert the model values to a list<string>
        //save the list<string> to the text file

        public void CreatePerson(PersonModel model)
        {
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;
            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;
            people.Add(model);
            people.SaveToPeopleFile();
        }

        public void CreatePrize(PrizeModel model)
        {
            //load all the prizes to find the id and convert the text to a prize model
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //set current id to one since no id in empty file when created and sets the first record in the model below
            int currentId = 1;
            //if there are prizes then we can get the max id
            if (prizes.Count > 0)
            {
                //Find the max id
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
             
            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile();

        }

        public void CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();

            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamFile();

        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();

            int currentId = 1;
            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            model.SaveRoundsToFile();

            tournaments.Add(model);

            tournaments.SaveToTournamentFile(GlobalConfig.TournamentFile);

        }

        public List<PersonModel> GetPerson_All()
        {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public List<TeamModel> GetTeam_all()
        {
            return GlobalConfig.TeamFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }

        public List<TournamentModel> GetTournament_All()
        {
            return GlobalConfig.TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels();
        }

        public void UpdateMatchup(MatchupModel model)
        {
            throw new NotImplementedException();
        }
    }
}
