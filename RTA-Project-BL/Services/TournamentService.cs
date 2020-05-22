using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RTA_Project_BL.Services
{
    public interface ITournamentService : IGenereicService<TournamentBL>
    {
        IQueryable<TournamentBL> QueryTournaments(int? playerId = null, Expression<Func<TournamentBL, bool>> filterForTournament = null);

        IQueryable<MatchBL> QueryTournamentMatches(int? playerId = null, Expression<Func<MatchBL, bool>> filterForMatch = null,
            Expression<Func<TournamentBL, bool>> filterForTournament = null);

        IQueryable<GameBL> QueryTournamentGames(int? playerId = null, Expression<Func<GameBL, bool>> filterForGame = null,
            Expression<Func<MatchBL, bool>> filterForMatch = null, Expression<Func<TournamentBL, bool>> filterForTournament = null);
    }


    public class TournamentService : GenericService<TournamentBL, Tournament>, ITournamentService
    {

        public TournamentService(IGenericRepository<Tournament> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public IQueryable<TournamentBL> QueryTournaments(int? playerId = null, Expression<Func<TournamentBL, bool>> filterForTournament = null)
        {
            var tournaments = _repository.QueryAll();

            if (playerId != null)
                tournaments = tournaments.Where(t => t.TournamentPlayers.Any(tp => tp.PlayerId == playerId));

            var tournamentsBL = Project(tournaments);

            if (filterForTournament != null)
                tournamentsBL = tournamentsBL.Where(filterForTournament);

            return tournamentsBL;
        }

        public IQueryable<MatchBL> QueryTournamentMatches(int? playerId = null, Expression<Func<MatchBL, bool>> filterForMatch = null,
            Expression<Func<TournamentBL, bool>> filterForTournament = null)
        {
            var tournamentsBL = QueryTournaments(playerId, filterForTournament);


            IQueryable<MatchBL> matchesBL;
            if (playerId != null)
            {
                matchesBL = tournamentsBL.SelectMany(t => t.TournamentGroups.
                    Where(tg =>  tg.TournamentGroupPlayers.Any(tgp=>tgp.TournamentPlayerId ==
                    t.TournamentPlayers.FirstOrDefault(tp=>tp.PlayerId==playerId).Id) )).SelectMany(gr => gr.Matches);
            }
            else
            {
                matchesBL = tournamentsBL.SelectMany(t => t.TournamentGroups).SelectMany(gr => gr.Matches);
            }

            if (filterForMatch != null)
                matchesBL = matchesBL.Where(filterForMatch);

            return matchesBL;
        }

        public IQueryable<GameBL> QueryTournamentGames(int? playerId = null, Expression<Func<GameBL, bool>> filterForGame = null,
            Expression<Func<MatchBL, bool>> filterForMatch = null, Expression<Func<TournamentBL, bool>> filterForTournament = null)
        {
            var matchesBL = QueryTournamentMatches(playerId, filterForMatch, filterForTournament);

            var gamesBL = matchesBL.SelectMany(m => m.Games);

            if (filterForGame != null)
                gamesBL = gamesBL.Where(filterForGame);

            return gamesBL;
        }

    }

}
