using MEC;
using Respawning;
using BetterDrops.Features.Data;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using Random = UnityEngine.Random;

namespace BetterDrops.Features
{
    public class EventManager
    {
        public List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();

        public void OnRestartingRound()
        {
            foreach (var coroutine in Coroutines)
                Timing.KillCoroutines(coroutine);
            Coroutines.Clear();
        }

        public void OnStartingRound()
        {
            if(BetterDrops.Cfg.RandomDrops)
                Coroutines.Add(Timing.RunCoroutine(RandomDropCoroutine(BetterDrops.Cfg.FirstRandomDropOffset)));
        }

        public void OnRespawningTeam(RespawningTeamEventArgs ev)
        {
            var team = ev.NextKnownTeam == SpawnableTeamType.NineTailedFox ? Team.FoundationForces : Team.ChaosInsurgency;

            if(team == Team.FoundationForces && !BetterDrops.Cfg.MtfDrops || team == Team.ChaosInsurgency && !BetterDrops.Cfg.ChaosDrops)
                return;

            for (var i = 0; i < BetterDrops.Cfg.NumberOfDrops[team]; i++)
            {
                new Drop(team.GetRandomDropSpawnPoint()).Spawn();
            }
        }

        public IEnumerator<float> RandomDropCoroutine(float offset)
        {
            yield return Timing.WaitForSeconds(offset);

            var team = Random.Range(0, 2) == 1 ? Team.FoundationForces : Team.ChaosInsurgency;
            
            for (var i = 0; i < BetterDrops.Cfg.NumberOfDrops[Team.OtherAlive]; i++)
                new Drop(team.GetRandomDropSpawnPoint()).Spawn();
            
            Coroutines.Add(Timing.RunCoroutine(RandomDropCoroutine(Random.Range(BetterDrops.Cfg.MinRandomDropsInterval, BetterDrops.Cfg.MaxRandomDropsInterval))));
        }
    }
}