﻿// ----------------------------------------------------------------------
// These are basic usings. Always let them be here.
// ----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

// ----------------------------------------------------------------------
// These are RimWorld-specific usings. Activate/Deactivate what you need:
// ----------------------------------------------------------------------
using UnityEngine;         // Always needed
//using VerseBase;         // Material/Graphics handling functions are found here
using Verse;               // RimWorld universal objects are here (like 'Building')
using Verse.AI;          // Needed when you do something with the AI
using Verse.AI.Group;
using Verse.Sound;       // Needed when you do something with Sound
using Verse.Noise;       // Needed when you do something with Noises
using RimWorld;            // RimWorld specific functions are found here (like 'Building_Battery')
using RimWorld.Planet;   // RimWorld specific functions for world creation
//using RimWorld.SquadAI;  // RimWorld specific functions for squad brains 

namespace CultOfCthulhu
{
    public class SpellWorker_NoLongerDomesticated : SpellWorker
    {
        
        protected IEnumerable<Pawn> Animals(Map map)
        {
                return from Pawn animal in map.mapPawns.AllPawns
                       where animal.RaceProps.Animal && animal.Faction == Faction.OfPlayer
                       select animal;
        }

        protected override bool CanFireNowSub(IncidentParms parms)
        {

            //Cthulhu.Utility.DebugReport("CanFire: " + this.def.defName);
            return true;
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            for (int i = 0; i < Rand.Range(3, 6); i++)
            {
                if (Animals((Map)parms.target).Count<Pawn>() != 0)
                {
                    Pawn animal;
                    if (Animals((Map)parms.target).TryRandomElement<Pawn>(out animal))
                    {
                        //Cthulhu.Utility.DebugReport("Destroyed: " + item.ToString());
                        animal.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.Manhunter, null, false);
                    }
                }
                else
                {
                    Cthulhu.Utility.DebugReport("No animals to drive insane.");
                }
            }
            return true;
        }

    }
}