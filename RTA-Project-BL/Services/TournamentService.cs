using AutoMapper;
using RTA_Project_BL.Models;
using RTA_Project_DAL.Models;
using RTA_Project_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RTA_Project_BL.Services
{
    public interface ITournamentService : IGenereicService<TournamentBL>
    {

        IEnumerable<TournamentBL> GetTournaments(int? playerId = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null);

        IEnumerable<MatchBL> GetTournamentMatches(int? playerId = null, Expression<Func<MatchBL, bool>>[] filtersForMatch = null,
            Expression<Func<TournamentBL, bool>>[] filtersForTournament = null);

        IEnumerable<GameBL> GetTournamentGames(int? playerId = null, Expression<Func<GameBL, bool>>[] filtersForGame = null,
            Expression<Func<MatchBL, bool>>[] filtersForMatch = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null);

        TournamentBL GetById(int id);

        void RegisterPlayer(int tournamentId, int playerId);
    }


    public class TournamentService : GenericService<TournamentBL, Tournament>, ITournamentService
    {

        public TournamentService(IGenericRepository<Tournament> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        private IQueryable<TournamentBL> QueryTournaments(int? playerId = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            var tournaments = _repository.QueryAll();

            if (playerId != null)
                tournaments = tournaments.Where(t => t.TournamentPlayers.Any(tp => tp.PlayerId == playerId));

            var tournamentsBL = Project(tournaments);

            if (filtersForTournament != null)
            {
                foreach (var filter in filtersForTournament)
                    tournamentsBL = tournamentsBL.Where(filter);
            }

            return tournamentsBL;
        }


        public TournamentBL GetById(int id)
        {
            var tournament = _repository.GetFirstOrDefault(t => t.Id == id, t => t.Description);
            var tournamentBL = Map(tournament);
            return tournamentBL;
        }

        public void RegisterPlayer(int tournamentId, int playerId)
        {
            var tournament = _repository.FindById(tournamentId);

            if (!tournament.TournamentPlayers.Any(tp => tp.PlayerId == playerId))
            {
                var tournamentPlayer = new TournamentPlayer { PlayerId = playerId };
                tournament.TournamentPlayers.Add(tournamentPlayer);
                _repository.Update(tournament);
            }

        }

        public IEnumerable<TournamentBL> GetTournaments(int? playerId = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            return QueryTournaments(playerId, filtersForTournament).ToList();
        }

        public IEnumerable<MatchBL> GetTournamentMatches(int? playerId = null, Expression<Func<MatchBL, bool>>[] filtersForMatch = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            return QueryTournamentMatches(playerId, filtersForMatch, filtersForTournament).ToList();
        }

        public IEnumerable<GameBL> GetTournamentGames(int? playerId = null, Expression<Func<GameBL, bool>>[] filtersForGame = null, Expression<Func<MatchBL, bool>>[] filtersForMatch = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            return QueryTournamentGames(playerId, filtersForGame, filtersForMatch, filtersForTournament).ToList();
        }



        private IQueryable<MatchBL> QueryTournamentMatches(int? playerId = null, Expression<Func<MatchBL, bool>>[] filtersForMatch = null,
    Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            var tournamentsBL = QueryTournaments(playerId, filtersForTournament);

            IQueryable<MatchBL> matchesBL;
            if (playerId != null)
            {
                matchesBL = tournamentsBL.SelectMany(t => t.TournamentGroups.
                    Where(tg => tg.TournamentGroupPlayers.Any(tgp => tgp.TournamentPlayerId ==
                   t.TournamentPlayers.FirstOrDefault(tp => tp.PlayerId == playerId).Id))).SelectMany(gr => gr.Matches.
                      Where(m => m.Player1Id == playerId || m.Player2Id == playerId));
            }
            else
            {
                matchesBL = tournamentsBL.SelectMany(t => t.TournamentGroups).SelectMany(gr => gr.Matches);
            }

            if (filtersForMatch != null)
            {
                foreach (var filter in filtersForMatch)
                    matchesBL = matchesBL.Where(filter);
            }

            return matchesBL;
        }

        private IQueryable<GameBL> QueryTournamentGames(int? playerId = null, Expression<Func<GameBL, bool>>[] filtersForGame = null,
            Expression<Func<MatchBL, bool>>[] filtersForMatch = null, Expression<Func<TournamentBL, bool>>[] filtersForTournament = null)
        {
            var matchesBL = QueryTournamentMatches(playerId, filtersForMatch, filtersForTournament);

            var gamesBL = matchesBL.SelectMany(m => m.Games);

            if (filtersForGame != null)
            {
                foreach (var filter in filtersForGame)
                    gamesBL = gamesBL.Where(filter);
            }

            return gamesBL;
        }


    }

}
