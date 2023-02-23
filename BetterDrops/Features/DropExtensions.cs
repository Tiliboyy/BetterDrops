using PlayerRoles;
using UnityEngine;

namespace BetterDrops.Features
{
    public static class DropExtensions
    {
        public static Vector3 GetRandomDropSpawnPoint(this Team team)
        {
            switch (team)
            {
                case Team.ChaosInsurgency:
                    return new Vector3(Random.Range(31f, -5f), 1000 + Random.Range(25f, 73f), Random.Range(-51, -37));
                case Team.FoundationForces:
                    return new Vector3(Random.Range(115, 138), 1000 + Random.Range(25f, 73f), Random.Range(-37f, -50));
                     
                default:
                    return Vector3.zero;
            }
        }
    }
}