using BepInEx;
using BepInEx.Logging;
using Combat;
using HarmonyLib;
using System;

namespace DifficultyMod
{
    [BepInPlugin("example.difficultymod", "Difficulty Mod", "1.0.0")]
    public class LoadTheMod : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource ModLogger;
        private void Awake()
        {
            var harmony = new Harmony("example.difficultymod");
            harmony.PatchAll();
            Logger.LogInfo("Difficulty Mod loaded, if you see this I hope I messed up nothing.");
        }
    }


    [HarmonyPatch(typeof(CombatManager), "createWave")]
    public static class EnemyTurnPatchSetup
    {
        public static int enemyActionPoints;
        public static int enemyMaxActionPoints;
        [HarmonyPrefix]
        public static void Prefix(CombatManager __instance)
        {
            enemyActionPoints = 0;
            enemyMaxActionPoints = 2;
        }

    }

    [HarmonyPatch(typeof(CombatManager), "handleTurn")]
    public static class EnemyTurnPatch
    {

        [HarmonyPrefix]
        public static bool Prefix(CombatManager __instance)
        {
            BepInEx.Logging.Logger.CreateLogSource("EnemyTurnPatch").LogInfo("Prefix called!");
            Traverse.Create(__instance).Field("firstTurnOccured").SetValue(true);
            if (__instance.Allies.Count == 0 || __instance.Enemies.Count == 0)
            {
                return false;
            }
            if (((HeroController)Traverse.Create(__instance).Field("heroControler").GetValue()).CheckTotalPriority())
            {
                return false;
            }
            TurnInfo turnInfo;
            if (Traverse.Create(__instance).Field("allyTurnIndex").GetValue<int>() < __instance.Allies.Count)
            {
                turnInfo = __instance.Allies[Traverse.Create(__instance).Field("allyTurnIndex").GetValue<int>()].TryExecuteTurn();
                if (turnInfo.CountAsTurn)
                {
                    if (__instance.Allies[Traverse.Create(__instance).Field("allyTurnIndex").GetValue<int>()] is FighterHero)
                    {
                        ((HeroController)Traverse.Create(__instance).Field("heroControler").GetValue()).CountTurn();
                    }
                    Traverse.Create(__instance).Field("allyTurnIndex").SetValue(Traverse.Create(__instance).Field("allyTurnIndex").GetValue<int>() + 1);
                }
            }
            else
            {
                ((HeroController)Traverse.Create(__instance).Field("heroControler").GetValue()).ResetProcs();
                while (Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>() >= __instance.Enemies.Count)
                {
                    Traverse.Create(__instance).Field("enemyTurnIndex").SetValue(Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>() - 1); ;
                }
                turnInfo = __instance.Enemies[Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>()].TryExecuteTurn();
                if (turnInfo.CountAsTurn && EnemyTurnPatchSetup.enemyActionPoints <= EnemyTurnPatchSetup.enemyMaxActionPoints)
                {
                    Traverse.Create(__instance).Field("enemyTurnIndex").SetValue(Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>()); ;
                    EnemyTurnPatchSetup.enemyActionPoints++;
                    Console.WriteLine(EnemyTurnPatchSetup.enemyActionPoints);
                }
                if (EnemyTurnPatchSetup.enemyActionPoints == EnemyTurnPatchSetup.enemyMaxActionPoints)
                {
                    Traverse.Create(__instance).Field("enemyTurnIndex").SetValue(Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>() + 1);
                    EnemyTurnPatchSetup.enemyActionPoints = 0;
                    EnemyTurnPatchSetup.enemyMaxActionPoints = 2;
                }
                if (Traverse.Create(__instance).Field("enemyTurnIndex").GetValue<int>() >= __instance.Enemies.Count)
                {
                    Traverse.Create(__instance).Field("allyTurnIndex").SetValue(0);
                    Traverse.Create(__instance).Field("enemyTurnIndex").SetValue(0);
                    EnemyTurnPatchSetup.enemyActionPoints = 0;
                    EnemyTurnPatchSetup.enemyMaxActionPoints = 2;
                }
            }
            if (turnInfo.SkippedTurn)
            {
                Traverse.Create(__instance).Method("callNextTurn", Traverse.Create(__instance).Field("settings").GetValue<CombatSettings>().TurnIntervalDelay, "SkipTurn").GetValue();
            }


            return false;
        }

    }
}
