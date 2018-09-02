using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Events;
using StardewValley.Menus;
using System.Collections.Generic;

namespace NoKidsEver
{    /// <summary>The mod entry point.</summary>
    public class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            GameEvents.UpdateTick += this.GameEvents_UpdateTick;
        }

        public void GameEvents_UpdateTick(object sender, EventArgs e)
        {
            //Check if you're being asked the question.
            if (!IsBabyQuestion() || !(Game1.activeClickableMenu is DialogueBox dialogue)) return;
            // answer question
            Response no = Helper.Reflection.GetField<List<Response>>(dialogue, "response").GetValue()[1];
            Game1.currentLocation.answerDialogue(no);
            dialogue.closeDialogue();
        }

        private bool IsBabyQuestion() => Game1.farmEvent is QuestionEvent && 
                                         Helper.Reflection.GetField<int>(Game1.farmEvent, "whichQuestion").GetValue() == QuestionEvent.pregnancyQuestion;
        /* Just my little "thank you" to Pathos for the INSANE level of patience displayed
* during the countless hours of me pestering them on discord with questions, and all
* the explanations provided. It could not have been done, by me at least, without their
* help. Thank you. :D */
    }
}